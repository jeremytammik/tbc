# GitHub Pages Conversion Requirements Specification

## Project Overview

Convert The Building Coder (tbc) blog archive for hosting on GitHub Pages at:
**https://github.com/parametrix/tbc-archive.github.io**

The hosted site will be accessible at:
**https://parametrix.github.io/tbc-archive.github.io/** (or with custom domain)

---

## ✅ Current Status

**The conversion is COMPLETE.** The repository has been fully processed and is ready for GitHub Pages deployment.

### Completed Work

| Task | Status | Details |
|------|--------|---------|
| Clone original repo | ✅ Done | From `jeremytammik/tbc` gh-pages branch |
| Fix internal links | ✅ Done | 13,500+ Typepad URLs → local paths |
| Wrap HTML fragments | ✅ Done | 2,066 files with proper document structure |
| Fix double-quote bugs | ✅ Done | 9,007 malformed href patterns |
| Convert unavailable links | ✅ Done | 640+ dead links → text with notes |
| Fix CSS formatting | ✅ Done | `white-space: pre-wrap` for code blocks |
| Create root redirect | ✅ Done | `index.html` redirects to `a/index.html` |
| Add `.nojekyll` | ✅ Done | Bypasses Jekyll processing |
| Organize scripts | ✅ Done | All processing scripts in `scripts/` |
| Git commits | ✅ Done | 2 commits with full documentation |

### Ready for Deployment

The repository can be pushed directly to `parametrix/tbc-archive.github.io`:

```bash
git remote set-url origin https://github.com/parametrix/tbc-archive.github.io.git
git push -u origin gh-pages:main
```

---

## 1. Repository Structure

### 1.1 Target Repository Layout

```
tbc-archive.github.io/
├── index.html              # Root redirect or landing page
├── a/                      # Blog content directory
│   ├── index.html          # Main blog index
│   ├── bc.css              # Stylesheet
│   ├── google-code-prettify/  # Syntax highlighting
│   ├── img/                # Images
│   ├── zip/                # Downloadable files
│   ├── icon/               # Icons
│   ├── lib/                # Library files
│   ├── 0001_*.htm          # Blog posts (0001-1350)
│   ├── 1351_*.html         # Blog posts (1351+)
│   └── 1351_*.md           # Markdown sources (1351+)
├── scripts/                # Update automation scripts
│   ├── update_from_upstream.py
│   ├── fix_all_links.py
│   ├── wrap_html.py
│   ├── fix_double_quotes.py
│   ├── fix_unavailable_links.py
│   ├── fix_remaining_links.py
│   ├── verify_links.py
│   └── requirements.txt
├── .github/
│   └── workflows/
│       └── update.yml      # GitHub Actions workflow
├── README.md
├── LICENSE
├── CNAME                   # Optional: custom domain
└── _config.yml             # Jekyll configuration (if needed)
```

### 1.2 Root Landing Page

✅ **CREATED**: Root `index.html` redirects to `a/index.html`

---

## 2. GitHub Pages Configuration

### 2.1 Repository Settings

1. **Repository Name**: `tbc-archive.github.io`
2. **Branch**: `main` (or `gh-pages`)
3. **Source**: Deploy from branch, root directory (`/`)
4. **Custom Domain** (optional): Configure in Settings > Pages

### 2.2 Jekyll Configuration

✅ **CREATED**: `.nojekyll` file added to bypass Jekyll processing completely.

No `_config.yml` needed since `.nojekyll` is present.
### 2.3 MIME Types

GitHub Pages serves files based on extension. Ensure:
- `.htm` files are served as `text/html`
- `.css` files are served as `text/css`
- `.js` files are served as `application/javascript`

No configuration needed - GitHub Pages handles this automatically.

---

## 3. Link Resolution Process

✅ **COMPLETED** - All link processing has been done.

### 3.1 Overview

All internal Typepad links have been converted to relative local paths.

### 3.2 Link Types Processed

