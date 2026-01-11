# Wiki-Style Table of Contents Sidebar - Requirements Document

## Overview

This document outlines the requirements and implementation approach for adding a **wiki-style table of contents (TOC) sidebar** to every page in The Building Coder archive. The sidebar will appear on the left side of each page, providing consistent navigation across all 2,000+ blog posts.

> **📌 Key Design Decision: TOPIC-BASED ORDERING**
> 
> The sidebar TOC uses **topic-based grouping** (by subject matter), NOT chronological ordering (by year/date). This matches the homepage's section #5 which organizes posts into 58 topic groups (55 main topics + 3 subtopics) like "Custom Exporter", "FilteredElementCollector", "Family API", etc.

> **✅ GitHub Pages + JavaScript: CONFIRMED WORKING**
> 
> JavaScript-injected content works perfectly on GitHub Pages. GitHub Pages serves static files and executes client-side JavaScript normally. Many GitHub Pages sites use JavaScript for navigation, search, and dynamic content.

---

# 📋 IMPLEMENTATION PLAN - FINAL REVIEW

## Executive Summary

This plan implements a **wiki-style Table of Contents (TOC) sidebar** for The Building Coder archive with the following confirmed features:

| Feature | Status | Priority |
|---------|--------|----------|
| Topic-based TOC (58 topics) | ✅ Selected | Must Have |
| JavaScript-loaded sidebar | ✅ Selected | Must Have |
| Real-time search | ✅ Selected | Must Have |
| Drag-to-resize sidebar | ✅ Selected | Must Have |
| Collapsible topic groups | ✅ Selected | Must Have |
| Current page highlighting | ✅ Selected | Must Have |
| Mobile hamburger menu | ✅ Selected | Must Have |
| Keyboard shortcuts | ✅ Selected | Nice to Have |
| Offline caching | ✅ Selected | Should Have |

---

## Selected Feature Configuration

### 1. TOC Organization: TOPIC-BASED

**NOT chronological (by year)**. Posts grouped into 58 topic categories matching homepage section #5:

```
TOPICS
├── 5.1  Custom Exporter (12 posts)
├── 5.2  2D Booleans and Adjacent Areas (11 posts)
├── 5.3  PostCommand (12 posts)
├── 5.4  Dockable Panels (8 posts)
├── 5.5  Material Management (15+ posts)
├── 5.6  Phase (4 posts)
├── 5.7  Preview Control (7 posts)
├── 5.8  RstLink (5 posts)
├── 5.9  FilteredElementCollector (10+ posts)
├── 5.10 Filter for Family Symbols
├── ...
├── 5.25 Family API
│   ├── 5.25.1 Creating Family Definitions
│   ├── 5.25.2 Family Instance Placement
│   └── 5.25.3 Loading a Family
├── ...
└── 5.56 Element Identifiers in RVT, IFC, NW
```

### 2. Sidebar Layout

```
┌────────────────────────────────┬─┬───────────────────────────────────┐
│  🏠 The Building Coder         │▐│                                   │
├────────────────────────────────┤▐│                                   │
│ 🔍 Search posts...          ✕  │▐│                                   │
│ 23 results                     │▐│      MAIN BLOG POST CONTENT       │
├────────────────────────────────┤▐│                                   │
│  Home                          │▐│                                   │
│  About                         │▐│                                   │
│  ─────────────────────────     │▐│                                   │
│  TOPICS                        │▐│                                   │
│  ▼ 5.1 Custom Exporter         │▐│                                   │
│    • Custom Exporter Intro     │▐│                                   │
│    • Export to STL Format   ◀──│▐│ ← Current page highlighted        │
│    • Geometry Export Options   │▐│                                   │
│  ▶ 5.2 2D Booleans             │▐│                                   │
│  ▶ 5.3 PostCommand             │▐│                                   │
│  ▶ 5.4 Dockable Panels         │▐│                                   │
│  ▶ 5.5 Materials               │▐│                                   │
│  ...                           │▐│                                   │
│  ▶ 5.56 Element Identifiers    │▐│                                   │
└────────────────────────────────┴─┴───────────────────────────────────┘
                                  ↑
                           Drag to resize
                           (180px - 500px)
```

### 3. Dimensions & Constraints

| Property | Value |
|----------|-------|
| Default width | 280px |
| Minimum width | 180px |
| Maximum width | 500px |
| Resize handle | 5px wide, right edge |
| Header height | 50px (sticky) |
| Search box height | 45px (sticky) |

### 4. Color Scheme

| Element | Color | Hex |
|---------|-------|-----|
| Sidebar background | Dark blue | `#1a365d` |
| Sidebar text | Light gray | `#e2e8f0` |
| Hover state | Medium blue | `#2c5282` |
| Active/current | Bright blue | `#4299e1` |
| Search highlight | Yellow | `#fff59d` |
| Resize handle hover | Transparent black | `rgba(0,0,0,0.1)` |

### 5. Responsive Breakpoints

| Screen Width | Behavior |
|--------------|----------|
| > 1024px | Fixed sidebar, always visible, resizable |
| 768-1024px | Collapsible sidebar, toggle button, resizable |
| < 768px | Hidden by default, hamburger menu, full-width overlay |

---

## File Structure

```
tbc/
├── index.html                      # Landing page (no sidebar)
├── WIKI_TOC_SIDEBAR_REQUIREMENTS.md # This document
│
├── a/                              # Blog posts directory
│   ├── index.html                  # Blog index (with sidebar)
│   ├── 0001_intro.html             # Individual posts (with sidebar)
│   ├── 0002_*.html                 # ...
│   ├── ...
│   ├── 2078_*.html                 # Latest post
│   │
│   └── toc/                        # NEW: Sidebar assets
│       ├── toc-data.json           # Topic-based TOC data
│       ├── toc-sidebar.js          # Sidebar JavaScript (~15KB)
│       ├── toc-sidebar.css         # Sidebar styles (~8KB)
│       └── toc-sidebar.min.js      # Minified version
│
└── scripts/                        # Build scripts
    ├── extract_topic_toc.py        # Extract topics from index.html
    ├── add_sidebar_to_pages.py     # Inject sidebar into all pages
    └── validate_sidebar.py         # Verify injection worked
```

---

## Implementation Phases

### Phase 1: Extract Topic-Based TOC Data (2-3 hours)

**Goal**: Parse `a/index.html` and create `toc-data.json`

