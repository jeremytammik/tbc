# tbc &ndash; The Building Coder blog source

Source and chronological index for [The Building Coder](http://thebuildingcoder.typepad.com) Revit API blog.

The repository hosts all the HTML, CSS and markdown source text for The Building Coder.

It also boasts a complete index of all posts.

## Features

- **2,079 blog posts** spanning 2008-2025
- **Wiki-style TOC sidebar** with 58 topic categories and 2,000+ posts
- **Pagefind search** - fast, lightweight full-text search across all content
- **Chronological navigation** - browse posts by year/month in the Timeline sidebar
- **Mobile-responsive** - hamburger menu for sidebar on small screens
- **Fully offline capable** - all internal links work locally

## Offline Local Hosting

Beginning 2026, The Building Coder has been modified for **fully offline local hosting**. 
All obsolete internal Typepad links have been converted to point to local files, allowing you to browse the entire blog archive without an internet connection.

### Changes Made

- **13,500+ internal links** converted from Typepad URLs to local file paths
- **2,066 HTML fragments** wrapped with proper document structure (`<html>`, `<head>`, `<body>`)
- **640+ unavailable resources** (old file downloads, category pages) converted to text with notes
- **CSS fix** for proper code block formatting with preserved indentation
- **Backup** of original files stored in `a_backup/`

## Search

The archive uses [Pagefind](https://pagefind.app/) for fast, lightweight full-text search:

- **Sharded index** - only downloads ~50-100KB per query (vs 6.9MB all-at-once)
- **2,079 posts indexed** with 69,000+ unique words
- **Auto-updated** via GitHub Actions when HTML files change
- **Works offline** once the page is loaded

### Running Locally

1. Navigate to the local `tbc` repository root/

2. Start a local HTTP server from the repository root:

   ```bash
   python -m http.server 9000 --directory .
   ```

3. Open your browser to: http://localhost:9000/a/index.html

4. Browse the complete blog archive offline with all internal links working.

### Original Repository

The obsolete Typepad-hosted version is still available in the subdirectory [a_backup/](a_backup/)

Each index entry includes a pointer to both the local source HTML or MD markdown and the official, global, Typepad-hosted blog entry.

You can download this repository to your local system to perform your own global text search or ensure offline access.

Please submit an [issue](https://github.com/jeremytammik/tbc/issues) if you discover any problem.

Thank you!

Enjoy.

For more background information, here are two articles on
publishing [The Building Coder source and index on GitHub](http://thebuildingcoder.typepad.com/blog/2016/02/tbc-the-building-coder-source-and-index-on-github.html)
and [hosting The 3D Web Coder source HTML and index on GitHub pages](http://the3dwebcoder.typepad.com/blog/2015/03/hosting-a-node-server-on-heroku-pages-and-3d-web.html#2).


## Archive Mirrors

Several archive mirrors of The Building Coder were published after Jeremy's retirement in June 2025.
Here are a few:

- [The Building Coder on ArchiLabs.ai](https://thebuildingcoder.archilabs.ai/) by ArchiLabs.ai
- [The Building Coder on AecWithCode.com](https://tbc.aecwithcode.com/) by Tomo Sugeta, [GitHub tsoumdoa/tbc-mirror-blog](https://github.com/tsoumdoa/tbc-mirror-blog)
- [The Building Coder &ndash; Modern MDX Mirror](https://github.com/tsoumdoa/tbc-mirror-content-preparation) by Tomo Sugeta


## Author

Jeremy Tammik,
[The Building Coder](http://thebuildingcoder.typepad.com) and
[The 3D Web Coder](http://the3dwebcoder.typepad.com),
[ADN](http://www.autodesk.com/adn)
[Open](http://www.autodesk.com/adnopen),
[Autodesk Inc.](http://www.autodesk.com)

## Offline Hosting Modifications

Modifications for offline hosting by [@parametrix](https://github.com/parametrix), January 2026.

## Documentation

- [PUBLISHING_GUIDE.md](PUBLISHING_GUIDE.md) - How to publish and manage blog posts
- [BLOG_STATS.md](BLOG_STATS.md) - Archive statistics
- [docs/archive/](docs/archive/) - Historical implementation documents

## License

This sample is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT). Please see the [LICENSE](LICENSE) file for full details.