| Link Type | Original Format | Converted Format |
|-----------|-----------------|------------------|
| Blog posts | `http://thebuildingcoder.typepad.com/blog/YYYY/MM/slug.html` | `NNNN_slug.htm` or `NNNN_slug.html` |
| Images | `http://thebuildingcoder.typepad.com/.a/XXXX` | `img/filename.png` (if local) |
| Downloads | `http://thebuildingcoder.typepad.com/blog/files/xxx.zip` | Text: "file unavailable" |
| Categories | `http://thebuildingcoder.typepad.com/blog/category` | Text: "category page" |
| About pages | `http://thebuildingcoder.typepad.com/blog/about-*.html` | Text only |
| External | `http://other-domains.com/*` | Keep as-is |

### 3.3 Processing Scripts

#### 3.3.1 `fix_all_links.py`
- Parse `index.html` to build URL-to-local-file mapping
- Replace Typepad URLs with local file paths
- Preserve anchor fragments (`#section`)
- Handle both `http://` and `https://` variants

#### 3.3.2 `fix_remaining_links.py`
- Catch links not in the index mapping
- Match by URL slug to local filename
- Build comprehensive slug-to-file mapping

#### 3.3.3 `fix_unavailable_links.py`
- Convert `/blog/files/*` links to text with "(file unavailable)"
- Convert category pages to text
- Convert about pages to plain text spans

#### 3.3.4 `wrap_html.py`
- Detect HTML fragments (missing `<!DOCTYPE>` or `<html>`)
- Wrap with proper document structure
- Include CSS references and navigation

#### 3.3.5 `fix_double_quotes.py`
- Fix malformed `href="file.htm""` patterns
- Clean up any regex artifacts

#### 3.3.6 `verify_links.py`
- Scan all HTML files for local links
- Verify referenced files exist
- Report missing resources

---

## 4. Update Workflow

### 4.1 Manual Update Process

```bash
# 1. Fetch upstream changes
git remote add upstream https://github.com/jeremytammik/tbc.git
git fetch upstream gh-pages

# 2. Identify new/changed files
git diff --name-only HEAD upstream/gh-pages -- a/

# 3. Merge upstream (or cherry-pick specific commits)
git merge upstream/gh-pages --no-commit

# 4. Run link resolution scripts
python scripts/fix_all_links.py
python scripts/fix_remaining_links.py
python scripts/wrap_html.py
python scripts/fix_double_quotes.py
python scripts/fix_unavailable_links.py

# 5. Verify
python scripts/verify_links.py

# 6. Commit and push
git add -A
git commit -m "Sync with upstream and resolve links"
git push origin main
```

### 4.2 Automated Update (GitHub Actions)

Create `.github/workflows/update.yml`:

```yaml
name: Sync from Upstream

on:
  schedule:
    # Run weekly on Sundays at 00:00 UTC
    - cron: '0 0 * * 0'
  workflow_dispatch:  # Allow manual trigger

jobs:
  sync:
    runs-on: ubuntu-latest
    
    steps:
      - name: Checkout repository
        uses: actions/checkout@v4
        with:
          fetch-depth: 0
      
      - name: Set up Python
        uses: actions/setup-python@v5
        with:
          python-version: '3.11'
      
      - name: Add upstream remote
        run: |
          git remote add upstream https://github.com/jeremytammik/tbc.git || true
          git fetch upstream gh-pages
      
      - name: Check for updates
        id: check
        run: |
          UPSTREAM_SHA=$(git rev-parse upstream/gh-pages)
          LAST_SYNC=$(cat .last-sync-sha 2>/dev/null || echo "none")
          if [ "$UPSTREAM_SHA" != "$LAST_SYNC" ]; then
            echo "has_updates=true" >> $GITHUB_OUTPUT
            echo "upstream_sha=$UPSTREAM_SHA" >> $GITHUB_OUTPUT
          else
            echo "has_updates=false" >> $GITHUB_OUTPUT
          fi
      
      - name: Merge upstream changes
        if: steps.check.outputs.has_updates == 'true'
        run: |
          git config user.name "GitHub Actions"
          git config user.email "actions@github.com"
          git merge upstream/gh-pages --no-edit -X theirs || true
      
      - name: Run link resolution
        if: steps.check.outputs.has_updates == 'true'
        run: |
          cd scripts
          pip install -r requirements.txt
          python fix_all_links.py
          python fix_remaining_links.py
          python wrap_html.py
          python fix_double_quotes.py
          python fix_unavailable_links.py
          python verify_links.py
      
      - name: Update sync marker
        if: steps.check.outputs.has_updates == 'true'
        run: |
          echo "${{ steps.check.outputs.upstream_sha }}" > .last-sync-sha
      
      - name: Commit and push
        if: steps.check.outputs.has_updates == 'true'
        run: |
          git add -A
          git commit -m "Sync from upstream: $(date +%Y-%m-%d)" || true
          git push
```

