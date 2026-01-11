# Search Index Implementation Specification

**Version**: 1.1 (Corrected January 10, 2026)

## Changelog

### v1.1 (January 10, 2026)
- **Fixed**: Python import ordering (sys must be imported before Path usage)
- **Fixed**: `update_post.py` integration - clarified it doesn't have `update_all_posts_section()`
- **Fixed**: CONFIG/state location - clarified these are inside Sidebar IIFE scope (lines 24, 40)
- **Fixed**: `loadSearchIndex()` integration - moved into `initSidebar()` after TOC loads (not separate `init()`)
- **Fixed**: LocalStorage cache keys - changed from `tbc-search-*` to `tbc-sidebar-search-*` for consistency
- **Added**: Note about existing `escapeHtml()` function (line 60) - avoid duplication
- **Clarified**: Phase 2 in-page highlighting is independent of sidebar IIFE scope

---

## Overview

This document provides a comprehensive specification and implementation plan for enhancing The Building Coder search functionality to include post content in addition to post titles. The solution uses a pre-built search index approach that balances performance, maintainability, and user experience.

## Current State

### Existing Search Implementation
- **Location**: `a/toc/toc-sidebar.js` (lines 546-655)
- **Functionality**: 
  - Searches only post titles
  - Uses simple substring matching with `includes(query)`
  - Real-time highlighting with debouncing (150ms)
  - Expands parent topics when posts match
  - Shows result count
- **Data Source**: Post titles are read directly from DOM elements (`.tbc-post-link`)
- **Performance**: Instant (searches ~2,079 titles in-memory)

### Data Sources
- **chrono-data.json**: Contains post metadata (num, file, title, date, year, month) - NO content
- **toc-data.json**: Contains hierarchical topic/post structure - NO content
- **Individual HTML files**: Full post content in 2,079+ HTML files (`a/####_*.htm`)

## Solution Architecture

### Pre-Built Search Index Approach

**Core Concept**: Generate a lightweight JSON index containing post excerpts during the build/publish process. Load this index client-side and perform fast in-memory searches.

**Key Benefits**:
1. Fast client-side search (no network requests during search)
2. No server-side infrastructure required (works with GitHub Pages)
3. Deterministic and repeatable index generation
4. Works offline once page is loaded
5. Enables relevance ranking and context snippets

**Trade-offs**:
1. Increases initial page load (~150-200KB compressed)
2. Requires regeneration on any content change
3. Must integrate into all publish/edit workflows

---

## Detailed Specification

### 1. Search Index File Structure

**File Path**: `a/toc/search-index.json`

**Format**:
```json
{
  "version": "1.0",
  "generated": "2026-01-09T12:00:00Z",
  "totalPosts": 2079,
  "posts": [
    {
      "num": 1,
      "file": "0001_welcome.htm",
      "title": "Welcome",
      "excerpt": "Welcome to The Building Coder, a blog dedicated to programmers working with the Revit API. My name is Jeremy Tammik and I work in the DevTech team...",
      "contentPreview": "welcome building coder blog dedicated programmers revit api jeremy tammik devtech developer technical services..."
    },
    {
      "num": 2,
      "file": "0002_devtech.htm",
      "title": "Introduction to DevTech",
      "excerpt": "What does DevTech do? DevTech is Autodesk's Developer Technical Services team...",
      "contentPreview": "devtech autodesk developer technical services team programming support consulting training..."
    }
  ]
}
```

**Field Definitions**:
- `version`: Schema version for future compatibility
- `generated`: ISO 8601 timestamp of index generation
- `totalPosts`: Total number of posts in index
- `posts`: Array of post search records
  - `num`: Post number (integer, for sorting/identification)
  - `file`: Filename (for linking)
  - `title`: Post title (already available, included for completeness)
  - `excerpt`: Human-readable excerpt (first 300-400 chars, for display)
  - `contentPreview`: Searchable text (cleaned, lowercased, ~500-1000 chars, for searching)

**Size Estimates** (updated with optimized CONTENT_PREVIEW_LENGTH=800):
- Average post size: ~250 bytes (title + excerpt + contentPreview)
- Total uncompressed: 2,079 × 250 = ~520 KB
- Gzip compressed: ~130-170 KB (typical 70% compression ratio)
- Load time on 10 Mbps: ~0.15 seconds

**Note**: CONTENT_PREVIEW_LENGTH reduced from 1000 to 800 characters to optimize file size while maintaining search quality.

### 2. Content Extraction Rules

**HTML Parsing Strategy**:
```python
def extract_post_content(html_file_path):
    """
    Extract searchable content from a post HTML file.
    
    Extraction rules:
    1. Look for <article> tag first (preferred container)
    2. Fall back to main content area if <article> not found
    3. Exclude navigation, headers, footers
    4. Exclude code blocks (keep text explanations only)
    5. Remove HTML tags
    6. Normalize whitespace
    7. Decode HTML entities
    """
```

**Content Areas to Include**:
- Main article text (paragraphs, headings)
- List items
- Blockquotes
- Image alt text (if meaningful)
- Table content (text only)

**Content Areas to Exclude**:
- Navigation links (`.nav`, header, footer)
- Code blocks (`<pre>`, `<code>` blocks)
  - *Rationale*: Code would dominate search results, users typically search for concepts not code
  - *Exception*: Keep inline code mentions like `ElementId` or `FilteredElementCollector`
- Script tags
- Style tags
- HTML/CSS syntax
- Excessive whitespace

**Text Processing Pipeline**:
```python
def process_content(raw_html):
    1. Parse HTML with BeautifulSoup
    2. Remove excluded elements (nav, footer, pre, script, style)
    3. Extract text from included elements
    4. Decode HTML entities (&amp; → &, &lt; → <, etc.)
    5. Normalize whitespace (multiple spaces → single space)
    6. Remove excessive punctuation
    7. Convert to lowercase for contentPreview
    8. Truncate to max length
    9. Create excerpt (preserve case, add ellipsis)
    10. Return both excerpt and contentPreview
```

**Excerpt Generation**:
- Length: First 300-400 characters
- Break on sentence boundary if possible
- Add "..." if truncated
- Preserve original case and formatting
- Purpose: Display in search results

**Content Preview Generation**:
- Length: First 800-1200 characters
- Lowercase for case-insensitive searching
- Remove extra whitespace
- Purpose: Fast text search

### 3. Search Index Generator Script

**File**: `scripts/build_search_index.py`

**Dependencies**:
```python
# Standard library
import json
import re
from pathlib import Path
from datetime import datetime
from typing import Dict, List, Optional

# Third-party (beautifulsoup4 already in requirements.txt)
from bs4 import BeautifulSoup
import html as html_lib
```

**Implementation**:

