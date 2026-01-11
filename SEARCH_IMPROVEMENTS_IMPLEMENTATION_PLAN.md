# Search Improvements Implementation Plan

**Issue:** GitHub Issue #33 - Search failures  
**Date:** January 11, 2026  
**Status:** Draft  

---

## Executive Summary

This plan addresses three issues identified in GitHub Issue #33:

1. **Pattern fix**: Include `.html` files in the search index (currently only `.htm` files are indexed)
2. **Content length**: Increase searchable content from 800 to 4000 characters
3. **Multi-word search**: Match all words independently instead of exact phrase matching
4. **Title lookup fix**: Use `toc-data.json` instead of `chrono-data.json` for proper titles

---

## Root Cause Analysis

### Current State

| Metric | Value |
|--------|-------|
| Posts on disk (`.htm`) | 1,350 |
| Posts on disk (`.html`) | 729 |
| **Total posts on disk** | **2,079** |
| Posts in search index | 1,350 |
| Posts in `toc-data.json` | 2,256 |
| Posts in `chrono-data.json` | 19 |

### Issues Identified

1. **File pattern** (`^\d{4}_.*\.htm$`) excludes 729 `.html` files
2. **Content preview** truncated to 800 chars - words beyond this are unsearchable
3. **Phrase matching** requires exact consecutive substring match
4. **Title lookup** falls back to filename when post not in `chrono-data.json` (1,331 posts affected)

---

## Phase 1: Pattern Fix

**File:** [scripts/build_search_index.py](scripts/build_search_index.py)

### Change 1.1: Update file pattern (Line 54)

**Current:**
```python
pattern = re.compile(r'^\d{4}_.*\.htm$')
```

**New:**
```python
pattern = re.compile(r'^\d{4}_.*\.html?$')
```

**Impact:**
- Posts indexed: 1,350 → 2,079 (+729 posts, +54%)
- Post 2079 with "rebirth" will be searchable
- Posts 1703, 1807 with "dashboard" in title will be searchable

---

## Phase 2: Increase Content Preview Length

**File:** [scripts/build_search_index.py](scripts/build_search_index.py)

### Change 2.1: Update content preview length (Line 41)

**Current:**
```python
CONTENT_PREVIEW_LENGTH = 800  # Characters for searchable content
```

**New:**
```python
CONTENT_PREVIEW_LENGTH = 4000  # Characters for searchable content
```

**Impact:**
- 5x more content searchable per post
- Words appearing after position 800 will now be found
- "dashboard" in post 1062 will be searchable (currently beyond 800 chars)

**Trade-off:**
- Index file size increase: ~1.2 MB → ~5 MB (estimated)
- Gzip size increase: ~360 KB → ~1.5 MB (estimated)
- Network impact: One-time download, cached locally

---

## Phase 3: Fix Title Lookup

**File:** [scripts/build_search_index.py](scripts/build_search_index.py)

### Change 3.1: Use toc-data.json instead of chrono-data.json

**Current** (Lines 36-37, 65-78):
```python
CHRONO_FILE = POSTS_DIR / "toc" / "chrono-data.json"

def load_chrono_data(self):
    """Load chrono-data.json for post metadata."""
    if not self.chrono_path.exists():
        print(f"Warning: {self.chrono_path} not found")
        return {}
    
    with open(self.chrono_path, 'r', encoding='utf-8') as f:
        data = json.load(f)
    
    # Build lookup by file name
    lookup = {}
    for year_data in data.get('years', []):
        for month_data in year_data.get('months', []):
            for post in month_data.get('posts', []):
                lookup[post['file']] = post
    
    return lookup
```

**New:**
```python
TOC_FILE = POSTS_DIR / "toc" / "toc-data.json"

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
```

**Also update:**
- Constructor parameter: `chrono_path` → `toc_path`
- `build_index()` method: `chrono_lookup = self.load_chrono_data()` → `toc_lookup = self.load_toc_data()`

**Impact:**
- All 2,079 posts will have proper human-readable titles
- Post 0191 will show "Store Project Data" instead of "0191_project_data.htm"

---

## Phase 4: Multi-Word Search

**File:** [a/toc/toc-sidebar.js](a/toc/toc-sidebar.js)

### Change 4.1: Add helper functions (After line 87, before `generateExcerptForDisplay`)

Insert new helper functions:

```javascript
/**
 * Parse search query into individual words
 * Filters out empty strings and trims whitespace
 * @param {string} query - Search query (already lowercase)
 * @returns {string[]} Array of non-empty words
 */
function parseSearchWords(query) {
  if (!query || typeof query !== 'string') return [];
  return query.trim().split(/\s+/).filter(word => word.length > 0);
}

/**
 * Check if text contains all search words (AND logic)
 * @param {string} text - Text to search (should be lowercase for content)
 * @param {string[]} words - Array of search words (lowercase)
 * @returns {boolean} True if ALL words are found
 */
function containsAllWords(text, words) {
  if (!text || !words || words.length === 0) return false;
  return words.every(word => text.includes(word));
}
```