| Task | Description | Output |
|------|-------------|--------|
| 1.1 | Analyze `a/index.html` section #5 structure | Understanding of HTML patterns |
| 1.2 | Create `extract_topic_toc.py` script | Python script |
| 1.3 | Parse all 58 topic groups (5.1 - 5.56, excluding redirects) | Topic list with IDs/titles |
| 1.4 | Extract post links within each topic | Post titles + file paths |
| 1.5 | Handle sub-topics (5.25.1, 5.25.2, 5.25.3) | Nested structure |
| 1.6 | Generate `toc-data.json` | ~150KB JSON file |
| 1.7 | Validate JSON structure | No errors |

**Output JSON Structure:**
```json
{
  "version": "1.0",
  "lastUpdated": "2026-01-02",
  "totalTopics": 58,
  "totalPosts": 2078,
  "navigation": [
    {"label": "Home", "href": "index.html"},
    {"label": "About", "href": "index.html#0"},
    {"label": "Contact", "href": "index.html#1"},
    {"label": "Getting Started", "href": "index.html#2"}
  ],
  "topics": [
    {
      "id": "5.1",
      "title": "Custom Exporter",
      "anchor": "5.1",
      "posts": [
        {"title": "Custom Exporter Introduction", "file": "1234_custom_exporter.html"},
        {"title": "Exporting Geometry to STL", "file": "1456_stl_export.html"}
      ]
    },
    {
      "id": "5.25",
      "title": "Family API, Loading and Placing Instances",
      "anchor": "5.25",
      "subTopics": [
        {
          "id": "5.25.1",
          "title": "Family API for Creating Definitions",
          "posts": [...]
        }
      ]
    }
  ]
}
```

---

### Phase 2: Create Sidebar CSS (2-3 hours)

**Goal**: Create `a/toc/toc-sidebar.css` with all styles

| Task | Description |
|------|-------------|
| 2.1 | Base sidebar styles (fixed positioning, scrolling) |
| 2.2 | Header with logo/title |
| 2.3 | Search box (sticky, input, clear button, result count) |
| 2.4 | Navigation links (Home, About, etc.) |
| 2.5 | Topic group styles (collapsible, expand/collapse icons) |
| 2.6 | Sub-topic indentation |
| 2.7 | Post link styles |
| 2.8 | Current page highlight |
| 2.9 | Hover and focus states |
| 2.10 | Resize handle styles |
| 2.11 | Mobile hamburger menu |
| 2.12 | Responsive breakpoints |
| 2.13 | Print styles (hide sidebar) |
| 2.14 | Accessibility (focus indicators, high contrast) |

**Key CSS Variables:**
```css
:root {
  --tbc-sidebar-width: 280px;
  --tbc-sidebar-min-width: 180px;
  --tbc-sidebar-max-width: 500px;
  --tbc-sidebar-bg: #1a365d;
  --tbc-sidebar-text: #e2e8f0;
  --tbc-sidebar-hover: #2c5282;
  --tbc-sidebar-active: #4299e1;
  --tbc-search-highlight: #fff59d;
}
```

---

### Phase 3: Create Sidebar JavaScript (4-6 hours)

**Goal**: Create `a/toc/toc-sidebar.js` with all functionality

| Task | Description | Priority |
|------|-------------|----------|
| 3.1 | Fetch and parse `toc-data.json` | Must Have |
| 3.2 | Generate sidebar HTML structure | Must Have |
| 3.3 | Inject sidebar into DOM | Must Have |
| 3.4 | Render topic groups (collapsed by default) | Must Have |
| 3.5 | Toggle expand/collapse on topic click | Must Have |
| 3.6 | Detect current page from URL | Must Have |
| 3.7 | Highlight current page and expand its topic | Must Have |
| 3.8 | Search box functionality | Must Have |
| 3.9 | Real-time filtering (debounced) | Must Have |
| 3.10 | Highlight matching text | Should Have |
| 3.11 | Clear search button | Must Have |
| 3.12 | Result count display | Should Have |
| 3.13 | Drag-to-resize functionality | Must Have |
| 3.14 | Save sidebar width to localStorage | Should Have |
| 3.15 | Restore sidebar width on load | Should Have |
| 3.16 | Double-click to reset width | Nice to Have |
| 3.17 | Touch support for resize | Nice to Have |
| 3.18 | Mobile hamburger toggle | Must Have |
| 3.19 | Keyboard shortcut: `/` to focus search | Nice to Have |
| 3.20 | Keyboard shortcut: `Escape` to clear | Nice to Have |
| 3.21 | Save expanded topics to localStorage | Should Have |
| 3.22 | Save scroll position to localStorage | Should Have |
| 3.23 | Cache `toc-data.json` in localStorage | Should Have |

**JavaScript Module Structure:**
```javascript
// toc-sidebar.js
(function() {
  'use strict';
  
  const TBCSidebar = {
    config: { /* settings */ },
    state: { /* runtime state */ },
    
    init: function() { /* entry point */ },
    
    // Data loading
    loadTocData: function() { /* fetch JSON */ },
    cacheTocData: function() { /* localStorage */ },
    
    // Rendering
    renderSidebar: function() { /* build HTML */ },
    renderTopics: function() { /* topic groups */ },
    renderPosts: function() { /* post links */ },
    
    // Interactions
    initCollapsible: function() { /* expand/collapse */ },
    initSearch: function() { /* search box */ },
    initResize: function() { /* drag handle */ },
    initMobile: function() { /* hamburger */ },
    
    // Current page
    detectCurrentPage: function() { /* URL matching */ },
    highlightCurrentPage: function() { /* add class */ },
    
    // Persistence
    saveState: function() { /* localStorage */ },
    restoreState: function() { /* localStorage */ }
  };
  
  // Auto-init on DOM ready
  document.addEventListener('DOMContentLoaded', () => TBCSidebar.init());
})();
```

---

### Phase 4: Inject Sidebar into All HTML Pages (2-3 hours)

**Goal**: Modify all 2,078 HTML files to include sidebar

| Task | Description |
|------|-------------|
| 4.1 | Create `add_sidebar_to_pages.py` script |
| 4.2 | Add `<link>` to CSS in `<head>` |
| 4.3 | Add `<div id="tbc-sidebar"></div>` after `<body>` |
| 4.4 | Wrap existing content in `<div id="tbc-content">` |
| 4.5 | Add `<script src="toc/toc-sidebar.js">` before `</body>` |
| 4.6 | Handle edge cases (malformed HTML, missing tags) |
| 4.7 | Run script on all files in `a/` directory |
| 4.8 | Skip `a/toc/` directory |
| 4.9 | Create backup before modification |
| 4.10 | Log modifications for verification |

**Before (existing page):**
```html
<html>
<head>
  <title>Post Title</title>
  <style>/* existing styles */</style>
</head>
<body>
  <!-- Original blog content -->
</body>
</html>
```