```python
#!/usr/bin/env python3
"""
Build search index for The Building Coder.

This script generates a search index (search-index.json) containing
post excerpts for client-side content search functionality.

Usage:
    python scripts/build_search_index.py
    
Output:
    a/toc/search-index.json
"""

import json
import re
from pathlib import Path
from datetime import datetime
from typing import Dict, List, Optional, Tuple
from bs4 import BeautifulSoup
import html as html_lib

# Configuration
REPO_ROOT = Path(__file__).parent.parent
POSTS_DIR = REPO_ROOT / "a"
CHRONO_DATA_PATH = REPO_ROOT / "a" / "toc" / "chrono-data.json"
OUTPUT_PATH = REPO_ROOT / "a" / "toc" / "search-index.json"

EXCERPT_LENGTH = 350  # Characters for display excerpt
CONTENT_PREVIEW_LENGTH = 800  # Characters for searchable content (optimized)
VERSION = "1.0"

class SearchIndexBuilder:
    """Builds search index from blog post HTML files."""
    
    def __init__(self):
        self.posts_dir = POSTS_DIR
        self.chrono_data_path = CHRONO_DATA_PATH
        self.output_path = OUTPUT_PATH
        
    def load_post_metadata(self) -> List[Dict]:
        """Load post metadata from chrono-data.json."""
        with open(self.chrono_data_path, 'r', encoding='utf-8') as f:
            data = json.load(f)
        return data['posts']
    
    def extract_content(self, html_path: Path) -> Tuple[str, str]:
        """
        Extract searchable content from HTML file.
        
        Returns:
            tuple: (excerpt, content_preview)
                - excerpt: Human-readable excerpt for display
                - content_preview: Lowercased searchable text
        """
        try:
            with open(html_path, 'r', encoding='utf-8') as f:
                html_content = f.read()
        except Exception as e:
            print(f"Warning: Could not read {html_path}: {e}")
            return "", ""
        
        soup = BeautifulSoup(html_content, 'html.parser')
        
        # Remove unwanted elements
        for element in soup.find_all(['script', 'style', 'nav', 'footer']):
            element.decompose()
        
        # Try to find main content area
        article = soup.find('article')
        if article:
            content_element = article
        else:
            # Fall back to body
            content_element = soup.find('body')
            if not content_element:
                content_element = soup
        
        # Remove code blocks (keep inline code)
        for pre in content_element.find_all('pre'):
            pre.decompose()
        
        # Remove large code blocks
        for code in content_element.find_all('code', class_=True):
            # Keep inline code (no class or simple highlighting)
            classes = code.get('class') or []
            if any(cls in classes for cls in ['prettyprint', 'code-block']):
                code.decompose()
        
        # Extract text
        text = content_element.get_text(separator=' ', strip=True)
        
        # Decode HTML entities
        text = html_lib.unescape(text)
        
        # Normalize whitespace
        text = re.sub(r'\s+', ' ', text)
        text = text.strip()
        
        # Generate excerpt (preserve case, sentence boundary)
        excerpt = self._generate_excerpt(text, EXCERPT_LENGTH)
        
        # Generate content preview (lowercase, for searching)
        content_preview = text[:CONTENT_PREVIEW_LENGTH].lower()
        
        return excerpt, content_preview
    
    def _generate_excerpt(self, text: str, max_length: int) -> str:
        """Generate human-readable excerpt with sentence boundary."""
        if len(text) <= max_length:
            return text
        
        # Try to break at sentence boundary
        truncated = text[:max_length]
        
        # Look for last sentence ending
        last_period = truncated.rfind('. ')
        last_exclaim = truncated.rfind('! ')
        last_question = truncated.rfind('? ')
        
        break_point = max(last_period, last_exclaim, last_question)
        
        if break_point > max_length * 0.7:  # Only use if not too short
            return truncated[:break_point + 1].strip()
        
        # Otherwise, break at word boundary
        last_space = truncated.rfind(' ')
        if last_space > 0:
            return truncated[:last_space] + "..."
        
        return truncated + "..."
    
    def build_index(self) -> Dict:
        """Build complete search index."""
        print("Building search index...")
        print(f"Reading post metadata from {self.chrono_data_path}")
        
        post_metadata = self.load_post_metadata()
        total_posts = len(post_metadata)
        
        print(f"Processing {total_posts} posts...")
        
        search_posts = []
        processed = 0
        skipped = 0
        
        for post in post_metadata:
            num = post['num']
            file = post['file']
            title = post['title']
            
            html_path = self.posts_dir / file
            
            if not html_path.exists():
                print(f"Warning: Post file not found: {html_path}")
                skipped += 1
                # Still include in index with empty content
                search_posts.append({
                    'num': num,
                    'file': file,
                    'title': title,
                    'excerpt': '',
                    'contentPreview': ''
                })
                continue
            
            excerpt, content_preview = self.extract_content(html_path)
            
            search_posts.append({
                'num': num,
                'file': file,
                'title': title,
                'excerpt': excerpt,
                'contentPreview': content_preview
            })
            
            processed += 1
            
            if processed % 100 == 0:
                print(f"  Processed {processed}/{total_posts} posts...")
        
        index = {
            'version': VERSION,
            'generated': datetime.utcnow().isoformat() + 'Z',
            'totalPosts': total_posts,
            'posts': search_posts
        }
        
        print(f"Completed: {processed} processed, {skipped} skipped")
        return index
    
    def save_index(self, index: Dict):
        """Save search index to JSON file."""
        print(f"Writing search index to {self.output_path}")
        
        # Ensure directory exists
        self.output_path.parent.mkdir(parents=True, exist_ok=True)
        
        with open(self.output_path, 'w', encoding='utf-8') as f:
            json.dump(index, f, ensure_ascii=False, indent=2)
        
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
```

**Error Handling**:
- Missing HTML files: Log warning, include post with empty content
- Parse errors: Log warning, skip content extraction
- Encoding errors: Decode using UTF-8; on failure, log a warning and skip or record empty content
- File system errors: Fail gracefully with informative error

**Testing**:
```bash
# Run manually
python scripts/build_search_index.py

# Verify output
ls -lh a/toc/search-index.json
cat a/toc/search-index.json | head -50
```

### 4. Integration with Publishing Scripts

#### 4.1 Update `publish_post.py`

**Current Flow**:
1. Convert markdown to HTML
2. Update chrono-data.json
3. Update toc-data.json
4. Update All Posts section

**New Flow** (add step):
1. Convert markdown to HTML
2. Update chrono-data.json
3. Update toc-data.json
4. Update All Posts section
5. **Regenerate search-index.json** ← NEW

**Implementation**:

```python
# NOTE: Path is already imported in publish_post.py at line 28
# Add sys import near other imports at top of file (around line 26)
import sys

# Add path handling AFTER existing Path import but BEFORE build_search_index import
SCRIPTS_DIR = Path(__file__).parent
if str(SCRIPTS_DIR) not in sys.path:
    sys.path.insert(0, str(SCRIPTS_DIR))

# Add this import after the path handling above
from build_search_index import SearchIndexBuilder

# Add new function (place near other update_* functions, around line 340)
def update_search_index(dry_run=False):
    """Regenerate search index after publishing a post."""
    if dry_run:
        print("[DRY RUN] Would regenerate search index")
        return
    
    print("\nUpdating search index...")
    try:
        builder = SearchIndexBuilder()
        builder.run()
        print("Search index updated successfully")
    except Exception as e:
        print(f"Warning: Failed to update search index: {e}")
        print("Search index may be out of date")
        # Don't fail the publish process

# Add to main() function, after update_all_posts_section()
def main():
    # ... existing code ...
    
    # Update all posts section
    update_all_posts_section(dry_run)
    
    # Update search index (NEW)
    update_search_index(dry_run)
    
    print(f"\n✓ Post published successfully!")
```

**Location of Changes**: End of `main()` function in `publish_post.py`

#### 4.2 Update `delete_post.py`

**Current Flow**:
1. Remove HTML file
2. Update chrono-data.json (renumber subsequent posts)
3. Update toc-data.json
4. Update All Posts section

