#!/usr/bin/env python3
"""
Wrap a single HTML file with proper HTML5 structure.
"""

import sys
from pathlib import Path

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
    <link rel="stylesheet" href="toc/toc-sidebar.css">
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
    <script src="toc/toc-sidebar.js"></script>
</body>
</html>
'''

def wrap_file(file_path):
    """Wrap a single HTML file with proper structure."""
    with open(file_path, 'r', encoding='utf-8', errors='replace') as f:
        content = f.read()
    
    # Check if already wrapped
    content_lower = content.lower()
    if '<!doctype html>' in content_lower or '<html' in content_lower:
        print(f'File already has HTML structure: {file_path}')
        return False
    
    wrapped = HTML_TEMPLATE.format(content=content)
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(wrapped)
    print(f'File wrapped successfully: {file_path}')
    return True

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage: python wrap_single_file.py <file_path>")
        sys.exit(1)
    
    file_path = sys.argv[1]
    if not Path(file_path).exists():
        print(f"File not found: {file_path}")
        sys.exit(1)
    
    wrap_file(file_path)
