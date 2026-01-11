# Content Match Indicator Implementation Plan

**Version**: 1.1  
**Created**: January 11, 2026  
**Updated**: January 11, 2026 (Codebase Review)  
**Specification**: [CONTENT_MATCH_INDICATOR_SPEC.md](CONTENT_MATCH_INDICATOR_SPEC.md)  
**Estimated Total Time**: 8-12 hours

---

## Table of Contents

1. [Codebase Review Findings](#codebase-review-findings)
2. [Prerequisites](#prerequisites)
3. [Phase 1: Core Indicators](#phase-1-core-indicators)
4. [Phase 2: Excerpt Display](#phase-2-excerpt-display)
5. [Phase 3: Enhanced Interactions](#phase-3-enhanced-interactions)
6. [Phase 4: Polish & Accessibility](#phase-4-polish--accessibility)
7. [Test Plan](#test-plan)
8. [Rollback Plan](#rollback-plan)

---

## Codebase Review Findings

> **Review Date**: January 11, 2026  
> **Files Reviewed**: toc-sidebar.js, toc-sidebar.css, build_search_index.py, publish_post.py, update_post.py, delete_post.py, search-index.json

### ✅ Compatible Elements (No Changes Required)

| Component | Spec Requirement | Codebase Status |
|-----------|------------------|-----------------|
| Search Index | Needs `contentPreview` field | ✅ Exists (800 chars, lowercase) |
| Search Index | Available at `toc/search-index.json` | ✅ File exists |
| Script Integration | Auto-regenerate on publish/update/delete | ✅ All 3 scripts call `update_search_index()` |
| `performContentSearch()` | Function exists | ✅ Line 808 in toc-sidebar.js |
| `highlightText()` | Yellow highlight for title matches | ✅ Uses `.tbc-search-highlight` class |
| CSS Variable | `--tbc-search-highlight` | ✅ Defined as `#fffacd` |
| `escapeRegex()` | Utility for safe regex | ✅ Line 71 in toc-sidebar.js |

### ⚠️ Key Implementation Notes

#### 1. Data Structure Change Required
**Current**: `matchingPosts` is a `Set`  
**Required**: Change to `Map` to store match metadata  
**Location**: `performContentSearch()` at line 812

#### 2. Search Index Has Two Excerpt Fields
```json
{
  "excerpt": "Welcome to The Building Coder...",     // 200 chars, PROPER CASE
  "contentPreview": "the building coder back to..."  // 800 chars, LOWERCASE
}
```
**Decision**: Use `excerpt` field for display (proper case), `contentPreview` for matching.

#### 3. DOM Structure Confirmed
Posts are rendered as: `.tbc-topic > .tbc-topic-posts > .tbc-post-link`  
Excerpt insertion via `insertBefore(excerptElement, postLink.nextSibling)` will work correctly.

#### 4. Existing Functions to Leverage
- `escapeRegex(string)` - Line 71
- `escapeHtml(text)` - Line 67
- `highlightText(element, query)` - Line 939
- `removeHighlight(element)` - Line 950

#### 5. Content Toggle Handler Needs Cleanup
When "Search in content" is toggled OFF during active search, must remove all excerpts and `.tbc-content-match` classes.

---

## Prerequisites

### Branch Setup
```bash
git checkout gh-pages
git pull origin gh-pages
git checkout -b feature/content-match-indicator
```

### Files to Modify
| File | Changes |
|------|---------|
| `a/toc/toc-sidebar.js` | Match type detection, excerpt generation, DOM manipulation |
| `a/toc/toc-sidebar.css` | Content match styling, excerpt styling, animations |

### No Changes Required
- `scripts/build_search_index.py` - Existing `contentPreview` field is sufficient
- `a/toc/search-index.json` - No schema changes needed

---

## Phase 1: Core Indicators

**Estimated Time**: 2-3 hours  
**Objective**: Add visual distinction between title and content matches

### Step 1.1: Add Match Type Constants

**File**: `a/toc/toc-sidebar.js`  
**Location**: Inside the Sidebar IIFE, after `state` object

```javascript
// Match type enumeration for search results
const MATCH_TYPE = Object.freeze({
  NONE: 0,
  TITLE_ONLY: 1,
  CONTENT_ONLY: 2,
  BOTH: 3
});
```

### Step 1.2: Create Match Type Detection Function

**File**: `a/toc/toc-sidebar.js`  
**Location**: After `MATCH_TYPE` constant

```javascript
/**
 * Determine the type of match for a post against a search query
 * @param {Object} post - Post object with title and contentPreview
 * @param {string} query - Lowercase search query
 * @returns {number} MATCH_TYPE value
 */
function getMatchType(post, query) {
  const titleMatch = post.title && post.title.toLowerCase().includes(query);
  const contentMatch = post.contentPreview && 
    post.contentPreview.toLowerCase().includes(query);
  
  if (titleMatch && contentMatch) return MATCH_TYPE.BOTH;
  if (titleMatch) return MATCH_TYPE.TITLE_ONLY;
  if (contentMatch) return MATCH_TYPE.CONTENT_ONLY;
  return MATCH_TYPE.NONE;
}
```

### Step 1.3: Update `performContentSearch()` to Track Match Types

**File**: `a/toc/toc-sidebar.js`  
**Location**: Modify existing `performContentSearch()` function

**Current code pattern to find**:
```javascript
if (titleMatch || contentMatch) {
  matchingPosts.add(post.file);
}
```

**Replace with**:
```javascript
const matchType = getMatchType(post, query);
if (matchType !== MATCH_TYPE.NONE) {
  matchingPosts.set(post.file, {
    matchType: matchType,
    title: post.title,
    contentPreview: post.contentPreview
  });
}
```

**Note**: Change `matchingPosts` from `Set` to `Map` to store match metadata.

### Step 1.4: Update Post Link Rendering to Add Content Match Class

**File**: `a/toc/toc-sidebar.js`  
**Location**: In the search results rendering loop

**Find the loop that shows/hides posts based on search matches and add**:
```javascript
// After determining post should be visible
const matchData = matchingPosts.get(postFile);
if (matchData && matchData.matchType === MATCH_TYPE.CONTENT_ONLY) {
  postLink.classList.add('tbc-content-match');
} else {
  postLink.classList.remove('tbc-content-match');
}
```

### Step 1.5: Update Result Count Display

**File**: `a/toc/toc-sidebar.js`  
**Location**: In the results count update section

```javascript
/**
 * Update search results count with match type breakdown
 * @param {Map} matchingPosts - Map of file -> matchData
 * @param {HTMLElement} resultsDiv - Results count element
 */
function updateResultsCountWithBreakdown(matchingPosts, resultsDiv) {
  if (!resultsDiv) return;
  
  let titleCount = 0;
  let contentOnlyCount = 0;
  
  matchingPosts.forEach((data) => {
    if (data.matchType === MATCH_TYPE.CONTENT_ONLY) {
      contentOnlyCount++;
    } else {
      titleCount++;
    }
  });
  
  const total = titleCount + contentOnlyCount;
  
  if (total === 0) {
    resultsDiv.textContent = 'No posts found';
    resultsDiv.classList.add('no-results');
  } else if (contentOnlyCount === 0) {
    resultsDiv.textContent = `${total} result${total === 1 ? '' : 's'}`;
    resultsDiv.classList.remove('no-results');
  } else {
    resultsDiv.innerHTML = `${total} result${total === 1 ? '' : 's'} ` +
      `<span class="tbc-result-breakdown">(${titleCount} in title, ${contentOnlyCount} in content)</span>`;
    resultsDiv.classList.remove('no-results');
  }
}
```

### Step 1.6: Add Content Match CSS

**File**: `a/toc/toc-sidebar.css`  
**Location**: After existing search highlight styles

```css
/* ============================================
   Content Match Indicator Styles
   ============================================ */

/* Content-only match background and indicator */
.tbc-post-link.tbc-content-match {
  background: linear-gradient(90deg, 
    rgba(66, 153, 225, 0.10) 0%, 
    rgba(66, 153, 225, 0.03) 100%);
  border-left: 2px solid var(--tbc-accent-primary, #0066cc);
  padding-left: 8px;
  margin-left: -10px;
  border-radius: 0 4px 4px 0;
}

/* Content match icon indicator */
.tbc-post-link.tbc-content-match::after {
  content: ' 📄';
  font-size: 10px;
  opacity: 0.7;
  vertical-align: middle;
}

/* Result count breakdown styling */
.tbc-result-breakdown {
  font-size: 10px;
  color: var(--tbc-sidebar-text-muted, #666);
  font-weight: normal;
}
```

### Phase 1 Validation Checkpoint

Before proceeding to Phase 2, verify:
- [ ] Content-only matches have blue-tinted background
- [ ] Content-only matches show 📄 icon
- [ ] Title matches still show yellow highlight (no regression)
- [ ] Result count shows breakdown when content matches exist
- [ ] Result count shows simple count when only title matches

---

## Phase 2: Excerpt Display

**Estimated Time**: 3-4 hours  
**Objective**: Show contextual excerpt for content-only matches

### Step 2.1: Create Excerpt Generation Function

**File**: `a/toc/toc-sidebar.js`  
**Location**: After `getMatchType()` function

> **CODEBASE NOTE**: The search index has two excerpt fields:
> - `excerpt` (200 chars, proper case) - Use for display
> - `contentPreview` (800 chars, lowercase) - Use for matching
> 
> The function below uses the proper-case `excerpt` field when available.

```javascript
/**
 * Generate an excerpt showing context around the matched term
 * Uses the proper-case 'excerpt' field for display when available,
 * falls back to 'contentPreview' for longer context.
 * 
 * @param {Object} postData - Post data with excerpt and contentPreview
 * @param {string} query - Search query (lowercase)
 * @param {number} maxLength - Maximum excerpt length (default 80)
 * @returns {Object|null} { text: string, query: string } or null
 */
function generateExcerptForDisplay(postData, query, maxLength = 80) {
  if (!query) return null;
  
  // Prefer the proper-case excerpt field if it contains the match
  const excerpt = postData.excerpt || '';
  const contentPreview = postData.contentPreview || '';
  
  // Check which field contains the match
  const excerptHasMatch = excerpt.toLowerCase().includes(query);
  const sourceText = excerptHasMatch ? excerpt : contentPreview;
  
  if (!sourceText) return null;
  
  const lowerSource = sourceText.toLowerCase();
  const matchIndex = lowerSource.indexOf(query);
  
  if (matchIndex === -1) return null;
  
  // Calculate window around match
  const halfWindow = Math.floor((maxLength - query.length) / 2);
  let start = Math.max(0, matchIndex - halfWindow);
  let end = Math.min(sourceText.length, matchIndex + query.length + halfWindow);
  
  // Adjust to word boundaries (don't cut words in half)
  if (start > 0) {
    const spaceAfterStart = sourceText.indexOf(' ', start);
    if (spaceAfterStart !== -1 && spaceAfterStart < matchIndex) {
      start = spaceAfterStart + 1;
    }
  }
  if (end < sourceText.length) {
    const spaceBeforeEnd = sourceText.lastIndexOf(' ', end);
    if (spaceBeforeEnd > matchIndex + query.length) {
      end = spaceBeforeEnd;
    }
  }
  
  let excerptText = sourceText.substring(start, end).trim();
  
  // Add ellipsis indicators
  if (start > 0) excerptText = '...' + excerptText;
  if (end < sourceText.length) excerptText = excerptText + '...';
  
  return {
    text: excerptText,
    query: query,
    isLowercase: !excerptHasMatch  // Flag if using lowercase contentPreview
  };
}
  
  // Calculate highlight position in the excerpt
  const highlightStart = (hasLeadingEllipsis ? 3 : 0) + (matchIndex - start);
  const highlightEnd = highlightStart + query.length;
  
  return {
    excerpt: excerpt,
    highlightStart: highlightStart,
    highlightEnd: highlightEnd
  };
}
```

### Step 2.2: Create Excerpt HTML Builder

**File**: `a/toc/toc-sidebar.js`  
**Location**: After `generateExcerptForDisplay()` function

> **CODEBASE NOTE**: `escapeRegex()` already exists at line 71, no need to add it again.
> Use `escapeHtml()` from line 67 for safe HTML output.

```javascript
/**
 * Create excerpt DOM element with highlighted search term
 * @param {Object} excerptData - Result from generateExcerptForDisplay()
 * @param {string} postFile - Post filename for unique ID
 * @returns {HTMLElement} Excerpt container element
 */
function createExcerptElement(excerptData, postFile) {
  const container = document.createElement('div');
  container.className = 'tbc-excerpt collapsed';
  container.id = `excerpt-${postFile.replace(/\./g, '-')}`;
  container.setAttribute('role', 'note');
  container.setAttribute('aria-label', 'Content excerpt');
  
  const textSpan = document.createElement('span');
  textSpan.className = 'tbc-excerpt-text';
  
  // Add lowercase indicator if using contentPreview fallback
  if (excerptData.isLowercase) {
    textSpan.classList.add('tbc-excerpt-lowercase');
  }
  
  // Highlight the search term within the excerpt (escapeRegex already exists)
  const regex = new RegExp(`(${escapeRegex(excerptData.query)})`, 'gi');
  const safeExcerpt = escapeHtml(excerptData.text);
  textSpan.innerHTML = '"' + safeExcerpt.replace(regex, '<mark>$1</mark>') + '"';
  
  const toggleBtn = document.createElement('button');
  toggleBtn.className = 'tbc-excerpt-toggle';
  toggleBtn.setAttribute('aria-label', 'Toggle excerpt visibility');
  toggleBtn.setAttribute('aria-expanded', 'false');
  toggleBtn.textContent = '▼';
  
  toggleBtn.addEventListener('click', (e) => {
    e.preventDefault();
    e.stopPropagation();
    const isExpanded = container.classList.contains('expanded');
    container.classList.toggle('expanded');
    container.classList.toggle('collapsed');
    toggleBtn.setAttribute('aria-expanded', !isExpanded);
    toggleBtn.textContent = isExpanded ? '▼' : '▲';
  });
  
  container.appendChild(textSpan);
  container.appendChild(toggleBtn);
  
  return container;
}
```

### Step 2.3: Update Post Rendering to Insert Excerpts

**File**: `a/toc/toc-sidebar.js`  
**Location**: In the search results rendering section (inside `performContentSearch()`)

> **CODEBASE NOTE**: Must also store `excerpt` field in matchingPosts Map (update Step 1.3).

```javascript
// Update Step 1.3 to include excerpt field:
matchingPosts.set(post.file, {
  matchType: matchType,
  title: post.title,
  excerpt: post.excerpt,           // ADD THIS - proper case, 200 chars
  contentPreview: post.contentPreview
});

// Then in the DOM update loop:
if (matchData && matchData.matchType === MATCH_TYPE.CONTENT_ONLY) {
  postLink.classList.add('tbc-content-match');
  
  // Remove any existing excerpt
  const existingExcerpt = postLink.parentElement.querySelector('.tbc-excerpt');
  if (existingExcerpt) {
    existingExcerpt.remove();
  }
  
  // Generate and insert new excerpt (using updated function)
  const excerptData = generateExcerptForDisplay(matchData, query);
  if (excerptData) {
    const excerptElement = createExcerptElement(excerptData, postFile);
    postLink.parentElement.insertBefore(excerptElement, postLink.nextSibling);
  }
} else {
  postLink.classList.remove('tbc-content-match');
  
  // Remove excerpt if match type changed
  const existingExcerpt = postLink.parentElement.querySelector('.tbc-excerpt');
  if (existingExcerpt) {
    existingExcerpt.remove();
  }
}
```

### Step 2.4: Add Excerpt CSS

**File**: `a/toc/toc-sidebar.css`  
**Location**: After content match indicator styles

```css
/* ============================================
   Excerpt Display Styles
   ============================================ */

/* Excerpt container - collapsed state (default) */
.tbc-excerpt {
  overflow: hidden;
  transition: max-height 0.25s ease, opacity 0.2s ease, padding 0.2s ease;
}

.tbc-excerpt.collapsed {
  max-height: 0;
  opacity: 0;
  padding: 0;
}

/* Excerpt container - expanded state */
.tbc-excerpt.expanded {
  max-height: 60px;
  opacity: 1;
  padding: 4px 0 6px 12px;
  margin-top: 2px;
}

/* Excerpt text styling */
.tbc-excerpt-text {
  font-size: 11px;
  color: var(--tbc-sidebar-text-muted, #666);
  font-style: italic;
  line-height: 1.4;
  display: block;
  padding-left: 8px;
  border-left: 1px solid var(--tbc-sidebar-border, #ddd);
  word-wrap: break-word;
  overflow-wrap: break-word;
}

/* Highlighted term within excerpt */
.tbc-excerpt-text mark {
  background: #fff3cd;
  color: inherit;
  padding: 0 2px;
  border-radius: 2px;
  font-style: normal;
}

/* Expand/collapse toggle button */
.tbc-excerpt-toggle {
  background: none;
  border: none;
  color: var(--tbc-sidebar-text-muted, #888);
  cursor: pointer;
  font-size: 10px;
  padding: 2px 4px;
  margin-left: 4px;
  transition: transform 0.2s ease;
  vertical-align: middle;
}

.tbc-excerpt-toggle:hover {
  color: var(--tbc-accent-primary, #0066cc);
}

.tbc-excerpt-toggle:focus {
  outline: 2px solid var(--tbc-accent-primary, #0066cc);
  outline-offset: 1px;
  border-radius: 2px;
}

/* Toggle rotation when expanded */
.tbc-excerpt.expanded .tbc-excerpt-toggle {
  transform: rotate(180deg);
}
```

### Step 2.5: Clean Up Excerpts on Search Clear

**File**: `a/toc/toc-sidebar.js`  
**Location**: In the `clearSearch()` function or equivalent

```javascript
// Remove all excerpt elements when search is cleared
function removeAllExcerpts() {
  const excerpts = document.querySelectorAll('.tbc-excerpt');
  excerpts.forEach(excerpt => excerpt.remove());
  
  // Also remove content-match class from all posts
  const contentMatches = document.querySelectorAll('.tbc-content-match');
  contentMatches.forEach(el => el.classList.remove('tbc-content-match'));
}
```

### Step 2.6: Add Cleanup to Content Toggle Handler

**File**: `a/toc/toc-sidebar.js`  
**Location**: Modify the existing content toggle event listener (around line 718)

> **CODEBASE NOTE**: When user toggles "Search in content" OFF during an active search, 
> all content match indicators and excerpts must be removed before re-running the search.

```javascript
// Find existing handler (around line 718-729) and modify:
if (contentToggle) {
  contentToggle.addEventListener('change', () => {
    state.searchInContent = contentToggle.checked;
    // Save preference
    try {
      localStorage.setItem(CONFIG.storageKeys.searchInContent, contentToggle.checked.toString());
    } catch (e) {
      // Ignore storage errors
    }
    
    // NEW: Clean up content match indicators when disabling content search
    if (!contentToggle.checked) {
      removeAllExcerpts();
    }
    
    // Re-run search with new mode
    if (state.searchQuery) {
      performSearch(state.searchQuery);
    }
  });
}
```

### Phase 2 Validation Checkpoint

Before proceeding to Phase 3, verify:
- [ ] Content-only matches show expandable excerpt below title
- [ ] Clicking toggle expands/collapses the excerpt
- [ ] Search term is highlighted (yellow) within excerpt
- [ ] Excerpt shows ellipsis when text is truncated
- [ ] Clearing search removes all excerpts
- [ ] New search updates excerpts correctly
- [ ] **NEW**: Toggling content search OFF removes all excerpts immediately

---

## Phase 3: Enhanced Interactions

**Estimated Time**: 2-3 hours  
**Objective**: Add convenience features for managing excerpts

### Step 3.1: Add Expand All / Collapse All Controls

**File**: `a/toc/toc-sidebar.js`  
**Location**: In the search results header area creation

```javascript
/**
 * Create excerpt control buttons (Expand All / Collapse All)
 * @returns {HTMLElement} Container with control buttons
 */
function createExcerptControls() {
  const container = document.createElement('div');
  container.className = 'tbc-excerpt-controls';
  container.id = 'tbc-excerpt-controls';
  container.style.display = 'none'; // Hidden until content matches exist
  
  const expandBtn = document.createElement('button');
  expandBtn.className = 'tbc-excerpt-control-btn';
  expandBtn.id = 'tbc-expand-all';
  expandBtn.textContent = 'Show excerpts';
  expandBtn.title = 'Expand all content excerpts';
  expandBtn.addEventListener('click', () => toggleAllExcerpts(true));
  
  const collapseBtn = document.createElement('button');
  collapseBtn.className = 'tbc-excerpt-control-btn';
  collapseBtn.id = 'tbc-collapse-all';
  collapseBtn.textContent = 'Hide excerpts';
  collapseBtn.title = 'Collapse all content excerpts';
  collapseBtn.addEventListener('click', () => toggleAllExcerpts(false));
  
  container.appendChild(expandBtn);
  container.appendChild(collapseBtn);
  
  return container;
}

/**
 * Expand or collapse all excerpts
 * @param {boolean} expand - true to expand, false to collapse
 */
function toggleAllExcerpts(expand) {
  const excerpts = document.querySelectorAll('.tbc-excerpt');
  excerpts.forEach(excerpt => {
    if (expand) {
      excerpt.classList.remove('collapsed');
      excerpt.classList.add('expanded');
    } else {
      excerpt.classList.remove('expanded');
      excerpt.classList.add('collapsed');
    }
    
    const toggle = excerpt.querySelector('.tbc-excerpt-toggle');
    if (toggle) {
      toggle.setAttribute('aria-expanded', expand);
      toggle.textContent = expand ? '▲' : '▼';
    }
  });
  
  // Save preference
  try {
    localStorage.setItem(CONFIG.storageKeys.excerptExpanded || 'tbc-excerpt-expanded', 
      expand ? 'all' : 'none');
  } catch (e) {
    // Ignore storage errors
  }
}
```

### Step 3.2: Show/Hide Excerpt Controls Based on Results

**File**: `a/toc/toc-sidebar.js`  
**Location**: In the results update function

```javascript
/**
 * Update visibility of excerpt controls based on content match count
 * @param {number} contentOnlyCount - Number of content-only matches
 */
function updateExcerptControlsVisibility(contentOnlyCount) {
  const controls = document.getElementById('tbc-excerpt-controls');
  if (controls) {
    controls.style.display = contentOnlyCount > 0 ? 'flex' : 'none';
  }
}
```

### Step 3.3: Add Hover-to-Expand (Desktop Only)

**File**: `a/toc/toc-sidebar.js`  
**Location**: After excerpt element creation

```javascript
/**
 * Add hover behavior for excerpt preview (desktop only)
 * @param {HTMLElement} postLink - The post link element
 * @param {HTMLElement} excerptElement - The excerpt container
 */
function addExcerptHoverBehavior(postLink, excerptElement) {
  // Only add hover behavior on non-touch devices
  if (window.matchMedia('(hover: hover)').matches) {
    let hoverTimeout;
    
    postLink.addEventListener('mouseenter', () => {
      hoverTimeout = setTimeout(() => {
        excerptElement.classList.remove('collapsed');
        excerptElement.classList.add('expanded');
        const toggle = excerptElement.querySelector('.tbc-excerpt-toggle');
        if (toggle) {
          toggle.setAttribute('aria-expanded', 'true');
          toggle.textContent = '▲';
        }
      }, 300); // 300ms delay to prevent flicker
    });
    
    postLink.addEventListener('mouseleave', () => {
      clearTimeout(hoverTimeout);
    });
    
    excerptElement.addEventListener('mouseleave', () => {
      // Only collapse if user preference isn't "all expanded"
      try {
        const pref = localStorage.getItem(CONFIG.storageKeys.excerptExpanded || 'tbc-excerpt-expanded');
        if (pref !== 'all') {
          excerptElement.classList.remove('expanded');
          excerptElement.classList.add('collapsed');
          const toggle = excerptElement.querySelector('.tbc-excerpt-toggle');
          if (toggle) {
            toggle.setAttribute('aria-expanded', 'false');
            toggle.textContent = '▼';
          }
        }
      } catch (e) {
        // Collapse anyway if storage fails
        excerptElement.classList.remove('expanded');
        excerptElement.classList.add('collapsed');
      }
    });
  }
}
```

### Step 3.4: Add Storage Key for Preference

**File**: `a/toc/toc-sidebar.js`  
**Location**: In CONFIG.storageKeys object

```javascript
storageKeys: {
  // ... existing keys
  excerptExpanded: 'tbc-sidebar-excerpt-expanded'
}
```

### Step 3.5: Add Excerpt Controls CSS

**File**: `a/toc/toc-sidebar.css`  
**Location**: After excerpt styles

```css
/* ============================================
   Excerpt Control Buttons
   ============================================ */

.tbc-excerpt-controls {
  display: flex;
  gap: 8px;
  margin-top: 6px;
  padding-top: 6px;
  border-top: 1px solid var(--tbc-sidebar-border, #eee);
}

.tbc-excerpt-control-btn {
  background: var(--tbc-sidebar-bg-secondary, #f5f5f5);
  border: 1px solid var(--tbc-sidebar-border, #ddd);
  border-radius: 4px;
  color: var(--tbc-sidebar-text-muted, #666);
  cursor: pointer;
  font-size: 10px;
  padding: 4px 8px;
  transition: all 0.15s ease;
}

.tbc-excerpt-control-btn:hover {
  background: var(--tbc-accent-primary, #0066cc);
  border-color: var(--tbc-accent-primary, #0066cc);
  color: white;
}

.tbc-excerpt-control-btn:focus {
  outline: 2px solid var(--tbc-accent-primary, #0066cc);
  outline-offset: 1px;
}
```

### Phase 3 Validation Checkpoint

Before proceeding to Phase 4, verify:
- [ ] "Show excerpts" / "Hide excerpts" buttons appear when content matches exist
- [ ] Buttons are hidden when no content-only matches
- [ ] "Show excerpts" expands all collapsed excerpts
- [ ] "Hide excerpts" collapses all expanded excerpts
- [ ] Hover on post link (desktop) expands excerpt after delay
- [ ] Preference persists across searches

---

## Phase 4: Polish & Accessibility

**Estimated Time**: 1-2 hours  
**Objective**: Ensure accessibility and edge case handling

### Step 4.1: Add Reduced Motion Support

**File**: `a/toc/toc-sidebar.css`  
**Location**: At end of file

```css
/* ============================================
   Reduced Motion Support
   ============================================ */

@media (prefers-reduced-motion: reduce) {
  .tbc-excerpt {
    transition: none;
  }
  
  .tbc-excerpt-toggle {
    transition: none;
  }
  
  .tbc-post-link.tbc-content-match {
    transition: none;
  }
}
```

### Step 4.2: Add Dark Mode Support (if applicable)

**File**: `a/toc/toc-sidebar.css`  
**Location**: After reduced motion styles

```css
/* ============================================
   Dark Mode Support
   ============================================ */

@media (prefers-color-scheme: dark) {
  .tbc-post-link.tbc-content-match {
    background: linear-gradient(90deg, 
      rgba(66, 153, 225, 0.15) 0%, 
      rgba(66, 153, 225, 0.05) 100%);
  }
  
  .tbc-excerpt-text {
    color: #aaa;
  }
  
  .tbc-excerpt-text mark {
    background: #5a4a00;
    color: #fff;
  }
  
  .tbc-excerpt-control-btn {
    background: #333;
    border-color: #555;
    color: #ccc;
  }
  
  .tbc-excerpt-control-btn:hover {
    background: #4a9eff;
    border-color: #4a9eff;
  }
}
```

### Step 4.3: Add ARIA Descriptions to Post Links

**File**: `a/toc/toc-sidebar.js`  
**Location**: When adding content-match class

```javascript
// After creating excerpt element
if (excerptElement) {
  postLink.setAttribute('aria-describedby', excerptElement.id);
}
```

### Step 4.4: Handle Edge Cases

**File**: `a/toc/toc-sidebar.js`  
**Location**: In `generateExcerpt()` function, add at start

```javascript
// Handle edge cases
if (!contentPreview || typeof contentPreview !== 'string') return null;
if (!query || typeof query !== 'string') return null;
if (query.length === 0) return null;
if (contentPreview.length === 0) return null;

// Limit query length to prevent ReDoS
const safeQuery = query.substring(0, 100);
```

### Step 4.5: Add Keyboard Navigation

**File**: `a/toc/toc-sidebar.js`  
**Location**: After excerpt toggle click handler

```javascript
// Add keyboard support for post links with excerpts
postLink.addEventListener('keydown', (e) => {
  if (e.key === ' ' && postLink.classList.contains('tbc-content-match')) {
    e.preventDefault();
    const excerpt = postLink.parentElement.querySelector('.tbc-excerpt');
    if (excerpt) {
      const toggle = excerpt.querySelector('.tbc-excerpt-toggle');
      if (toggle) toggle.click();
    }
  }
});
```

### Phase 4 Validation Checkpoint

Final verification:
- [ ] Animations disabled with `prefers-reduced-motion`
- [ ] Dark mode colors are readable
- [ ] Screen reader announces excerpt content correctly
- [ ] Space key toggles excerpt when post link is focused
- [ ] Very long excerpts don't break layout
- [ ] Special characters in search term don't cause errors

---

## Test Plan

### Unit Tests (Manual or Automated)

#### Test Group 1: Match Type Detection

| Test ID | Description | Input | Expected Output |
|---------|-------------|-------|-----------------|
| MT-01 | Title only match | query="revit", title="Revit API", content="no match" | MATCH_TYPE.TITLE_ONLY |
| MT-02 | Content only match | query="facade", title="Building Exterior", content="facade system" | MATCH_TYPE.CONTENT_ONLY |
| MT-03 | Both match | query="wall", title="Wall Types", content="wall layers" | MATCH_TYPE.BOTH |
| MT-04 | No match | query="xyz123", title="Something", content="other" | MATCH_TYPE.NONE |
| MT-05 | Case insensitive | query="REVIT", title="revit api", content="x" | MATCH_TYPE.TITLE_ONLY |
| MT-06 | Null content preview | query="test", title="test", content=null | MATCH_TYPE.TITLE_ONLY |
| MT-07 | Empty content preview | query="test", title="test", content="" | MATCH_TYPE.TITLE_ONLY |

#### Test Group 2: Excerpt Generation

> **CODEBASE NOTE**: Tests should verify that proper-case `excerpt` field is used when 
> available, with fallback to lowercase `contentPreview` field.

| Test ID | Description | Input | Expected Output |
|---------|-------------|-------|-----------------|
| EX-01 | Basic excerpt from proper-case field | excerpt="The Wall layer contains insulation", query="layer" | "...Wall layer contains..." with "layer" marked (proper case preserved) |
| EX-02 | Fallback to contentPreview | excerpt="No match here", contentPreview="facade system details", query="facade" | "facade system..." (lowercase, `isLowercase: true`) |
| EX-03 | Match at start | content="ElementId is important for API", query="elementid" | "ElementId is important..." |
| EX-04 | Match at end | content="Consider the ElementId", query="elementid" | "...the ElementId" |
| EX-05 | Word boundary respect | content="configuration settings here", query="config" | Doesn't cut "configuration" |
| EX-06 | Very long content | 500 char content, query in middle | ~80 char excerpt with ellipsis both sides |
| EX-07 | Query not found | content="no match here", query="xyz" | null |
| EX-08 | Special characters | content="C# and .NET", query="c#" | Works without regex error |
| EX-09 | Both fields empty | excerpt="", contentPreview="" | null |

#### Test Group 3: Visual Indicators

| Test ID | Description | Steps | Expected Result |
|---------|-------------|-------|-----------------|
| VI-01 | Content match styling | Search content-only term | Blue tint + 📄 icon visible |
| VI-02 | Title match styling | Search title term | Yellow highlight, no 📄 icon |
| VI-03 | Both match styling | Search term in both | Yellow highlight only (title precedence) |
| VI-04 | Clear removes styling | Clear search | No tint, no icons, no excerpts |
| VI-05 | Mobile responsive | Test on 375px viewport | All indicators visible and usable |

#### Test Group 4: Excerpt Interactions

| Test ID | Description | Steps | Expected Result |
|---------|-------------|-------|-----------------|
| EI-01 | Toggle expand | Click ▼ button | Excerpt expands, button shows ▲ |
| EI-02 | Toggle collapse | Click ▲ button | Excerpt collapses, button shows ▼ |
| EI-03 | Expand all | Click "Show excerpts" | All excerpts expand |
| EI-04 | Collapse all | Click "Hide excerpts" | All excerpts collapse |
| EI-05 | Hover expand (desktop) | Hover post link 300ms+ | Excerpt expands |
| EI-06 | Keyboard toggle | Focus link, press Space | Excerpt toggles |
| EI-07 | Persistence | Expand all, new search | New excerpts follow preference |
| EI-08 | **Toggle OFF cleanup** | Content search active → toggle OFF | All excerpts removed, blue tints cleared |

#### Test Group 5: Result Count

| Test ID | Description | Input | Expected Display |
|---------|-------------|-------|------------------|
| RC-01 | Title only results | 5 title matches | "5 results" |
| RC-02 | Mixed results | 3 title + 2 content | "5 results (3 in title, 2 in content)" |
| RC-03 | Content only results | 0 title + 4 content | "4 results (0 in title, 4 in content)" |
| RC-04 | No results | 0 matches | "No posts found" |
| RC-05 | Single result | 1 title match | "1 result" |

#### Test Group 6: Accessibility

| Test ID | Description | Tool/Method | Expected Result |
|---------|-------------|-------------|-----------------|
| AC-01 | Screen reader | NVDA/VoiceOver | Excerpt announced when expanded |
| AC-02 | Keyboard only | Tab navigation | All interactive elements focusable |
| AC-03 | Reduced motion | System setting | No animations |
| AC-04 | High contrast | Windows HC mode | All text readable |
| AC-05 | ARIA attributes | Browser inspector | Correct aria-expanded, aria-label |

#### Test Group 7: Edge Cases

| Test ID | Description | Steps | Expected Result |
|---------|-------------|-------|-----------------|
| EC-01 | Empty contentPreview | Search with post lacking content | Post still matches title |
| EC-02 | Very short query | Search "a" | Works without error |
| EC-03 | Very long query | Paste 200+ char query | Truncated, no crash |
| EC-04 | Unicode in content | Search term with emoji | Correct match and display |
| EC-05 | HTML in content | Content has &amp; etc | Displays decoded correctly |
| EC-06 | Rapid search | Type quickly | No race conditions, correct final state |
| EC-07 | Scroll position | Expand excerpt while scrolled | No layout jump |

### Integration Tests

| Test ID | Description | Steps | Expected Result |
|---------|-------------|-------|-----------------|
| IT-01 | Full search flow | Enable content search → search → expand → click | Navigate to post with ?highlight= |
| IT-02 | Toggle content search OFF | Search with content matches → toggle OFF | All excerpts and blue tints removed immediately |
| IT-03 | Toggle content search ON | Title-only search → toggle ON | Content matches appear with excerpts |
| IT-04 | Combine with year filter | Filter 2023 → content search | Only 2023 posts with content match |
| IT-05 | Mobile sidebar | Open sidebar on mobile → content search | All features work in mobile view |
| IT-06 | Page reload | Search → reload page → return | Search state may reset (expected) |

### Performance Tests

| Test ID | Description | Threshold | Method |
|---------|-------------|-----------|--------|
| PT-01 | Search 1000+ posts | < 500ms | Performance.now() timing |
| PT-02 | Generate 50 excerpts | < 200ms | Measure DOM insertion time |
| PT-03 | Memory usage | < 10MB increase | Chrome DevTools Memory |
| PT-04 | Expand all (50 excerpts) | < 100ms | Animation frame timing |

---

## Rollback Plan

If issues are discovered after deployment:

### Quick Rollback (Disable Feature)

Add to CONFIG:
```javascript
enableContentMatchIndicator: false
```

In `performContentSearch()`:
```javascript
if (!CONFIG.enableContentMatchIndicator) {
  // Skip all content match indicator logic
  return;
}
```

### Full Rollback (Revert Code)

```bash
git checkout gh-pages
git revert <commit-hash>
git push origin gh-pages
```

### CSS-Only Rollback (Hide Visuals)

```css
.tbc-content-match { background: none !important; border: none !important; }
.tbc-content-match::after { display: none !important; }
.tbc-excerpt { display: none !important; }
.tbc-excerpt-controls { display: none !important; }
```

---

## Deployment Checklist

- [ ] All Phase 1-4 code implemented
- [ ] All tests in test plan pass
- [ ] Code reviewed
- [ ] Tested on Chrome, Firefox, Safari, Edge
- [ ] Tested on iOS Safari and Android Chrome
- [ ] Tested with screen reader
- [ ] Performance benchmarks met
- [ ] Documentation updated
- [ ] Commit message follows convention
- [ ] PR created for review

---

## Related Documents

- [CONTENT_MATCH_INDICATOR_SPEC.md](CONTENT_MATCH_INDICATOR_SPEC.md) - Feature specification
- [SEARCH_INDEX_IMPLEMENTATION_SPEC.md](SEARCH_INDEX_IMPLEMENTATION_SPEC.md) - Search index details
- [WIKI_TOC_SIDEBAR_REQUIREMENTS.md](WIKI_TOC_SIDEBAR_REQUIREMENTS.md) - Sidebar requirements
