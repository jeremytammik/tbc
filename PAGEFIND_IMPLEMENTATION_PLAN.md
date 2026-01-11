# Pagefind Integration Implementation Plan

**Date:** January 11, 2026  
**Purpose:** Replace custom search implementation with Pagefind  
**Branch:** `feature/pagefind-search`

---

## Overview

This plan replaces the current custom search implementation (6.9 MB JSON index) with Pagefind, a static search library that uses intelligent index sharding to load only ~50-100 KB per search query.

### Key Changes:
1. **Remove:** Custom search index (`search-index.json`) and build script
2. **Remove:** "Search in content" checkbox (content search becomes default)
3. **Update:** Search placeholder text to "Search posts..."
4. **Add:** Pagefind integration with custom UI matching existing sidebar design
5. **Add:** GitHub Actions workflow to build Pagefind index on deploy

---

## Phase 1: Pagefind Build Setup

### 1.1 Create Pagefind Configuration File

Create `pagefind.yml` in repository root:

```yaml
# Pagefind configuration
# See: https://pagefind.app/docs/config-options/

# Source directory containing HTML files
source: a

# Output directory for Pagefind index (relative to source)
bundle_dir: pagefind

# Only index blog post pages (####_*.htm and ####_*.html)
# Note: Pagefind globs are relative to source directory
glob: "[0-9][0-9][0-9][0-9]_*.{htm,html}"

# Exclude non-content elements from indexing
exclude_selectors:
  - "#tbc-sidebar"
  - ".tbc-sidebar"
  - "#tbc-chrono-nav"
  - ".tbc-chrono-nav"
  - "nav"
  - "footer"
  - "script"
  - "style"
  - ".tbc-sidebar-overlay"
  - ".tbc-mobile-toggle"

# Keep URLs relative (important for GitHub Pages subdirectory)
keep_index_url: false
```

**Note:** The `bundle_dir` is relative to `source`, so Pagefind output goes to `a/pagefind/`.

### 1.2 Update .gitignore

Add Pagefind output directory:

```
# Pagefind generated files
a/pagefind/
```

### 1.3 Create Build Script

Create `scripts/build_pagefind.py` or use npm script:

```bash
# Option A: npm (recommended for CI)
npx pagefind --site a --output-path a/pagefind

# Option B: Python wrapper
python3 -m pip install 'pagefind[extended]'
python3 -m pagefind --site a --output-path a/pagefind
```

---

## Phase 2: Update toc-sidebar.js

### 2.1 Remove Content Search Toggle

**File:** `a/toc/toc-sidebar.js`

Remove these elements from CONFIG:
- `searchIndexUrl`
- `enableContentSearch`
- `searchCacheTime`
- `storageKeys.searchIndex`
- `storageKeys.searchIndexTime`
- `storageKeys.searchInContent`

Remove from state:
- `searchIndex`
- `searchIndexLoaded`
- `searchInContent`

### 2.2 Update Search Box HTML

**Location:** `createSidebarHTML()` function (around line 562)

**Before:**
```html
<input type="text" 
       id="tbc-search-input" 
       placeholder="Search post titles..." 
       autocomplete="off"
       aria-label="Search post titles">
```

**After:**
```html
<input type="text" 
       id="tbc-search-input" 
       placeholder="Search posts..." 
       autocomplete="off"
       aria-label="Search posts">
```

### 2.3 Remove Content Toggle Checkbox

**Location:** `createSidebarHTML()` function (around line 572-577)

**Remove this entire block:**
```html
<div class="tbc-search-options">
  <label class="tbc-search-toggle">
    <input type="checkbox" id="tbc-search-content-toggle" disabled title="Loading search index...">
    <span>Search in content</span>
  </label>
</div>
```

### 2.4 Remove Custom Search Index Functions

Remove these functions entirely:
- `loadSearchIndex()` (lines 366-468) - Loads custom JSON index
- `updateContentToggleState()` (lines 469-486) - Updates checkbox state
- `parseSearchWords()` (lines 69-77) - Multi-word parsing
- `containsAllWords()` (lines 80-87) - AND logic helper
- `getMatchType()` (lines 89-110+) - MATCH_TYPE determination
- `generateExcerptForDisplay()` (lines 119-180+) - Excerpt generation
- `performContentSearch()` (lines 1036-1200+) - Custom content search
- `MATCH_TYPE` constant (lines 58-66) - No longer needed

Modify:
- `updateResultsCount()` (line 1210) - Remove `isContentSearch` parameter
- `performSearch()` (line 961) - Simplify to only call Pagefind search

### 2.5 Keep Title-Only Fallback Search

**Important:** Keep the existing `performTitleSearch()` function (lines 976-1034) as a fallback when Pagefind is unavailable. This ensures search works even if:
- Pagefind index hasn't been built yet
- User is viewing locally without running Pagefind build
- Network issues prevent loading Pagefind chunks

### 2.6 Add Pagefind Integration

Add new Pagefind search function:

