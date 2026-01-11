# The Building Coder Archive - Audit Deviations Report

Generated: January 5, 2026
Updated: January 5, 2026 - **ALL ISSUES RESOLVED**

This document tracks deviations found during systematic audits of the archive.
Use this information to update publish scripts and maintain data integrity.

## File Inventory Summary

| File Type | Count | Notes |
|-----------|-------|-------|
| `.htm` posts | 1,350 | Posts 0001-1350 |
| `.html` posts | 730 | Posts 1351-2080 |
| `.md` source files | 728 | Posts 1351-2078 (markdown sources) |
| **Total unique posts** | **2,080** | |

### File Structure Notes

- Posts 0001-1350 are `.htm` files (legacy format)
- Posts 1351-2080 are `.html` files (newer format)
- Posts 1351-2078 also have corresponding `.md` source files in the same directory
- The `.md` files are the markdown source; `.html` files are the rendered versions

---

## Deviations Found and Resolved

### 1. ✅ RESOLVED: Missing HTML File (Post 1397)

**Issue**: Post 1397 existed only as a `.md` file, not converted to `.html`

| Field | Value |
|-------|-------|
| Post Number | 1397 |
| Expected File | `1397_track_changes.html` |
| Title | Tracking Element Modification |
| Date | 2016-01-22 |

**Resolution**: ✅ Converted `1397_track_changes.md` to `1397_track_changes.html` on January 5, 2026

---

### 2. ✅ RESOLVED: Duplicate Entry in chrono-data.json (Post 2001)

**Issue**: Post 2001 had two entries in chrono-data.json

| Entry | Filename | Status |
|-------|----------|--------|
| Entry 1 | `2001_all_cats_params.html` | **WRONG** - file doesn't exist |
| Entry 2 | `2001_boundingbox.html` | Correct - file exists |

**Resolution**: ✅ Removed duplicate entry from chrono-data.json on January 5, 2026

---

### 3. ✅ RESOLVED: Commented-Out Entry in index.html (Post 2001)

**Issue**: index.html had a commented-out entry for post 2001 with wrong filename

**Resolution**: ✅ Removed the commented-out entry on January 5, 2026

---

## Data File Status (All Correct)

### chrono-data.json

| Metric | Value | Status |
|--------|-------|--------|
| Total posts | 2,080 | ✅ Correct |
| Files exist | All | ✅ All files present |
| Duplicates | 0 | ✅ None |

### toc-data.json

| Metric | Value | Notes |
|--------|-------|-------|
| Topics | 57 | Correct |
| Post links | 536 | Includes anchors |
| Unique files | 419 | 20.2% of all posts |
| Missing files | 0 | All referenced files exist |

---

## Scripts Updated

### 1. generate_chrono_data.py

The script now includes:
- [x] Skip commented-out entries when parsing index.html
- [x] Remove duplicate entries for the same post number
- [x] Validate that referenced files actually exist (warns if missing)

### 2. comprehensive_audit.py

Updated to:
- [x] Check if .md files have corresponding .html conversions
- [x] Only flag .md files that are missing their .html version

### 3. wrap_single_file.py (NEW)

Created utility script to wrap individual HTML files with proper HTML5 structure.

---

## Actions Completed

All issues identified have been resolved:

| # | Action | Status |
|---|--------|--------|
| 1 | Convert post 1397 from .md to .html | ✅ Done |
| 2 | Remove duplicate entry for post 2001 from chrono-data.json | ✅ Done |
| 3 | Remove commented-out entry from index.html | ✅ Done |
| 4 | Add validation to generate_chrono_data.py | ✅ Done |
| 5 | Regenerate chrono-data.json | ✅ Done |

---

## Verification Commands

Run the comprehensive audit to verify current status:

```bash
python scripts/comprehensive_audit.py
```

Check specific posts:
```bash
# Check if file exists
ls a/1397_track_changes.*
ls a/2001_*
```

---

## History

| Date | Action | By |
|------|--------|-----|
| 2026-01-05 | Initial audit and deviation report | Comprehensive audit |
