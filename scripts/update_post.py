#!/usr/bin/env python3
"""
update_post.py - Update metadata for a published blog post

This script updates the title, date, or categories of an existing post
across all relevant files:
- a/index.html (chronological table)
- a/toc/chrono-data.json (timeline navigation)
- a/toc/toc-data.json (topic sidebar - if post is in a topic)
- The HTML file itself (optional, updates <title> tag)

Usage:
    python update_post.py 2079_my_post.html --title "New Title"
    python update_post.py 2079_my_post.html --date 2026-01-10
    python update_post.py 2079_my_post.html --categories "Revit API, Geometry"
    python update_post.py 2079_my_post.html --title "New Title" --date 2026-01-10 --dry-run

Author: parametrix
Date: January 6, 2026
"""

import argparse
import json
import re
import sys
from datetime import datetime
from pathlib import Path

# Add scripts directory to path for local imports
SCRIPTS_DIR = Path(__file__).parent
if str(SCRIPTS_DIR) not in sys.path:
    sys.path.insert(0, str(SCRIPTS_DIR))

# Configuration
REPO_ROOT = Path(__file__).parent.parent
POSTS_DIR = REPO_ROOT / "a"
INDEX_FILE = POSTS_DIR / "index.html"
TOC_FILE = POSTS_DIR / "toc" / "toc-data.json"
CHRONO_FILE = POSTS_DIR / "toc" / "chrono-data.json"


def get_post_number(filename):
    """Extract the post number from a filename."""
    match = re.match(r"(\d{4})_", filename)
    if match:
        return int(match.group(1))
    return None


def update_html_title(filename, new_title, dry_run=False):
    """Update the <title> tag in the HTML file."""
    file_path = POSTS_DIR / filename
    
    if not file_path.exists():
        print(f"Warning: HTML file not found: {file_path}")
        return False
    
    content = file_path.read_text(encoding='utf-8')
    
    # Update <title> tag
    pattern = r'<title>.*?</title>'
    new_tag = f'<title>{new_title} - The Building Coder</title>'
    new_content, count = re.subn(pattern, new_tag, content, count=1)
    
    if count > 0:
        if dry_run:
            print(f"[DRY RUN] Would update <title> in {filename}")
        else:
            file_path.write_text(new_content, encoding='utf-8')
            print(f"Updated <title> in {filename}")
        return True
    else:
        print(f"Note: No <title> tag found in {filename}")
        return False


def update_index(filename, new_title=None, new_date=None, new_categories=None, dry_run=False):
    """Update the post entry in index.html."""
    if not INDEX_FILE.exists():
        print(f"Warning: Index file not found: {INDEX_FILE}")
        return False
    
    content = INDEX_FILE.read_text(encoding='utf-8')
    post_num = get_post_number(filename)
    
    if not post_num:
        print(f"Warning: Could not extract post number from {filename}")
        return False
    
    # Pattern to match the table row for this post
    # Format: <tr><td align="right">NNNN</td><td>DATE</td><td><a href="file">Title</a>...</td><td>Categories</td></tr>
    # Note: categories field uses .*? to handle potential HTML or special characters
    pattern = rf'(<tr><td[^>]*>{post_num}</td><td>)(\d{{4}}-\d{{2}}-\d{{2}})(</td><td><a href="{re.escape(filename)}">)([^<]+)(</a>.*?</td><td>)(.*?)(</td></tr>)'
    
    match = re.search(pattern, content, flags=re.DOTALL)
    
    if not match:
        print(f"Warning: Could not find post {post_num} in index.html")
        return False
    
    # Get current values
    current_date = match.group(2)
    current_title = match.group(4)
    current_categories = match.group(6)
    
    # Apply updates
    updated_date = new_date.strftime("%Y-%m-%d") if new_date else current_date
    updated_title = new_title if new_title else current_title
    updated_categories = new_categories if new_categories else current_categories
    
    # Build replacement string
    replacement = f"{match.group(1)}{updated_date}{match.group(3)}{updated_title}{match.group(5)}{updated_categories}{match.group(7)}"
    
    new_content = content[:match.start()] + replacement + content[match.end():]
    
    if dry_run:
        changes = []
        if new_title: changes.append(f"title: '{current_title}' → '{updated_title}'")
        if new_date: changes.append(f"date: '{current_date}' → '{updated_date}'")
        if new_categories: changes.append(f"categories: '{current_categories}' → '{updated_categories}'")
        print(f"[DRY RUN] Would update in index.html: {', '.join(changes)}")
    else:
        INDEX_FILE.write_text(new_content, encoding='utf-8')
        print(f"Updated index.html for post #{post_num}")
    
    return True


