# Content Match Indicator Specification

**Version**: 1.0 (January 11, 2026)

## Overview

This document specifies the implementation of visual indicators for content-matched search results in The Building Coder sidebar. When a search term matches in post **content** but not in the **title**, users currently see no highlighting, making it unclear why a post appears in results. This feature adds clear visual distinction and context for content-only matches.

---

## Problem Statement

### Current Behavior
1. User enables "Search in content" toggle
2. User searches for a term (e.g., "ElementId")
3. Posts matching in title get yellow highlight on the matching text
4. Posts matching in content only appear in results but have **no highlighting**
5. User cannot distinguish title matches from content matches
6. User has no context for why a content-only match appeared

### User Impact
- Confusion about why certain posts appear in results
- No way to preview the matched content before clicking
- Wasted clicks on posts where content match isn't relevant to user's actual need

---

## Solution Design

### Approach
Implement a two-tier visual system:
1. **Title matches**: Yellow highlight on matching text (current behavior)
2. **Content-only matches**: Distinct background color + expandable excerpt showing matched context

### Visual Mockup

```
┌─────────────────────────────────────────┐
│ 🔍 [Search: ElementId          ] [×]    │
│ ☑ Search in content                     │
│ 42 results (title + content)            │
├─────────────────────────────────────────┤
│ 📁 Revit API Fundamentals (8)      [−]  │
│   ├─ Working with [ElementId] Types     │  ← Title match (yellow)
│   │                                     │
│   ├─ Document Class Overview       📄   │  ← Content match (blue tint)
│   │   └─ "...retrieve the ElementId..." │  ← Expandable excerpt
│   │                                     │
│   ├─ Introduction to [ElementId]s       │  ← Title match (yellow)
│   │                                     │
│   └─ Parameter Access Methods      📄   │  ← Content match (blue tint)
│       └─ "...using the ElementId to..." │  ← Expandable excerpt
└─────────────────────────────────────────┘

Legend:
  [ElementId]  = Yellow highlight (title match)
  📄           = Content match indicator icon
  Blue tint    = Content-only match background
  Italic text  = Expandable excerpt (collapsed by default)
```

---

## Detailed Specification

### 1. Match Type Detection

During `performContentSearch()`, determine match type for each post:

```javascript
// Match types
const MATCH_TYPE = {
  NONE: 0,
  TITLE_ONLY: 1,
  CONTENT_ONLY: 2,
  BOTH: 3
};

function getMatchType(post, query) {
  const titleMatch = post.title.toLowerCase().includes(query);
  const contentMatch = post.contentPreview && post.contentPreview.includes(query);
  
  if (titleMatch && contentMatch) return MATCH_TYPE.BOTH;
  if (titleMatch) return MATCH_TYPE.TITLE_ONLY;
  if (contentMatch) return MATCH_TYPE.CONTENT_ONLY;
  return MATCH_TYPE.NONE;
}
```

### 2. Visual Indicators

#### 2.1 Title Match (Existing Behavior)
- Yellow background on matching text: `#fff3cd`
- Applied via `<span class="tbc-search-highlight">term</span>`

#### 2.2 Content-Only Match (New)

**Background Color**:
```css
.tbc-post-link.tbc-content-match {
  background: linear-gradient(90deg, 
    rgba(66, 153, 225, 0.08) 0%, 
    rgba(66, 153, 225, 0.03) 100%);
  border-left: 2px solid var(--tbc-accent-primary, #0066cc);
  padding-left: 8px;
  margin-left: -10px;
}
```

**Indicator Icon**:
```css
.tbc-post-link.tbc-content-match::after {
  content: '📄';
  font-size: 10px;
  margin-left: 6px;
  opacity: 0.7;
  vertical-align: middle;
}
```

**Alternative Icon Options**:
- `📄` - Document (shows content was searched)
- `...` - Ellipsis (indicates more context available)
- `◉` - Filled circle (subtle indicator)
- Custom SVG icon

### 3. Expandable Excerpt

#### 3.1 Excerpt Generation

Extract a snippet around the matched term from `contentPreview`:

```javascript
function generateExcerpt(contentPreview, query, maxLength = 60) {
  const lowerContent = contentPreview.toLowerCase();
  const index = lowerContent.indexOf(query);
  
  if (index === -1) return null;
  
  // Calculate window around match
  const halfWindow = Math.floor((maxLength - query.length) / 2);
  let start = Math.max(0, index - halfWindow);
  let end = Math.min(contentPreview.length, index + query.length + halfWindow);
  
  // Adjust to word boundaries
  if (start > 0) {
    const spaceIndex = contentPreview.indexOf(' ', start);
    if (spaceIndex !== -1 && spaceIndex < index) {
      start = spaceIndex + 1;
    }
  }
  if (end < contentPreview.length) {
    const spaceIndex = contentPreview.lastIndexOf(' ', end);
    if (spaceIndex > index + query.length) {
      end = spaceIndex;
    }
  }
  
  let excerpt = contentPreview.substring(start, end);
  
  // Add ellipsis
  if (start > 0) excerpt = '...' + excerpt;
  if (end < contentPreview.length) excerpt = excerpt + '...';
  
  return excerpt;
}
```

#### 3.2 Excerpt HTML Structure

```html
<a href="0123_post.htm?highlight=term" class="tbc-post-link tbc-content-match">
  Document Class Overview
</a>
<div class="tbc-excerpt collapsed">
  <span class="tbc-excerpt-text">
    "...retrieve the <mark>ElementId</mark> from the..."
  </span>
  <button class="tbc-excerpt-toggle" aria-label="Toggle excerpt">▼</button>
</div>
```

#### 3.3 Excerpt Visibility States

**Collapsed (Default)**:
```css
.tbc-excerpt.collapsed {
  max-height: 0;
  overflow: hidden;
  opacity: 0;
  transition: all 0.2s ease;
}
```

**Expanded**:
```css
.tbc-excerpt.expanded {
  max-height: 50px;
  opacity: 1;
  padding: 4px 0 4px 12px;
  margin-top: 2px;
}
```

#### 3.4 Excerpt Styling

```css
.tbc-excerpt-text {
  font-size: 11px;
  color: var(--tbc-sidebar-text-muted, #666);
  font-style: italic;
  line-height: 1.4;
  display: block;
  padding-left: 8px;
  border-left: 1px solid var(--tbc-sidebar-border, #ddd);
}

.tbc-excerpt-text mark {
  background: #fff3cd;
  padding: 0 2px;
  border-radius: 2px;
}

.tbc-excerpt-toggle {
  background: none;
  border: none;
  color: var(--tbc-sidebar-text-muted);
  cursor: pointer;
  font-size: 10px;
  padding: 2px;
  transition: transform 0.2s ease;
}

.tbc-excerpt.expanded .tbc-excerpt-toggle {
  transform: rotate(180deg);
}
```

### 4. Interaction Behavior

#### 4.1 Expand/Collapse Trigger

**Option A: Click on indicator icon**
```javascript
excerptToggle.addEventListener('click', (e) => {
  e.preventDefault();
  e.stopPropagation();
  excerptDiv.classList.toggle('expanded');
  excerptDiv.classList.toggle('collapsed');
});
```

**Option B: Auto-expand on hover (desktop only)**
```javascript
if (!isMobile()) {
  postLink.addEventListener('mouseenter', () => {
    excerptDiv.classList.remove('collapsed');
    excerptDiv.classList.add('expanded');
  });
  
  postLink.addEventListener('mouseleave', () => {
    excerptDiv.classList.remove('expanded');
    excerptDiv.classList.add('collapsed');
  });
}
```

**Recommended**: Option A for explicit control, with Option B as enhancement.

#### 4.2 Expand All / Collapse All

Add buttons to the search results area:

```html
<div id="tbc-search-results">
  <span class="tbc-result-count">42 results (title + content)</span>
  <button id="tbc-expand-excerpts" class="tbc-excerpt-control" title="Expand all excerpts">▼</button>
  <button id="tbc-collapse-excerpts" class="tbc-excerpt-control" title="Collapse all excerpts">▲</button>
</div>
```

### 5. Search Result Count Enhancement

Update result count to show breakdown:

```javascript
function updateResultsCount(titleMatches, contentMatches, resultsDiv) {
  if (resultsDiv) {
    const total = titleMatches + contentMatches;
    if (total === 0) {
      resultsDiv.textContent = 'No posts found';
      resultsDiv.classList.add('no-results');
    } else if (contentMatches === 0) {
      resultsDiv.textContent = `${total} result${total === 1 ? '' : 's'}`;
      resultsDiv.classList.remove('no-results');
    } else {
      resultsDiv.innerHTML = `${total} result${total === 1 ? '' : 's'} ` +
        `<span class="tbc-result-breakdown">(${titleMatches} title, ${contentMatches} content)</span>`;
      resultsDiv.classList.remove('no-results');
    }
  }
}
```

