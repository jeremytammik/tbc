# Mobile Chronological Navigation — Bottom Sheet with Month Tabs

## Design Overview

This document presents detailed UI mockups for an improved mobile navigation experience for the chronological timeline. The design uses a **bottom sheet pattern** with **horizontal month tabs** to reduce scrolling when browsing posts within a year.

### Key Benefits
- **Reduces scroll distance** by ~12x (month groups vs. full year list)
- **Familiar mobile pattern** (bottom sheets are standard on iOS/Android)
- **Progressive disclosure** — shows year grid first, then month tabs
- **Thumb-friendly** — primary interactions near bottom of screen

---

## Portrait Orientation

### State 1: Collapsed (Default View)

When viewing a post, the chronological navigation appears as a compact bar at the bottom.

```
┌─────────────────────────────────────┐
│ ≡                    🔍             │ ← Hamburger (TOC) + Search
├─────────────────────────────────────┤
│                                     │
│  ┌───────────────────────────────┐  │
│  │ ← Prev                  Next →│  │ ← Prev/Next navigation
│  │ #1841                    #1843│  │
│  └───────────────────────────────┘  │
│                                     │
│  Wall Geometry and                  │
│  Dimensioning Challenges            │
│  ═══════════════════════════════    │
│                                     │
│  Posted: 2024-03-15                 │
│                                     │
│  Today we explore how to extract    │
│  wall geometry data from Revit      │
│  elements using the API...          │
│                                     │
│  [Article content continues...]     │
│                                     │
│                                     │
│                                     │
├─────────────────────────────────────┤
│ ▲  2024 · Post #1842 of 156         │ ← Collapsed bar (tap to expand)
└─────────────────────────────────────┘
     ↑
   Drag handle or tap to expand
```

---

### State 2: Partially Expanded (Year Selection)

User taps or swipes up on the bottom bar. Shows year browser.

```
┌─────────────────────────────────────┐
│ ≡                    🔍             │
├─────────────────────────────────────┤
│                                     │
│  Wall Geometry and                  │
│  Dimensioning Challenges            │
│  ═══════════════════════════════    │
│                                     │
│  Posted: 2024-03-15                 │
│                                     │
│  Today we explore how to...         │
│                                     │
├─────────────────────────────────────┤
│ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ │ ← Drag handle
│                                     │
│   Browse by Year                    │
│   ──────────────                    │
│                                     │
│   ┌──────┐ ┌──────┐ ┌──────┐        │
│   │ 2026 │ │ 2025 │ │ 2024 │        │ ← Year chips
│   │  12  │ │ 142  │ │ 156  │        │   (count shown)
│   └──────┘ └──────┘ └─▔▔▔▔─┘        │
│                       ↑ current     │
│   ┌──────┐ ┌──────┐ ┌──────┐        │
│   │ 2023 │ │ 2022 │ │ 2021 │        │
│   │ 148  │ │ 139  │ │ 145  │        │
│   └──────┘ └──────┘ └──────┘        │
│                                     │
│   ┌──────┐ ┌──────┐ ┌──────┐        │
│   │ 2020 │ │ 2019 │ │ 2018 │        │
│   │ 132  │ │ 128  │ │ 124  │        │
│   └──────┘ └──────┘ └──────┘        │
│                                     │
│            ⋮ scroll for more        │
│                                     │
└─────────────────────────────────────┘
```

---

### State 3: Fully Expanded (Year Selected → Month Tabs)

User taps a year (2014). Sheet expands to show month tabs and post list.

```
┌─────────────────────────────────────┐
│ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ │ ← Drag to dismiss
├─────────────────────────────────────┤
│                                     │
│   ← Back          2014          ✕   │ ← Header with close
│                 156 posts           │
│                                     │
├─────────────────────────────────────┤
│                                     │
│  ◀ Dec  Nov  Oct  Sep  Aug  Jul  ▶  │ ← Horizontally scrollable
│    ▔▔▔                              │   month tabs
│    14                               │   (underline = selected)
│                                     │
├─────────────────────────────────────┤
│                                     │
│   December 2014 (14 posts)          │
│   ─────────────────────────         │
│                                     │
│   #892  Revit 2015 API Changes      │ ← Post list for month
│         2014-12-28                  │
│                                     │
│   #891  Element Filter Performance  │
│         2014-12-24                  │
│                                     │
│   #890  Room Boundary Detection     │
│         2014-12-21                  │
│                                     │
│   #889  Wall Geometry Deep Dive     │
│         2014-12-18                  │
│                                     │
│   #888  Parameter Binding Tips      │
│         2014-12-14                  │
│                                     │
│   #887  Linked File Coordinates     │
│         2014-12-11                  │
│                                     │
│            ⋮ scroll                 │
│                                     │
└─────────────────────────────────────┘
```

