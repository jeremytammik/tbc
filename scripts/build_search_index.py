#!/usr/bin/env python3
"""
build_search_index.py - DEPRECATED

NOTE: This script is DEPRECATED as of January 2026.
Search functionality is now handled by Pagefind (https://pagefind.app/).
The Pagefind index is automatically built by the GitHub Actions workflow
(.github/workflows/pagefind.yml) when HTML files are pushed to gh-pages.

To build the Pagefind index locally for testing:
    npx pagefind --site a --output-path a/pagefind

The old JSON search index (a/toc/search-index.json) is no longer used
and can be deleted.

Original description:
This script scans all HTML blog posts and generates a JSON search index
containing post metadata and content excerpts for client-side full-text search.

The index is saved to a/toc/search-index.json and loaded by toc-sidebar.js
to enable searching within post content, not just titles.

Usage:
    python scripts/build_search_index.py

Output:
    a/toc/search-index.json

Author: parametrix
Date: January 10, 2026
"""

import json
import re
import sys
import warnings
from datetime import datetime
from pathlib import Path

# Show deprecation warning
warnings.warn(
    "build_search_index.py is deprecated. "
    "Search is now powered by Pagefind. "
    "The index is built automatically by GitHub Actions when HTML files change.",
    DeprecationWarning,
    stacklevel=2
)

try:
    from bs4 import BeautifulSoup
except ImportError as exc:
    raise ImportError(
        "BeautifulSoup is required. Install it with: pip install beautifulsoup4"
    ) from exc

# Configuration
REPO_ROOT = Path(__file__).parent.parent
POSTS_DIR = REPO_ROOT / "a"
OUTPUT_FILE = POSTS_DIR / "toc" / "search-index.json"
TOC_FILE = POSTS_DIR / "toc" / "toc-data.json"

# Index settings
EXCERPT_LENGTH = 200  # Characters for human-readable excerpt
CONTENT_PREVIEW_LENGTH = 4000  # Characters for searchable content