**New Flow** (add step):
1. Remove HTML file
2. Update chrono-data.json
3. Update toc-data.json
4. Update All Posts section
5. **Regenerate search-index.json** ← NEW

**Implementation**:

```python
# Add to imports at top of file
import sys

# Add path handling before other imports
SCRIPTS_DIR = Path(__file__).parent
if str(SCRIPTS_DIR) not in sys.path:
    sys.path.insert(0, str(SCRIPTS_DIR))

from build_search_index import SearchIndexBuilder

# Add new function
def update_search_index(dry_run=False):
    """Regenerate search index after deleting a post."""
    if dry_run:
        print("[DRY RUN] Would regenerate search index")
        return
    
    print("\nUpdating search index...")
    try:
        builder = SearchIndexBuilder()
        builder.run()
        print("Search index updated successfully")
    except Exception as e:
        print(f"Warning: Failed to update search index: {e}")
        print("Search index may be out of date")

# Add to main() function
def main():
    # ... existing code ...
    
    # Update all posts section
    # (existing code)
    
    # Update search index (NEW)
    update_search_index(dry_run)
    
    print(f"\n✓ Post deleted successfully!")
```

#### 4.3 Update `update_post.py`

**Current Flow**:
1. Update HTML file (title tag)
2. Update metadata in index.html if changed
3. Update chrono-data.json if changed
4. Update toc-data.json if changed

> **Note**: Unlike `publish_post.py` and `delete_post.py`, this script does NOT call
> `update_all_posts_section()`. The search index update should be added after the
> existing update operations.

**New Flow** (add step):
1. Update HTML file (title tag)
2. Update metadata in index.html if changed
3. Update chrono-data.json if changed
4. Update toc-data.json if changed
5. **Regenerate search-index.json if content changed** ← NEW

**Implementation**: Same pattern as above

```python
# Add to imports at top of file
import sys

# Add path handling before other imports
SCRIPTS_DIR = Path(__file__).parent
if str(SCRIPTS_DIR) not in sys.path:
    sys.path.insert(0, str(SCRIPTS_DIR))

from build_search_index import SearchIndexBuilder

# Add function
def update_search_index(dry_run=False):
    """Regenerate search index after updating a post."""
    if dry_run:
        print("[DRY RUN] Would regenerate search index")
        return
    
    print("\nUpdating search index...")
    try:
        builder = SearchIndexBuilder()
        builder.run()
        print("Search index updated successfully")
    except Exception as e:
        print(f"Warning: Failed to update search index: {e}")

# Add to main()
def main():
    # ... existing code ...
    update_search_index(dry_run)
    print(f"\n✓ Post updated successfully!")
```

### 5. Client-Side Search Enhancement

#### 5.1 Load Search Index

**Location**: `a/toc/toc-sidebar.js` (Sidebar IIFE, lines 1-893)

> **IMPORTANT**: The sidebar IIFE has its own `CONFIG` (line 24) and `state` (line 40)
> objects that are scoped within the IIFE. Add these properties to the EXISTING objects,
> not as new declarations.

**Add to CONFIG** (line 24, inside Sidebar IIFE):
```javascript
const CONFIG = {
  tocDataUrl: 'toc/toc-data.json',
  defaultWidth: 300,
  minWidth: 200,
  maxWidth: 600,
  searchDebounce: 150,
  // NEW: Add these properties
  searchIndexUrl: 'toc/search-index.json',
  enableContentSearch: true,  // Feature flag
  searchCacheTime: 24 * 60 * 60 * 1000, // 24 hours
  storageKeys: {
    width: 'tbc-sidebar-width',
    expanded: 'tbc-expanded-topics',
  },
};
```

**Add to state** (line 40, inside Sidebar IIFE):
```javascript
const state = {
  tocData: null,
  currentPage: null,
  expandedTopics: new Set(),
  isResizing: false,
  isMobileOpen: false,
  searchQuery: '',
  // NEW: Add these properties
  searchIndex: null,
  searchIndexLoaded: false,
  searchInContent: false,  // User preference
};
```

**Add Load Function**:
```javascript
async function loadSearchIndex() {
  // Check cache first (use 'tbc-sidebar-' prefix for consistency)
  try {
    const cached = localStorage.getItem('tbc-sidebar-search-index');
    const cacheTime = localStorage.getItem('tbc-sidebar-search-index-time');
    
    if (cached && cacheTime) {
      const cachedData = JSON.parse(cached);
      const cacheTimeNum = Number.parseInt(cacheTime, 10);
      
      if (Number.isFinite(cacheTimeNum) && cacheTimeNum >= 0) {
        const age = Date.now() - cacheTimeNum;

        // Validate cache structure
        const isValid = cachedData && 
                        typeof cachedData === 'object' &&
                        typeof cachedData.version === 'string' &&
                        typeof cachedData.totalPosts === 'number' &&
                        Array.isArray(cachedData.posts);
        
        // Invalidate if cache expired, version doesn't match, or structure is invalid
        if (isValid && age >= 0 && age < CONFIG.searchCacheTime && cachedData.version === '1.0') {
          console.log('Using cached search index');
          state.searchIndex = cachedData;
          state.searchIndexLoaded = true;
          return;
        }
      } else {
        console.warn('Ignoring search index cache due to invalid cache time value:', cacheTime);
      }
    }
  } catch (e) {
    console.warn('Failed to load cached search index');
  }
  
  // Fetch fresh index
  // Determine basePath using same logic as loadTocData()
  let basePath = '';
  const currentPath = window.location.pathname;
  
  if (currentPath.includes('/a/') && !currentPath.endsWith('/a/') && !currentPath.endsWith('/a/index.html')) {
    basePath = '';  // Already in /a/ subdirectory
  } else if (currentPath.endsWith('/a/') || currentPath.endsWith('/a/index.html')) {
    basePath = '';  // At /a/ root
  } else if (currentPath === '/index.html' || currentPath === '/' || currentPath.match(/^\/\d{4}_/)) {
    basePath = '';  // Serving from root with post pattern
  } else {
    basePath = 'a/';  // At site root, need to navigate to /a/
  }
  
  try {
    const url = basePath + CONFIG.searchIndexUrl;
    // Add a timeout to prevent the fetch from hanging indefinitely
    const FETCH_TIMEOUT_MS = 10000; // 10 seconds
    const controller = new AbortController();
    const { signal } = controller;
    let timeoutId;
    
    try {
      timeoutId = window.setTimeout(() => {
        controller.abort();
      }, FETCH_TIMEOUT_MS);
      
      const response = await fetch(url, { signal });
      
      if (!response.ok) {
        throw new Error(`HTTP ${response.status}`);
      }
      
      const index = await response.json();
      state.searchIndex = index;
      state.searchIndexLoaded = true;
      
      // Cache it (use 'tbc-sidebar-' prefix for consistency with other sidebar storage)
      try {
        localStorage.setItem('tbc-sidebar-search-index', JSON.stringify(index));
        localStorage.setItem('tbc-sidebar-search-index-time', Date.now().toString());
      } catch (e) {
        console.warn('Failed to cache search index');
      }
      
      console.log(`Search index loaded: ${index.totalPosts} posts`);
    } finally {
      if (timeoutId !== undefined) {
        clearTimeout(timeoutId);
      }
    }
  } catch (error) {
    if (error.name === 'AbortError') {
      console.error('Failed to load search index: request timed out');
    } else {
      console.error('Failed to load search index:', error);
    }
    state.searchIndexLoaded = false;
    // Content search will be disabled
  }
}
```