**After (modified page):**
```html
<!DOCTYPE html>
<html>
<head>
  <title>Post Title</title>
  <link rel="stylesheet" href="toc/toc-sidebar.css">
  <style>/* existing styles */</style>
</head>
<body>
  <div id="tbc-sidebar"></div>
  <div id="tbc-content">
    <!-- Original blog content -->
  </div>
  <script src="toc/toc-sidebar.js"></script>
</body>
</html>
```

---

### Phase 5: Testing (2-3 hours)

**Goal**: Verify all functionality works correctly

| Test Category | Test Cases |
|---------------|------------|
| **Sidebar Display** | Appears on all 2,078 pages |
| | Correct width (280px default) |
| | Scrollable when content overflows |
| **Topic Groups** | All 58 topics displayed |
| | Expand/collapse works |
| | Sub-topics display correctly |
| | Post counts accurate |
| **Search** | Real-time filtering works |
| | Matches post titles |
| | Matches topic names |
| | Highlight visible |
| | Clear button works |
| | Result count accurate |
| | "No results" message shows |
| **Resize** | Drag handle visible on hover |
| | Can drag to resize |
| | Respects min/max constraints |
| | Width persists across pages |
| | Double-click resets |
| **Current Page** | Correct page highlighted |
| | Parent topic auto-expanded |
| | Scrolls to current page |
| **Mobile** | Hamburger menu appears < 768px |
| | Sidebar slides in/out |
| | Content not visible behind |
| | Touch resize works |
| **Keyboard** | `/` focuses search |
| | `Escape` clears search |
| | Tab navigation works |
| **Cross-Browser** | Chrome ✓ |
| | Firefox ✓ |
| | Safari ✓ |
| | Edge ✓ |
| | Mobile Safari ✓ |
| | Mobile Chrome ✓ |
| **Performance** | Load time < 2 seconds |
| | Smooth scrolling |
| | No jank on expand/collapse |
| | No jank on search |
| **Accessibility** | Screen reader compatible |
| | ARIA labels present |
| | Focus indicators visible |
| | Color contrast passes |

---

### Phase 6: Deployment (1 hour)

| Task | Description |
|------|-------------|
| 6.1 | Commit all changes |
| 6.2 | Push to `parametrix/thebuildingcoder-archive` |
| 6.3 | Verify GitHub Pages build succeeds |
| 6.4 | Test live site: `https://parametrix.github.io/thebuildingcoder-archive/` |
| 6.5 | Spot-check 10 random pages |
| 6.6 | Test on mobile device |
| 6.7 | Create release tag (e.g., `v2.0-sidebar`) |

---

## Estimated Timeline

| Phase | Duration | Cumulative |
|-------|----------|------------|
| Phase 1: Extract TOC Data | 2-3 hours | 2-3 hours |
| Phase 2: Create CSS | 2-3 hours | 4-6 hours |
| Phase 3: Create JavaScript | 4-6 hours | 8-12 hours |
| Phase 4: Inject into Pages | 2-3 hours | 10-15 hours |
| Phase 5: Testing | 2-3 hours | 12-18 hours |
| Phase 6: Deployment | 1 hour | **13-19 hours** |

**Total Estimated Time: 13-19 hours**

---

## Risk Assessment

| Risk | Likelihood | Impact | Mitigation |
|------|------------|--------|------------|
| HTML parsing errors | Medium | High | Create backup, test on sample first |
| Performance issues with 2000+ posts | Low | Medium | Lazy loading, virtual scrolling |
| Browser compatibility issues | Low | Medium | Test early on all browsers |
| Mobile layout breaks | Medium | Medium | Test responsiveness early |
| localStorage quota exceeded | Low | Low | Limit cached data, handle gracefully |
| TOC data extraction misses posts | Medium | High | Validate counts, manual review |

---

## Success Criteria

The implementation is complete when:

- [ ] All 58 topic groups appear in sidebar
- [ ] All posts accessible via topic navigation
- [ ] Search finds posts by title and topic
- [ ] Sidebar resizable via drag
- [ ] Current page highlighted and visible
- [ ] Mobile hamburger menu functional
- [ ] Width preference persists across sessions
- [ ] Works offline after first load
- [ ] No console errors
- [ ] Loads in < 2 seconds
- [ ] Accessible via keyboard
- [ ] Works on Chrome, Firefox, Safari, Edge

---

## Approval Checklist

Before implementation begins, please confirm:

- [ ] **Topic-based organization** (not chronological) is correct
- [ ] **58 topics** from homepage section #5 is the source
- [ ] **Search is Must Have** (not Nice to Have)
- [ ] **Drag-to-resize** is required
- [ ] **Color scheme** (dark blue sidebar) is acceptable
- [ ] **Width constraints** (180-500px) are acceptable
- [ ] **Mobile breakpoint** (768px) is acceptable
- [ ] **Estimated timeline** (13-19 hours) is acceptable

---

## Table of Contents