def update_chrono(filename, new_title=None, new_date=None, dry_run=False):
    """Update the post in chrono-data.json."""
    if not CHRONO_FILE.exists():
        print(f"Warning: Chrono file not found: {CHRONO_FILE}")
        return False
    
    try:
        chrono_data = json.loads(CHRONO_FILE.read_text(encoding='utf-8'))
    except json.JSONDecodeError as e:
        print(f"Warning: Could not parse chrono file: {e}")
        return False
    
    post_num = get_post_number(filename)
    
    # Find the post
    post = None
    for p in chrono_data.get('posts', []):
        if p.get('file') == filename or p.get('num') == post_num:
            post = p
            break
    
    if not post:
        print(f"Note: {filename} not found in chrono-data.json")
        return False
    
    changes = []
    old_year = post.get('year')
    
    if new_title:
        changes.append(f"title: '{post.get('title')}' → '{new_title}'")
        post['title'] = new_title
    
    if new_date:
        old_date = post.get('date')
        new_date_str = new_date.strftime("%Y-%m-%d")
        changes.append(f"date: '{old_date}' → '{new_date_str}'")
        post['date'] = new_date_str
        post['year'] = new_date.year
        post['month'] = new_date.month
        
        # Update year statistics if year changed
        new_year = new_date.year
        if old_year != new_year:
            # Decrement old year count and recompute its first/last post
            old_year_entry = None
            for year_entry in chrono_data.get('years', []):
                if year_entry.get('year') == old_year:
                    year_entry['count'] = max(0, year_entry['count'] - 1)
                    old_year_entry = year_entry
                    break
            
            # Recalculate firstPost/lastPost for old year or remove if empty
            if old_year_entry is not None:
                if old_year_entry.get('count', 0) > 0:
                    # Recalculate first/last post from remaining posts
                    first_post = None
                    last_post = None
                    for p in chrono_data.get('posts', []):
                        if p.get('year') == old_year:
                            post_no = p.get('num')
                            if post_no is None:
                                continue
                            if first_post is None or post_no < first_post:
                                first_post = post_no
                            if last_post is None or post_no > last_post:
                                last_post = post_no
                    if first_post is not None and last_post is not None:
                        old_year_entry['firstPost'] = first_post
                        old_year_entry['lastPost'] = last_post
                else:
                    # Remove years that no longer have any posts
                    chrono_data['years'] = [
                        y for y in chrono_data.get('years', [])
                        if y.get('year') != old_year
                    ]
            
            # Increment new year count (or create entry)
            year_found = False
            for year_entry in chrono_data.get('years', []):
                if year_entry.get('year') == new_year:
                    year_entry['count'] += 1
                    year_entry['lastPost'] = max(year_entry['lastPost'], post_num)
                    year_entry['firstPost'] = min(year_entry['firstPost'], post_num)
                    year_found = True
                    break
            
            if not year_found:
                chrono_data['years'].append({
                    'year': new_year,
                    'count': 1,
                    'firstPost': post_num,
                    'lastPost': post_num
                })
                chrono_data['years'].sort(key=lambda y: y['year'], reverse=True)
    
    if changes:
        chrono_data['lastUpdated'] = datetime.now().strftime("%Y-%m-%d")
        
        if dry_run:
            print(f"[DRY RUN] Would update in chrono-data.json: {', '.join(changes)}")
        else:
            CHRONO_FILE.write_text(
                json.dumps(chrono_data, indent=2, ensure_ascii=False),
                encoding='utf-8'
            )
            print(f"Updated chrono-data.json for post #{post_num}")
        return True
    
    return False


