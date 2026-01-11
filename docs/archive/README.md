# Archived Documentation

This folder contains historical documentation from completed or superseded feature implementations.

## Contents

| File | Description | Status |
|------|-------------|--------|
| **SEARCH_INDEX_IMPLEMENTATION_SPEC.md** | Original 6.9MB JSON search index implementation | ❌ Superseded by Pagefind |
| **SEARCH_IMPROVEMENTS_IMPLEMENTATION_PLAN.md** | Improvements to old search index | ❌ Superseded by Pagefind |
| **SEARCH_ALTERNATIVES_ANALYSIS.md** | Evaluation of search alternatives (recommended Pagefind) | ✅ Analysis complete, Pagefind implemented |
| **CONTENT_MATCH_INDICATOR_IMPLEMENTATION_PLAN.md** | Content match indicator feature (old search) | ❌ Depended on old search architecture |
| **CONTENT_MATCH_INDICATOR_SPEC.md** | Content match indicator specification | ❌ Depended on old search architecture |
| **PAGEFIND_IMPLEMENTATION_PLAN.md** | Pagefind search implementation plan | ✅ Implemented January 2026 |
| **ALL_POSTS_SECTION_PLAN.md** | All Posts section in sidebar | ✅ Implemented |
| **WIKI_TOC_SIDEBAR_REQUIREMENTS.md** | Wiki-style TOC sidebar requirements | ✅ Implemented |
| **CHRONOLOGICAL_NAV_SIDEBAR_REQUIREMENTS.md** | Chronological navigation sidebar | ✅ Implemented |
| **GITHUB_PAGES_REQUIREMENTS.md** | GitHub Pages hosting requirements | ✅ Complete |
| **AUDIT_DEVIATIONS.md** | Audit of implementation deviations | ✅ All issues resolved |

## Why Archived?

These documents were moved here on January 2026 during the Pagefind search migration:

1. **Obsolete** - Old search index documents are no longer relevant after migrating to Pagefind
2. **Completed** - Feature requirements/plans that have been fully implemented
3. **Historical** - Useful reference for understanding design decisions

## Current Search Implementation

The site now uses **Pagefind v1.4.0** for search:
- Sharded index (~50-100KB per query vs 6.9MB all-at-once)
- Auto-rebuilt via GitHub Actions on HTML changes
- Built-in UI with customizable styling
- See [Pagefind documentation](https://pagefind.app/) for details