---

### State 4: Month Tab Switching (Swipe Animation)

User swipes left on month tabs or taps "Nov".

```
┌─────────────────────────────────────┐
│ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ │
├─────────────────────────────────────┤
│                                     │
│   ← Back          2014          ✕   │
│                 156 posts           │
│                                     │
├─────────────────────────────────────┤
│                                     │
│  ◀ Dec  Nov  Oct  Sep  Aug  Jul  ▶  │
│         ▔▔▔                         │ ← November now selected
│          11                         │
│                                     │
├─────────────────────────────────────┤
│                                     │
│   November 2014 (11 posts)          │
│   ─────────────────────────         │
│                                     │
│   #878  Family Instance Creation    │
│         2014-11-28                  │
│                                     │
│   #877  View Filters Deep Dive      │
│         2014-11-25                  │
│                                     │
│   #876  Solid Boolean Operations    │
│         2014-11-21                  │
│                                     │
│   #875  Curtain Wall Panels         │
│         2014-11-18                  │
│                                     │
│   #874  Schedule API Updates        │
│         2014-11-14                  │
│                                     │
│   #873  External Events             │
│         2014-11-11                  │
│                                     │
│            ⋮                        │
│                                     │
└─────────────────────────────────────┘
```

---

### State 5: Post Selected (Navigation Complete)

User taps a post. Sheet dismisses, page navigates.

```
┌─────────────────────────────────────┐
│ ≡                    🔍             │
├─────────────────────────────────────┤
│                                     │
│  ┌───────────────────────────────┐  │
│  │ ← Prev                  Next →│  │
│  │ #877                    #879  │  │
│  └───────────────────────────────┘  │
│                                     │
│  View Filters Deep Dive             │  ← New post loaded
│  ══════════════════════             │
│                                     │
│  Posted: 2014-11-25                 │
│                                     │
│  View filters in Revit allow you    │
│  to control element visibility...   │
│                                     │
│  [Article content...]               │
│                                     │
│                                     │
│                                     │
│                                     │
│                                     │
├─────────────────────────────────────┤
│ ▲  2014 · Post #878 of 156          │ ← Bar updates to new context
└─────────────────────────────────────┘
```

---

## Landscape Orientation

### State 1: Collapsed (Default View - Landscape)

In landscape, content has more horizontal space. Bottom bar remains compact.

```
┌───────────────────────────────────────────────────────────────────────────────┐
│ ≡                              The Building Coder                    🔍       │
├───────────────────────────────────────────────────────────────────────────────┤
│                                                                               │
│  ┌─────────────────────────────────────────────────────────────────────────┐  │
│  │ ← #1841 Previous Post                            Next Post #1843 →     │  │
│  └─────────────────────────────────────────────────────────────────────────┘  │
│                                                                               │
│  Wall Geometry and Dimensioning Challenges                                    │
│  ═════════════════════════════════════════                                    │
│                                                                               │
│  Posted: 2024-03-15                                                           │
│                                                                               │
│  Today we explore how to extract wall geometry data from Revit elements       │
│  using the API. This is particularly useful when you need to...              │
│                                                                               │
├───────────────────────────────────────────────────────────────────────────────┤
│ ▲  2024 · Post #1842 of 156                                                   │
└───────────────────────────────────────────────────────────────────────────────┘
```

---

### State 2: Year Selection (Landscape)

Year grid uses more columns in landscape for efficient space usage.

```
┌───────────────────────────────────────────────────────────────────────────────┐
│ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ │
├───────────────────────────────────────────────────────────────────────────────┤
│                                                                               │
│   Browse by Year                                                          ✕   │
│   ──────────────                                                              │
│                                                                               │
│   ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐     │
│   │ 2026 │ │ 2025 │ │ 2024 │ │ 2023 │ │ 2022 │ │ 2021 │ │ 2020 │ │ 2019 │     │
│   │  12  │ │ 142  │ │ 156  │ │ 148  │ │ 139  │ │ 145  │ │ 132  │ │ 128  │     │
│   └──────┘ └──────┘ └─▔▔▔▔─┘ └──────┘ └──────┘ └──────┘ └──────┘ └──────┘     │
│                       ↑ current                                               │
│   ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐     │
│   │ 2018 │ │ 2017 │ │ 2016 │ │ 2015 │ │ 2014 │ │ 2013 │ │ 2012 │ │ 2011 │     │
│   │ 124  │ │ 118  │ │ 115  │ │ 108  │ │ 156  │ │ 142  │ │ 128  │ │  98  │     │
│   └──────┘ └──────┘ └──────┘ └──────┘ └──────┘ └──────┘ └──────┘ └──────┘     │
│                                                                               │
│   ┌──────┐ ┌──────┐                                                           │
│   │ 2010 │ │ 2009 │                                                           │
│   │  85  │ │  42  │                                                           │
│   └──────┘ └──────┘                                                           │
│                                                                               │
└───────────────────────────────────────────────────────────────────────────────┘
```

