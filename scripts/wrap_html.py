#!/usr/bin/env python3
"""
Wrap HTML fragments with proper HTML structure including head, body, and CSS.
"""

import os
from pathlib import Path

BLOG_DIR = Path(__file__).parent / "a"

# HTML wrapper template
HTML_TEMPLATE = '''<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>The Building Coder</title>
    <link rel="icon" type="image/svg+xml" href="favicon.svg">
    <link rel="stylesheet" href="bc.css">
    <link rel="stylesheet" href="google-code-prettify/prettify.css">
    <script src="google-code-prettify/run_prettify.js"></script>
    <style>
        body {{
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
            line-height: 1.6;
            max-width: 900px;
            margin: 0 auto;
            padding: 20px;
            color: #333;
        }}
        a {{ color: #0066cc; }}
        img {{ max-width: 100%; height: auto; }}
        .nav {{ margin-bottom: 20px; padding: 10px; background: #f5f5f5; border-radius: 5px; }}
        .nav a {{ margin-right: 15px; }}
    </style>
</head>
<body>
    <div class="nav">
        <a href="index.html">← Back to Index</a>
    </div>
    <article>
{content}
    </article>
    <div class="nav">
        <a href="index.html">← Back to Index</a>
    </div>
</body>
</html>
'''

def needs_wrapping(content):
    """Check if the file is a fragment (no <html> tag)."""
    content_lower = content.lower()
    return '<html' not in content_lower and '<!doctype' not in content_lower

def wrap_file(file_path):
    """Wrap a single HTML fragment file."""
    with open(file_path, 'r', encoding='utf-8', errors='replace') as f:
        content = f.read()
    
    if not needs_wrapping(content):
        return False  # Already has proper HTML structure
    
    # Indent the content
    indented_content = '\n'.join('        ' + line if line.strip() else '' for line in content.split('\n'))
    
    wrapped = HTML_TEMPLATE.format(content=content)
    
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(wrapped)
    
    return True

def main():
    print("Wrapping HTML fragments with proper structure...")
    
    # Get all HTML/HTM files
    html_files = list(BLOG_DIR.glob("*.htm")) + list(BLOG_DIR.glob("*.html"))
    html_files = [f for f in html_files if f.name not in ('index.html', 'index_local.html')]
    
    wrapped_count = 0
    skipped_count = 0
    
    for i, file_path in enumerate(html_files):
        if wrap_file(file_path):
            wrapped_count += 1
        else:
            skipped_count += 1
        
        if (i + 1) % 200 == 0:
            print(f"  Processed {i + 1}/{len(html_files)} files...")
    
    print(f"\nComplete!")
    print(f"  Wrapped: {wrapped_count} files")
    print(f"  Skipped (already proper HTML): {skipped_count} files")

if __name__ == "__main__":
    main()
