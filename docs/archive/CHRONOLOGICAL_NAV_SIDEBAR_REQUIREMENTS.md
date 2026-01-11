# Chronological Navigation Panel - Requirements Document

## Overview

This document specifies the requirements for **relocating chronological post navigation** from the left sidebar to a **right-side panel** on each blog post page. The existing left sidebar will retain **topic-based navigation only**.

---

## 📌 Design Goal

| Current State | Target State |
|---------------|--------------|
| Left sidebar contains both topic-based TOC AND chronological ordering | Left sidebar: **Topic-based TOC only** |
| No right panel | Right panel: **Chronological navigation** |
| Single navigation paradigm | Dual navigation: Topics (left) + Timeline (right) |

---

## 🖼️ Layout Sketch - Final Design

### Design Principle: Integrated Right Column

The chronological navigation on the right is **NOT a visually separate panel**. Instead, it shares the same background as the main content area, creating a seamless reading experience. The navigation elements appear as a natural extension of the content, similar to a book's margin notes or a document's side annotations.

### Desktop View (> 1024px)

```
┌──────────────────────────────────────────────────────────────────────────────────────────┐
│                            🏠 THE BUILDING CODER                                          │
│                     Revit API by Jeremy Tammik                                            │
├─────────────────────────┬────────────────────────────────────────────────────────────────┤
│                         │                                                                │
│  🔍 Search topics...    │  ┌─────────────────────────────────┐                           │
│                         │  │                                 │   ◀ Prev Post             │
│  ─────────────────────  │  │                                 │   #1234: Title...         │
│                         │  │                                 │                           │
│  📂 TOPICS              │  │     BLOG POST CONTENT           │   ─ ─ ─ ─ ─ ─ ─ ─         │
│                         │  │                                 │                           │
│  ▶ Custom Exporter      │  │     Article title, date,        │   ▶ Next Post             │
│  ▶ 2D Booleans          │  │     images, code samples,       │   #1236: Title...         │
│  ▼ Family API           │  │     and text content            │                           │
│    • Creating Defs      │  │                                 │   ─ ─ ─ ─ ─ ─ ─ ─         │
│    • Instance Placement │  │                                 │                           │
│    • Loading Family  ◀──│──│── (current)                     │   📆 YEARS                │
│  ▶ FilteredElemCollect  │  │                                 │   2024 (156)              │
│  ▶ Materials            │  │                                 │   2023 (142)              │
│  ▶ Geometry             │  │                                 │   2022 (138)              │
│  ▶ Parameters           │  │                                 │   ...                     │
│  ▶ Views                │  │                                 │                           │
│  ▶ Worksharing          │  │                                 │                           │
│  ...                    │  │                                 │                           │
│                         │  │                                 │                           │
│  ─────────────────────  │  │                                 │                           │
│  ℹ️ About               │  │                                 │                           │
│  📧 Contact             │  └─────────────────────────────────┘                           │
│                         │                                                                │
├─────────────────────────┼────────────────────────────────────────────────────────────────┤
│      LEFT SIDEBAR       │              UNIFIED CONTENT AREA                              │
│        280px            │        (Blog content + integrated timeline navigation)         │
│    (topic-based TOC)    │                      Flexible width                            │
│   Dark blue background  │                   Light/white background                       │
└─────────────────────────┴────────────────────────────────────────────────────────────────┘
```

**Key Visual Characteristics:**
- ✅ No border or dividing line between content and timeline
- ✅ Same background color (`#ffffff` or `#f5f5f5`) as blog content
- ✅ Timeline text styled as subtle margin annotations
- ✅ Prev/Next links use understated typography (not buttons)
- ✅ Year browser appears as lightweight text list, not a widget

**Content Maximization Principle:**
- ✅ Blog content takes ALL available horizontal space
- ✅ Timeline column is minimal width (~150px) and hugs right edge
- ✅ No fixed max-width on content - expands with viewport
- ✅ Timeline appears only when viewport is wide enough (> 1000px)
- ✅ On narrower screens, timeline moves to bottom (100% content width)

### Tablet View (768px - 1024px)