---

## 5. URL Structure

### 5.1 GitHub Pages URLs

| Resource | URL |
|----------|-----|
| Landing page | `https://parametrix.github.io/tbc-archive.github.io/` |
| Blog index | `https://parametrix.github.io/tbc-archive.github.io/a/index.html` |
| Blog post | `https://parametrix.github.io/tbc-archive.github.io/a/0979_custom_exporter.htm` |
| CSS | `https://parametrix.github.io/tbc-archive.github.io/a/bc.css` |
| Images | `https://parametrix.github.io/tbc-archive.github.io/a/img/*.png` |

### 5.2 Custom Domain (Optional)

To use a custom domain like `tbc-archive.parametrix.com`:

1. Create `CNAME` file with domain name
2. Configure DNS with CNAME record pointing to `parametrix.github.io`
3. Enable HTTPS in repository settings

---

## 6. Content Modifications

### 6.1 Navigation Updates

Update `a/index.html` header to reflect the archive status:

```html
<div class="archive-notice">
    <p><strong>📚 Archive Notice:</strong> This is an offline-accessible archive of 
    <a href="http://thebuildingcoder.typepad.com">The Building Coder</a> blog. 
    For the latest content, visit the 
    <a href="http://thebuildingcoder.typepad.com">original blog</a>.</p>
</div>
```

### 6.2 CSS Additions

Add to `a/bc.css`:

```css
/* Archive notice banner */
.archive-notice {
    background: #fff3cd;
    border: 1px solid #ffc107;
    border-radius: 5px;
    padding: 10px 15px;
    margin-bottom: 20px;
}

.archive-notice p {
    margin: 0;
}

/* Unavailable link styling */
.unavailable-link {
    color: #666;
}

.unavailable-link em {
    font-size: 0.85em;
    color: #999;
}
```

### 6.3 Footer Attribution

Add footer to blog pages:

```html
<footer class="archive-footer">
    <p>Archive hosted by <a href="https://github.com/parametrix">Parametrix</a> | 
    Original content © <a href="http://thebuildingcoder.typepad.com">Jeremy Tammik</a> | 
    <a href="https://github.com/parametrix/tbc-archive.github.io">Source</a></p>
</footer>
```

---

## 7. Scripts Requirements

### 7.1 Python Dependencies

Create `scripts/requirements.txt`:

```
# No external dependencies required
# All scripts use Python standard library only
```

### 7.2 Script Modifications for CI/CD

Update scripts to:
- Accept command-line arguments for paths
- Return proper exit codes
- Output progress to stdout for logging
- Handle partial updates (new files only)

Example modification:

```python
#!/usr/bin/env python3
import sys
import argparse
from pathlib import Path

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('--repo-dir', default='.', help='Repository root')
    parser.add_argument('--verbose', action='store_true')
    args = parser.parse_args()
    
    # ... script logic ...
    
    return 0 if success else 1

if __name__ == '__main__':
    sys.exit(main())
```

---

## 8. Testing Requirements

### 8.1 Pre-Deployment Checks

