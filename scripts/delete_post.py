#!/usr/bin/env python3
"""
delete_post.py - Delete a published blog post

This script removes a post and updates all related files:
- Deletes the HTML file
- Removes entry from a/index.html
- Removes entry from a/toc/toc-data.json
- Does NOT update homepage stats (manual review recommended)

Usage:
    python delete_post.py 2079_sample_post.html
    python delete_post.py 2079_sample_post.html --dry-run

Author: parametrix
Date: January 4, 2026
"""

import argparse
import html
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


def delete_html_file(filename, dry_run=False):
    """Delete the HTML post file."""
    file_path = POSTS_DIR / filename
    
    if not file_path.exists():
        print(f"Warning: File not found: {file_path}")
        return False
    
    if dry_run:
        print(f"[DRY RUN] Would delete: {file_path}")
    else:
        file_path.unlink()
        print(f"Deleted: {file_path}")
    
    return True


def remove_from_index(filename, dry_run=False):
    """Remove the post entry from index.html."""
    if not INDEX_FILE.exists():
        print(f"Warning: Index file not found: {INDEX_FILE}")
        return False
    
    content = INDEX_FILE.read_text(encoding='utf-8')
    
    # Pattern to match the table row containing this file
    # The row format is: <tr><td>NUM</td><td>DATE</td><td><a href="file">Title</a>...<a href="file">web</a>...</td><td>Categories</td></tr>
    pattern = rf'<tr><td[^>]*>\d+</td><td>[^<]+</td><td>.*?<a href="{re.escape(filename)}".*?</td><td>[^<]*</td></tr>\n?'
    
    new_content, count = re.subn(pattern, '', content, flags=re.DOTALL)
    
    if count > 0:
        if dry_run:
            print(f"[DRY RUN] Would remove from index.html: {filename}")
        else:
            INDEX_FILE.write_text(new_content, encoding='utf-8')
            print(f"Removed from index.html: {filename}")
        return True
    else:
        print(f"Warning: Could not find {filename} in index.html")
        return False


def remove_from_toc(filename, dry_run=False):
    """Remove the post from toc-data.json."""
    if not TOC_FILE.exists():
        print(f"Warning: TOC file not found: {TOC_FILE}")
        return False
    
    try:
        toc_data = json.loads(TOC_FILE.read_text(encoding='utf-8'))
    except json.JSONDecodeError as e:
        print(f"Warning: Could not parse TOC file: {e}")
        return False
    
    removed = False
    
    # Search all topics for the file
    for topic in toc_data.get('topics', []):
        posts = topic.get('posts', [])
        original_len = len(posts)
        
        # Filter out the post with matching filename
        topic['posts'] = [p for p in posts if p.get('file') != filename]
        
        if len(topic['posts']) < original_len:
            removed = True
            if dry_run:
                print(f"[DRY RUN] Would remove from TOC topic '{topic.get('title')}': {filename}")
            else:
                print(f"Removed from TOC topic '{topic.get('title')}': {filename}")
    
    if removed:
        # Update metadata
        toc_data['lastUpdated'] = datetime.now().strftime("%Y-%m-%d")
        toc_data['totalPostLinks'] = sum(len(t.get('posts', [])) for t in toc_data['topics'])
        
        if not dry_run:
            TOC_FILE.write_text(
                json.dumps(toc_data, indent=2, ensure_ascii=False),
                encoding='utf-8'
            )
            print(f"Updated toc-data.json (now {toc_data['totalPostLinks']} links)")
    else:
        print(f"Note: {filename} was not found in toc-data.json")
    
    return removed