**Initialize on Startup** (inside `initSidebar()` function, after line 374):

> **Note**: There is no separate `init()` function. The search index should be loaded
> inside `initSidebar()` AFTER the TOC data is loaded successfully. This ensures the
> sidebar UI is ready before attempting to load the search index.

```javascript
async function initSidebar() {
  // ... existing code up to line 374 ...
  
  // Load TOC data
  try {
    state.tocData = await loadTocData();
    renderSidebar();
    sidebar.classList.remove('loading');
    
    // NEW: Load search index (don't block UI)
    if (CONFIG.enableContentSearch) {
      loadSearchIndex().catch(err => {
        console.warn('Search index unavailable, content search disabled');
      });
    }
  } catch (error) {
    // ... existing error handling ...
  }
  
  // ... rest of initSidebar ...
}
```

#### 5.2 Enhanced Search Function

**Replace `performSearch()` function**:

```javascript
function performSearch(query) {
  const resultsDiv = document.getElementById('tbc-search-results');
  query = query.toLowerCase().trim();
  
  if (!query) {
    resetSearch();
    return;
  }
  
  // Search mode: title only or title + content
  const searchContent = state.searchInContent && state.searchIndexLoaded;
  
  if (searchContent) {
    performContentSearch(query);
  } else {
    performTitleSearch(query);
  }
}

function performTitleSearch(query) {
  // Existing implementation (unchanged)
  const resultsDiv = document.getElementById('tbc-search-results');
  const topics = document.querySelectorAll('.tbc-topic');
  const posts = document.querySelectorAll('.tbc-post-link');
  let matchCount = 0;
  
  // Search posts by title
  posts.forEach(post => {
    const title = post.textContent.toLowerCase();
    const matches = title.includes(query);
    
    post.classList.toggle('tbc-search-no-match', !matches);
    
    if (matches) {
      matchCount++;
      highlightText(post, query);
      
      const topic = post.closest('.tbc-topic');
      if (topic) {
        topic.classList.add('expanded');
        state.expandedTopics.add(topic.dataset.topicId);
      }
    } else {
      removeHighlight(post);
    }
  });
  
  // Search topics (unchanged)
  topics.forEach(topic => {
    const topicTitle = topic.querySelector('.tbc-topic-title');
    const topicTitleText = topicTitle ? topicTitle.textContent.toLowerCase() : '';
    const topicMatches = topicTitleText.includes(query);
    const hasVisiblePosts = topic.querySelector('.tbc-post-link:not(.tbc-search-no-match)');
    
    if (topicMatches) {
      topic.classList.remove('tbc-search-no-match');
      topic.classList.add('expanded');
      state.expandedTopics.add(topic.dataset.topicId);
      
      topic.querySelectorAll('.tbc-post-link').forEach(p => {
        p.classList.remove('tbc-search-no-match');
        matchCount++;
      });
      
      if (topicTitle) highlightText(topicTitle, query);
    } else {
      topic.classList.toggle('tbc-search-no-match', !hasVisiblePosts);
      if (topicTitle) removeHighlight(topicTitle);
    }
  });
  
  updateResultsCount(matchCount, resultsDiv);
}

function performContentSearch(query) {
  // Enhanced search with content matching
  const resultsDiv = document.getElementById('tbc-search-results');
  const topics = document.querySelectorAll('.tbc-topic');
  const posts = document.querySelectorAll('.tbc-post-link');
  const matchingPosts = new Set();
  
  // Search in index
  if (state.searchIndex && state.searchIndex.posts) {
    state.searchIndex.posts.forEach(post => {
      const titleMatch = post.title.toLowerCase().includes(query);
      const contentMatch = post.contentPreview.includes(query);
      
      if (titleMatch || contentMatch) {
        matchingPosts.add(post.file);
      }
    });
  }
  
  let matchCount = 0;
  
  // Update DOM based on matches
  posts.forEach(post => {
    const href = post.getAttribute('href');
    const matches = matchingPosts.has(href);
    
    post.classList.toggle('tbc-search-no-match', !matches);
    
    if (matches) {
      matchCount++;
      
      // Highlight only title (content not visible in sidebar)
      const title = post.textContent.toLowerCase();
      if (title.includes(query)) {
        highlightText(post, query);
      }
      
      const topic = post.closest('.tbc-topic');
      if (topic) {
        topic.classList.add('expanded');
        state.expandedTopics.add(topic.dataset.topicId);
      }
    } else {
      removeHighlight(post);
    }
  });
  
  // Handle topics
  topics.forEach(topic => {
    const topicTitle = topic.querySelector('.tbc-topic-title');
    const hasVisiblePosts = topic.querySelector('.tbc-post-link:not(.tbc-search-no-match)');
    
    topic.classList.toggle('tbc-search-no-match', !hasVisiblePosts);
    
    if (hasVisiblePosts) {
      topic.classList.add('expanded');
      state.expandedTopics.add(topic.dataset.topicId);
    }
  });
  
  updateResultsCount(matchCount, resultsDiv, true);
}

function updateResultsCount(matchCount, resultsDiv, isContentSearch = false) {
  if (resultsDiv) {
    if (matchCount === 0) {
      resultsDiv.textContent = 'No posts found';
      resultsDiv.classList.add('no-results');
    } else {
      const mode = isContentSearch ? ' (title + content)' : '';
      resultsDiv.textContent = `${matchCount} result${matchCount === 1 ? '' : 's'}${mode}`;
      resultsDiv.classList.remove('no-results');
    }
  }
}
```

> **Note**: The `escapeHtml()` function already exists in the Sidebar IIFE at line 60.
> Do NOT duplicate it - reuse the existing function for any HTML escaping needs.

#### 5.3 UI Enhancement - Content Search Toggle

**Update Search HTML** (in `generateSidebarHTML()`):

```javascript
function generateSidebarHTML() {
  return `
    <div id="tbc-resize-handle" title="Drag to resize"></div>
    <button id="tbc-sidebar-close" aria-label="Close sidebar">×</button>
    
    <div id="tbc-search-container">
      <div class="tbc-search-wrapper">
        <span class="tbc-search-icon">🔍</span>
        <input type="search" 
               id="tbc-search-input" 
               placeholder="Search post titles..." 
               autocomplete="off"
               aria-label="Search post titles">
        <button id="tbc-search-clear" class="hidden" aria-label="Clear search">×</button>
      </div>
      
      <!-- NEW: Content search toggle -->
      <div class="tbc-search-options">
        <label class="tbc-search-toggle">
          <input type="checkbox" id="tbc-search-content-toggle">
          <span>Search in content</span>
        </label>
      </div>
      
      <div id="tbc-search-results"></div>
    </div>
    
    <!-- ... rest of sidebar ... -->
  `;
}
```

**Add CSS Styles** (insert into the embedded styles in `toc-sidebar.js` around line 800-1300):