```javascript
// ================================
// Pagefind Integration
// ================================

let pagefind = null;

/**
 * Initialize Pagefind search
 */
async function initPagefind() {
  try {
    // Determine base path for Pagefind assets
    const basePath = getBasePath();
    pagefind = await import(basePath + 'pagefind/pagefind.js');
    await pagefind.init();
    console.log('Pagefind initialized successfully');
  } catch (error) {
    console.error('Failed to initialize Pagefind:', error);
    // Fallback to title-only search
  }
}

/**
 * Perform search using Pagefind
 * @param {string} query - Search query
 */
async function performPagefindSearch(query) {
  if (!pagefind) {
    // Fallback to title-only TOC search
    performTitleSearch(query);
    return;
  }

  const resultsContainer = document.getElementById('tbc-search-results');
  if (!resultsContainer) return;

  try {
    // Show loading state
    resultsContainer.innerHTML = '<div class="tbc-search-loading">Searching...</div>';

    // Perform debounced search
    const search = await pagefind.debouncedSearch(query, {}, 150);
    
    // If null, a newer search superseded this one
    if (search === null) return;

    if (search.results.length === 0) {
      resultsContainer.innerHTML = `
        <div class="tbc-search-no-results">
          No results for "<strong>${escapeHtml(query)}</strong>"
        </div>`;
      return;
    }

    // Load first 20 results
    const maxResults = 20;
    const resultsToLoad = search.results.slice(0, maxResults);
    const loadedResults = await Promise.all(resultsToLoad.map(r => r.data()));

    // Render results
    let html = `<div class="tbc-search-count">${search.results.length} result${search.results.length !== 1 ? 's' : ''}</div>`;
    html += '<ul class="tbc-search-results-list">';

    for (const result of loadedResults) {
      const title = result.meta?.title || 'Untitled';
      const url = result.url;
      const excerpt = result.excerpt || '';
      
      html += `
        <li class="tbc-search-result-item">
          <a href="${escapeHtml(url)}" class="tbc-search-result-link">
            <span class="tbc-search-result-title">${escapeHtml(title)}</span>
            ${excerpt ? `<span class="tbc-search-result-excerpt">${excerpt}</span>` : ''}
          </a>
        </li>`;
    }

    html += '</ul>';

    if (search.results.length > maxResults) {
      html += `<div class="tbc-search-more">Showing ${maxResults} of ${search.results.length} results</div>`;
    }

    resultsContainer.innerHTML = html;

  } catch (error) {
    console.error('Pagefind search error:', error);
    resultsContainer.innerHTML = '<div class="tbc-search-error">Search error. Please try again.</div>';
  }
}
```

### 2.6 Update Search Event Handler

**Location:** `setupEventListeners()` function

Replace the search input handler to use Pagefind:

```javascript
// Search input handler
const searchInput = document.getElementById('tbc-search-input');
if (searchInput) {
  searchInput.addEventListener('input', (e) => {
    const query = e.target.value.trim();
    state.searchQuery = query;

    // Show/hide clear button
    const clearBtn = document.getElementById('tbc-search-clear');
    if (clearBtn) {
      clearBtn.classList.toggle('hidden', query.length === 0);
    }

    if (query.length === 0) {
      clearSearchResults();
      showTopics();
      return;
    }

    if (query.length >= 2) {
      hideTopics();
      performPagefindSearch(query);
    }
  });
}
```

### 2.7 Initialize Pagefind on Load

In the `initializeSidebar()` function, add:

```javascript
// Initialize Pagefind
initPagefind().catch(err => {
  console.warn('Pagefind not available, using title-only search:', err.message);
});
```

---

## Phase 3: Update CSS Styles

### 3.1 Update Search Results Styles

**File:** `a/toc/toc-sidebar.css`

Add/update styles for Pagefind results with highlighting:

```css
/* Pagefind search results */
.tbc-search-results-list {
  list-style: none;
  padding: 0;
  margin: 0;
}

.tbc-search-result-item {
  border-bottom: 1px solid var(--tbc-border-color, #e0e0e0);
}

.tbc-search-result-link {
  display: block;
  padding: 10px 12px;
  text-decoration: none;
  color: inherit;
}

.tbc-search-result-link:hover {
  background-color: var(--tbc-hover-bg, #f5f5f5);
}

.tbc-search-result-title {
  display: block;
  font-weight: 500;
  color: var(--tbc-link-color, #1a73e8);
  margin-bottom: 4px;
}

.tbc-search-result-excerpt {
  display: block;
  font-size: 0.85em;
  color: var(--tbc-text-muted, #666);
  line-height: 1.4;
}

/* Pagefind highlight marks */
.tbc-search-result-excerpt mark {
  background-color: #fff3cd;
  color: inherit;
  padding: 0 2px;
  border-radius: 2px;
}

.tbc-search-count {
  padding: 8px 12px;
  font-size: 0.85em;
  color: var(--tbc-text-muted, #666);
  border-bottom: 1px solid var(--tbc-border-color, #e0e0e0);
}

.tbc-search-more {
  padding: 8px 12px;
  font-size: 0.85em;
  color: var(--tbc-text-muted, #666);
  text-align: center;
  font-style: italic;
}

.tbc-search-loading {
  padding: 20px;
  text-align: center;
  color: var(--tbc-text-muted, #666);
}

.tbc-search-no-results {
  padding: 20px;
  text-align: center;
  color: var(--tbc-text-muted, #666);
}

.tbc-search-error {
  padding: 20px;
  text-align: center;
  color: #dc3545;
}
```