---

### State 3: Month Tabs + Post List (Landscape)

In landscape, use a **side-by-side layout**: months on left, posts on right.

```
┌───────────────────────────────────────────────────────────────────────────────┐
│ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ │
├───────────────────────────────────────────────────────────────────────────────┤
│                                                                               │
│   ← Back                         2014 (156 posts)                         ✕   │
│                                                                               │
├───────────────────────┬───────────────────────────────────────────────────────┤
│                       │                                                       │
│   MONTHS              │   December 2014 (14 posts)                            │
│   ──────              │   ────────────────────────                            │
│                       │                                                       │
│   ▶ Dec (14)          │   #892  Revit 2015 API Changes           2014-12-28   │
│     Nov (11)          │   #891  Element Filter Performance       2014-12-24   │
│     Oct (13)          │   #890  Room Boundary Detection          2014-12-21   │
│     Sep (12)          │   #889  Wall Geometry Deep Dive          2014-12-18   │
│     Aug (15)          │   #888  Parameter Binding Tips           2014-12-14   │
│     Jul (11)          │   #887  Linked File Coordinates          2014-12-11   │
│     Jun (14)          │   #886  Transaction Groups               2014-12-07   │
│     May (12)          │   #885  Document Events                  2014-12-04   │
│     Apr (13)          │   #884  Family API Overview              2014-12-01   │
│     Mar (14)          │                                                       │
│     Feb (12)          │            ⋮ scroll for more                          │
│     Jan (15)          │                                                       │
│                       │                                                       │
└───────────────────────┴───────────────────────────────────────────────────────┘
```

---

### State 4: Different Month Selected (Landscape)

User taps "Mar" in the left column.

```
┌───────────────────────────────────────────────────────────────────────────────┐
│ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ │
├───────────────────────────────────────────────────────────────────────────────┤
│                                                                               │
│   ← Back                         2014 (156 posts)                         ✕   │
│                                                                               │
├───────────────────────┬───────────────────────────────────────────────────────┤
│                       │                                                       │
│   MONTHS              │   March 2014 (14 posts)                               │
│   ──────              │   ─────────────────────                               │
│                       │                                                       │
│     Dec (14)          │   #812  Stair Automation                 2014-03-28   │
│     Nov (11)          │   #811  MEP Connector Manager            2014-03-25   │
│     Oct (13)          │   #810  Roof Footprint Editing           2014-03-21   │
│     Sep (12)          │   #809  Structural Framing               2014-03-18   │
│     Aug (15)          │   #808  Rebar API Introduction           2014-03-14   │
│     Jul (11)          │   #807  Adaptive Components              2014-03-11   │
│     Jun (14)          │   #806  Worksharing Events               2014-03-07   │
│     May (12)          │   #805  Cloud Model Access               2014-03-04   │
│     Apr (13)          │   #804  IFC Export Options               2014-03-01   │
│   ▶ Mar (14)          │                                                       │
│     Feb (12)          │            ⋮ scroll for more                          │
│     Jan (15)          │                                                       │
│                       │                                                       │
└───────────────────────┴───────────────────────────────────────────────────────┘
```

---

## Component Details

### Month Tab Bar (Portrait - Zoomed)

```
┌─────────────────────────────────────────────────────┐
│                                                     │
│   ◀  Dec   Nov   Oct   Sep   Aug   Jul   Jun   ▶   │
│       ▔▔▔                                           │
│       14    11    13    12    15    11    14        │
│       ↑                                             │
│    Selected                                         │
│    (accent color                                    │
│     underline)                                      │
│                                                     │
└─────────────────────────────────────────────────────┘
        ←───── Swipe to scroll ─────→
```

### Year Chip (Zoomed)

```
┌─────────────────────────────────────┐
│                                     │
│   ┌─────────┐  ┌─────────┐          │
│   │  2024   │  │  2023   │          │
│   │         │  │         │          │
│   │   156   │  │   148   │          │
│   │  posts  │  │  posts  │          │
│   └─────────┘  └─────────┘          │
│       ↑                             │
│   Highlighted border                │
│   if current year                   │
│                                     │
│   Chip Specs:                       │
│   - Width: 72px                     │
│   - Height: 64px                    │
│   - Border-radius: 8px              │
│   - Year: 16px bold                 │
│   - Count: 13px regular             │
│                                     │
└─────────────────────────────────────┘
```

### Post List Item (Zoomed)