```css
/* Search options */
.tbc-search-options {
  padding: 8px 15px 0 15px;
  font-size: 0.85rem;
}

.tbc-search-toggle {
  display: flex;
  align-items: center;
  gap: 6px;
  cursor: pointer;
  color: var(--text-light);
  user-select: none;
}

.tbc-search-toggle input[type="checkbox"] {
  cursor: pointer;
}

.tbc-search-toggle:hover {
  color: var(--text);
}

.tbc-search-toggle input[type="checkbox"]:disabled {
  cursor: not-allowed;
  opacity: 0.5;
}

.tbc-search-toggle input[type="checkbox"]:disabled + span {
  opacity: 0.5;
  cursor: not-allowed;
}
```

**Initialize Toggle**:

```javascript
function initSearch() {
  const input = document.getElementById('tbc-search-input');
  const clearBtn = document.getElementById('tbc-search-clear');
  const contentToggle = document.getElementById('tbc-search-content-toggle');
  
  if (!input) return;
  
  // Load saved preference
  const savedPref = localStorage.getItem('tbc-search-in-content');
  if (savedPref === 'true' && state.searchIndexLoaded) {
    contentToggle.checked = true;
    state.searchInContent = true;
  }
  
  // Disable toggle if index not loaded
  if (!state.searchIndexLoaded) {
    contentToggle.disabled = true;
    contentToggle.title = 'Search index not available';
  }
  
  const performSearchDebounced = debounce(performSearch, CONFIG.searchDebounce);
  
  input.addEventListener('input', () => {
    state.searchQuery = input.value;
    clearBtn.classList.toggle('hidden', !input.value);
    performSearchDebounced(input.value);
  });
  
  clearBtn.addEventListener('click', () => {
    input.value = '';
    state.searchQuery = '';
    clearBtn.classList.add('hidden');
    resetSearch();
    input.focus();
  });
  
  // NEW: Handle content toggle
  contentToggle.addEventListener('change', () => {
    state.searchInContent = contentToggle.checked;
    
    // Save preference
    localStorage.setItem('tbc-search-in-content', contentToggle.checked.toString());
    
    // Re-run search if there's a query
    if (state.searchQuery) {
      performSearch(state.searchQuery);
    }
    
    // Update placeholder
    input.placeholder = contentToggle.checked 
      ? 'Search titles and content...' 
      : 'Search post titles...';
  });
}
```

### 6. Additional Enhancements (Optional)

#### 6.1 Search Result Snippets

When user clicks a search result, show matching content snippets:

```javascript
// Helper function to escape HTML special characters
function escapeHtml(text) {
  const div = document.createElement('div');
  div.textContent = text;
  return div.innerHTML;
}

// Helper function to escape special regex characters
function escapeRegex(text) {
  return text.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
}

function showSearchResultDetails(postNum) {
  if (!state.searchIndex || !state.searchQuery) return;
  
  const post = state.searchIndex.posts.find(p => p.num === postNum);
  if (!post) return;
  
  // Find matching snippet
  const query = state.searchQuery.toLowerCase();
  const content = post.contentPreview;
  const index = content.indexOf(query);
  
  if (index === -1) return; // Title match only
  
  // Extract context around match
  const contextLength = 150;
  const start = Math.max(0, index - contextLength);
  const end = Math.min(content.length, index + query.length + contextLength);
  
  let snippet = content.substring(start, end);
  
  if (start > 0) snippet = '...' + snippet;
  if (end < content.length) snippet = snippet + '...';
  
  // Highlight match in snippet
  const regex = new RegExp(`(${escapeRegex(query)})`, 'gi');
  snippet = escapeHtml(snippet).replace(regex, '<mark>$1</mark>');
  
  return snippet;
}
```

#### 6.2 Search Ranking

Rank results by relevance:

```javascript
function rankSearchResults(posts, query) {
  return posts.map(post => {
    let score = 0;
    const lowerQuery = query.toLowerCase();
    const lowerTitle = post.title.toLowerCase();
    
    // Title exact match: highest score
    if (lowerTitle === lowerQuery) score += 100;
    
    // Title starts with query: high score
    else if (lowerTitle.startsWith(lowerQuery)) score += 50;
    
    // Title contains query: medium score
    else if (lowerTitle.includes(lowerQuery)) score += 25;
    
    // Content contains query: low score
    else if (post.contentPreview.includes(lowerQuery)) score += 10;
    
    // Boost recent posts slightly based on post date (more robust than post number)
    const now = Date.now();
    const postTime = new Date(post.date).getTime();
    if (!Number.isNaN(postTime)) {
      const daysOld = (now - postTime) / (1000 * 60 * 60 * 24);
      // Posts newer than 1 year get a boost between 1 (new) and 0 (1 year old)
      const recencyBoost = Math.max(0, 1 - Math.min(daysOld, 365) / 365);
      score += recencyBoost;
    }
    
    return { ...post, score };
  }).sort((a, b) => b.score - a.score);
}
```

### 7. Testing & Validation

#### 7.1 Unit Tests for Index Builder

Create `scripts/test_build_search_index.py`:

```python
import unittest
from pathlib import Path
from build_search_index import SearchIndexBuilder

class TestSearchIndexBuilder(unittest.TestCase):
    
    def setUp(self):
        self.builder = SearchIndexBuilder()
    
    def test_generate_excerpt(self):
        text = "This is a test. This is only a test. More text here."
        excerpt = self.builder._generate_excerpt(text, 30)
        self.assertTrue(excerpt.endswith('.') or excerpt.endswith('...'))
        self.assertLessEqual(len(excerpt), 35)
    
    def test_extract_content(self):
        # Test with sample HTML
        test_html = Path('a/0001_welcome.htm')
        if test_html.exists():
            excerpt, content = self.builder.extract_content(test_html)
            self.assertGreater(len(excerpt), 0)
            self.assertGreater(len(content), 0)
            self.assertIn('building coder', content.lower())

if __name__ == '__main__':
    unittest.main()
```

#### 7.2 Manual Testing Checklist

- [ ] Run `python scripts/build_search_index.py` successfully
- [ ] Verify `a/toc/search-index.json` is created
- [ ] Check file size is reasonable (< 1 MB)
- [ ] Verify JSON is valid (use `json.tool`)
- [ ] Test search with content toggle off (title only)
- [ ] Test search with content toggle on (title + content)
- [ ] Verify results are correct for both modes
- [ ] Test with queries that match content but not title
- [ ] Test with special characters in query
- [ ] Test cache persistence (reload page)
- [ ] Test on mobile viewport
- [ ] Verify keyboard shortcuts still work (/)
- [ ] Test error states:
  - [ ] Network failure when loading index
  - [ ] Corrupted JSON in cache
  - [ ] Search index file missing (404)
  - [ ] Old cached version with different schema
  - [ ] Disabled state handling for content toggle
  - [ ] Keyboard shortcut when toggle disabled

#### 7.3 Performance Testing

```javascript
// Add performance monitoring
function performSearch(query) {
  const startTime = performance.now();
  
  // ... search logic ...
  
  const endTime = performance.now();
  console.log(`Search completed in ${(endTime - startTime).toFixed(2)}ms`);
}
```

**Performance Targets**:
- Title search: < 50ms
- Content search: < 150ms
- Index load: < 500ms (cached: < 50ms)
- UI update: < 100ms

### 8. Deployment & Rollout

#### 8.1 Deployment Steps

1. **Develop branch**:
   ```bash
   git checkout -b feature/content-search
   ```

2. **Implement changes**:
   - Create `build_search_index.py`
   - Update `publish_post.py`
   - Update `delete_post.py`
   - Update `update_post.py`
   - Update `toc-sidebar.js`
   - Update `toc-sidebar.css`