### 3.2 Remove Content Toggle Styles

Remove any CSS related to `.tbc-search-toggle` and `#tbc-search-content-toggle`.

---

## Phase 4: GitHub Actions Workflow

### 4.1 Create/Update Workflow

**File:** `.github/workflows/pagefind.yml`

```yaml
name: Build Pagefind Index

on:
  push:
    branches:
      - gh-pages
    paths:
      - 'a/**/*.htm'
      - 'a/**/*.html'
  workflow_dispatch:

jobs:
  build-index:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout repository
        uses: actions/checkout@v4

      - name: Setup Node.js
        uses: actions/setup-node@v4
        with:
          node-version: '20'

      - name: Build Pagefind index
        run: npx -y pagefind --site a --output-path a/pagefind

      - name: Commit and push Pagefind index
        run: |
          git config user.name github-actions
          git config user.email github-actions@github.com
          git add a/pagefind/
          git diff --staged --quiet || git commit -m "Update Pagefind search index"
          git push
```

**Note:** This workflow builds the Pagefind index whenever HTML files change and commits the result back to the repository.

### 4.2 Alternative: Commit Pagefind Index to Repository

For simplicity, you can also commit the Pagefind index directly to the repository instead of rebuilding on every push. This is simpler but increases repository size:

```bash
# Build locally once
npx pagefind --site a

# Commit the index
git add a/pagefind/
git commit -m "Add Pagefind search index"
git push
```

**Trade-off:** Repository grows with each rebuild, but no CI complexity.

---

## Phase 5: Cleanup

### 5.1 Files to Remove

- `a/toc/search-index.json` (6.9 MB custom index)
- Remove search index references from `.gitignore` if any

### 5.2 Files to Modify

- `scripts/build_search_index.py` - Can be deleted or kept as backup

### 5.3 Update Documentation

Update `SEARCH_ALTERNATIVES_ANALYSIS.md` to note Pagefind implementation.

---

## Phase 6: Testing

### 6.1 Local Testing

```bash
# Build Pagefind index locally
npx pagefind --site a --serve

# Opens http://localhost:1414 for testing
```

### 6.2 Test Cases

1. **Basic search:** Type "revit" → should show relevant results
2. **Multi-word search:** Type "project dashboard" → should find all 9 posts
3. **No results:** Type "xyznonexistent" → should show "No results" message
4. **Clear search:** Click X button → should return to topic view
5. **Result click:** Click a result → should navigate to the post
6. **Highlighting:** Excerpts should have search terms highlighted with `<mark>` tags
7. **Mobile:** Test on mobile viewport → sidebar should work correctly

### 6.3 Performance Validation

- Initial page load should NOT download the search index
- First search should download ~50-100 KB of index chunks
- Subsequent searches should be fast (cached chunks)

---

## Implementation Checklist

### Phase 1: Build Setup
- [ ] Create `pagefind.yml` configuration
- [ ] Update `.gitignore` for Pagefind output
- [ ] Test local Pagefind build

### Phase 2: JavaScript Updates
- [ ] Remove custom search index loading code
- [ ] Remove content search toggle state
- [ ] Update search placeholder text
- [ ] Remove content toggle checkbox HTML
- [ ] Add Pagefind initialization function
- [ ] Add Pagefind search function
- [ ] Update search event handlers
- [ ] Add fallback for when Pagefind unavailable

### Phase 3: CSS Updates  
- [ ] Add Pagefind result styles
- [ ] Add highlight mark styles
- [ ] Remove content toggle styles

### Phase 4: CI/CD
- [ ] Create GitHub Actions workflow
- [ ] Test workflow on feature branch

### Phase 5: Cleanup
- [ ] Remove `search-index.json`
- [ ] Optionally remove/archive `build_search_index.py`
- [ ] Update documentation

### Phase 6: Testing
- [ ] Test local build
- [ ] Test all search scenarios
- [ ] Test mobile responsiveness
- [ ] Validate performance improvement

---

## Rollback Plan

If issues arise, the custom search implementation can be restored by:

1. Reverting the `toc-sidebar.js` changes
2. Regenerating `search-index.json` with the Python script
3. Removing Pagefind files from the repository

---

## Expected Outcomes

| Metric | Before | After |
|--------|--------|-------|
| Initial load (search) | 6.9 MB | ~15 KB (library only) |
| Per-query load | 0 | ~50-100 KB (chunks) |
| Search features | Title + content substring | Full-text with highlighting |
| Multi-word search | Custom AND logic | Built-in support |
| Typo tolerance | None | Basic fuzzy matching |
| Maintenance | Manual index builds | Automatic via GitHub Actions |

---

## References

- Pagefind Documentation: https://pagefind.app/docs/
- Pagefind JavaScript API: https://pagefind.app/docs/api/
- Pagefind Configuration: https://pagefind.app/docs/config-options/
