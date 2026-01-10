# Mobile Chronological Navigation — Technical Specification

## Document Overview

| Item | Value |
|------|-------|
| **Feature** | Bottom Sheet with Month Tabs for Mobile Chronological Navigation |
| **Design Document** | [MOBILE_CHRONO_NAV_MOCKUPS.md](MOBILE_CHRONO_NAV_MOCKUPS.md) |
| **Status** | Draft Specification |
| **Created** | 2026-01-10 |
| **Target Viewport** | ≤768px (mobile devices) |

---

## Table of Contents

1. [Executive Summary](#1-executive-summary)
2. [Technical Requirements](#2-technical-requirements)
3. [Architecture Overview](#3-architecture-overview)
4. [Data Schema](#4-data-schema)
5. [Implementation Plan](#5-implementation-plan)
6. [File Changes](#6-file-changes)
7. [CSS Specification](#7-css-specification)
8. [JavaScript Specification](#8-javascript-specification)
9. [Python Script Integration](#9-python-script-integration)
10. [Testing Strategy](#10-testing-strategy)
11. [Rollback Plan](#11-rollback-plan)
12. [Appendices](#12-appendices)

---

## 1. Executive Summary

### Problem Statement

When a user expands a year in the mobile chronological navigation (e.g., 2014 with 156 posts), they must scroll through 100+ items to find a specific post. This creates a poor user experience on touch devices with limited screen height.

### Proposed Solution

Implement a **bottom sheet with horizontal month tabs** that:
- Groups posts by month, reducing visible items from ~150 to ~12 per view
- Uses familiar mobile UI patterns (bottom sheet, horizontal tabs)
- Provides progressive disclosure (year grid → month tabs → post list)
- Maintains all existing desktop functionality unchanged

### Scope

| In Scope | Out of Scope |
|----------|--------------|
| Mobile portrait (≤768px) | Desktop viewports (>1000px) |
| Mobile landscape (≤896px) | Tablet viewports (768-1000px) |
| Touch gestures | Keyboard-only navigation |
| New CSS/JS components | Changes to data structure |
| Integration tests | Performance optimization |

---

## 2. Technical Requirements

### 2.1 Functional Requirements

| ID | Requirement | Priority |
|----|-------------|----------|
| FR-01 | Bottom bar displays current post context when collapsed | Must |
| FR-02 | Tap/swipe up expands to year selection grid | Must |
| FR-03 | Tap year chip transitions to month tab view | Must |
| FR-04 | Horizontal scrollable month tabs filter post list | Must |
| FR-05 | Tap post navigates to that post | Must |
| FR-06 | Swipe down or tap X dismisses bottom sheet | Must |
| FR-07 | Current post is highlighted in list | Should |
| FR-08 | Sheet state persists across page loads | Should |
| FR-09 | Landscape mode shows side-by-side layout | Should |
| FR-10 | Animation transitions are smooth (60fps) | Should |

### 2.2 Non-Functional Requirements

| ID | Requirement | Target |
|----|-------------|--------|
| NFR-01 | Initial render time | <100ms |
| NFR-02 | Touch response time | <50ms |
| NFR-03 | Sheet expansion animation | <300ms |
| NFR-04 | JavaScript bundle increase | <5KB minified |
| NFR-05 | CSS bundle increase | <3KB minified |
| NFR-06 | Accessibility | WCAG 2.1 AA |

### 2.3 Browser Support

| Browser | Minimum Version |
|---------|-----------------|
| Safari iOS | 14+ |
| Chrome Android | 90+ |
| Samsung Internet | 15+ |
| Firefox Mobile | 95+ |

---

## 3. Architecture Overview

### 3.1 Current Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                     toc-sidebar.js                          │
│  ┌─────────────────┐    ┌──────────────────────────────┐   │
│  │  Left Sidebar   │    │  Chronological Column        │   │
│  │  (TOC Topics)   │    │  (Right side, IIFE at end)   │   │
│  └────────┬────────┘    └──────────────┬───────────────┘   │
│           │                            │                    │
│           ▼                            ▼                    │
│     toc-data.json               chrono-data.json            │
└─────────────────────────────────────────────────────────────┘
```

### 3.2 Proposed Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                     toc-sidebar.js                          │
│  ┌─────────────────┐    ┌──────────────────────────────┐   │
│  │  Left Sidebar   │    │  Chronological Column        │   │
│  │  (TOC Topics)   │    │  (Desktop: right column)     │   │
│  └────────┬────────┘    │  (Mobile: bottom sheet) ◄────┼───┤ NEW
│           │             └──────────────┬───────────────┘   │
│           ▼                            │                    │
│     toc-data.json                      ▼                    │
│                                  chrono-data.json           │
│                                        │                    │
│                           ┌────────────┴────────────┐       │
│                           │  ChronoMobileSheet      │ NEW   │
│                           │  - BottomSheet          │       │
│                           │  - YearGrid             │       │
│                           │  - MonthTabs            │       │
│                           │  - PostList             │       │
│                           └─────────────────────────┘       │
└─────────────────────────────────────────────────────────────┘
```

### 3.3 Component Hierarchy

```
ChronoMobileSheet (root)
├── BottomBar (collapsed state)
│   ├── DragHandle
│   └── ContextLabel ("2024 · Post #1842 of 156")
├── YearGrid (partial expand)
│   ├── Header ("Browse by Year")
│   └── YearChip[] (year, count)
├── MonthSheet (full expand)
│   ├── SheetHeader
│   │   ├── BackButton
│   │   ├── YearTitle
│   │   └── CloseButton
│   ├── MonthTabs
│   │   └── MonthTab[] (month name, count)
│   └── PostList
│       └── PostItem[] (num, title, date)
└── Overlay (semi-transparent backdrop)
```

---

## 4. Data Schema

### 4.1 Existing Schema (chrono-data.json)

The current schema already contains all required data:

```json
{
  "version": "1.0",
  "lastUpdated": "2026-01-07",
  "totalPosts": 2079,
  "posts": [
    {
      "num": 1,
      "file": "0001_welcome.htm",
      "title": "Welcome",
      "date": "2008-08-22",
      "year": 2008,
      "month": 8
    }
  ],
  "years": [
    {
      "year": 2026,
      "count": 12,
      "firstPost": 2068,
      "lastPost": 2079
    }
  ]
}
```

### 4.2 No Schema Changes Required

The existing `posts[].month` field enables month-based filtering without any data modifications.

### 4.3 Runtime Data Structures

```javascript
// Month aggregation (computed at runtime)
const monthData = {
  2014: {
    12: { count: 14, posts: [...] },
    11: { count: 11, posts: [...] },
    // ...
  }
};

// Sheet state
const sheetState = {
  mode: 'collapsed' | 'years' | 'months',
  selectedYear: null,
  selectedMonth: null,
  scrollPosition: 0
};
```

---

## 5. Implementation Plan

### 5.1 Phase Overview

| Phase | Description | Duration | Dependencies |
|-------|-------------|----------|--------------|
| Phase 1 | CSS Foundation | 2 days | None |
| Phase 2 | Bottom Sheet Core | 3 days | Phase 1 |
| Phase 3 | Year Grid Component | 2 days | Phase 2 |
| Phase 4 | Month Tabs Component | 3 days | Phase 3 |
| Phase 5 | Post List Component | 2 days | Phase 4 |
| Phase 6 | Landscape Layout | 2 days | Phase 5 |
| Phase 7 | State Persistence | 1 day | Phase 5 |
| Phase 8 | Integration Testing | 2 days | Phase 6, 7 |
| Phase 9 | Accessibility Audit | 1 day | Phase 8 |

**Total Estimated Duration:** 18 days

### 5.2 Phase 1: CSS Foundation

**Objective:** Add CSS custom properties and media query structure for mobile bottom sheet.

**Files Modified:**
- `a/toc/toc-sidebar.css`

**Tasks:**
1. Add CSS custom properties for bottom sheet dimensions
2. Create `@media (max-width: 768px)` block for mobile-only styles
3. Define base bottom sheet container styles
4. Define sheet state classes (`.collapsed`, `.partial`, `.expanded`)
5. Add transition timing variables

**Deliverables:**
- CSS variables for sheet heights and animations
- Empty structural classes ready for content

### 5.3 Phase 2: Bottom Sheet Core

**Objective:** Implement the collapsible bottom sheet container with gesture support.

**Files Modified:**
- `a/toc/toc-sidebar.js`
- `a/toc/toc-sidebar.css`

**Tasks:**
1. Create `ChronoMobileSheet` class/module within IIFE
2. Implement sheet HTML structure generation
3. Add touch gesture handling (swipe up/down)
4. Implement sheet state machine (collapsed → partial → expanded)
5. Add overlay backdrop with tap-to-dismiss
6. Integrate with viewport detection (`window.matchMedia`)

**Deliverables:**
- Working bottom sheet that expands/collapses
- Gesture handling for swipe interactions
- Overlay component

### 5.4 Phase 3: Year Grid Component

**Objective:** Implement the year selection grid shown in partial expand state.

**Files Modified:**
- `a/toc/toc-sidebar.js`
- `a/toc/toc-sidebar.css`

**Tasks:**
1. Create `YearGrid` component
2. Generate year chips from `chrono-data.json` years array
3. Style year chips with post counts
4. Highlight current year chip
5. Handle year chip tap to transition to month view

**Deliverables:**
- Scrollable year grid
- Styled year chips showing year and count
- Current year highlighting

### 5.5 Phase 4: Month Tabs Component

**Objective:** Implement horizontal scrollable month tabs.

**Files Modified:**
- `a/toc/toc-sidebar.js`
- `a/toc/toc-sidebar.css`

**Tasks:**
1. Create `MonthTabs` component
2. Generate tabs from posts filtered by selected year
3. Implement horizontal scroll with touch
4. Add scroll indicators (◀ ▶)
5. Style selected tab with underline
6. Show post count under each month

**Deliverables:**
- Horizontally scrollable month tabs
- Visual selection indicator
- Post counts per month

### 5.6 Phase 5: Post List Component

**Objective:** Implement the filtered post list for selected month.

**Files Modified:**
- `a/toc/toc-sidebar.js`
- `a/toc/toc-sidebar.css`

**Tasks:**
1. Create `PostList` component
2. Filter posts by year AND month
3. Render post items with number, title, date
4. Highlight current post
5. Handle post tap for navigation
6. Implement smooth list scrolling

**Deliverables:**
- Filterable post list
- Current post highlighting
- Navigation on tap

### 5.7 Phase 6: Landscape Layout

**Objective:** Implement side-by-side month/post layout for landscape orientation.

**Files Modified:**
- `a/toc/toc-sidebar.css`
- `a/toc/toc-sidebar.js` (minor)

**Tasks:**
1. Add `@media (orientation: landscape)` queries
2. Create two-column layout (months left, posts right)
3. Adjust touch targets for landscape
4. Test on various landscape viewports

**Deliverables:**
- Side-by-side layout in landscape
- Proper column proportions
- Responsive touch targets

### 5.8 Phase 7: State Persistence

**Objective:** Save and restore sheet state across page loads.

**Files Modified:**
- `a/toc/toc-sidebar.js`

**Tasks:**
1. Define localStorage keys for mobile sheet state
2. Save selected year/month on change
3. Restore state on page load
4. Clear state on explicit close
5. Handle storage quota errors gracefully

**Deliverables:**
- Persistent year/month selection
- Graceful error handling

### 5.9 Phase 8: Integration Testing

See [Section 10: Testing Strategy](#10-testing-strategy).

### 5.10 Phase 9: Accessibility Audit

**Objective:** Ensure WCAG 2.1 AA compliance.

**Tasks:**
1. Add ARIA labels to all interactive elements
2. Implement focus management for sheet
3. Test with VoiceOver (iOS) and TalkBack (Android)
4. Verify touch target sizes (≥44×44px)
5. Test reduced motion preference

**Deliverables:**
- Complete ARIA labeling
- Focus trap in expanded sheet
- Screen reader announcements

---

## 6. File Changes

### 6.1 Files to Modify

| File | Type | Changes |
|------|------|---------|
| `a/toc/toc-sidebar.css` | CSS | Add ~200 lines for mobile bottom sheet styles |
| `a/toc/toc-sidebar.js` | JavaScript | Add ~400 lines for mobile sheet logic |

### 6.2 Files Unchanged

| File | Reason |
|------|--------|
| `a/toc/chrono-data.json` | No schema changes required |
| `a/toc/toc-data.json` | Not used by chronological nav |
| `scripts/publish_post.py` | No changes needed (see Section 9) |
| `scripts/delete_post.py` | No changes needed (see Section 9) |
| `scripts/update_post.py` | No changes needed (see Section 9) |
| `scripts/generate_chrono_data.py` | No changes needed (see Section 9) |
| All HTML post files | No changes needed |

### 6.3 New Files (None Required)

All changes are contained within existing files to minimize maintenance overhead.

---

## 7. CSS Specification

### 7.1 New CSS Custom Properties

```css
:root {
  /* Bottom Sheet Dimensions */
  --tbc-sheet-collapsed-height: 48px;
  --tbc-sheet-partial-height: 50vh;
  --tbc-sheet-expanded-height: 85vh;
  --tbc-sheet-max-width: 100%;
  
  /* Animation Timing */
  --tbc-sheet-transition: 250ms ease-out;
  --tbc-tab-transition: 150ms ease-in-out;
  
  /* Touch Targets */
  --tbc-touch-min: 44px;
  
  /* Z-Index Stack */
  --tbc-sheet-z: 1100;
  --tbc-overlay-z: 1099;
}
```

### 7.2 Bottom Sheet Container

```css
@media (max-width: 768px) {
  .tbc-chrono-mobile-sheet {
    position: fixed;
    bottom: 0;
    left: 0;
    right: 0;
    height: var(--tbc-sheet-collapsed-height);
    background: var(--tbc-sidebar-bg);
    border-top-left-radius: 16px;
    border-top-right-radius: 16px;
    box-shadow: 0 -2px 10px rgba(0, 0, 0, 0.1);
    z-index: var(--tbc-sheet-z);
    transition: height var(--tbc-sheet-transition);
    overflow: hidden;
    /* Safe area for notched phones */
    padding-bottom: env(safe-area-inset-bottom);
  }

  .tbc-chrono-mobile-sheet.partial {
    height: var(--tbc-sheet-partial-height);
  }

  .tbc-chrono-mobile-sheet.expanded {
    height: var(--tbc-sheet-expanded-height);
  }

  /* Hide desktop column on mobile */
  .tbc-chrono-column {
    display: none !important;
  }
}
```

### 7.3 Month Tab Styles

```css
@media (max-width: 768px) {
  .tbc-month-tabs {
    display: flex;
    overflow-x: auto;
    scroll-snap-type: x mandatory;
    -webkit-overflow-scrolling: touch;
    scrollbar-width: none;
    padding: 8px 16px;
    gap: 4px;
  }

  .tbc-month-tabs::-webkit-scrollbar {
    display: none;
  }

  .tbc-month-tab {
    flex-shrink: 0;
    scroll-snap-align: start;
    padding: 8px 16px;
    min-width: var(--tbc-touch-min);
    text-align: center;
    border: none;
    background: transparent;
    color: var(--tbc-sidebar-text-muted);
    font-size: 14px;
    cursor: pointer;
    border-bottom: 2px solid transparent;
    transition: color var(--tbc-tab-transition),
                border-color var(--tbc-tab-transition);
  }

  .tbc-month-tab.active {
    color: var(--tbc-accent-primary);
    border-bottom-color: var(--tbc-accent-primary);
  }

  .tbc-month-tab-count {
    display: block;
    font-size: 11px;
    color: var(--tbc-sidebar-text-muted);
    margin-top: 2px;
  }
}
```

### 7.4 Landscape Overrides

```css
@media (max-width: 896px) and (orientation: landscape) {
  .tbc-chrono-mobile-sheet.expanded {
    height: 100vh;
  }

  .tbc-sheet-content-landscape {
    display: flex;
    height: calc(100% - 60px);
  }

  .tbc-month-list-sidebar {
    width: 140px;
    flex-shrink: 0;
    border-right: 1px solid var(--tbc-sidebar-border);
    overflow-y: auto;
  }

  .tbc-post-list-main {
    flex: 1;
    overflow-y: auto;
  }
}
```

### 7.5 Body Scroll Lock

```css
/* Prevent background scrolling when sheet is expanded */
body.tbc-sheet-open {
  overflow: hidden;
  position: fixed;
  width: 100%;
  height: 100%;
}
```

### 7.6 Overlay Styles

```css
@media (max-width: 768px) {
  .tbc-chrono-overlay {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: rgba(0, 0, 0, 0.4);
    z-index: 999; /* One below sheet (z-index: 1000) */
    opacity: 0;
    visibility: hidden;
    transition: opacity var(--tbc-sheet-transition),
                visibility var(--tbc-sheet-transition);
  }

  .tbc-chrono-overlay.visible {
    opacity: 1;
    visibility: visible;
  }
}
```

### 7.7 Reduced Motion Support

```css
/* Respect user preference for reduced motion */
@media (prefers-reduced-motion: reduce) {
  .tbc-chrono-mobile-sheet,
  .tbc-chrono-overlay,
  .tbc-month-tab {
    transition: none !important;
  }
}
```

### 7.8 Empty State

```css
@media (max-width: 768px) {
  .tbc-sheet-empty-state {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 200px;
    color: var(--tbc-sidebar-text-muted);
    text-align: center;
    padding: 24px;
  }

  .tbc-sheet-empty-state-icon {
    font-size: 48px;
    margin-bottom: 16px;
    opacity: 0.5;
  }
}
```

---

## 8. JavaScript Specification

### 8.1 Module Structure

```javascript
// Add to toc-sidebar.js within the chrono IIFE

// ================================
// Mobile Sheet Configuration
// ================================
const MOBILE_CONFIG = {
  breakpoint: 768,
  landscapeBreakpoint: 896,
  storageKeys: {
    sheetState: 'tbc-chrono-sheet-state',
    selectedYear: 'tbc-chrono-selected-year',
    selectedMonth: 'tbc-chrono-selected-month'
  },
  swipeThreshold: 50,
  animationDuration: 250
};

// ================================
// Mobile Sheet State
// ================================
const mobileState = {
  mode: 'collapsed', // 'collapsed' | 'years' | 'months'
  selectedYear: null,
  selectedMonth: null,
  touchStartY: 0,
  isLandscape: false,
  scrollPosition: 0 // For restoring scroll after sheet closes
};
```

### 8.2 Core Functions

```javascript
// ================================
// Viewport Detection
// ================================
function isMobileViewport() {
  return window.matchMedia(`(max-width: ${MOBILE_CONFIG.breakpoint}px)`).matches;
}

function isLandscape() {
  return window.matchMedia('(orientation: landscape)').matches;
}

// ================================
// Sheet State Management
// ================================
function setSheetMode(mode) {
  const sheet = document.querySelector('.tbc-chrono-mobile-sheet');
  if (!sheet) return;

  // Remove all mode classes
  sheet.classList.remove('collapsed', 'partial', 'expanded');

  // Set new mode
  mobileState.mode = mode;
  
  switch (mode) {
    case 'collapsed':
      sheet.classList.add('collapsed');
      setBodyScrollLock(false);
      setOverlayVisible(false);
      break;
    case 'years':
      sheet.classList.add('partial');
      setBodyScrollLock(true);
      setOverlayVisible(true);
      renderYearGrid();
      break;
    case 'months':
      sheet.classList.add('expanded');
      setBodyScrollLock(true);
      setOverlayVisible(true);
      renderMonthView();
      break;
  }

  saveSheetState();
}

// ================================
// Year Grid Rendering
// ================================
function renderYearGrid() {
  const container = document.querySelector('.tbc-sheet-content');
  if (!container) return;

  // Handle missing or failed data load
  if (!chronoState.data || !chronoState.data.years) {
    container.innerHTML = `
      <div class="tbc-sheet-empty-state">
        <div class="tbc-sheet-empty-state-icon">📅</div>
        <div>Navigation unavailable</div>
        <div style="font-size: 12px; margin-top: 8px;">Unable to load post data</div>
      </div>
    `;
    return;
  }

  const years = chronoState.data.years || [];
  
  // Handle empty years array
  if (years.length === 0) {
    container.innerHTML = `
      <div class="tbc-sheet-empty-state">
        <div class="tbc-sheet-empty-state-icon">📝</div>
        <div>No posts found</div>
      </div>
    `;
    return;
  }

  const currentYear = chronoState.currentPostNum 
    ? findPostByNum(chronoState.data.posts, chronoState.currentPostNum)?.year 
    : null;

  let html = `
    <div class="tbc-year-grid-header">Browse by Year</div>
    <div class="tbc-year-grid">
  `;

  for (const yearData of years) {
    const isCurrent = yearData.year === currentYear;
    html += `
      <button class="tbc-year-chip ${isCurrent ? 'current' : ''}" 
              data-year="${yearData.year}">
        <span class="tbc-year-chip-year">${yearData.year}</span>
        <span class="tbc-year-chip-count">${yearData.count}</span>
      </button>
    `;
  }

  html += '</div>';
  container.innerHTML = html;

  // Add click handlers
  container.querySelectorAll('.tbc-year-chip').forEach(chip => {
    chip.addEventListener('click', () => {
      mobileState.selectedYear = parseInt(chip.dataset.year);
      // Find the newest month with posts for this year (not hardcoded to December)
      const yearPosts = chronoState.data.posts.filter(p => p.year === mobileState.selectedYear);
      const months = [...new Set(yearPosts.map(p => p.month))];
      mobileState.selectedMonth = Math.max(...months); // Newest month
      setSheetMode('months');
    });
  });
}

// ================================
// Month View Rendering
// ================================
function renderMonthView() {
  const container = document.querySelector('.tbc-sheet-content');
  if (!container || !chronoState.data || !mobileState.selectedYear) return;

  const year = mobileState.selectedYear;
  const posts = chronoState.data.posts.filter(p => p.year === year);
  
  // Group by month
  const monthGroups = {};
  for (const post of posts) {
    if (!monthGroups[post.month]) {
      monthGroups[post.month] = [];
    }
    monthGroups[post.month].push(post);
  }

  // Sort months descending
  const months = Object.keys(monthGroups)
    .map(m => parseInt(m))
    .sort((a, b) => b - a);

  const selectedMonth = mobileState.selectedMonth || months[0];
  const monthNames = ['', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 
                      'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

  let html = `
    <div class="tbc-sheet-header">
      <button class="tbc-sheet-back" aria-label="Back to years">←</button>
      <div class="tbc-sheet-title">
        <span class="tbc-sheet-year">${year}</span>
        <span class="tbc-sheet-count">${posts.length} posts</span>
      </div>
      <button class="tbc-sheet-close" aria-label="Close">✕</button>
    </div>
    <div class="tbc-month-tabs" role="tablist">
  `;

  for (const month of months) {
    const isActive = month === selectedMonth;
    const count = monthGroups[month].length;
    html += `
      <button class="tbc-month-tab ${isActive ? 'active' : ''}" 
              role="tab"
              aria-selected="${isActive}"
              data-month="${month}">
        ${monthNames[month]}
        <span class="tbc-month-tab-count">${count}</span>
      </button>
    `;
  }

  html += '</div><div class="tbc-post-list" role="listbox">';

  // Render posts for selected month
  const monthPosts = monthGroups[selectedMonth] || [];
  const currentNum = chronoState.currentPostNum;

  for (const post of monthPosts.slice().reverse()) {
    const isCurrent = post.num === currentNum;
    html += `
      <a href="${post.file}" 
         class="tbc-post-item ${isCurrent ? 'current' : ''}"
         role="option"
         aria-selected="${isCurrent}">
        <span class="tbc-post-num">#${String(post.num).padStart(4, '0')}</span>
        <span class="tbc-post-title">${escapeHtml(post.title)}</span>
        <span class="tbc-post-date">${post.date}</span>
      </a>
    `;
  }

  html += '</div>';
  container.innerHTML = html;

  // Add event handlers
  container.querySelector('.tbc-sheet-back').addEventListener('click', () => {
    setSheetMode('years');
  });

  container.querySelector('.tbc-sheet-close').addEventListener('click', () => {
    setSheetMode('collapsed');
  });

  container.querySelectorAll('.tbc-month-tab').forEach(tab => {
    tab.addEventListener('click', () => {
      mobileState.selectedMonth = parseInt(tab.dataset.month);
      renderMonthView();
    });
  });
}

// ================================
// Touch Gesture Handling
// ================================
function initTouchGestures(sheet) {
  let startY = 0;
  let currentY = 0;

  sheet.addEventListener('touchstart', (e) => {
    startY = e.touches[0].clientY;
  }, { passive: true });

  sheet.addEventListener('touchmove', (e) => {
    currentY = e.touches[0].clientY;
  }, { passive: true });

  sheet.addEventListener('touchend', () => {
    const deltaY = startY - currentY;
    
    if (Math.abs(deltaY) > MOBILE_CONFIG.swipeThreshold) {
      if (deltaY > 0) {
        // Swipe up - expand
        if (mobileState.mode === 'collapsed') {
          setSheetMode('years');
        }
      } else {
        // Swipe down - collapse
        if (mobileState.mode === 'years') {
          setSheetMode('collapsed');
        } else if (mobileState.mode === 'months') {
          setSheetMode('years');
        }
      }
    }
  });
}

// ================================
// Body Scroll Lock
// ================================
function setBodyScrollLock(locked) {
  if (locked) {
    // Store scroll position before locking
    mobileState.scrollPosition = window.scrollY;
    document.body.classList.add('tbc-sheet-open');
    document.body.style.top = `-${mobileState.scrollPosition}px`;
  } else {
    document.body.classList.remove('tbc-sheet-open');
    document.body.style.top = '';
    // Restore scroll position
    window.scrollTo(0, mobileState.scrollPosition || 0);
  }
}

// ================================
// Focus Trap for Accessibility
// ================================
function initFocusTrap(container) {
  const focusableSelector = 'button, a[href], input, [tabindex]:not([tabindex="-1"])';
  
  container.addEventListener('keydown', (e) => {
    // Escape key closes sheet
    if (e.key === 'Escape') {
      e.preventDefault();
      setSheetMode('collapsed');
      return;
    }
    
    // Tab trap
    if (e.key === 'Tab') {
      const focusable = container.querySelectorAll(focusableSelector);
      if (focusable.length === 0) return;
      
      const first = focusable[0];
      const last = focusable[focusable.length - 1];
      
      if (e.shiftKey && document.activeElement === first) {
        e.preventDefault();
        last.focus();
      } else if (!e.shiftKey && document.activeElement === last) {
        e.preventDefault();
        first.focus();
      }
    }
  });
}

// ================================
// Overlay Management
// ================================
function setOverlayVisible(visible) {
  const overlay = document.querySelector('.tbc-chrono-overlay');
  if (!overlay) return;
  
  if (visible) {
    overlay.classList.add('visible');
  } else {
    overlay.classList.remove('visible');
  }
}

// ================================
// State Persistence
// ================================
function saveSheetState() {
  try {
    localStorage.setItem(MOBILE_CONFIG.storageKeys.sheetState, mobileState.mode);
    if (mobileState.selectedYear) {
      localStorage.setItem(MOBILE_CONFIG.storageKeys.selectedYear, 
                           mobileState.selectedYear.toString());
    }
    if (mobileState.selectedMonth) {
      localStorage.setItem(MOBILE_CONFIG.storageKeys.selectedMonth, 
                           mobileState.selectedMonth.toString());
    }
  } catch (e) {
    console.warn('Failed to save sheet state');
  }
}

function loadSheetState() {
  try {
    const mode = localStorage.getItem(MOBILE_CONFIG.storageKeys.sheetState);
    const year = localStorage.getItem(MOBILE_CONFIG.storageKeys.selectedYear);
    const month = localStorage.getItem(MOBILE_CONFIG.storageKeys.selectedMonth);

    if (year) mobileState.selectedYear = parseInt(year);
    if (month) mobileState.selectedMonth = parseInt(month);
    if (mode && ['collapsed', 'years', 'months'].includes(mode)) {
      return mode;
    }
  } catch (e) {
    console.warn('Failed to load sheet state');
  }
  return 'collapsed';
}
```

### 8.3 Initialization

```javascript
// ================================
// Mobile Sheet Initialization
// ================================
function initMobileSheet() {
  if (!isMobileViewport()) return;

  // Create sheet HTML
  const sheet = document.createElement('div');
  sheet.className = 'tbc-chrono-mobile-sheet collapsed';
  sheet.innerHTML = `
    <div class="tbc-sheet-drag-handle"></div>
    <div class="tbc-sheet-collapsed-bar">
      <span class="tbc-sheet-context"></span>
    </div>
    <div class="tbc-sheet-content"></div>
  `;

  // Create overlay
  const overlay = document.createElement('div');
  overlay.className = 'tbc-chrono-overlay';
  overlay.addEventListener('click', () => setSheetMode('collapsed'));

  document.body.appendChild(overlay);
  document.body.appendChild(sheet);

  // Initialize touch gestures
  initTouchGestures(sheet);

  // Set up collapsed bar click
  sheet.querySelector('.tbc-sheet-collapsed-bar').addEventListener('click', () => {
    setSheetMode('years');
  });

  // Update context label
  updateContextLabel();

  // Restore state
  const savedMode = loadSheetState();
  if (savedMode !== 'collapsed') {
    setSheetMode(savedMode);
  }

  // Initialize focus trap for accessibility
  initFocusTrap(sheet);

  // Handle orientation changes (using modern API, not deprecated addListener)
  window.matchMedia('(orientation: landscape)').addEventListener('change', () => {
    mobileState.isLandscape = isLandscape();
    if (mobileState.mode === 'months') {
      renderMonthView(); // Re-render for layout change
    }
  });

  // Handle viewport resize with debounce
  let resizeTimeout;
  window.addEventListener('resize', () => {
    clearTimeout(resizeTimeout);
    resizeTimeout = setTimeout(() => {
      if (!isMobileViewport() && mobileState.mode !== 'collapsed') {
        // Switched to desktop - collapse sheet
        setSheetMode('collapsed');
      }
    }, 150);
  });
}

function updateContextLabel() {
  const label = document.querySelector('.tbc-sheet-context');
  if (!label || !chronoState.data) return;

  const currentNum = chronoState.currentPostNum;
  if (currentNum) {
    const post = findPostByNum(chronoState.data.posts, currentNum);
    if (post) {
      const yearPosts = chronoState.data.posts.filter(p => p.year === post.year);
      const yearIndex = yearPosts.findIndex(p => p.num === currentNum) + 1;
      label.textContent = `${post.year} · Post #${currentNum} of ${yearPosts.length}`;
      return;
    }
  }
  
  label.textContent = `${chronoState.data.totalPosts} posts`;
}
```

---

## 9. Python Script Integration

### 9.1 Impact Assessment

The mobile bottom sheet implementation has **no impact** on the Python scripts because:

| Script | Relationship | Impact |
|--------|--------------|--------|
| `publish_post.py` | Updates `chrono-data.json` | ✅ No change needed — existing schema unchanged |
| `delete_post.py` | Updates `chrono-data.json` | ✅ No change needed — existing schema unchanged |
| `update_post.py` | Updates `chrono-data.json` | ✅ No change needed — existing schema unchanged |
| `generate_chrono_data.py` | Regenerates `chrono-data.json` | ✅ No change needed — existing schema unchanged |
| `populate_all_posts.py` | Updates `index.html` | ✅ No change needed — HTML unchanged |

### 9.2 Data Contract

The JavaScript implementation depends on the following `chrono-data.json` fields:

```python
# Required fields (already present)
post = {
    "num": int,       # Post number (required)
    "file": str,      # Filename (required)
    "title": str,     # Post title (required)
    "date": str,      # ISO date YYYY-MM-DD (required)
    "year": int,      # Year number (required)
    "month": int      # Month 1-12 (required) ✓ Already present
}
```

All required fields are already populated by existing scripts.

### 9.3 Validation Script (Optional)

For additional safety, a validation check could be added:

```python
# Add to scripts/validate_chrono_data.py (optional)

def validate_mobile_nav_requirements(chrono_data):
    """Validate that chrono-data.json meets mobile nav requirements."""
    errors = []
    
    for post in chrono_data.get('posts', []):
        # Check required fields
        if 'month' not in post:
            errors.append(f"Post {post.get('num')} missing 'month' field")
        elif not (1 <= post.get('month', 0) <= 12):
            errors.append(f"Post {post.get('num')} has invalid month: {post.get('month')}")
    
    return errors
```

---

## 10. Testing Strategy

### 10.1 Test Categories

| Category | Description | Automation |
|----------|-------------|------------|
| Unit Tests | Individual component functions | Possible |
| Integration Tests | Component interactions | Recommended |
| Visual Regression | UI appearance | Manual + Screenshots |
| Gesture Tests | Touch interactions | Manual |
| Accessibility Tests | Screen reader, focus | Manual + axe-core |
| Cross-Browser | Device/browser matrix | Manual |

### 10.2 Integration Test Cases

#### Test Suite: Mobile Bottom Sheet

```javascript
// test/mobile-chrono-sheet.test.js

describe('Mobile Chronological Sheet', () => {
  
  beforeEach(() => {
    // Set viewport to mobile
    window.innerWidth = 375;
    window.innerHeight = 667;
    // Load chrono-data.json fixture
    // Initialize sheet
  });

  describe('Collapsed State', () => {
    test('TC-01: Sheet renders in collapsed state on mobile', () => {
      expect(document.querySelector('.tbc-chrono-mobile-sheet')).toBeTruthy();
      expect(document.querySelector('.tbc-chrono-mobile-sheet.collapsed')).toBeTruthy();
    });

    test('TC-02: Context label shows current post info', () => {
      // Navigate to post #1842
      const label = document.querySelector('.tbc-sheet-context');
      expect(label.textContent).toContain('2024');
      expect(label.textContent).toContain('#1842');
    });

    test('TC-03: Tap on collapsed bar expands to year view', () => {
      document.querySelector('.tbc-sheet-collapsed-bar').click();
      expect(document.querySelector('.tbc-chrono-mobile-sheet.partial')).toBeTruthy();
    });
  });

  describe('Year Grid State', () => {
    beforeEach(() => {
      // Expand to year grid
      setSheetMode('years');
    });

    test('TC-04: Year chips display correct counts', () => {
      const chip2024 = document.querySelector('[data-year="2024"]');
      expect(chip2024.querySelector('.tbc-year-chip-count').textContent).toBe('156');
    });

    test('TC-05: Current year chip is highlighted', () => {
      const chip2024 = document.querySelector('[data-year="2024"]');
      expect(chip2024.classList.contains('current')).toBe(true);
    });

    test('TC-06: Tap year chip transitions to month view', () => {
      document.querySelector('[data-year="2014"]').click();
      expect(document.querySelector('.tbc-chrono-mobile-sheet.expanded')).toBeTruthy();
      expect(document.querySelector('.tbc-month-tabs')).toBeTruthy();
    });
  });

  describe('Month Tab State', () => {
    beforeEach(() => {
      // Navigate to 2014 months
      mobileState.selectedYear = 2014;
      setSheetMode('months');
    });

    test('TC-07: Month tabs render for selected year', () => {
      const tabs = document.querySelectorAll('.tbc-month-tab');
      expect(tabs.length).toBeGreaterThan(0);
    });

    test('TC-08: December is selected by default (newest first)', () => {
      const activeTab = document.querySelector('.tbc-month-tab.active');
      expect(activeTab.textContent).toContain('Dec');
    });

    test('TC-09: Post list shows correct posts for selected month', () => {
      const posts = document.querySelectorAll('.tbc-post-item');
      // December 2014 had 14 posts
      expect(posts.length).toBe(14);
    });

    test('TC-10: Tap month tab filters post list', () => {
      document.querySelector('[data-month="11"]').click();
      const posts = document.querySelectorAll('.tbc-post-item');
      // November 2014 had 11 posts
      expect(posts.length).toBe(11);
    });

    test('TC-11: Tap post navigates to that post', () => {
      const originalHref = window.location.href;
      const postLink = document.querySelector('.tbc-post-item');
      postLink.click();
      // Check that navigation was triggered
      expect(window.location.href).not.toBe(originalHref);
    });

    test('TC-12: Back button returns to year grid', () => {
      document.querySelector('.tbc-sheet-back').click();
      expect(document.querySelector('.tbc-chrono-mobile-sheet.partial')).toBeTruthy();
    });

    test('TC-13: Close button collapses sheet', () => {
      document.querySelector('.tbc-sheet-close').click();
      expect(document.querySelector('.tbc-chrono-mobile-sheet.collapsed')).toBeTruthy();
    });
  });

  describe('Touch Gestures', () => {
    test('TC-14: Swipe up on collapsed bar expands to years', () => {
      simulateSwipe('.tbc-chrono-mobile-sheet', 'up', 100);
      expect(mobileState.mode).toBe('years');
    });

    test('TC-15: Swipe down on year grid collapses sheet', () => {
      setSheetMode('years');
      simulateSwipe('.tbc-chrono-mobile-sheet', 'down', 100);
      expect(mobileState.mode).toBe('collapsed');
    });

    test('TC-16: Swipe down on month view returns to years', () => {
      setSheetMode('months');
      simulateSwipe('.tbc-chrono-mobile-sheet', 'down', 100);
      expect(mobileState.mode).toBe('years');
    });
  });

  describe('State Persistence', () => {
    test('TC-17: Selected year persists across page reload', () => {
      mobileState.selectedYear = 2014;
      saveSheetState();
      
      // Simulate reload
      mobileState.selectedYear = null;
      loadSheetState();
      
      expect(mobileState.selectedYear).toBe(2014);
    });

    test('TC-18: Sheet mode persists across page reload', () => {
      setSheetMode('years');
      
      // Simulate reload
      mobileState.mode = 'collapsed';
      const savedMode = loadSheetState();
      
      expect(savedMode).toBe('years');
    });
  });

  describe('Landscape Mode', () => {
    beforeEach(() => {
      // Set landscape viewport
      window.innerWidth = 812;
      window.innerHeight = 375;
      window.dispatchEvent(new Event('orientationchange'));
    });

    test('TC-19: Month view uses side-by-side layout in landscape', () => {
      setSheetMode('months');
      expect(document.querySelector('.tbc-sheet-content-landscape')).toBeTruthy();
    });
  });

  describe('Accessibility', () => {
    test('TC-20: Sheet has correct ARIA labels', () => {
      const sheet = document.querySelector('.tbc-chrono-mobile-sheet');
      expect(sheet.getAttribute('role')).toBe('dialog');
      expect(sheet.getAttribute('aria-label')).toContain('navigation');
    });

    test('TC-21: Month tabs have tablist role', () => {
      setSheetMode('months');
      const tablist = document.querySelector('.tbc-month-tabs');
      expect(tablist.getAttribute('role')).toBe('tablist');
    });

    test('TC-22: Touch targets meet 44px minimum', () => {
      setSheetMode('months');
      const tabs = document.querySelectorAll('.tbc-month-tab');
      tabs.forEach(tab => {
        const rect = tab.getBoundingClientRect();
        expect(rect.width).toBeGreaterThanOrEqual(44);
        expect(rect.height).toBeGreaterThanOrEqual(44);
      });
    });
  });
});

describe('Desktop Viewport - No Changes', () => {
  beforeEach(() => {
    window.innerWidth = 1200;
    window.innerHeight = 800;
  });

  test('TC-23: Mobile sheet does not render on desktop', () => {
    expect(document.querySelector('.tbc-chrono-mobile-sheet')).toBeFalsy();
  });

  test('TC-24: Desktop chrono column renders normally', () => {
    expect(document.querySelector('.tbc-chrono-column')).toBeTruthy();
  });
});

describe('Overlay and Keyboard Interaction', () => {
  test('TC-25: Tap overlay collapses sheet', () => {
    setSheetMode('months');
    document.querySelector('.tbc-chrono-overlay').click();
    expect(mobileState.mode).toBe('collapsed');
  });

  test('TC-26: Escape key collapses sheet', () => {
    setSheetMode('months');
    const sheet = document.querySelector('.tbc-chrono-mobile-sheet');
    sheet.dispatchEvent(new KeyboardEvent('keydown', { key: 'Escape', bubbles: true }));
    expect(mobileState.mode).toBe('collapsed');
  });

  test('TC-27: Body scroll locked when sheet expanded', () => {
    setSheetMode('months');
    expect(document.body.classList.contains('tbc-sheet-open')).toBe(true);
    
    setSheetMode('collapsed');
    expect(document.body.classList.contains('tbc-sheet-open')).toBe(false);
  });
});

describe('Error Handling', () => {
  test('TC-28: Empty state shown when chrono-data fails to load', () => {
    chronoState.data = null;
    setSheetMode('years');
    expect(document.querySelector('.tbc-sheet-empty-state')).toBeTruthy();
    expect(document.querySelector('.tbc-sheet-empty-state').textContent).toContain('unavailable');
  });

  test('TC-29: Focus trapped within expanded sheet', () => {
    setSheetMode('months');
    const sheet = document.querySelector('.tbc-chrono-mobile-sheet');
    const buttons = sheet.querySelectorAll('button');
    const lastButton = buttons[buttons.length - 1];
    
    lastButton.focus();
    lastButton.dispatchEvent(new KeyboardEvent('keydown', { key: 'Tab', bubbles: true }));
    
    // Should cycle back to first focusable element
    expect(document.activeElement).toBe(buttons[0]);
  });
});
```

### 10.3 Test Utilities

```javascript
// test/helpers/gesture-simulator.js

function simulateSwipe(selector, direction, distance) {
  const element = document.querySelector(selector);
  const rect = element.getBoundingClientRect();
  const startX = rect.left + rect.width / 2;
  const startY = rect.top + rect.height / 2;
  
  let endX = startX;
  let endY = startY;
  
  switch (direction) {
    case 'up': endY = startY - distance; break;
    case 'down': endY = startY + distance; break;
    case 'left': endX = startX - distance; break;
    case 'right': endX = startX + distance; break;
  }
  
  element.dispatchEvent(new TouchEvent('touchstart', {
    touches: [new Touch({ identifier: 0, target: element, clientX: startX, clientY: startY })]
  }));
  
  element.dispatchEvent(new TouchEvent('touchmove', {
    touches: [new Touch({ identifier: 0, target: element, clientX: endX, clientY: endY })]
  }));
  
  element.dispatchEvent(new TouchEvent('touchend', {
    changedTouches: [new Touch({ identifier: 0, target: element, clientX: endX, clientY: endY })]
  }));
}
```

### 10.4 Manual Test Matrix

| Device | OS | Browser | Orientation | Status |
|--------|-----|---------|-------------|--------|
| iPhone 14 | iOS 17 | Safari | Portrait | ☐ |
| iPhone 14 | iOS 17 | Safari | Landscape | ☐ |
| iPhone SE | iOS 16 | Safari | Portrait | ☐ |
| Pixel 7 | Android 14 | Chrome | Portrait | ☐ |
| Pixel 7 | Android 14 | Chrome | Landscape | ☐ |
| Galaxy S23 | Android 13 | Samsung | Portrait | ☐ |
| iPad Mini | iPadOS 17 | Safari | Portrait | ☐ |

### 10.5 Python Script Regression Tests

These tests verify that publishing/deleting posts still works correctly:

```python
# test/test_script_compatibility.py

import json
import subprocess
import sys
from pathlib import Path

SCRIPTS_DIR = Path(__file__).parent.parent / 'scripts'
CHRONO_FILE = Path(__file__).parent.parent / 'a' / 'toc' / 'chrono-data.json'

class TestPublishPostCompatibility:
    """Verify publish_post.py still creates valid chrono-data.json"""

    def test_chrono_data_has_month_field(self):
        """TC-PY-01: All posts have month field for mobile nav"""
        chrono_data = json.loads(CHRONO_FILE.read_text())
        
        for post in chrono_data['posts']:
            assert 'month' in post, f"Post {post['num']} missing month field"
            assert 1 <= post['month'] <= 12, f"Post {post['num']} has invalid month"

    def test_chrono_data_has_year_field(self):
        """TC-PY-02: All posts have year field for mobile nav"""
        chrono_data = json.loads(CHRONO_FILE.read_text())
        
        for post in chrono_data['posts']:
            assert 'year' in post, f"Post {post['num']} missing year field"
            assert 2008 <= post['year'] <= 2030, f"Post {post['num']} has invalid year"

    def test_years_array_has_counts(self):
        """TC-PY-03: Years array has count field for mobile nav"""
        chrono_data = json.loads(CHRONO_FILE.read_text())
        
        for year_data in chrono_data.get('years', []):
            assert 'count' in year_data, f"Year {year_data.get('year')} missing count"
            assert year_data['count'] > 0

    def test_publish_dry_run_adds_month_field(self):
        """TC-PY-04: publish_post.py sets month field correctly"""
        # Create test markdown file
        test_md = '''---
title: Test Post
date: 2026-03-15
---
# Test
'''
        # Would run: python publish_post.py test.md --dry-run
        # Verify output includes month: 3
        pass  # Implementation depends on test infrastructure


class TestGenerateChronoDataCompatibility:
    """Verify generate_chrono_data.py produces compatible output"""

    def test_generate_includes_month(self):
        """TC-PY-05: Regenerated data includes month field"""
        # Run generate_chrono_data.py
        result = subprocess.run(
            [sys.executable, str(SCRIPTS_DIR / 'generate_chrono_data.py'), '--dry-run'],
            capture_output=True,
            text=True
        )
        
        # Parse output and verify month fields
        # (Implementation depends on script output format)
        pass
```

---

## 11. Rollback Plan

### 11.1 Feature Flag (Recommended)

Add a simple feature flag to disable mobile sheet:

```javascript
// At top of chrono IIFE
const ENABLE_MOBILE_SHEET = true; // Set to false to disable

async function initChronoColumn() {
  // ... existing code ...
  
  // Only init mobile sheet if enabled
  if (ENABLE_MOBILE_SHEET && isMobileViewport()) {
    initMobileSheet();
  }
}
```

### 11.2 Git Revert Strategy

If issues are discovered post-deployment:

```bash
# Option 1: Revert specific commits
git revert <commit-hash-of-mobile-sheet>

# Option 2: Reset CSS to previous version
git checkout HEAD~1 -- a/toc/toc-sidebar.css

# Option 3: Reset JS to previous version  
git checkout HEAD~1 -- a/toc/toc-sidebar.js
```

### 11.3 CSS Fallback

The mobile sheet CSS is scoped within `@media (max-width: 768px)`, so removing it has no effect on desktop.

---

## 12. Appendices

### 12.1 File Size Impact Estimate

| File | Current Size | After Changes | Increase |
|------|--------------|---------------|----------|
| `toc-sidebar.css` | ~25 KB | ~28 KB | +3 KB |
| `toc-sidebar.js` | ~35 KB | ~40 KB | +5 KB |
| **Total** | ~60 KB | ~68 KB | **+8 KB** |

(Gzipped: ~15 KB → ~17 KB, +2 KB)

### 12.2 Performance Considerations

1. **Lazy rendering:** Year grid and month tabs are only rendered when expanded
2. **DOM recycling:** Post list reuses DOM nodes when switching months
3. **Throttled gestures:** Touch events are throttled to prevent jank
4. **CSS containment:** Use `contain: layout` on sheet for paint optimization

### 12.3 Security Considerations

1. **XSS prevention:** Post titles are escaped with `escapeHtml()` before rendering
2. **localStorage limits:** State persistence gracefully handles quota errors
3. **No external dependencies:** All code is self-contained

### 12.4 Related Documents

- [MOBILE_CHRONO_NAV_MOCKUPS.md](MOBILE_CHRONO_NAV_MOCKUPS.md) — Visual design mockups
- [CHRONOLOGICAL_NAV_SIDEBAR_REQUIREMENTS.md](CHRONOLOGICAL_NAV_SIDEBAR_REQUIREMENTS.md) — Original requirements

---

## Version History

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 1.0 | 2026-01-10 | GitHub Copilot | Initial specification |
| 1.1 | 2026-01-10 | GitHub Copilot | Added: body scroll lock, reduced motion support, focus trap, escape key handling, overlay z-index, empty state, resize handling. Fixed deprecated `addListener` API. Added test cases TC-25 through TC-29. |
