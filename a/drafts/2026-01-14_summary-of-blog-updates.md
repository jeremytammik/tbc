---
title: "Summary of Blog Updates Since Release 0.2078"
date: 2026-01-14
categories: [GitHub Pages, Getting Started]
tags: [github, updates, blog, offline, search]
---

### Summary of Blog Updates Since Release 0.2078

The Building Coder has undergone a major transformation since release 0.2078.0 (August 2025). Here's what's new.

#### Executive Summary

The Building Coder blog has migrated from Typepad to a fully self-hosted GitHub Pages site. Key highlights:

- **Offline-capable** — All 2,079 posts (2008-2025) work without internet
- **Dual navigation** — 58 topic categories + chronological timeline sidebars
- **Pagefind search** — Fast, sharded full-text search across 69,000+ words
- **Automated publishing** — Push a Markdown draft and GitHub Actions does the rest
- **7 releases** since August 2025, with version scheme change to the `1.x` era

#### Contents

- [The Big Picture](#2)
- [New Features](#3)
- [Search Capabilities](#4)
- [Publishing Workflow](#5)
- [Release History](#6)

#### <a name="2"></a> The Big Picture

**The Typepad era is over.** The Building Coder is now a fully self-hosted static site on GitHub Pages with complete offline capability. All 2,079 blog posts from 2008-2025 are preserved and accessible without an internet connection.

Key architectural changes:

- **13,500+ internal links** converted from Typepad URLs to local file paths
- **2,066 HTML fragments** wrapped with proper document structure
- All resources use relative paths for GitHub Pages compatibility

#### <a name="3"></a> New Features

**Wiki-Style TOC Sidebar** — Navigate 2,079 posts via:

- **58 topic categories** with 2,256 curated post links (left sidebar)
- **Chronological timeline** organized by year/month (right sidebar)
- **Real-time search** filtering across all content
- **Mobile responsive** hamburger menu on smaller screens

**Copy to Clipboard** — Hover over any code block to copy with one click.

**Mobile Bottom Sheet** — Swipe-enabled chronological navigation on phones with three states: collapsed, years, and months.

#### <a name="4"></a> Search Capabilities

The archive now features **Pagefind** search, replacing the previous 6.9MB custom index:

- **Sharded index** — Loads only 50-100KB per query
- **69,042 unique words** indexed across all posts
- **Multi-word AND search** — All terms must match
- **In-page highlighting** — Use `?highlight=term` URL parameter
- **Keyboard navigation** — F3/Shift+F3 for next/prev match
- **Content match indicators** with expandable excerpts

#### <a name="5"></a> Publishing Workflow

New posts can be added via GitHub Actions or local Python scripts:

1. Create a Markdown draft in `a/drafts/` with YAML front matter
2. Push to GitHub — the Action auto-publishes
3. Optionally assign to a topic via the "Manage Topics" workflow

Available scripts:

- `publish_post.py` — Convert Markdown to HTML
- `delete_post.py` — Remove posts from all indices
- `update_post.py` — Modify published post metadata
- `manage_topics.py` — Add/remove posts from topic categories

#### <a name="6"></a> Release History

| Release | Date | Summary |
|---------|------|---------|
| 0.2078.0 | Aug 29, 2025 | Last Typepad-era release (post 2078 by Pedro) |
| 1.2078.0 | Jan 5, 2026 | Complete re-architecture for GitHub Pages |
| 1.2078.1 | Jan 6, 2026 | Fixed faulty topic group links |
| 1.2079.0 | Jan 8, 2026 | First post (2079) in new era, content fixes |
| 1.2079.1 | Jan 10, 2026 | Content search functionality (WIP) |
| 1.2079.2 | Jan 13, 2026 | Content search completed |
| 1.2079.3 | Jan 14, 2026 | Contributions report and AI coding agent instructions |

The version scheme changed from `0.NNNN.x` (post count) to `1.NNNN.x` to mark the new self-hosted era.