3. **Generate initial index**:
   ```bash
   python scripts/build_search_index.py
   ```

4. **Test locally**:
   - Open `a/index.html` in browser
   - Test search functionality
   - Verify console logs

5. **Commit changes**:
   ```bash
   git add .
   git commit -m "feat: Add content search with pre-built index"
   ```

6. **Merge to gh-pages**:
   ```bash
   git checkout gh-pages
   git merge feature/content-search
   git push origin gh-pages
   ```

7. **Verify production**:
   - Wait for GitHub Pages deployment
   - Test on live site

#### 8.2 Rollback Plan

If issues occur:

```bash
# Revert commit
git revert HEAD

# Or reset to previous commit
git reset --hard <previous-commit-hash>

# Force push
git push origin gh-pages --force
```

### 9. Maintenance

#### 9.1 Regular Maintenance Tasks

- **Weekly**: Verify search index is up to date
- **Monthly**: Check index file size trends
- **Quarterly**: Review search performance metrics
- **Annually**: Update dependencies (BeautifulSoup, etc.)

#### 9.2 Monitoring

Add logging to track:
- Index generation time
- Index file size
- Search query performance
- Cache hit/miss ratio
- Most common search queries (privacy-preserving)

### 10. Future Enhancements

**Phase 2 Possibilities**:
1. ~~Full-text search with fuzzy matching~~ (see Phase 2 below)
2. Search suggestions/autocomplete
3. Search filters (by year, topic, tag)
4. Search history
5. Advanced search syntax (AND, OR, NOT, quotes)
6. Search result export
7. Search analytics dashboard
8. Multi-language support
9. Voice search
10. Search API for external tools

---

## Phase 2: In-Page Content Highlighting

### 10.1 Overview

When a user searches for a term and navigates to a result, the search term should be **highlighted within the page content** itself, not just in the sidebar. This provides visual confirmation that the user found the right page and helps them locate the relevant content quickly.

### 10.2 Implementation Approach

**URL Parameter Method**: Append `?highlight=<term>` to search result URLs. On page load, detect the parameter and highlight matching terms in the content.

### 10.3 JavaScript Implementation

> **IMPORTANT**: This code is **independent** of the Sidebar IIFE. It should be added as a
> **new separate IIFE** at the end of `toc-sidebar.js` (after line 1842), or in a separate
> file (`content-highlight.js`). This code operates on the page content, not the sidebar.

**Add as new IIFE at end of `toc-sidebar.js`** (after line 1842):

```javascript
// ================================
// In-Page Content Highlighting (Independent Module)
// ================================
(function initContentHighlightModule() {
  'use strict';
  
  const HIGHLIGHT_CONFIG = {
    paramName: 'highlight',
    className: 'tbc-content-highlight',
    maxHighlights: 100,  // Prevent performance issues on pages with many matches
    scrollToFirst: true,
    animateDuration: 2000  // Time before highlight fades slightly
  };

  /**
   * Initialize in-page content highlighting from URL parameter
   */
  function initContentHighlighting() {
    const urlParams = new URLSearchParams(window.location.search);
    const highlightTerm = urlParams.get(HIGHLIGHT_CONFIG.paramName);
    
    if (!highlightTerm || highlightTerm.trim().length < 2) return;
    
    const term = highlightTerm.trim();
    console.log(`Highlighting content for: "${term}"`);
    
    // Wait for content to be ready
    requestAnimationFrame(() => {
      highlightContentMatches(term);
    });
  }

  /**
   * Highlight all matches of term in the page content
   */
  function highlightContentMatches(term) {
    // Target the main content area (avoid sidebar, nav, code blocks)
    const contentArea = document.querySelector('#tbc-content .tbc-blog-content') ||
                        document.querySelector('#tbc-content article') ||
                        document.querySelector('#tbc-content') ||
                        document.querySelector('article') ||
                        document.body;
    
    if (!contentArea) {
      console.warn('No content area found for highlighting');
      return;
    }
    
    // Elements to skip
    const skipSelectors = [
      'script', 'style', 'noscript', 'iframe',
      'pre', 'code',  // Skip code blocks
      '.tbc-sidebar', '#tbc-sidebar',
      '.tbc-chrono-column', '.tbc-chrono-nav',
    '.tbc-chrono-mobile-sheet',
    'nav', 'header', 'footer'
  ];
  
  let highlightCount = 0;
  let firstHighlight = null;
  
  // Walk text nodes and highlight matches
  const walker = document.createTreeWalker(
    contentArea,
    NodeFilter.SHOW_TEXT,
    {
      acceptNode: function(node) {
        // Check if parent or ancestor should be skipped
        let parent = node.parentElement;
        while (parent) {
          if (skipSelectors.some(sel => parent.matches && parent.matches(sel))) {
            return NodeFilter.FILTER_REJECT;
          }
          parent = parent.parentElement;
        }
        
        // Only accept nodes with actual text content
        if (node.textContent.trim().length === 0) {
          return NodeFilter.FILTER_REJECT;
        }
        
        return NodeFilter.FILTER_ACCEPT;
      }
    }
  );
  
  const nodesToProcess = [];
  let node;
  while (node = walker.nextNode()) {
    if (node.textContent.toLowerCase().includes(term.toLowerCase())) {
      nodesToProcess.push(node);
    }
  }
  
  // Process nodes (can't modify during tree walk)
  for (const textNode of nodesToProcess) {
    if (highlightCount >= HIGHLIGHT_CONFIG.maxHighlights) break;
    
    const result = highlightTextNode(textNode, term);
    highlightCount += result.count;
    
    if (!firstHighlight && result.firstMark) {
      firstHighlight = result.firstMark;
    }
  }
  
  console.log(`Highlighted ${highlightCount} matches`);
  
  // Scroll to first match
  if (HIGHLIGHT_CONFIG.scrollToFirst && firstHighlight) {
    setTimeout(() => {
      firstHighlight.scrollIntoView({ behavior: 'smooth', block: 'center' });
      
      // Add pulse animation to first match
      firstHighlight.classList.add('tbc-highlight-pulse');
    }, 300);
  }
  
  // Add highlight counter badge
  if (highlightCount > 0) {
    showHighlightCounter(highlightCount, term);
  }
}

/**
 * Highlight term within a single text node
 */
function highlightTextNode(textNode, term) {
  const text = textNode.textContent;
  const lowerText = text.toLowerCase();
  const lowerTerm = term.toLowerCase();
  
  let result = { count: 0, firstMark: null };
  
  // Find all matches
  const matches = [];
  let pos = 0;
  while ((pos = lowerText.indexOf(lowerTerm, pos)) !== -1) {
    matches.push({
      start: pos,
      end: pos + term.length,
      text: text.substring(pos, pos + term.length)  // Preserve original case
    });
    pos += term.length;
  }
  
  if (matches.length === 0) return result;
  
  // Create fragment with highlighted spans
  const fragment = document.createDocumentFragment();
  let lastEnd = 0;
  
  for (const match of matches) {
    // Text before match
    if (match.start > lastEnd) {
      fragment.appendChild(document.createTextNode(text.substring(lastEnd, match.start)));
    }
    
    // Highlighted match
    const mark = document.createElement('mark');
    mark.className = HIGHLIGHT_CONFIG.className;
    mark.textContent = match.text;
    fragment.appendChild(mark);
    
    if (!result.firstMark) {
      result.firstMark = mark;
    }
    result.count++;
    
    lastEnd = match.end;
  }
  
  // Text after last match
  if (lastEnd < text.length) {
    fragment.appendChild(document.createTextNode(text.substring(lastEnd)));
  }
  
  // Replace original text node
  textNode.parentNode.replaceChild(fragment, textNode);
  
  return result;
}

/**
 * Show floating counter badge with match count
 */
function showHighlightCounter(count, term) {
  // Remove existing counter
  const existing = document.querySelector('.tbc-highlight-counter');
  if (existing) existing.remove();
  
  const counter = document.createElement('div');
  counter.className = 'tbc-highlight-counter';
  counter.innerHTML = `
    <span class="tbc-highlight-count">${count}</span>
    <span class="tbc-highlight-label">matches for "${escapeHtml(term)}"</span>
    <button class="tbc-highlight-clear" aria-label="Clear highlights">✕</button>
    <button class="tbc-highlight-nav tbc-highlight-prev" aria-label="Previous match">↑</button>
    <button class="tbc-highlight-nav tbc-highlight-next" aria-label="Next match">↓</button>
  `;
  
  document.body.appendChild(counter);
  
  // Current match index for navigation
  let currentIndex = 0;
  const allMarks = document.querySelectorAll('.' + HIGHLIGHT_CONFIG.className);
  
  // Clear button
  counter.querySelector('.tbc-highlight-clear').addEventListener('click', () => {
    clearContentHighlights();
    counter.remove();
    
    // Remove highlight param from URL
    const url = new URL(window.location);
    url.searchParams.delete(HIGHLIGHT_CONFIG.paramName);
    window.history.replaceState({}, '', url);
  });
  
  // Navigation buttons
  counter.querySelector('.tbc-highlight-prev').addEventListener('click', () => {
    if (allMarks.length === 0) return;
    currentIndex = (currentIndex - 1 + allMarks.length) % allMarks.length;
    scrollToHighlight(allMarks[currentIndex]);
  });
  
  counter.querySelector('.tbc-highlight-next').addEventListener('click', () => {
    if (allMarks.length === 0) return;
    currentIndex = (currentIndex + 1) % allMarks.length;
    scrollToHighlight(allMarks[currentIndex]);
  });
  
  // Keyboard navigation
  document.addEventListener('keydown', function highlightNav(e) {
    if (e.key === 'Escape') {
      clearContentHighlights();
      counter.remove();
      document.removeEventListener('keydown', highlightNav);
    } else if (e.key === 'F3' || (e.ctrlKey && e.key === 'g')) {
      e.preventDefault();
      if (e.shiftKey) {
        currentIndex = (currentIndex - 1 + allMarks.length) % allMarks.length;
      } else {
        currentIndex = (currentIndex + 1) % allMarks.length;
      }
      scrollToHighlight(allMarks[currentIndex]);
    }
  });
}

function scrollToHighlight(mark) {
  // Remove active class from all
  document.querySelectorAll('.' + HIGHLIGHT_CONFIG.className + '.active')
    .forEach(m => m.classList.remove('active'));
  
  // Add active class and scroll
  mark.classList.add('active');
  mark.scrollIntoView({ behavior: 'smooth', block: 'center' });
}

/**
 * Clear all content highlights
 */
function clearContentHighlights() {
  const marks = document.querySelectorAll('.' + HIGHLIGHT_CONFIG.className);
  
  marks.forEach(mark => {
    const text = document.createTextNode(mark.textContent);
    mark.parentNode.replaceChild(text, mark);
  });
  
  // Normalize to merge adjacent text nodes
  document.body.normalize();
}

// Initialize on DOM ready
  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initContentHighlighting);
  } else {
    initContentHighlighting();
  }

})(); // End of Content Highlight IIFE
```