### Change 4.2: Update `getMatchType()` function (Lines 74-87)

**Current:**
```javascript
function getMatchType(post, query) {
  const titleMatch = post.title && post.title.toLowerCase().includes(query);
  // contentPreview is stored in lowercase in the search index (see spec),
  // and `query` is already lowercase, so we can compare directly.
  const contentMatch = post.contentPreview &&
    post.contentPreview.includes(query);
  
  if (titleMatch && contentMatch) return MATCH_TYPE.BOTH;
  if (titleMatch) return MATCH_TYPE.TITLE_ONLY;
  if (contentMatch) return MATCH_TYPE.CONTENT_ONLY;
  return MATCH_TYPE.NONE;
}
```

**New:**
```javascript
/**
 * Determine the type of match for a post against a search query
 * Supports multi-word queries with AND logic (all words must match)
 * @param {Object} post - Post object with title and contentPreview
 * @param {string} query - Lowercase search query (may contain multiple words)
 * @returns {number} MATCH_TYPE value
 */
function getMatchType(post, query) {
  const words = parseSearchWords(query);
  if (words.length === 0) return MATCH_TYPE.NONE;
  
  const titleLower = post.title ? post.title.toLowerCase() : '';
  const titleMatch = containsAllWords(titleLower, words);
  
  // contentPreview is stored in lowercase in the search index
  const contentMatch = containsAllWords(post.contentPreview, words);
  
  if (titleMatch && contentMatch) return MATCH_TYPE.BOTH;
  if (titleMatch) return MATCH_TYPE.TITLE_ONLY;
  if (contentMatch) return MATCH_TYPE.CONTENT_ONLY;
  return MATCH_TYPE.NONE;
}
```

### Change 4.3: Update `generateExcerptForDisplay()` function (Lines 98-148)

**Current:** Finds single query string in content

**New:** Find excerpt containing the first matching word for display

```javascript
/**
 * Generate an excerpt showing context around matched terms
 * For multi-word queries, centers on the first found word
 * 
 * @param {Object} postData - Post data with excerpt and contentPreview
 * @param {string} query - Search query (lowercase, may be multi-word)
 * @param {number} maxLength - Maximum excerpt length (default 80)
 * @returns {Object|null} { text: string, query: string, isLowercase: boolean } or null
 */
function generateExcerptForDisplay(postData, query, maxLength = 80) {
  if (!query || typeof query !== 'string' || query.length === 0) return null;
  
  // Parse words and limit total length to prevent ReDoS
  const words = parseSearchWords(query.substring(0, 100));
  if (words.length === 0) return null;
  
  const excerpt = postData.excerpt || '';
  const contentPreview = postData.contentPreview || '';
  
  // Check which source has matches
  const excerptLower = excerpt.toLowerCase();
  const excerptMatchCount = words.filter(w => excerptLower.includes(w)).length;
  
  // contentPreview is already lowercase
  const contentMatchCount = words.filter(w => contentPreview.includes(w)).length;
  
  // Prefer excerpt if it has any matches, otherwise use contentPreview
  const sourceText = (excerptMatchCount > 0) ? excerpt : contentPreview;
  if (!sourceText || sourceText.length === 0) return null;
  
  const lowerSource = sourceText.toLowerCase();
  
  // Find the first matching word and its position
  let firstMatchIndex = -1;
  let matchedWord = '';
  for (const word of words) {
    const idx = lowerSource.indexOf(word);
    if (idx !== -1 && (firstMatchIndex === -1 || idx < firstMatchIndex)) {
      firstMatchIndex = idx;
      matchedWord = word;
    }
  }
  
  if (firstMatchIndex === -1) return null;
  
  // Calculate window around first match
  const halfWindow = Math.floor((maxLength - matchedWord.length) / 2);
  let start = Math.max(0, firstMatchIndex - halfWindow);
  let end = Math.min(sourceText.length, firstMatchIndex + matchedWord.length + halfWindow);
  
  // Adjust to word boundaries (don't cut words in half)
  if (start > 0) {
    const spaceAfterStart = sourceText.indexOf(' ', start);
    if (spaceAfterStart !== -1 && spaceAfterStart < firstMatchIndex) {
      start = spaceAfterStart + 1;
    }
  }
  if (end < sourceText.length) {
    const spaceBeforeEnd = sourceText.lastIndexOf(' ', end);
    if (spaceBeforeEnd > firstMatchIndex + matchedWord.length) {
      end = spaceBeforeEnd;
    }
  }
  
  let excerptText = sourceText.substring(start, end).trim();
  
  // Add ellipsis indicators
  if (start > 0) excerptText = '...' + excerptText;
  if (end < sourceText.length) excerptText = excerptText + '...';
  
  return {
    text: excerptText,
    query: matchedWord,  // First matched word for highlighting
    isLowercase: excerptMatchCount === 0  // Flag if using lowercase contentPreview
  };
}
```