```css
.tbc-result-breakdown {
  font-size: 10px;
  color: var(--tbc-sidebar-text-muted);
}
```

---

## Implementation Plan

### Phase 1: Core Indicators (2-3 hours)
1. Add `MATCH_TYPE` detection in `performContentSearch()`
2. Add `.tbc-content-match` class to content-only matched posts
3. Add CSS for content match background and indicator icon
4. Update result count to show breakdown

### Phase 2: Excerpt Display (3-4 hours)
1. Implement `generateExcerpt()` function
2. Create excerpt DOM elements dynamically
3. Add CSS for excerpt styling (collapsed/expanded states)
4. Add click handler for expand/collapse toggle

### Phase 3: Enhanced Interactions (2-3 hours)
1. Add hover-to-expand behavior (desktop only)
2. Add "Expand All" / "Collapse All" buttons
3. Add keyboard navigation for excerpts
4. Persist expand/collapse preference

### Phase 4: Polish & Accessibility (1-2 hours)
1. Add ARIA attributes for screen readers
2. Add reduced motion support
3. Test on mobile viewports
4. Add dark mode support

**Total Estimated Time**: 8-12 hours

---

## CSS Variables

Add to `:root` or sidebar variables:

```css
:root {
  /* Content match colors */
  --tbc-content-match-bg: rgba(66, 153, 225, 0.08);
  --tbc-content-match-border: var(--tbc-accent-primary, #0066cc);
  --tbc-excerpt-text: var(--tbc-sidebar-text-muted, #666);
  --tbc-excerpt-border: var(--tbc-sidebar-border, #ddd);
  --tbc-excerpt-highlight: #fff3cd;
}
```

---

## Data Requirements

### Search Index Changes
No changes required. The existing `contentPreview` field (800 characters) provides sufficient context for excerpt generation.

### Storage Keys
Add preference for excerpt visibility:
```javascript
storageKeys: {
  // ... existing keys
  excerptExpanded: 'tbc-sidebar-excerpt-expanded' // 'all' | 'none' | 'hover'
}
```

---

## Accessibility Considerations

### Screen Reader Support
```html
<a href="..." class="tbc-post-link tbc-content-match" 
   aria-describedby="excerpt-123">
  Document Class Overview
</a>
<div id="excerpt-123" class="tbc-excerpt" role="note" aria-label="Content excerpt">
  ...
</div>
```

### Keyboard Navigation
- `Tab` to move between posts
- `Enter` on post link navigates to post
- `Space` on post link toggles excerpt (when focused)

### Reduced Motion
```css
@media (prefers-reduced-motion: reduce) {
  .tbc-excerpt {
    transition: none;
  }
}
```

---

## Testing Checklist

### Functional Tests
- [ ] Title-only matches show yellow highlight
- [ ] Content-only matches show blue tint + icon
- [ ] Both-type matches show yellow highlight (title takes precedence)
- [ ] Excerpt shows correct context around matched term
- [ ] Excerpt highlights the matched term within excerpt
- [ ] Click toggle expands/collapses excerpt
- [ ] "Expand All" / "Collapse All" work correctly
- [ ] Result count shows correct breakdown
- [ ] Navigating to post preserves `?highlight=` parameter

### Visual Tests
- [ ] Content match indicator visible on all viewport sizes
- [ ] Excerpt readable on mobile (font size, padding)
- [ ] Dark mode styling (if applicable)
- [ ] No layout shift when expanding excerpts

### Edge Cases
- [ ] Very long excerpts truncate properly
- [ ] Special characters in search term handled
- [ ] Unicode/emoji in content preview
- [ ] Multiple matches in same content preview
- [ ] Empty contentPreview field handled gracefully

---

## Future Enhancements

1. **Relevance scoring**: Sort content matches by number of occurrences
2. **Multiple excerpts**: Show multiple snippets if term appears several times
3. **Fuzzy matching indicator**: Different styling for approximate matches
4. **Search history**: Recently searched terms with match counts
5. **Export results**: Copy list of matching posts to clipboard

---

## Related Documents

- [SEARCH_INDEX_IMPLEMENTATION_SPEC.md](SEARCH_INDEX_IMPLEMENTATION_SPEC.md) - Search index generation and content search
- [WIKI_TOC_SIDEBAR_REQUIREMENTS.md](WIKI_TOC_SIDEBAR_REQUIREMENTS.md) - Original sidebar requirements