### 10.4 CSS Styles for In-Page Highlighting

**Add to `toc-sidebar.css`**:

```css
/* ================================
   In-Page Content Highlighting
   ================================ */

/* Highlighted text */
.tbc-content-highlight {
  background-color: #fff3cd;
  color: inherit;
  padding: 1px 2px;
  border-radius: 2px;
  box-shadow: 0 0 0 1px rgba(255, 193, 7, 0.3);
  transition: background-color 0.3s ease;
}

.tbc-content-highlight.active {
  background-color: #ffc107;
  box-shadow: 0 0 0 2px rgba(255, 193, 7, 0.5);
}

/* Pulse animation for first match */
.tbc-content-highlight.tbc-highlight-pulse {
  animation: highlightPulse 1.5s ease-out;
}

@keyframes highlightPulse {
  0% { background-color: #ffc107; transform: scale(1.1); }
  50% { background-color: #ffeb3b; }
  100% { background-color: #fff3cd; transform: scale(1); }
}

/* Floating counter badge */
.tbc-highlight-counter {
  position: fixed;
  top: 20px;
  right: 20px;
  background: var(--tbc-sidebar-bg, #f5f5f5);
  border: 1px solid var(--tbc-sidebar-border, #ddd);
  border-radius: 8px;
  padding: 10px 15px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  z-index: 10000;
  display: flex;
  align-items: center;
  gap: 10px;
  font-size: 14px;
  max-width: 350px;
}

.tbc-highlight-count {
  background: var(--tbc-accent-primary, #0066cc);
  color: white;
  padding: 2px 8px;
  border-radius: 12px;
  font-weight: bold;
  min-width: 24px;
  text-align: center;
}

.tbc-highlight-label {
  color: var(--tbc-sidebar-text, #333);
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  max-width: 150px;
}

.tbc-highlight-clear,
.tbc-highlight-nav {
  background: transparent;
  border: 1px solid var(--tbc-sidebar-border, #ddd);
  border-radius: 4px;
  width: 28px;
  height: 28px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: background-color 0.15s ease;
}

.tbc-highlight-clear:hover,
.tbc-highlight-nav:hover {
  background: var(--tbc-sidebar-hover, #e8e8e8);
}

/* Mobile adjustments */
@media (max-width: 768px) {
  .tbc-highlight-counter {
    top: auto;
    bottom: 70px; /* Above mobile sheet */
    right: 10px;
    left: 10px;
    max-width: none;
    justify-content: center;
  }
  
  .tbc-highlight-label {
    display: none;
  }
}

/* Reduced motion */
@media (prefers-reduced-motion: reduce) {
  .tbc-content-highlight,
  .tbc-content-highlight.tbc-highlight-pulse {
    animation: none;
    transition: none;
  }
}

/* Print: hide counter, keep highlights */
@media print {
  .tbc-highlight-counter {
    display: none;
  }
  
  .tbc-content-highlight {
    background-color: #ffff00 !important;
    -webkit-print-color-adjust: exact;
    print-color-adjust: exact;
  }
}
```

### 10.5 Update Search Result Links

**Modify the search results to include highlight parameter**:

```javascript
// In performContentSearch() - update post links
function updateSearchResultLinks(query) {
  const posts = document.querySelectorAll('.tbc-post-link:not(.tbc-search-no-match)');
  
  posts.forEach(post => {
    const href = post.getAttribute('href');
    if (!href) return;
    
    // Parse existing URL
    const url = new URL(href, window.location.origin);
    
    // Add highlight parameter
    url.searchParams.set('highlight', query);
    
    // Update href
    post.setAttribute('href', url.toString());
    
    // Store original href for cleanup
    if (!post.dataset.originalHref) {
      post.dataset.originalHref = href;
    }
  });
}

// In resetSearch() - restore original hrefs
function resetSearchResultLinks() {
  const posts = document.querySelectorAll('.tbc-post-link[data-original-href]');
  
  posts.forEach(post => {
    post.setAttribute('href', post.dataset.originalHref);
    delete post.dataset.originalHref;
  });
}
```