```
┌────────────────────────────────────────────────────────────────────┐
│                    🏠 THE BUILDING CODER                           │
├────────────────────────────────────────────────────────────────────┤
│  ☰ Topics                                                          │
├────────────────────────────────────────────────────────────────────┤
│                                                                    │
│   ┌────────────────────────────────────────────────────────────┐   │
│   │                                                            │   │
│   │                    BLOG POST CONTENT                       │   │
│   │                                                            │   │
│   │              (Full width when sidebar collapsed)           │   │
│   │                                                            │   │
│   └────────────────────────────────────────────────────────────┘   │
│                                                                    │
│   ◀ #1234 Previous Post        ·        Next Post #1236 ▶         │
│                                                                    │
│   2024 · 2023 · 2022 · 2021 · 2020 · ...     (inline year links)   │
│                                                                    │
└────────────────────────────────────────────────────────────────────┘
      ↑
  Hamburger menu
  opens left sidebar
```

**Tablet Behavior:**
- Timeline navigation moves **below content** (not a side column)
- Prev/Next appears as inline text links with centered dot separator
- Year browser becomes horizontal inline links

### Mobile View (< 768px)

```
┌──────────────────────────────────┐
│  ☰  🏠 BUILDING CODER            │
├──────────────────────────────────┤
│                                  │
│     BLOG POST CONTENT            │
│                                  │
│     (Full width)                 │
│                                  │
├──────────────────────────────────┤
│                                  │
│  ◀ Previous        Next ▶        │
│                                  │
│  2024 · 2023 · 2022 · 2021 · ... │
│                                  │
└──────────────────────────────────┘
      ↑
   Topics menu
   (overlay)
```

**Mobile Behavior:**
- Timeline navigation inline at bottom of content
- No separate right panel or overlay needed
- Year links as compact horizontal list

---

## 📋 Feature Specifications

### Right Column - Integrated Chronological Navigation

| Feature | Description | Priority |
|---------|-------------|----------|
| **Previous/Next Links** | Simple text links to adjacent posts | Must Have |
| **Current Post Indicator** | Subtle display of post number | Should Have |
| **Year Browser** | Compact list of years as text links | Must Have |
| **Post List by Year** | Click year to expand inline | Should Have |
| **Seamless Background** | Matches content area (no visual separation) | Must Have |
| **Subtle Typography** | Lighter weight, smaller size than content | Must Have |
| **Sticky Behavior** | Optionally follows scroll (CSS sticky) | Nice to Have |

### Visual Integration Requirements

| Requirement | Description |
|-------------|-------------|
| **No Border** | No vertical line or shadow separating from content |
| **Shared Background** | Same color as blog post content area |
| **Margin Annotation Style** | Appears as side notes, not a widget |
| **Reduced Visual Weight** | Smaller font, muted colors, no boxes |
| **Graceful Fade** | Content area max-width centers, timeline flows outside |

### Left Sidebar Changes

| Change | Current | Target |
|--------|---------|--------|
| Content | Topics + Chronological | **Topics only** |
| Structure | Mixed navigation | Pure topic-based TOC |
| Search | Searches all | Searches topics/titles only |

---

## 📐 Dimensions & Layout

### Design Priority: MAXIMIZE CONTENT SPACE

The blog content area should expand to use **all available horizontal space**. The timeline column is intentionally minimal and only appears when there's sufficient viewport width.

### Desktop (> 1000px)

| Element | Width | Notes |
|---------|-------|-------|
| Left Sidebar | 280px (default) | Resizable: 180px - 400px |
| Content Area | **Flexible (fills remaining)** | No max-width constraint, expands fully |
| Right Column | 150px fixed | Minimal, hugs right edge |
| Gap between content & timeline | 30px | Breathing room |

### Width Calculation

```
Viewport Width = Sidebar + Content + Gap + Timeline

Example at 1400px viewport:
- Sidebar: 280px
- Content: 1400 - 280 - 30 - 150 = 940px  ← MAXIMIZED
- Gap: 30px  
- Timeline: 150px

Example at 1800px viewport:
- Sidebar: 280px
- Content: 1800 - 280 - 30 - 150 = 1340px  ← SCALES UP
- Gap: 30px
- Timeline: 150px
```