class SearchIndexBuilder:
    """Builds a search index from HTML blog posts."""
    
    def __init__(self, posts_dir=POSTS_DIR, output_path=OUTPUT_FILE, toc_path=TOC_FILE):
        self.posts_dir = Path(posts_dir)
        self.output_path = Path(output_path)
        self.toc_path = Path(toc_path)
        
    def get_post_files(self):
        """Get all HTML post files matching the pattern ####_*.htm or ####_*.html."""
        pattern = re.compile(r'^\d{4}_.*\.html?$')
        files = []
        
        for file in self.posts_dir.iterdir():
            if file.is_file() and pattern.match(file.name):
                files.append(file)
        
        # Sort by post number
        files.sort(key=lambda f: int(f.name[:4]))
        return files
    
    def load_toc_data(self):
        """Load toc-data.json for post metadata."""
        if not self.toc_path.exists():
            print(f"Warning: {self.toc_path} not found")
            return {}
        
        with open(self.toc_path, 'r', encoding='utf-8') as f:
            data = json.load(f)
        
        # Build lookup by file name from topics
        lookup = {}
        for topic in data.get('topics', []):
            for post in topic.get('posts', []):
                # Avoid duplicates (same file can appear in multiple topics)
                if post['file'] not in lookup:
                    lookup[post['file']] = post
        
        return lookup
    
    def extract_content(self, html_path):
        """Extract text content from HTML file.
        
        Returns:
            tuple: (excerpt, content_preview)
        """
        try:
            with open(html_path, 'r', encoding='utf-8') as f:
                html = f.read()
        except UnicodeDecodeError:
            # Try with latin-1 fallback
            try:
                with open(html_path, 'r', encoding='latin-1') as f:
                    html = f.read()
            except Exception as e:
                print(f"Warning: Could not read {html_path}: {e}")
                return "", ""
        except Exception as e:
            print(f"Warning: Could not read {html_path}: {e}")
            return "", ""
        
        soup = BeautifulSoup(html, 'html.parser')
        
        # Remove script, style, and navigation elements
        for tag in soup(['script', 'style', 'nav', 'header', 'footer', 'noscript']):
            tag.decompose()
        
        # Remove code blocks for cleaner content (keep for display but not search)
        for tag in soup(['pre', 'code']):
            tag.decompose()
        
        # Get text content
        text = soup.get_text(separator=' ', strip=True)
        
        # Clean up whitespace
        text = re.sub(r'\s+', ' ', text).strip()
        
        # Generate excerpt (human-readable, sentence-aware)
        excerpt = self._generate_excerpt(text, EXCERPT_LENGTH)
        
        # Generate content preview (for searching, lowercase, normalized)
        content_preview = self._generate_content_preview(text, CONTENT_PREVIEW_LENGTH)
        
        return excerpt, content_preview
    
    def _generate_excerpt(self, text, max_length):
        """Generate a human-readable excerpt that ends at a sentence boundary."""
        if len(text) <= max_length:
            return text
        
        # Try to break at sentence boundary
        truncated = text[:max_length]
        
        # Look for last sentence-ending punctuation
        last_period = truncated.rfind('.')
        last_question = truncated.rfind('?')
        last_exclaim = truncated.rfind('!')
        
        last_sentence = max(last_period, last_question, last_exclaim)
        
        if last_sentence > max_length * 0.5:  # At least half the excerpt
            return truncated[:last_sentence + 1]
        
        # Fall back to word boundary
        last_space = truncated.rfind(' ')
        if last_space > max_length * 0.7:
            return truncated[:last_space] + '...'
        
        return truncated + '...'
    
    def _generate_content_preview(self, text, max_length):
        """Generate searchable content preview (lowercase, normalized)."""
        # Lowercase for case-insensitive search
        normalized = text.lower()
        
        # Remove special characters but keep spaces
        normalized = re.sub(r'[^\w\s]', ' ', normalized)
        
        # Collapse whitespace
        normalized = re.sub(r'\s+', ' ', normalized).strip()
        
        # Truncate at word boundary
        if len(normalized) <= max_length:
            return normalized
        
        truncated = normalized[:max_length]
        last_space = truncated.rfind(' ')
        
        if last_space > max_length * 0.8:
            return truncated[:last_space]
        
        return truncated
    
    def build_index(self):
        """Build the complete search index."""
        print("Building search index...")
        
        # Load metadata
        toc_lookup = self.load_toc_data()
        
        # Get post files
        post_files = self.get_post_files()
        print(f"Found {len(post_files)} post files")
        
        posts = []
        errors = 0
        
        for i, html_path in enumerate(post_files):
            if (i + 1) % 100 == 0:
                print(f"  Processing {i + 1}/{len(post_files)}...")
            
            filename = html_path.name
            post_num = int(filename[:4])
            
            # Get metadata from toc-data
            meta = toc_lookup.get(filename, {})
            title = meta.get('title', filename)
            
            # Extract content
            excerpt, content_preview = self.extract_content(html_path)
            
            if not content_preview:
                errors += 1
                # Include post with empty content rather than skip
                content_preview = ""
                excerpt = ""
            
            posts.append({
                "num": post_num,
                "file": filename,
                "title": title,
                "excerpt": excerpt,
                "contentPreview": content_preview
            })
        
        if errors > 0:
            print(f"Warning: {errors} posts had content extraction issues")
        
        index = {
            "version": "1.0",
            "generated": datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%SZ"),
            "totalPosts": len(posts),
            "posts": posts
        }
        
        return index
    
    def save_index(self, index):
        """Save index to JSON file."""
        # Ensure output directory exists
        self.output_path.parent.mkdir(parents=True, exist_ok=True)
        
        with open(self.output_path, 'w', encoding='utf-8') as f:
            json.dump(index, f, ensure_ascii=False, separators=(',', ':'))
        
        print(f"Search index saved to: {self.output_path}")
        
        # Print file size
        size_kb = self.output_path.stat().st_size / 1024
        print(f"Index file size: {size_kb:.1f} KB")
        
        # Calculate estimated gzip size (rough estimate)
        estimated_gzip_kb = size_kb * 0.3  # Typical JSON compression
        print(f"Estimated gzip size: {estimated_gzip_kb:.1f} KB")
    
    def run(self):
        """Run the complete index building process."""
        index = self.build_index()
        self.save_index(index)
        print("Search index generation complete!")


def main():
    builder = SearchIndexBuilder()
    builder.run()


if __name__ == '__main__':
    main()