def update_all_posts_section(dry_run=False):
    """
    Regenerate the complete 'All Posts' table from chrono-data.json.
    This ensures the section stays synchronized with the chronological data.
    
    Args:
        dry_run: If True, show preview without writing
        
    Returns:
        bool: True if successful, False otherwise
    """
    if not CHRONO_FILE.exists():
        print(f"Warning: Chrono file not found: {CHRONO_FILE}")
        return False
    
    if not INDEX_FILE.exists():
        print(f"Warning: Index file not found: {INDEX_FILE}")
        return False
    
    try:
        chrono_data = json.loads(CHRONO_FILE.read_text(encoding='utf-8'))
    except json.JSONDecodeError as e:
        print(f"Warning: Could not parse chrono file: {e}")
        return False
    
    posts = chrono_data.get('posts', [])
    
    # Generate table rows
    rows = []
    for post in posts:
        post_num = post.get('num', 0)
        date = post.get('date', '')
        title = post.get('title', '')
        filename = post.get('file', '')
        categories = ''  # Not in chrono-data.json
        
        row = (
            f'<tr><td align="right">{post_num}</td>'
            f'<td>{date}</td>'
            f'<td><a href="{filename}">{title}</a>'
            f'&nbsp;&nbsp;&nbsp;<a href="{filename}">web</a>'
            f'&nbsp;&nbsp;&nbsp;&nbsp;</td>'
            f'<td>{categories}</td></tr>'
        )
        rows.append(row)
    
    table_rows_html = '\n'.join(rows)
    
    # Update index.html
    content = INDEX_FILE.read_text(encoding='utf-8')
    pattern = r'(<h3>All Posts</h3>.*?<th>Categories</th>\s*</tr>)(.*?)(</table>)'
    match = re.search(pattern, content, flags=re.DOTALL)
    
    if not match:
        print("Warning: Could not find 'All Posts' table in index.html")
        return False
    
    new_content = content[:match.start(2)] + '\n' + table_rows_html + '\n' + content[match.end(2):]
    
    if dry_run:
        print(f"[DRY RUN] Would update 'All Posts' section with {len(posts)} posts")
    else:
        INDEX_FILE.write_text(new_content, encoding='utf-8')
        print(f"Updated 'All Posts' section with {len(posts)} posts")
    
    return True


def update_toc(filename, new_title=None, dry_run=False):
    """Update the post title in toc-data.json if it's in a topic."""
    if not TOC_FILE.exists():
        print(f"Warning: TOC file not found: {TOC_FILE}")
        return False
    
    if not new_title:
        return False  # Only title can be updated in TOC
    
    try:
        toc_data = json.loads(TOC_FILE.read_text(encoding='utf-8'))
    except json.JSONDecodeError as e:
        print(f"Warning: Could not parse TOC file: {e}")
        return False
    
    updated = False
    topic_name = None
    
    # Search all topics for the file
    for topic in toc_data.get('topics', []):
        for post in topic.get('posts', []):
            if post.get('file') == filename:
                post['title'] = new_title
                updated = True
                topic_name = topic.get('title')
                break
        
        # Also check subtopics
        for subtopic in topic.get('subTopics', []):
            for post in subtopic.get('posts', []):
                if post.get('file') == filename:
                    post['title'] = new_title
                    updated = True
                    topic_name = subtopic.get('title')
                    break
    
    if updated:
        toc_data['lastUpdated'] = datetime.now().strftime("%Y-%m-%d")
        
        if dry_run:
            print(f"[DRY RUN] Would update title in toc-data.json (topic: {topic_name})")
        else:
            TOC_FILE.write_text(
                json.dumps(toc_data, indent=2, ensure_ascii=False),
                encoding='utf-8'
            )
            print(f"Updated toc-data.json (topic: {topic_name})")
        return True
    else:
        print(f"Note: {filename} not found in any topic in toc-data.json")
        return False


def update_search_index(dry_run=False):
    """Regenerate search index after updating a post.
    
    Args:
        dry_run: If True, show preview without writing
        
    Returns:
        bool: True if successful, False otherwise
    """
    if dry_run:
        print("[DRY RUN] Would regenerate search index")
        return True
    
    print("\nUpdating search index...")
    try:
        from build_search_index import SearchIndexBuilder
        builder = SearchIndexBuilder()
        builder.run()
        return True
    except ImportError as e:
        print(f"Warning: Could not import search index builder: {e}")
        print("Search index may be out of date")
        return False
    except Exception as e:
        print(f"Warning: Failed to update search index: {e}")
        print("Search index may be out of date")
        return False