### Integrated Column Behavior

| Property | Right Timeline Column |
|----------|----------------------|
| Width | Fixed 150px (minimal footprint) |
| Background | Same as content (`#ffffff` or `#f5f5f5`) |
| Border | None |
| Position | CSS `position: sticky; top: 100px;` |
| Typography | Smaller, lighter than main content |
| Visibility | Only on wide screens (> 1000px) |
| Flex behavior | `flex-shrink: 0` (doesn't compress) |

### Content Area Behavior

| Property | Blog Content Area |
|----------|------------------|
| Width | `flex: 1` (takes ALL remaining space) |
| Max-width | None (expands fully) |
| Min-width | 500px (before responsive collapse) |
| Padding | Internal padding only, no margin waste |

---

## 🎨 Visual Design

### Right Column Styling (Integrated, Not Panel)

| Element | Style | Value |
|---------|-------|-------|
| Background | Transparent/inherited | Same as content area |
| Border | None | No separation |
| Text color | Muted gray | `#6b7280` |
| Link color | Subtle blue | `#4b5563` → `#2563eb` on hover |
| Font size | Smaller | `13px` (vs 16px content) |
| Font weight | Light | `400` or `300` |
| Spacing | Generous | `line-height: 1.8` |

### Typography Hierarchy (Right Column)

| Element | Style |
|---------|-------|
| Section labels | 11px, uppercase, letter-spacing: 1px, `#9ca3af` |
| Prev/Next links | 13px, regular, with arrow symbols |
| Year links | 12px, tabular numbers, hover underline |
| Post titles (expanded) | 12px, truncated, gray → blue on hover |

### No Visual Separation

```css
/* Anti-pattern - DO NOT USE */
.tbc-chrono-panel {
  background: #f8fafc;      /* ❌ Different background */
  border-left: 1px solid;   /* ❌ Visible border */
  box-shadow: -2px 0 5px;   /* ❌ Shadow separation */
}

/* Correct approach - USE THIS */
.tbc-chrono-column {
  background: transparent;  /* ✅ Inherits content bg */
  border: none;             /* ✅ No borders */
  box-shadow: none;         /* ✅ No shadow */
}
```

---

## 🔧 Technical Implementation

### New Files

```
a/toc/
├── toc-sidebar.css         # Existing - ADD integrated right column styles
├── toc-sidebar.js          # Existing - ADD right column logic
├── toc-data.json           # Existing - unchanged (topic data)
└── chrono-data.json        # NEW: Chronological index
```

### chrono-data.json Structure

```json
{
  "version": "1.0",
  "lastUpdated": "2026-01-05",
  "totalPosts": 2078,
  "posts": [
    {
      "num": 2078,
      "file": "2078_latest_post.htm",
      "title": "Latest Post Title",
      "date": "2025-12-15",
      "year": 2025,
      "month": 12
    },
    {
      "num": 2077,
      "file": "2077_another_post.htm",
      "title": "Another Post Title",
      "date": "2025-12-10",
      "year": 2025,
      "month": 12
    }
  ],
  "years": [
    {"year": 2025, "count": 156, "firstPost": 1923, "lastPost": 2078},
    {"year": 2024, "count": 142, "firstPost": 1781, "lastPost": 1922},
    {"year": 2023, "count": 138, "firstPost": 1643, "lastPost": 1780}
  ]
}
```

### HTML Structure (Integrated Column)

```html
<!-- Injected into each blog post page -->
<div class="tbc-content-wrapper">
  <article class="tbc-blog-content">
    <!-- Existing blog post content -->
  </article>
  
  <aside class="tbc-chrono-column">
    <nav class="tbc-chrono-nav" aria-label="Post navigation">
      
      <div class="tbc-chrono-prevnext">
        <a href="prev.htm" class="tbc-chrono-prev">
          <span class="tbc-chrono-label">← Previous</span>
          <span class="tbc-chrono-title">#1234: Post Title...</span>
        </a>
        <a href="next.htm" class="tbc-chrono-next">
          <span class="tbc-chrono-label">Next →</span>
          <span class="tbc-chrono-title">#1236: Post Title...</span>
        </a>
      </div>
      
      <div class="tbc-chrono-years">
        <span class="tbc-chrono-section-label">Browse by Year</span>
        <ul>
          <li><a href="#" data-year="2024">2024</a> <span>(156)</span></li>
          <li><a href="#" data-year="2023">2023</a> <span>(142)</span></li>
          <!-- ... -->
        </ul>
      </div>
      
    </nav>
  </aside>
</div>
```

### CSS Structure (Integrated, No Panel)

```css
/* Content wrapper - flexbox layout, NO max-width constraint */
.tbc-content-wrapper {
  display: flex;
  gap: 30px;
  /* NO max-width - content expands to fill available space */
  padding: 20px;
  padding-right: 20px;
}

/* Main blog content - MAXIMIZED */
.tbc-blog-content {
  flex: 1;           /* Takes ALL remaining space */
  /* NO max-width constraint */
  min-width: 500px;  /* Collapse threshold */
}

/* Right column - MINIMAL footprint */
.tbc-chrono-column {
  width: 150px;      /* Fixed minimal width */
  flex-shrink: 0;    /* Never compress */
  background: transparent;  /* Same as content area */
  border: none;             /* No separation */
  padding-top: 20px;
}

/* Sticky positioning */
.tbc-chrono-nav {
  position: sticky;
  top: 100px;
}

/* Muted typography */
.tbc-chrono-column {
  font-size: 13px;
  color: #6b7280;
  line-height: 1.8;
}

/* Section labels */
.tbc-chrono-section-label {
  font-size: 11px;
  text-transform: uppercase;
  letter-spacing: 1px;
  color: #9ca3af;
  margin-bottom: 8px;
  display: block;
}

/* Links - subtle styling */
.tbc-chrono-column a {
  color: #4b5563;
  text-decoration: none;
}
.tbc-chrono-column a:hover {
  color: #2563eb;
  text-decoration: underline;
}

/* Responsive - collapse to bottom */
@media (max-width: 900px) {
  .tbc-content-wrapper {
    flex-direction: column;
  }
  .tbc-chrono-column {
    width: 100%;
    border-top: 1px solid #e5e7eb;
    padding-top: 30px;
    margin-top: 30px;
  }
  .tbc-chrono-nav {
    position: static;
  }
  .tbc-chrono-years ul {
    display: flex;
    flex-wrap: wrap;
    gap: 8px 16px;
  }
}
```

### JavaScript Functions (to add to toc-sidebar.js)

```javascript
// Right Column Functions (integrated, not panel)
TBCSidebar.chronoColumn = {
  
  // Load chronological data
  loadChronoData: function() { /* fetch chrono-data.json */ },
  
  // Render the integrated column
  renderChronoColumn: function() { /* build HTML structure */ },
  
  // Get prev/next posts for current page
  getPrevNext: function(currentFile) { /* return {prev, next} */ },
  
  // Render year browser (expandable list)
  renderYearBrowser: function() { /* compact year list */ },
  
  // Toggle year expansion (inline)
  toggleYear: function(year) { /* show/hide posts for year */ },
  
  // No resize needed - fixed width, integrated
  // No toggle needed - always visible on desktop
  
  // Responsive handling
  handleResponsive: function() { /* move to bottom on mobile */ }
};
```

---

## 📱 Responsive Behavior

| Screen Width | Left Sidebar | Content + Timeline |
|--------------|--------------|-------------------|
| > 1000px | Fixed 280px | Flexbox: **content expands fully** + timeline (150px) |
| < 1000px | Collapsible overlay | **100% content width**, timeline moves to bottom |

### Responsive Priority: Content First

When viewport narrows, the timeline is the first element to be removed from the side layout. The content area should **never be constrained** to make room for navigation—navigation adapts to content, not vice versa.

### Responsive Transition

On screens < 1000px, the right column moves below the content, giving **100% width to the blog content**:

```
┌────────────────────────────────────────┐
│  ☰  🏠 THE BUILDING CODER              │
├────────────────────────────────────────┤
│                                        │
│        BLOG POST CONTENT               │
│                                        │
│   (FULL WIDTH - no side constraints)   │
│                                        │
├────────────────────────────────────────┤
│                                        │
│   ◀ Previous Post      Next Post ▶     │
│                                        │
│   Years: 2024 · 2023 · 2022 · 2021...  │
│                                        │
└────────────────────────────────────────┘

Timeline moves to footer position, content gets
maximum available width at all viewport sizes.
```

---

## ⌨️ Keyboard Shortcuts

| Shortcut | Action |
|----------|--------|
| `]` or `→` (when not in input) | Go to next post |
| `[` or `←` (when not in input) | Go to previous post |
| `Escape` | Close any open overlay (left sidebar on mobile) |

---

## 💾 Persistence (localStorage)

| Key | Value | Description |
|-----|-------|-------------|
| `tbc-chrono-expanded-years` | Array | Which years are expanded in year browser |

*Note: No width/collapsed state needed since the column is integrated, not a toggleable panel.*

---

## 🚀 Implementation Phases

### Phase 1: Data Preparation (1-2 hours)

| Task | Description |
|------|-------------|
| 1.1 | Create script to generate `chrono-data.json` |
| 1.2 | Extract post numbers, titles, dates from all .htm files |
| 1.3 | Calculate year groupings and counts |
| 1.4 | Validate JSON output |

### Phase 2: CSS Styling (2-3 hours)

| Task | Description |
|------|-------------|
| 2.1 | Add content wrapper flexbox layout |
| 2.2 | Style integrated right column (transparent bg, no border) |
| 2.3 | Style prev/next navigation links (subtle, text-based) |
| 2.4 | Style year browser (compact, muted typography) |
| 2.5 | Add sticky positioning for desktop |
| 2.6 | Implement responsive breakpoint (< 900px moves to bottom) |

### Phase 3: JavaScript Implementation (2-3 hours)

| Task | Description |
|------|-------------|
| 3.1 | Load chrono-data.json |
| 3.2 | Render integrated column structure |
| 3.3 | Implement prev/next detection based on current file |
| 3.4 | Add year browser with expand/collapse |
| 3.5 | Add keyboard shortcuts for navigation |
| 3.6 | Save expanded years to localStorage |

### Phase 4: Integration (1-2 hours)

| Task | Description |
|------|-------------|
| 4.1 | Remove chronological content from left sidebar |
| 4.2 | Wrap existing content in flexbox wrapper |
| 4.3 | Test on sample pages |
| 4.4 | Validate responsive behavior |

### Phase 5: Deployment (1 hour)

| Task | Description |
|------|-------------|
| 5.1 | Update all 2000+ .htm files with new wrapper structure |
| 5.2 | Test on GitHub Pages |
| 5.3 | Document any changes |

---

## ✅ Acceptance Criteria

1. **Left sidebar** displays topic-based TOC only (no chronological elements)
2. **Right column** is visually integrated with content (same background, no border)
3. **Right column** shows:
   - Previous post link with subtle arrow and truncated title
   - Next post link with subtle arrow and truncated title
   - Compact year browser with expandable year list
4. **Navigation** using prev/next links works correctly
5. **Year browser** expands inline to show posts from selected year
6. **Responsive design**: column moves below content on screens < 900px
7. **Keyboard shortcuts** work for prev/next navigation
8. **Sticky positioning** keeps timeline visible while scrolling (desktop)
9. **Visual integration**: no visible separation between content and timeline

---

## 📊 Success Metrics

| Metric | Target |
|--------|--------|
| Column render time | < 50ms |
| chrono-data.json size | < 200KB |
| Visual weight | Unobtrusive, doesn't distract from content |
| Mobile usability score | > 90 |
| Accessibility score | > 95 |

---

*Document Version: 1.1*  
*Created: January 5, 2026*  
*Updated: January 5, 2026 - Changed from separate panel to integrated column*  
*Status: Requirements Complete - Ready for Implementation*