```
┌─────────────────────────────────────┐
│                                     │
│   #892  Revit 2015 API Changes      │ ← Post number + Title
│         2014-12-28              →   │ ← Date + Chevron
│   ─────────────────────────────     │ ← Subtle separator
│                                     │
│   #891  Element Filter Performance  │ ← Current post
│         2014-12-24              →   │   (highlighted bg)
│   ═════════════════════════════     │ ← Accent left border
│                                     │
│   #890  Room Boundary Detection     │
│         2014-12-21              →   │
│                                     │
│   Item Specs:                       │
│   - Padding: 12px 16px              │
│   - Post #: 13px semibold #666      │
│   - Title: 15px regular #333        │
│   - Date: 12px regular #999         │
│   - Min height: 56px                │
│   - Current: bg #f0f7ff             │
│                                     │
└─────────────────────────────────────┘
```

### Bottom Bar - Collapsed (Zoomed)

```
┌─────────────────────────────────────┐
│                                     │
│   ┌─────────────────────────────┐   │
│   │ ━━━ ← Drag handle           │   │
│   │                             │   │
│   │ ▲  2024 · Post #1842 of 156 │   │
│   │                             │   │
│   └─────────────────────────────┘   │
│                                     │
│   Bar Specs:                        │
│   - Height: 48px                    │
│   - Background: #f5f5f5             │
│   - Border-top: 1px solid #e0e0e0   │
│   - Drag handle: 32px × 4px         │
│   - Text: 14px #666                 │
│   - Safe area padding: 8px bottom   │
│                                     │
└─────────────────────────────────────┘
```

---

## Gesture Summary

| Gesture | Action |
|---------|--------|
| Tap bottom bar | Expand to year selection |
| Swipe up on bar | Expand to year selection |
| Tap year chip | Show month tabs for that year |
| Swipe left/right on months | Navigate between months (portrait) |
| Tap month tab/item | Jump to that month |
| Tap post | Navigate to post, dismiss sheet |
| Tap ✕ or swipe down | Dismiss sheet |
| Tap "← Back" | Return to year selection |

---

## Accessibility Considerations

```
Focus order:
1. Drag handle (aria-label="Expand timeline navigation")
2. Back button (if visible)
3. Year/Title heading
4. Close button
5. Month tabs (role="tablist")
6. Post list (role="listbox")

Screen reader announcements:
- "2014, 156 posts. December selected, 14 posts."
- "Post 892, Revit 2015 API Changes, December 28 2014. Link."
- "Navigated to November, 11 posts."

Keyboard navigation:
- Tab: Move between major sections
- Arrow keys: Navigate within tabs/list
- Enter/Space: Activate selection
- Escape: Dismiss sheet
```

---

## Animation Timing

| Transition | Duration | Easing |
|------------|----------|--------|
| Bar → Partial expand | 250ms | ease-out |
| Partial → Full expand | 200ms | ease-out |
| Month tab switch | 150ms | ease-in-out |
| Dismiss (swipe down) | 200ms | ease-in |
| Post list scroll | 60fps | native momentum |
| Year chip tap feedback | 100ms | ease-out |

---

## Color Specifications

| Element | Light Mode | Dark Mode (future) |
|---------|------------|-------------------|
| Sheet background | #ffffff | #1e1e1e |
| Drag handle | #cccccc | #555555 |
| Year chip bg | #f5f5f5 | #2d2d2d |
| Year chip border (current) | #0066cc | #3399ff |
| Month tab text | #666666 | #aaaaaa |
| Month tab active | #0066cc | #3399ff |
| Month tab underline | #0066cc | #3399ff |
| Post title | #333333 | #eeeeee |
| Post number | #666666 | #999999 |
| Post date | #999999 | #777777 |
| Current post bg | #f0f7ff | #1a2a3a |
| Separator | #e5e5e5 | #3d3d3d |

---

## Implementation Notes

### Data Requirements

The existing `chrono-data.json` already contains:
- `posts[].num` — Post number
- `posts[].title` — Post title  
- `posts[].date` — Full date (YYYY-MM-DD)
- `posts[].month` — Month number (1-12)
- `posts[].year` — Year number

This is sufficient for the bottom sheet implementation.

### CSS Breakpoints

```css
/* Portrait phone */
@media (max-width: 480px) and (orientation: portrait) {
  /* Stacked month tabs + post list */
}

/* Landscape phone */
@media (max-width: 896px) and (orientation: landscape) {
  /* Side-by-side month list + post list */
}

/* Tablet portrait */
@media (min-width: 481px) and (max-width: 768px) {
  /* Larger touch targets, more year chips per row */
}
```

### Touch Targets

All interactive elements meet minimum 44×44px touch target size per WCAG guidelines.

---

## Version History

| Version | Date | Changes |
|---------|------|---------|
| 1.0 | 2026-01-10 | Initial mockups for portrait and landscape |