def remove_from_chrono(filename, dry_run=False):
    """Remove the post from chrono-data.json (right column timeline)."""
    if not CHRONO_FILE.exists():
        print(f"Warning: Chrono file not found: {CHRONO_FILE}")
        return False
    
    try:
        chrono_data = json.loads(CHRONO_FILE.read_text(encoding='utf-8'))
    except json.JSONDecodeError as e:
        print(f"Warning: Could not parse chrono file: {e}")
        return False
    
    # Find and remove the post
    original_len = len(chrono_data.get('posts', []))
    removed_post = None
    
    for post in chrono_data.get('posts', []):
        if post.get('file') == filename:
            removed_post = post
            break
    
    if removed_post:
        chrono_data['posts'] = [p for p in chrono_data['posts'] if p.get('file') != filename]
        chrono_data['totalPosts'] = len(chrono_data['posts'])
        
        # Update year statistics
        year = removed_post.get('year')
        for year_entry in chrono_data.get('years', []):
            if year_entry.get('year') == year:
                year_entry['count'] -= 1
                if year_entry['count'] <= 0:
                    # Remove the year entry if no posts left
                    chrono_data['years'] = [y for y in chrono_data['years'] if y.get('year') != year]
                break
        
        chrono_data['lastUpdated'] = datetime.now().strftime("%Y-%m-%d")
        
        if dry_run:
            print(f"[DRY RUN] Would remove from chrono-data.json: {filename}")
        else:
            CHRONO_FILE.write_text(
                json.dumps(chrono_data, indent=2, ensure_ascii=False),
                encoding='utf-8'
            )
            print(f"Removed from chrono-data.json: {filename}")
        return True
    else:
        print(f"Note: {filename} was not found in chrono-data.json")
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
            f'<tr><td align="right">{html.escape(str(post_num))}</td>'
            f'<td>{html.escape(str(date))}</td>'
            f'<td><a href="{html.escape(filename, quote=True)}">{html.escape(title)}</a>'
            f'&nbsp;&nbsp;&nbsp;<a href="{html.escape(filename, quote=True)}">web</a>'
            f'&nbsp;&nbsp;&nbsp;&nbsp;</td>'
            f'<td>{html.escape(categories)}</td></tr>'
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


# NOTE: The update_search_index function has been removed.
# Search is now powered by Pagefind, which is automatically rebuilt
# by the GitHub Actions workflow (.github/workflows/pagefind.yml)
# when HTML files are pushed to gh-pages.


def delete_post(filename, dry_run=False):
    """Delete a post and update all related files."""
    
    # Normalize filename
    filename = Path(filename).name
    
    print(f"Deleting post: {filename}")
    print("=" * 50)
    
    # Delete HTML file
    html_deleted = delete_html_file(filename, dry_run)
    
    # Remove from index
    index_updated = remove_from_index(filename, dry_run)
    
    # Remove from TOC
    toc_updated = remove_from_toc(filename, dry_run)
    
    # Remove from chrono-data.json
    chrono_updated = remove_from_chrono(filename, dry_run)
    
    # Update "All Posts" section in index.html
    if chrono_updated:
        update_all_posts_section(dry_run)
    
    # Note: Search index (Pagefind) is automatically rebuilt by GitHub Actions
    # when HTML files are pushed to gh-pages. No local update needed.
    
    # Summary
    print()
    print("=" * 50)
    if dry_run:
        print("[DRY RUN] Summary:")
        print(f"  Would delete HTML file: {'Yes' if html_deleted else 'No (not found)'}")
        print(f"  Would update index.html: {'Yes' if index_updated else 'No'}")
        print(f"  Would update toc-data.json: {'Yes' if toc_updated else 'No'}")
        print(f"  Would update chrono-data.json: {'Yes' if chrono_updated else 'No'}")
    else:
        print("Deletion complete.")
        print("\nNote: Homepage stats were not updated.")
        print("If needed, manually edit index.html to update the post count.")
        print("\nNext steps:")
        print("  git add -A")
        print(f'  git commit -m "Delete post: {filename}"')
        print("  git push")
    
    return html_deleted or index_updated


def main():
    parser = argparse.ArgumentParser(
        description="Delete a published blog post",
        formatter_class=argparse.RawDescriptionHelpFormatter,
        epilog="""
Examples:
  python delete_post.py 2079_sample_post.html
  python delete_post.py 2079_sample_post.html --dry-run
  python delete_post.py a/2079_sample_post.html  # Path is also accepted

This script removes:
  - The HTML file from a/
  - The entry from a/index.html
  - The entry from a/toc/toc-data.json (left sidebar)
  - The entry from a/toc/chrono-data.json (right column timeline)
        """
    )
    
    parser.add_argument(
        "filename",
        help="Filename of the post to delete (e.g., 2079_sample_post.html)"
    )
    parser.add_argument(
        "--dry-run",
        action="store_true",
        help="Preview changes without deleting files"
    )
    
    args = parser.parse_args()
    
    success = delete_post(args.filename, dry_run=args.dry_run)
    
    sys.exit(0 if success else 1)


if __name__ == "__main__":
    main()