def update_post(filename, title=None, date=None, categories=None, 
                update_html=True, dry_run=False):
    """Update a post's metadata across all relevant files."""
    
    # Normalize filename
    filename = Path(filename).name
    
    print(f"Updating post: {filename}")
    print("=" * 50)
    
    # Parse date if provided as string
    if date and isinstance(date, str):
        try:
            date = datetime.strptime(date, "%Y-%m-%d")
        except ValueError:
            print(f"Error: Invalid date format '{date}'. Expected YYYY-MM-DD (e.g., 2026-01-10).")
            sys.exit(1)
    
    results = {
        'html': False,
        'index': False,
        'chrono': False,
        'toc': False
    }
    
    # Update HTML file title
    if title and update_html:
        results['html'] = update_html_title(filename, title, dry_run)
    
    # Update index.html
    results['index'] = update_index(filename, title, date, categories, dry_run)
    
    # Update chrono-data.json
    results['chrono'] = update_chrono(filename, title, date, dry_run)
    
    # Update toc-data.json (only for title changes)
    if title:
        results['toc'] = update_toc(filename, title, dry_run)
    
    # Update "All Posts" section in index.html if chrono was updated
    if results['chrono']:
        update_all_posts_section(dry_run)
    
    # Update search index when searchable metadata (currently the title) changes
    if title:
        update_search_index(dry_run)
    
    # Summary
    print()
    print("=" * 50)
    if dry_run:
        print("[DRY RUN] Summary:")
    else:
        print("Update complete.")
    
    print(f"  Updated HTML file: {'Yes' if results['html'] else 'No'}")
    print(f"  Updated index.html: {'Yes' if results['index'] else 'No'}")
    print(f"  Updated chrono-data.json: {'Yes' if results['chrono'] else 'No'}")
    print(f"  Updated toc-data.json: {'Yes' if results['toc'] else 'No/Not in topic'}")
    
    if not dry_run and any(results.values()):
        print("\nNext steps:")
        print("  git add -A")
        print(f'  git commit -m "Update post: {filename}"')
        print("  git push")
    
    return any(results.values())


def main():
    parser = argparse.ArgumentParser(
        description="Update metadata for a published blog post",
        formatter_class=argparse.RawDescriptionHelpFormatter,
        epilog="""
Examples:
  python update_post.py 2079_my_post.html --title "New Title"
  python update_post.py 2079_my_post.html --date 2026-01-10
  python update_post.py 2079_my_post.html --categories "Revit API, Geometry"
  python update_post.py 2079_my_post.html --title "New" --date 2026-01-10 --dry-run

This script updates:
  - a/index.html (title, date, categories)
  - a/toc/chrono-data.json (title, date)
  - a/toc/toc-data.json (title only, if post is in a topic)
  - The HTML file's <title> tag (title only)
  
Note: For content changes, just edit the HTML file directly.
      For topic changes, use manage_topics.py instead.
        """
    )
    
    parser.add_argument(
        "filename",
        help="Filename of the post to update (e.g., 2079_my_post.html)"
    )
    parser.add_argument(
        "--title", "-t",
        help="New title for the post"
    )
    parser.add_argument(
        "--date", "-d",
        help="New date for the post (YYYY-MM-DD)"
    )
    parser.add_argument(
        "--categories", "-c",
        help="New categories (comma-separated)"
    )
    parser.add_argument(
        "--no-html",
        action="store_true",
        help="Don't update the HTML file's <title> tag"
    )
    parser.add_argument(
        "--dry-run",
        action="store_true",
        help="Preview changes without writing files"
    )
    
    args = parser.parse_args()
    
    if not args.title and not args.date and not args.categories:
        parser.error("At least one of --title, --date, or --categories is required")
    
    success = update_post(
        args.filename,
        title=args.title,
        date=args.date,
        categories=args.categories,
        update_html=not args.no_html,
        dry_run=args.dry_run
    )
    
    sys.exit(0 if success else 1)


if __name__ == "__main__":
    main()