1. [Objectives](#objectives)
2. [Current State Analysis](#current-state-analysis)
3. [Technical Constraints](#technical-constraints)
4. [Implementation Approaches](#implementation-approaches)
5. [Recommended Approach](#recommended-approach)
6. [Detailed Technical Specification](#detailed-technical-specification)
7. [TOC Structure Requirements](#toc-structure-requirements)
8. [User Experience Requirements](#user-experience-requirements)
9. [Mobile Responsiveness](#mobile-responsiveness)
10. [Performance Considerations](#performance-considerations)
11. [Implementation Steps](#implementation-steps)
12. [File Modifications](#file-modifications)
13. [Testing Requirements](#testing-requirements)

---

## Objectives

1. **Add a persistent left sidebar** to every HTML page containing a navigable TOC
2. **Match the TOC order** from the homepage (`a/index.html`)
3. **Provide quick navigation** to any post by number, date, or title
4. **Work offline** without requiring server-side processing
5. **Be compatible with GitHub Pages** static hosting
6. **Highlight the current page** in the TOC when viewing a post

---

## Current State Analysis

### Existing Structure
- **Homepage**: `a/index.html` (3,890 lines) contains the master TOC as an HTML table
- **Blog Posts**: ~2,078 HTML files in the `a/` directory (e.g., `0001_intro.html` through `2078_*.html`)
- **File Format**: Plain HTML files, not wrapped in a common template
- **No Template System**: Each file is standalone with its own `<html>`, `<head>`, `<body>` tags

### TOC Data in Homepage
The TOC is structured as an HTML table with columns:
- Post number (1-2078)
- Date (YYYY-MM-DD)
- Title with link to HTML file
- Tags/Topics

---

## Technical Constraints

### GitHub Pages Limitations
1. **Static files only** - No server-side processing (PHP, Python, Node.js)
2. **No Jekyll processing** - `.nojekyll` file disables Jekyll
3. **Client-side JavaScript** - Only option for dynamic behavior
4. **No database** - All data must be in static files (HTML, JSON, JS)

### Browser Constraints
1. Must work offline after initial load
2. Should work without JavaScript (graceful degradation)
3. Cross-browser compatibility (Chrome, Firefox, Safari, Edge)

---

## Implementation Approaches

### Approach A: Inject HTML/CSS into Every Page (Static)
**Description**: Modify all 2,078 HTML files to include the sidebar HTML and CSS directly.

| Pros | Cons |
|------|------|
| Works offline immediately | Large file size increase (~50KB per file for full TOC) |
| No JavaScript required | Maintenance nightmare (update all files for changes) |
| Fast initial load | Total repo size increase: ~100MB+ |
| No external dependencies | Repetitive content |

### Approach B: JavaScript-Loaded Sidebar (Recommended)
**Description**: Add a small JavaScript snippet to each page that loads the TOC from a shared JSON file.

| Pros | Cons |
|------|------|
| Single source of truth (JSON file) | Requires JavaScript |
| Easy to update TOC | Slight delay on first load |
| Minimal file size increase | Offline requires caching strategy |
| Can add search/filter features | |

### Approach C: iframe-Based Sidebar
**Description**: Use an iframe to embed a shared sidebar HTML file.

| Pros | Cons |
|------|------|
| Single sidebar file | iframe styling challenges |
| Easy to update | Navigation within iframe is clunky |
| Works without much modification | SEO and accessibility issues |
| | Cross-origin restrictions |

### Approach D: CSS-Only Approach (Limited)
**Description**: Use CSS to create a fixed sidebar with hardcoded links.

| Pros | Cons |
|------|------|
| No JavaScript | Cannot list 2000+ items |
| Fast | Only for category navigation |
| Simple | Not a true TOC |

---

## Recommended Approach

### **Approach B: JavaScript-Loaded Sidebar** ✅

This approach provides the best balance of maintainability, performance, and functionality for GitHub Pages hosting.

#### Architecture Overview

```
┌─────────────────────────────────────────────────────────────┐
│                     HTML Page Structure                      │
├─────────────────────────────────────────────────────────────┤
│  ┌──────────────┐  ┌────────────────────────────────────┐   │
│  │   Sidebar    │  │           Main Content             │   │
│  │    (TOC)     │  │                                    │   │
│  │              │  │   Original blog post content       │   │
│  │  - Home      │  │                                    │   │
│  │  - About     │  │                                    │   │
│  │  ───────     │  │                                    │   │
│  │  TOPICS:     │  │                                    │   │
│  │  ▶ 5.1 Custom│  │                                    │   │
│  │    Exporter  │  │                                    │   │
│  │  ▶ 5.2 2D    │  │                                    │   │
│  │    Booleans  │  │                                    │   │
│  │  ▼ 5.3 Post  │  │                                    │   │
│  │    Command ◀ │  │  ← Current topic highlighted       │   │
│  │    - Post A  │  │                                    │   │
│  │    - Post B  │  │                                    │   │
│  │  ▶ 5.4 Dock  │  │                                    │   │
│  │  ...         │  │                                    │   │
│  └──────────────┘  └────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
```

---

## Detailed Technical Specification

### File Structure

```
tbc/
├── index.html              # Landing page (already updated)
├── a/
│   ├── index.html          # Main blog index
│   ├── 0001_intro.html     # Individual posts
│   ├── ...
│   ├── 2078_*.html
│   ├── toc/                # NEW: TOC assets directory
│   │   ├── toc-data.json   # TOC data in JSON format
│   │   ├── toc-sidebar.js  # Sidebar injection script
│   │   └── toc-sidebar.css # Sidebar styles
│   └── icon/               # Existing icons
└── scripts/                # Processing scripts
```

### 1. TOC Data File (`a/toc/toc-data.json`)

The JSON structure uses **topic-based grouping** (not chronological):

```json
{
  "lastUpdated": "2025-01-02",
  "totalPosts": 2078,
  "totalTopics": 58,
  "sections": {
    "about": {
      "title": "About",
      "links": [
        {"label": "Jeremy Tammik", "href": "index.html#0"},
        {"label": "Contact & Support", "href": "index.html#1"},
        {"label": "Getting Started", "href": "index.html#2"},
        {"label": "License", "href": "index.html#3"},
        {"label": "Disclaimer", "href": "index.html#4"}
      ]
    }
  },
  "topics": [
    {
      "id": "5.1",
      "title": "Custom Exporter",
      "posts": [
        {"title": "Custom Exporter Introduction", "file": "1234_custom_exporter.html"},
        {"title": "Exporting Geometry to STL", "file": "1456_stl_export.html"}
        // ... more posts in this topic
      ]
    },
    {
      "id": "5.2",
      "title": "2D Booleans and Adjacent Areas",
      "posts": [
        {"title": "2D Boolean Operations", "file": "0987_2d_booleans.html"}
        // ... more posts
      ]
    },
    {
      "id": "5.3",
      "title": "PostCommand",
      "posts": [
        // Posts about PostCommand API
      ]
    },
    {
      "id": "5.25",
      "title": "Family API, Loading and Placing Instances",
      "subTopics": [
        {
          "id": "5.25.1",
          "title": "Family API for Creating Definitions",
          "posts": [/* ... */]
        },
        {
          "id": "5.25.2",
          "title": "Family Instance Placement",
          "posts": [/* ... */]
        },
        {
          "id": "5.25.3",
          "title": "Loading a Family",
          "posts": [/* ... */]
        }
      ]
    }
    // ... all 58 topic groups extracted from a/index.html
  ]
}
```

### Extracting Topic Data

The topic data must be extracted from `a/index.html` section #5, which contains:
- 55 main topic groups (5.1 through 5.56, excluding 5.12 redirect) + 3 subtopics
- Some topics have sub-topics (e.g., 5.25.1, 5.25.2, 5.25.3)
- Each topic contains curated lists of related blog posts with links

### 2. Sidebar JavaScript (`a/toc/toc-sidebar.js`)

Key functionality:
- Fetch and parse `toc-data.json`
- Generate sidebar HTML dynamically with topic groups
- Insert into page DOM
- Highlight current page in TOC (and expand its topic group)
- Handle collapsible topic sections (55 topics + 3 sub-topics = 58 total)
- **Drag-to-resize sidebar width** (see Resizable Sidebar section below)
- Implement search/filter (optional)
- Support keyboard navigation
- Remember scroll position, expanded topics, and sidebar width in localStorage

### 3. Sidebar CSS (`a/toc/toc-sidebar.css`)

Key styles:
- Fixed left sidebar (default 280px, min 180px, max 500px)
- **Resize handle** on right edge (cursor: col-resize)
- Scrollable topic list
- Sticky header within sidebar
- Current page highlight
- Hover states
- Collapsible topic groups (58 topics)
- Nested sub-topics (e.g., 5.25.1, 5.25.2, 5.25.3)
- Mobile hamburger menu
- Print styles (hide sidebar)

---

## Resizable Sidebar (Drag to Expand/Contract)

### Feature Description

Users can **drag the right edge of the sidebar** to resize it horizontally. This allows customization of the sidebar width based on personal preference and screen size.

### Implementation Details

```
┌────────────────────┬─┬──────────────────────────────────┐
│                    │▐│                                  │
│    Sidebar TOC     │▐│       Main Content Area          │
│                    │▐│                                  │
│  ▶ 5.1 Custom...   │▐│  Blog post content here...       │
│  ▶ 5.2 2D Bool...  │▐│                                  │
│  ▼ 5.3 PostCmd     │▐│                                  │
│    - Post A        │▐│                                  │
│    - Post B        │▐│                                  │
│                    │▐│                                  │
└────────────────────┴─┴──────────────────────────────────┘
                      ↑
              Drag handle (▐)
              cursor: col-resize
```

### HTML Structure

```html
<div id="tbc-sidebar" style="width: 280px;">
  <!-- Sidebar content -->
  <div id="tbc-resize-handle"></div>
</div>
<div id="tbc-content">
  <!-- Main content -->
</div>
```

### CSS for Resize Handle

```css
#tbc-sidebar {
  position: fixed;
  left: 0;
  top: 0;
  height: 100vh;
  width: 280px;           /* Default width */
  min-width: 180px;       /* Minimum width */
  max-width: 500px;       /* Maximum width */
  overflow-y: auto;
  background: #f5f5f5;
  border-right: 1px solid #ddd;
}

#tbc-resize-handle {
  position: absolute;
  right: 0;
  top: 0;
  width: 5px;
  height: 100%;
  cursor: col-resize;
  background: transparent;
  transition: background 0.2s;
}

#tbc-resize-handle:hover,
#tbc-resize-handle.dragging {
  background: rgba(0, 0, 0, 0.1);
}

#tbc-content {
  margin-left: 280px;     /* Matches sidebar width */
  transition: margin-left 0.1s;
}
```

### JavaScript for Drag Resize

```javascript
function initResizableSidebar() {
  const sidebar = document.getElementById('tbc-sidebar');
  const handle = document.getElementById('tbc-resize-handle');
  const content = document.getElementById('tbc-content');
  
  let isResizing = false;
  
  handle.addEventListener('mousedown', (e) => {
    isResizing = true;
    handle.classList.add('dragging');
    document.body.style.cursor = 'col-resize';
    document.body.style.userSelect = 'none';
    e.preventDefault();
  });
  
  document.addEventListener('mousemove', (e) => {
    if (!isResizing) return;
    
    let newWidth = e.clientX;
    
    // Enforce min/max constraints
    newWidth = Math.max(180, Math.min(500, newWidth));
    
    sidebar.style.width = newWidth + 'px';
    content.style.marginLeft = newWidth + 'px';
  });
  
  document.addEventListener('mouseup', () => {
    if (isResizing) {
      isResizing = false;
      handle.classList.remove('dragging');
      document.body.style.cursor = '';
      document.body.style.userSelect = '';
      
      // Save preference to localStorage
      localStorage.setItem('tbc-sidebar-width', sidebar.style.width);
    }
  });
  
  // Restore saved width on page load
  const savedWidth = localStorage.getItem('tbc-sidebar-width');
  if (savedWidth) {
    sidebar.style.width = savedWidth;
    content.style.marginLeft = savedWidth;
  }
}
```

### Touch Support (Mobile/Tablet)

```javascript
// Add touch event support for tablets
handle.addEventListener('touchstart', (e) => {
  isResizing = true;
  e.preventDefault();
});

document.addEventListener('touchmove', (e) => {
  if (!isResizing) return;
  const touch = e.touches[0];
  let newWidth = Math.max(180, Math.min(500, touch.clientX));
  sidebar.style.width = newWidth + 'px';
  content.style.marginLeft = newWidth + 'px';
});

document.addEventListener('touchend', () => {
  if (isResizing) {
    isResizing = false;
    localStorage.setItem('tbc-sidebar-width', sidebar.style.width);
  }
});
```

### Double-Click to Reset

```javascript
// Double-click on handle resets to default width
handle.addEventListener('dblclick', () => {
  const defaultWidth = '280px';
  sidebar.style.width = defaultWidth;
  content.style.marginLeft = defaultWidth;
  localStorage.setItem('tbc-sidebar-width', defaultWidth);
});
```

### Requirements Summary

| ID | Requirement | Priority |
|----|-------------|----------|
| R1 | Drag right edge to resize sidebar | Must Have |
| R2 | Min width: 180px, Max width: 500px | Must Have |
| R3 | Visual feedback on hover (cursor change) | Should Have |
| R4 | Persist width preference in localStorage | Should Have |
| R5 | Double-click to reset to default width | Nice to Have |
| R6 | Touch support for tablets | Nice to Have |
| R7 | Smooth animation during resize | Nice to Have |

---

### 4. Page Wrapper Template

Each blog post HTML file needs modification:

**Before:**
```html
<html>
<head>
  <title>Post Title</title>
  ...
</head>
<body>
  <!-- Original content -->
</body>
</html>
```

**After:**
```html
<!DOCTYPE html>
<html>
<head>
  <title>Post Title</title>
  <link rel="stylesheet" href="toc/toc-sidebar.css">
  ...
</head>
<body>
  <div id="tbc-sidebar"></div>
  <div id="tbc-content">
    <!-- Original content (wrapped) -->
  </div>
  <script src="toc/toc-sidebar.js"></script>
</body>
</html>
```

---

## TOC Structure Requirements

### Hierarchy Order (TOPIC-BASED - matching homepage section #5)

1. **Header Section**
   - Logo/Title: "The Building Coder"
   - Link to homepage

2. **Navigation Section**
   - Home (a/index.html)
   - About (#0)
   - Contact (#1)
   - Getting Started (#2)
   - License (#3)
   - Disclaimer (#4)

3. **Topics Section** (58 TOPIC GROUPS - collapsible)
   
   Each topic is a collapsible group containing curated lists of related posts:
   
   - ▶ 5.1 Custom Exporter (12 posts)
   - ▶ 5.2 2D Booleans and Adjacent Areas (11 posts)
   - ▶ 5.3 PostCommand (12 posts)
   - ▶ 5.4 Dockable Panels (8 posts)
   - ▶ 5.5 Material Management and Libraries (15+ posts)
   - ▶ 5.6 Phase (4 posts)
   - ▶ 5.7 Preview Control (7 posts)
   - ▶ 5.8 RstLink (5 posts)
   - ▶ 5.9 FilteredElementCollector (10+ posts)
   - ▶ 5.10 Filter for Family Symbols
   - ▶ 5.11 Model Review
   - ▶ 5.12 Wall Layers
   - ▶ 5.13 GitHub
   - ▶ 5.14 Moving a Cable Tray
   - ▶ 5.15 Room Boundaries
   - ▶ 5.16 Unit Testing
   - ▶ 5.17 Wall Layers and Compound Structure
   - ▶ 5.18 Room Properties
   - ▶ 5.19 Pick Point
   - ▶ 5.20 Visual Studio Revit Add-In Wizards
   - ▶ 5.21 RoomEditorApp
   - ▶ 5.22 Advanced Revit 2014 API Features
   - ▶ 5.23 Extensible Storage
   - ▶ 5.24 Control Element Colour and Material
   - ▶ 5.25 Family API, Loading and Placing Instances
     - ▶ 5.25.1 Family API for Creating Definitions
     - ▶ 5.25.2 Family Instance Placement
     - ▶ 5.25.3 Loading a Family
   - ▶ 5.26 PushButtonData Usage Examples
   - ▶ 5.27 FamilyElementVisibility
   - ▶ 5.28 Idling and External Events
   - ▶ 5.29 ExtrusionAnalyzer
   - ▶ 5.30 3D Booleans, Cutting and Joining
   - ▶ 5.31 Dynamic Model Updater Framework DMU
   - ▶ 5.32 Detecting and Handling Dialogues
   - ▶ 5.33 Need to Regenerate
   - ▶ 5.34 BipChecker
   - ▶ 5.35 Cloud-based Round-trip 2D Revit BIM Editor
   - ▶ 5.36 Source Code Colourizer
   - ▶ 5.37 Creating a 3D View
   - ▶ 5.38 Creating a Section View
   - ▶ 5.39 Splitting an Element into Parts
   - ▶ 5.40 ADN Revit MEP HVAC Sample AdnRme
   - ▶ 5.41 Revit and Its API is Different
   - ▶ 5.42 Texture Bitmap and UV Coordinate Access
   - ▶ 5.43 Point Clouds
   - ▶ 5.44 Creating a Floor
   - ▶ 5.45 Creating Dimensioning
   - ▶ 5.46 Autodesk View and Data API
   - ▶ 5.47 Exporting Individual Element Geometry
   - ▶ 5.48 Element Intersection and Collision Detection
   - ▶ 5.49 Edit and Continue, Live Development
   - ▶ 5.50 DirectShape Element
   - ▶ 5.51 Spatial Adjacency and Thermal Energy
   - ▶ 5.52 Revit API Util Classes
   - ▶ 5.53 Handling Transactions and Transaction Groups
   - ▶ 5.54 Structural Extensions, REX
   - ▶ 5.55 DA4R – Design Automation for Revit
   - ▶ 5.56 Element Identifiers in RVT, IFC, NW

4. **Footer Section**
   - Search box (optional)
   - "Back to top" link

### Note on Topic-Based Organization

The sidebar uses **topic-based grouping** (NOT chronological by year). Posts are grouped by subject matter as curated by Jeremy Tammik in the homepage TOC. This allows users to:

- Find all posts about a specific Revit API topic in one place
- Navigate by subject matter rather than publication date
- Discover related posts they might have missed

---

## User Experience Requirements

### Functional Requirements

| ID | Requirement | Priority |
|----|-------------|----------|
| F1 | Sidebar visible on all blog post pages | Must Have |
| F2 | Current page highlighted in sidebar | Must Have |
| F3 | Collapsible topic groups | Must Have |
| F4 | **Search/filter posts** (see Search Feature section) | **Must Have** |
| F5 | Sidebar scroll position preserved on navigation | Should Have |
| F6 | Remember collapsed/expanded state | Should Have |
| F7 | Keyboard navigation (arrow keys) | Nice to Have |

### Non-Functional Requirements

| ID | Requirement | Target |
|----|-------------|--------|
| NF1 | Sidebar load time | < 500ms |
| NF2 | Total JS + CSS size | < 50KB |
| NF3 | Works in all major browsers | Chrome, Firefox, Safari, Edge |
| NF4 | Accessible (WCAG 2.1 AA) | Screen reader compatible |
| NF5 | Works offline after first load | Service worker caching |

---

## Search Feature Specification

The sidebar includes a **real-time search box** that filters posts and topics as the user types.

### Search Box Location

```
┌──────────────────────┐
│  The Building Coder  │  <- Header
├──────────────────────┤
│ 🔍 Search posts...   │  <- SEARCH BOX (sticky)
├──────────────────────┤
│  Home                │
│  About               │
│  ──────────────────  │
│  TOPICS              │
│  ▶ 5.1 Custom Export │
│  ▶ 5.2 2D Booleans   │
│  ...                 │
└──────────────────────┘
```

### Search Behavior

| Behavior | Description |
|----------|-------------|
| **Real-time filtering** | Results update as user types (debounced 150ms) |
| **Search scope** | Searches post titles AND topic names |
| **Case insensitive** | "filter" matches "FilteredElementCollector" |
| **Partial matching** | "extrusion" matches "ExtrusionAnalyzer" |
| **Highlight matches** | Matching text highlighted in yellow |
| **Expand matching topics** | Topics with matching posts auto-expand |
| **No results message** | Shows "No posts found" when nothing matches |
| **Result count** | Shows "X results" below search box |

### HTML Structure

```html
<div id="tbc-search-container">
  <div class="search-input-wrapper">
    <span class="search-icon">🔍</span>
    <input type="text" 
           id="tbc-search-input" 
           placeholder="Search posts..." 
           autocomplete="off">
    <button id="tbc-search-clear" class="hidden">✕</button>
  </div>
  <div id="tbc-search-results-count"></div>
</div>
```

### CSS Styles

```css
#tbc-search-container {
  position: sticky;
  top: 0;
  background: #f5f5f5;
  padding: 10px;
  border-bottom: 1px solid #ddd;
  z-index: 10;
}

.search-input-wrapper {
  display: flex;
  align-items: center;
  background: white;
  border: 1px solid #ccc;
  border-radius: 4px;
  padding: 6px 10px;
}

.search-input-wrapper:focus-within {
  border-color: #0066cc;
  box-shadow: 0 0 0 2px rgba(0, 102, 204, 0.2);
}

#tbc-search-input {
  border: none;
  outline: none;
  flex: 1;
  font-size: 14px;
  padding: 2px 8px;
}

.search-icon {
  color: #666;
  font-size: 14px;
}

#tbc-search-clear {
  background: none;
  border: none;
  cursor: pointer;
  color: #999;
  font-size: 14px;
  padding: 2px 4px;
}

#tbc-search-clear:hover {
  color: #333;
}

#tbc-search-clear.hidden {
  display: none;
}

#tbc-search-results-count {
  font-size: 12px;
  color: #666;
  margin-top: 6px;
  text-align: center;
}

.search-highlight {
  background-color: #fff59d;
  padding: 0 2px;
  border-radius: 2px;
}

.search-no-match {
  display: none !important;
}
```

### JavaScript Implementation

```javascript
function initSearch() {
  const input = document.getElementById('tbc-search-input');
  const clearBtn = document.getElementById('tbc-search-clear');
  const resultsCount = document.getElementById('tbc-search-results-count');
  const topics = document.querySelectorAll('.tbc-topic');
  const posts = document.querySelectorAll('.tbc-post-link');
  
  let debounceTimer;
  
  input.addEventListener('input', () => {
    clearTimeout(debounceTimer);
    debounceTimer = setTimeout(() => performSearch(input.value), 150);
    
    // Show/hide clear button
    clearBtn.classList.toggle('hidden', !input.value);
  });
  
  clearBtn.addEventListener('click', () => {
    input.value = '';
    clearBtn.classList.add('hidden');
    resetSearch();
    input.focus();
  });
  
  function performSearch(query) {
    query = query.toLowerCase().trim();
    
    if (!query) {
      resetSearch();
      return;
    }
    
    let matchCount = 0;
    
    posts.forEach(post => {
      const title = post.textContent.toLowerCase();
      const matches = title.includes(query);
      
      post.classList.toggle('search-no-match', !matches);
      
      if (matches) {
        matchCount++;
        highlightMatch(post, query);
        // Expand parent topic
        const topic = post.closest('.tbc-topic');
        if (topic) topic.classList.add('expanded');
      } else {
        removeHighlight(post);
      }
    });
    
    // Hide topics with no matching posts
    topics.forEach(topic => {
      const hasVisiblePosts = topic.querySelector('.tbc-post-link:not(.search-no-match)');
      const topicTitle = topic.querySelector('.tbc-topic-title').textContent.toLowerCase();
      const topicMatches = topicTitle.includes(query);
      
      if (topicMatches) {
        topic.classList.remove('search-no-match');
        topic.classList.add('expanded');
        // Show all posts in matching topic
        topic.querySelectorAll('.tbc-post-link').forEach(p => {
          p.classList.remove('search-no-match');
        });
      } else {
        topic.classList.toggle('search-no-match', !hasVisiblePosts);
      }
    });
    
    // Update results count
    resultsCount.textContent = matchCount === 0 
      ? 'No posts found' 
      : `${matchCount} result${matchCount === 1 ? '' : 's'}`;
  }
  
  function resetSearch() {
    posts.forEach(post => {
      post.classList.remove('search-no-match');
      removeHighlight(post);
    });
    topics.forEach(topic => {
      topic.classList.remove('search-no-match');
    });
    resultsCount.textContent = '';
  }
  
  function highlightMatch(element, query) {
    const text = element.textContent;
    const regex = new RegExp(`(${escapeRegex(query)})`, 'gi');
    element.innerHTML = text.replace(regex, '<span class="search-highlight">$1</span>');
  }
  
  function removeHighlight(element) {
    element.innerHTML = element.textContent;
  }
  
  function escapeRegex(string) {
    return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
  }
}
```

### Keyboard Shortcuts

| Shortcut | Action |
|----------|--------|
| `/` | Focus search box (when not in input) |
| `Escape` | Clear search and blur |
| `↓` | Move to first result |
| `Enter` | Navigate to selected result |

```javascript
document.addEventListener('keydown', (e) => {
  const input = document.getElementById('tbc-search-input');
  
  // Press '/' to focus search (like GitHub)
  if (e.key === '/' && document.activeElement !== input) {
    e.preventDefault();
    input.focus();
  }
  
  // Press Escape to clear and blur
  if (e.key === 'Escape' && document.activeElement === input) {
    input.value = '';
    input.blur();
    resetSearch();
  }
});
```

### Search Requirements Summary

| ID | Requirement | Priority |
|----|-------------|----------|
| S1 | Search box sticky at top of sidebar | Must Have |
| S2 | Real-time filtering as user types | Must Have |
| S3 | Search post titles | Must Have |
| S4 | Search topic names | Must Have |
| S5 | Clear button to reset search | Must Have |
| S6 | Result count display | Should Have |
| S7 | Highlight matching text | Should Have |
| S8 | Auto-expand topics with matches | Should Have |
| S9 | Keyboard shortcut `/` to focus | Nice to Have |
| S10 | Escape to clear and blur | Nice to Have |

---

## Mobile Responsiveness

### Breakpoints

| Screen Width | Sidebar Behavior |
|--------------|------------------|
| > 1024px | Fixed sidebar, always visible |
| 768-1024px | Collapsible sidebar, toggle button |
| < 768px | Hidden by default, hamburger menu |

### Mobile Implementation

```
┌─────────────────────────────────┐
│ ☰  The Building Coder     🔍   │  <- Header with hamburger
├─────────────────────────────────┤
│                                 │
│     Main Content Area           │
│                                 │
│                                 │
└─────────────────────────────────┘

When hamburger clicked:
┌─────────────────────────────────┐
│ ✕  The Building Coder          │  <- Close button
├─────────────────────────────────┤
│  Home                           │
│  About                          │
│  Getting Started                │
│  ─────────────────              │
│  TOPICS                         │
│  ▼ 5.1 Custom Exporter          │
│    - Post about exporters...    │
│    - Another exporter post...   │
│  ▶ 5.2 2D Booleans              │
│  ▶ 5.3 PostCommand              │
│  ▶ 5.4 Dockable Panels          │
│  ▶ 5.5 Materials                │
│  ...                            │
│  ▶ 5.56 Element Identifiers     │
└─────────────────────────────────┘
```

---

## Performance Considerations

### Optimization Strategies

1. **Lazy Loading Topic Contents**
   - Load only topic headers initially (58 topics)
   - Load posts within a topic on expand
   - This keeps initial render fast

2. **Virtual Scrolling** (optional, for expanded topics)
   - Only render visible items within large topic groups
   - Use intersection observer

3. **Caching**
   - Cache `toc-data.json` in localStorage
   - Use Service Worker for offline support

4. **Minification**
   - Minify JS and CSS for production
   - Gzip compression on GitHub Pages

### Estimated File Sizes

| File | Uncompressed | Gzipped |
|------|--------------|---------|
| toc-data.json | ~150KB | ~20KB |
| toc-sidebar.js | ~15KB | ~5KB |
| toc-sidebar.css | ~8KB | ~2KB |
| **Total** | **~173KB** | **~27KB** |

---

## Implementation Steps

### Phase 1: Create TOC Assets
1. **Extract topic-based TOC data** from `a/index.html` section #5 into `toc-data.json`
   - Parse 58 topic groups (5.1 through 5.56, excluding 5.12 redirect)
   - Extract post links within each topic
   - Handle sub-topics (e.g., 5.25.1, 5.25.2, 5.25.3)
2. Create `toc-sidebar.css` with styles
3. Create `toc-sidebar.js` with sidebar logic
4. Test on a single page

### Phase 2: Modify All HTML Files
1. Create Python script to:
   - Add CSS link to `<head>`
   - Wrap `<body>` content in `<div id="tbc-content">`
   - Add sidebar placeholder `<div id="tbc-sidebar">`
   - Add JS script before `</body>`
2. Process all 2,078 HTML files
3. Verify no content corruption

### Phase 3: Testing & Refinement
1. Test on multiple pages
2. Test mobile responsiveness
3. Test offline functionality
4. Fix any layout issues
5. Performance testing

### Phase 4: Deployment
1. Commit all changes
2. Push to GitHub
3. Verify GitHub Pages deployment
4. Test live site

---

## File Modifications

### Files to Create

| File | Description |
|------|-------------|
| `a/toc/toc-data.json` | JSON file with all post data |
| `a/toc/toc-sidebar.js` | JavaScript for sidebar functionality |
| `a/toc/toc-sidebar.css` | CSS styles for sidebar |
| `scripts/add_sidebar_to_pages.py` | Script to modify all HTML files |
| `scripts/extract_toc_data.py` | Script to extract TOC from index.html |

### Files to Modify

| File(s) | Modification |
|---------|--------------|
| `a/*.html` (2,078 files) | Add sidebar placeholder, CSS link, JS script |
| `a/index.html` | Add sidebar (may need different treatment) |
| `index.html` | Optional: Add link to TOC page |

---

## Testing Requirements

### Unit Tests
- [ ] TOC data loads correctly
- [ ] Sidebar renders all posts
- [ ] Current page detection works
- [ ] Collapsible sections work
- [ ] Search/filter works (if implemented)

### Integration Tests
- [ ] Sidebar appears on all pages
- [ ] No layout conflicts with existing content
- [ ] Links navigate correctly
- [ ] Back/forward browser navigation works

### Cross-Browser Tests
- [ ] Chrome (latest)
- [ ] Firefox (latest)
- [ ] Safari (latest)
- [ ] Edge (latest)
- [ ] Mobile Safari (iOS)
- [ ] Chrome Mobile (Android)

### Accessibility Tests
- [ ] Screen reader navigation
- [ ] Keyboard-only navigation
- [ ] Color contrast compliance
- [ ] Focus indicators visible

### Performance Tests
- [ ] Initial load time < 2s
- [ ] Sidebar responsive to scrolling
- [ ] No jank during year expand/collapse

---

## Appendix: Sample Code Snippets

### Extract TOC Script (Python)

```python
#!/usr/bin/env python3
"""
extract_toc_data.py - Extract TOC from index.html to JSON
"""
import re
import json
from pathlib import Path
from bs4 import BeautifulSoup

def extract_toc():
    index_path = Path("a/index.html")
    html = index_path.read_text(encoding="utf-8")
    soup = BeautifulSoup(html, "html.parser")
    
    posts = []
    # Find all table rows with post data
    for row in soup.select("table tr"):
        cells = row.find_all("td")
        if len(cells) >= 4:
            num = cells[0].get_text(strip=True)
            date = cells[1].get_text(strip=True)
            link = cells[2].find("a")
            if link and num.isdigit():
                posts.append({
                    "num": int(num),
                    "date": date,
                    "title": link.get_text(strip=True),
                    "file": link.get("href")
                })
    
    toc_data = {
        "lastUpdated": "2025-01-02",
        "totalPosts": len(posts),
        "posts": sorted(posts, key=lambda x: x["num"], reverse=True)
    }
    
    output_path = Path("a/toc/toc-data.json")
    output_path.parent.mkdir(exist_ok=True)
    output_path.write_text(json.dumps(toc_data, indent=2), encoding="utf-8")
    print(f"Extracted {len(posts)} posts to {output_path}")

if __name__ == "__main__":
    extract_toc()
```

### Sidebar CSS (Partial)

```css
/* toc-sidebar.css */
:root {
  --sidebar-width: 280px;
  --sidebar-bg: #1a365d;
  --sidebar-text: #e2e8f0;
  --sidebar-hover: #2c5282;
  --sidebar-active: #4299e1;
}

#tbc-sidebar {
  position: fixed;
  left: 0;
  top: 0;
  width: var(--sidebar-width);
  height: 100vh;
  background: var(--sidebar-bg);
  color: var(--sidebar-text);
  overflow-y: auto;
  z-index: 1000;
}

#tbc-content {
  margin-left: var(--sidebar-width);
  padding: 20px;
}

/* Mobile */
@media (max-width: 768px) {
  #tbc-sidebar {
    transform: translateX(-100%);
    transition: transform 0.3s;
  }
  #tbc-sidebar.open {
    transform: translateX(0);
  }
  #tbc-content {
    margin-left: 0;
  }
}
```

---

## Conclusion

This requirements document provides a comprehensive blueprint for implementing a wiki-style TOC sidebar across The Building Coder archive. The recommended JavaScript-loaded approach offers the best balance of maintainability, performance, and user experience while remaining compatible with GitHub Pages' static hosting constraints.

**Estimated Implementation Time**: 8-16 hours

**Key Deliverables**:
1. TOC data extraction script
2. Sidebar JavaScript component
3. Sidebar CSS styles
4. HTML modification script
5. Updated HTML files (2,078)
6. Testing and deployment

---

*Document Version: 1.0*
*Created: January 2, 2026*
*Author: GitHub Copilot*