### 10.6 Keyboard Shortcuts

| Key | Action |
|-----|--------|
| `Escape` | Clear all highlights and close counter |
| `F3` or `Ctrl+G` | Jump to next highlight |
| `Shift+F3` or `Ctrl+Shift+G` | Jump to previous highlight |

### 10.7 Testing Checklist

- [ ] Highlights appear for URL parameter `?highlight=term`
- [ ] First match is scrolled into view
- [ ] Counter badge shows correct count
- [ ] Previous/Next navigation works
- [ ] Clear button removes all highlights
- [ ] Escape key clears highlights
- [ ] Code blocks are NOT highlighted
- [ ] Sidebar content is NOT highlighted
- [ ] Works on mobile viewport
- [ ] Works with special characters in search term
- [ ] Performance acceptable with 100+ matches
- [ ] Print styles work correctly

### 11. Additional Implementation Notes

#### 11.1 Git Configuration

Ensure `a/toc/search-index.json` is **NOT** in `.gitignore`:
```bash
# Verify search-index.json will be tracked
git check-ignore a/toc/search-index.json
# Should return nothing (file is not ignored)
```

#### 11.2 Search Limitations

Document that searches won't find:
- Text within code blocks (intentionally excluded)
- Text in images (no OCR)
- Very long compound words beyond contentPreview truncation
- Content added after last index generation (until next update)

#### 11.3 Mobile Performance

- Index size (~130-170KB compressed) is acceptable for mobile
- Consider lazy loading for slower connections
- Toggle defaults to "off" to prefer faster title-only search

#### 11.4 Content Preview Truncation

When contentPreview is truncated at 800 characters:
- Clearly indicated in code comments
- Breaking on word boundaries when possible
- Still provides good coverage for most posts
- Optimizes file size without sacrificing search quality

#### 11.5 Keyboard Shortcut Handling

When content search toggle is disabled (index not loaded):
- "/" shortcut still works for title search
- Toggle shows disabled state with tooltip
- User can still access all search features
- Graceful degradation maintained

---

## Implementation Timeline

### Phase 1: Search Index & Content Search

| Step | Task | Effort | Dependencies |
|------|------|--------|--------------|
| 1 | Create `build_search_index.py` | 4-6 hours | BeautifulSoup |
| 2 | Generate initial index | 30 min | Step 1 |
| 3 | Update publish scripts | 2 hours | Step 1 |
| 4 | Update JavaScript search | 4-6 hours | Step 2 |
| 5 | Add UI toggle | 2 hours | Step 4 |
| 6 | Testing & debugging | 4 hours | Step 5 |
| 7 | Documentation | 2 hours | All steps |
| **Phase 1 Total** | | **18-24 hours** | |

### Phase 2: In-Page Content Highlighting

| Step | Task | Effort | Dependencies |
|------|------|--------|--------------|
| 8 | Implement `initContentHighlighting()` | 3-4 hours | Phase 1 |
| 9 | Add highlight CSS styles | 1 hour | Step 8 |
| 10 | Implement counter badge & navigation | 2 hours | Step 8 |
| 11 | Update search result links | 1 hour | Step 8 |
| 12 | Add keyboard shortcuts | 1 hour | Step 10 |
| 13 | Testing & debugging | 2 hours | All Phase 2 |
| **Phase 2 Total** | | **10-12 hours** | |

### Combined Timeline

| Phase | Effort | Cumulative |
|-------|--------|------------|
| Phase 1: Search Index | 18-24 hours | 18-24 hours |
| Phase 2: In-Page Highlighting | 10-12 hours | 28-36 hours |
| **Total** | | **28-36 hours** |

---

## Dependencies & Requirements

### Python Dependencies

**Note**: `beautifulsoup4>=4.12.0` is already present in `scripts/requirements.txt`

Optionally add for better HTML parsing:
```
html5lib>=1.1  # Optional: better HTML parsing
```

Install/update:
```bash
pip install -r scripts/requirements.txt
```

### Browser Requirements

- Modern browsers with ES6 support
- LocalStorage enabled
- JavaScript enabled
- Fetch API support

### Performance Requirements

- Initial load time: < 2 seconds additional
- Search response: < 200ms
- Memory usage: < 50 MB additional
- Disk cache: < 500 KB

---

## Success Metrics

### Quantitative Metrics
1. Search index generation time: < 5 minutes
2. Index file size: < 1 MB uncompressed
3. Title search time: < 50ms
4. Content search time: < 200ms
5. False positive rate: < 5%
6. False negative rate: < 1%

### Qualitative Metrics
1. User can find posts by content keywords
2. Search feels responsive (subjective)
3. No noticeable performance degradation
4. Search results are relevant
5. UI is intuitive

---

## Risk Assessment

| Risk | Probability | Impact | Mitigation |
|------|-------------|--------|------------|
| Large index file | Medium | Medium | Use excerpts only, compress |
| Slow search | Low | High | Optimize algorithm, use Web Workers |
| Index out of sync | Medium | Medium | Auto-regenerate in all scripts |
| HTML parsing errors | Medium | Low | Robust error handling |
| Cache issues | Low | Low | Version-based cache invalidation |
| Browser compatibility | Low | Medium | Feature detection, fallbacks |

---

## Conclusion

This specification provides a complete roadmap for implementing content search with a pre-built index (Phase 1) and in-page content highlighting (Phase 2). The approach balances performance, maintainability, and user experience while working within the constraints of a static GitHub Pages site.

### Phase 1 Key Features (Search Index)
- ✅ Fast client-side content search
- ✅ No server infrastructure required
- ✅ Works offline after initial load
- ✅ Automatic maintenance via scripts
- ✅ Graceful degradation if index unavailable
- ✅ Backwards compatible (Python 3.7+)
- ✅ Optimized file size (~130-170KB compressed)
- ✅ Comprehensive error handling

### Phase 2 Key Features (In-Page Highlighting)
- ✅ Highlights search terms within page content
- ✅ Scrolls to first match automatically
- ✅ Floating counter with match navigation
- ✅ Keyboard shortcuts (F3, Escape)
- ✅ Skips code blocks and navigation areas
- ✅ Mobile-responsive counter positioning
- ✅ Reduced motion support
- ✅ Print-friendly styles

**Fixes Applied** (v1.2):
1. ✅ All fixes from v1.1
2. ✅ Added Phase 2: In-Page Content Highlighting
3. ✅ URL parameter `?highlight=term` for deep-linking
4. ✅ Tree walker for efficient DOM traversal
5. ✅ Skip selectors for code blocks, sidebar, nav
6. ✅ Maximum highlight limit for performance
7. ✅ Navigation between highlights with keyboard
8. ✅ Clear highlights with Escape key
9. ✅ Mobile-responsive counter badge
10. ✅ Updated timeline for combined phases (28-36 hours)

Implementation should proceed in phases, with thorough testing at each stage to ensure quality and performance. All identified issues have been addressed in this updated specification.