### Change 4.4: Update title highlighting in `performContentSearch()` (Lines 1067-1073)

**Current:**
```javascript
// Highlight only title (content not visible in sidebar)
const title = post.textContent.toLowerCase();
if (title.includes(query)) {
  highlightText(post, query);
} else {
  removeHighlight(post);
}
```

**New:**
```javascript
// Highlight matching words in title
const title = post.textContent.toLowerCase();
const words = parseSearchWords(query);
const matchingWords = words.filter(w => title.includes(w));
if (matchingWords.length > 0) {
  // Highlight first matching word (multi-word highlighting is a future enhancement)
  highlightText(post, matchingWords[0]);
} else {
  removeHighlight(post);
}
```

### Change 4.5: Update topic title matching (Lines 1108-1110)

**Current:**
```javascript
const topicTitleText = topicTitle ? topicTitle.textContent.toLowerCase() : '';
const topicMatches = topicTitleText.includes(query);
```

**New:**
```javascript
const topicTitleText = topicTitle ? topicTitle.textContent.toLowerCase() : '';
const words = parseSearchWords(query);
const topicMatches = words.length > 0 && containsAllWords(topicTitleText, words);
```

### Change 4.6: Update topic title highlighting (Line 1125)

**Current:**
```javascript
if (topicTitle) highlightText(topicTitle, query);
```

**New:**
```javascript
if (topicTitle) {
  const words = parseSearchWords(query);
  const matchingWords = words.filter(w => topicTitleText.includes(w));
  if (matchingWords.length > 0) {
    highlightText(topicTitle, matchingWords[0]);
  }
}
```

---

## Phase 5: Regenerate Search Index

After implementing Phases 1-3:

```bash
cd scripts
python build_search_index.py
```

**Expected output:**
```
Building search index...
Found 2079 post files
  Processing 100/2079...
  Processing 200/2079...
  ...
Search index saved to: a/toc/search-index.json
Index file size: ~5000 KB
Estimated gzip size: ~1500 KB
Search index generation complete!
```

**Verification:**
```powershell
$json = Get-Content "a\toc\search-index.json" -Raw | ConvertFrom-Json
$json.totalPosts  # Should be 2079
($json.posts | Where-Object { $_.file -like "*.html" }).Count  # Should be 729
($json.posts | Where-Object { $_.title -eq $_.file }).Count  # Should be 0 (no filename as title)
```

---

## Testing Checklist

| Test Case | Search Query | Expected Result |
|-----------|--------------|-----------------|
| Single word in .html file | `rebirth` | Post 2079 found |
| Single word (previously beyond 800 chars) | `dashboard` | Posts 1062, 1703, 1807 found |
| Multi-word AND query | `project dashboard` | Posts containing BOTH words |
| Multi-word AND query | `revit api filter` | Posts containing ALL 3 words |
| Phrase still works | `project data` | Posts with exact phrase + posts with both words |
| Title search | `building coder` | Posts with both words in title |
| Topic search | `getting started` | Topic header matches |
| Empty search | (clear input) | All posts shown |
| Single word | `wall` | Works as before |
| Proper title display | Search any term | Titles show readable text, not filenames |

---

## File Changes Summary

| File | Changes | Lines Affected |
|------|---------|----------------|
| `scripts/build_search_index.py` | Pattern, content length, title lookup | ~25 lines |
| `a/toc/toc-sidebar.js` | Multi-word search functions | ~80 lines |
| `a/toc/search-index.json` | Regenerated | Full file |

---

## Risks & Mitigations

| Risk | Impact | Mitigation |
|------|--------|------------|
| Larger index file (~5 MB) | Slower initial load | Gzip compression (~1.5 MB), lazy loading already implemented |
| AND logic may be too strict | Fewer results for multi-word queries | Single-word searches unchanged; matches are more relevant |
| Index generation time | Longer build time | One-time cost, ~2-3 minutes for 2079 posts |
| Breaking existing behavior | User confusion | Single-word searches work identically |

---

## Future Enhancements (Out of Scope)

1. **Highlight all matched words** - Currently only first word is highlighted
2. **OR search support** - Using `|` or `OR` operator
3. **Phrase search with quotes** - `"exact phrase"` syntax
4. **Fuzzy matching** - Typo tolerance
5. **Search ranking** - Sort by relevance (match count, position)

---

## Implementation Order

1. ✅ Phase 1: Pattern fix (low risk, high impact)
2. ✅ Phase 2: Content length increase (low risk, medium impact)
3. ✅ Phase 3: Title lookup fix (low risk, high impact)
4. ✅ Phase 4: Multi-word search (medium risk, high impact)
5. ✅ Phase 5: Regenerate index and test
