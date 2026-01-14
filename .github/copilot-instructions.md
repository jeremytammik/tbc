# The Building Coder - AI Coding Agent Instructions

## Project Overview

This is **The Building Coder** blog archive - a static site hosting 2,079 Revit API blog posts (2008-2025). It's a GitHub Pages site with offline-capable browsing, Pagefind search, and dual sidebar navigation (topics + chronological timeline).

## Architecture

### Core Content Structure
- **`a/`** - All blog posts as `.htm`/`.html` files (numbered `0001_` to `2079_`)
- **`a/drafts/`** - Markdown drafts awaiting publication
- **`a/toc/`** - Sidebar data and UI:
  - `toc-data.json` - 58 topic categories with ~2,256 post links (left sidebar)
  - `chrono-data.json` - Chronological index by year/month (right sidebar)
  - `toc-sidebar.js/css` - Sidebar navigation components
- **`a/index.html`** - Main index page with searchable post table
- **`scripts/`** - Python publishing and maintenance scripts

### Key Data Flows
1. **Publishing**: Draft `.md` → `publish_post.py` → numbered `.htm` + updates to `index.html`, `chrono-data.json`
2. **Topic Assignment**: Use `manage_topics.py` to add posts to subject topics in `toc-data.json`
3. **Search Index**: Pagefind indexes on push via GitHub Actions (`pagefind.yml`)

## Developer Workflows

### Publishing a New Post
```bash
# Option 1: Local script
python scripts/publish_post.py a/drafts/2026-01-15-my-post.md

# Option 2: Just push - GitHub Actions auto-publishes
git add a/drafts/my-post.md && git commit -m "Add draft" && git push
```

### Managing Topics
```bash
python scripts/manage_topics.py list                    # List all 58 topics
python scripts/manage_topics.py add-post 5.9 2079_post.html "Title"
```

### Running Locally
```bash
python -m http.server 9000 --directory .
# Browse: http://localhost:9000/a/index.html
```

### Python Environment
```bash
pip install -r scripts/requirements.txt  # markdown, python-frontmatter, pyyaml, beautifulsoup4
```

## File Naming Conventions

- **Posts**: `NNNN_slug.htm` where `NNNN` is zero-padded 4-digit number (e.g., `2079_my_post.htm`)
- **Drafts**: `YYYY-MM-DD-slug.md` with YAML front matter:
  ```yaml
  ---
  title: "Post Title"
  date: 2026-01-15
  ---
  ```

## HTML Post Template

All posts use this structure (see `POST_TEMPLATE` in [scripts/publish_post.py](scripts/publish_post.py)):
- Links `bc.css`, `google-code-prettify/prettify.css`, `toc/toc-sidebar.css`
- Scripts: `run_prettify.js` (syntax highlighting), `toc-sidebar.js`, `copy-code.js`
- Navigation divs with "← Back to Index" links

## Critical Files to Update Together

When modifying posts, these files often need synchronized updates:
| Action | Files Affected |
|--------|----------------|
| Add post | `a/NNNN_slug.htm`, `a/index.html`, `a/toc/chrono-data.json` |
| Add to topic | `a/toc/toc-data.json` |
| Delete post | All above (use `scripts/delete_post.py`) |

## GitHub Actions Workflows

- **`publish-post.yml`** - Auto-publishes drafts on push to `a/drafts/*.md`
- **`remove-post.yml`** - Deletes posts via workflow dispatch
- **`manage-topics.yml`** - Add/remove posts from topics
- **`pagefind.yml`** - Rebuilds search index when HTML changes

## Important Context

- Original Typepad blog URLs were converted to local file paths (13,500+ links)
- Backup of original files in `a_backup/`
- Code syntax highlighting uses Google Prettify (class `prettyprint`)
- The site is fully offline-capable once loaded