1. **Link Verification**: Run `verify_links.py` - should report < 1% broken internal links
2. **HTML Validation**: Spot-check pages with W3C validator
3. **Local Server Test**: Test with Python HTTP server before pushing
4. **Cross-Browser**: Test in Chrome, Firefox, Edge

### 8.2 Post-Deployment Checks

1. **GitHub Pages Build**: Check Actions tab for build success
2. **Site Accessibility**: Verify main URL loads
3. **Sample Pages**: Test 5-10 random blog posts
4. **Navigation**: Test index links, internal links, back buttons
5. **Assets**: Verify images, CSS, syntax highlighting load

### 8.3 Regression Testing

After each sync, verify:
- [ ] New posts appear in index
- [ ] New posts have proper HTML structure
- [ ] Internal links in new posts are resolved
- [ ] Images in new posts load correctly
- [ ] Existing posts still work

---

## 9. Maintenance

### 9.1 Monitoring

- Set up GitHub Actions notifications for workflow failures
- Periodically check site accessibility
- Monitor upstream for structural changes that might break scripts

### 9.2 Known Limitations

1. **File Downloads**: Old Typepad-hosted files (`.zip`, `.pdf`) are not available locally
2. **Dynamic Content**: Comment sections, search functionality not preserved
3. **External Links**: Links to third-party sites may become stale over time
4. **Typepad Media**: Some embedded Typepad images/media may not be preserved

### 9.3 Future Improvements

- [ ] Add search functionality (client-side with Lunr.js or similar)
- [ ] Generate sitemap.xml for SEO
- [ ] Add RSS feed
- [ ] Implement tag/category browsing
- [ ] Add "last updated" timestamps
- [ ] Create mobile-optimized navigation

---

## 10. Migration Checklist

### 10.1 Initial Setup

- [ ] Create `parametrix/tbc-archive.github.io` repository
- [ ] Push converted content to repository
- [ ] Enable GitHub Pages in repository settings
- [ ] Verify site loads at GitHub Pages URL
- [ ] Add `.nojekyll` file if needed
- [ ] Create root `index.html` redirect

### 10.2 Automation Setup

- [ ] Move scripts to `scripts/` directory
- [ ] Update script paths for new structure
- [ ] Create `.github/workflows/update.yml`
- [ ] Test workflow with manual trigger
- [ ] Verify automated sync works

### 10.3 Documentation

- [ ] Update README.md with hosting information
- [ ] Document update process
- [ ] Add contributing guidelines
- [ ] Add issue templates for broken links

---

## 11. Repository Comparison

| Aspect | Original (jeremytammik/tbc) | Archive (parametrix/tbc-archive.github.io) |
|--------|----------------------------|-------------------------------------------|
| Hosting | GitHub Pages (static) | GitHub Pages (static) |
| Links | Point to Typepad | Point to local files |
| Updates | Manual by author | Automated sync + processing |
| Downloads | External Typepad links | Marked as unavailable |
| Search | None | None (potential future enhancement) |
| Purpose | Source repository | Offline-accessible archive |

---

## Appendix A: File Statistics

Based on current repository state:

| Metric | Count |
|--------|-------|
| Total HTML files | 2,079 |
| Total blog posts | 2,078 |
| Images | ~1,500 |
| ZIP downloads | ~500 |
| CSS files | 3 |
| JavaScript files | ~10 |
| Total size | ~1.9 GB |

---

## Appendix B: Script Execution Order

For updates, scripts must be run in this order:

1. `fix_all_links.py` - Main URL mapping and replacement
2. `fix_remaining_links.py` - Catch unmapped URLs by slug matching
3. `wrap_html.py` - Add document structure to HTML fragments
4. `fix_double_quotes.py` - Clean up regex artifacts
5. `fix_unavailable_links.py` - Convert dead links to text
6. `fix_about_links.py` - Handle about-page links with nested tags
7. `verify_links.py` - Final verification (optional, for reporting)

---

*Document created: January 2, 2026*
*Last updated: January 2, 2026*
