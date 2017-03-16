<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

RevitLookup and DevDays Online API News #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge http://bit.ly/devdays2016online

&ndash; 
...

-->

### Q4R4 and RevitLookup 

Furthermore, we are proud to present another little update of the revamped version of RevitLookup:

- [](#2)
- [](#3)
- [Thoughts on the Revit API question answering system Q4R4](#4)
- [RevitLookup bug fixes](#8)
- [RevitLookup icons](#9)

<center>
<img src="img/.png" alt="" width="505"/>
</center>

#### <a name="2"></a>

#### <a name="3"></a>

#### <a name="4"></a>Thoughts on the Revit API question answering system Q4R4

One useful approach that comes to mind might be:

Given a query, return the most relevant results separately from several different resource collections:

- The Revit API help file
- The Revit add-in developer guide
- The Revit SDK sample collection
- Revit API discussion forum threads
- The Building Coder blog posts
- Anonymised ADN case answers
- StackOverflow queries

I am still reading about Elasticsearch and figuring out how to set up a very first experimental system.

I will probably start with the The Building Coder blog posts, since I have them all in handy text format, either HTML or Markdown, publicly accessible in
the [tbc GitHub repository](https://github.com/jeremytammik/tbc).

I want to import all posts' full text into Elasticsearch.

A similar topic is addressed in [having fun with Python and Elasticsearch, Part 1](https://bitquabit.com/post/having-fun-python-and-elasticsearch-part-1/).

I installed the [Elasticsearch Python library](https://www.elastic.co/guide/en/elasticsearch/client/python-api/current/index.html) and
implemented a module `tbcimport.py` to read
the [tbc main blog post index](http://jeremytammik.github.io/tbc/a) and open each HTML file on the local system.

Now I need to extract the text from the HTML and put it into a JSON document for Elasticsearch to imbibe.

- [Hitchhiker's Guide to Python &ndash; HTML Scraping](http://docs.python-guide.org/en/latest/scenarios/scrape/)
- [Extracting text from HTML file using Python](http://stackoverflow.com/questions/328356/extracting-text-from-html-file-using-python)

I settled for a very simple HTML text extractor using the `htmllib` `HTMLParser`.

It initially wrote the text to standard output, but I was able to pass a file-like `StringIO` object into the `DumbWriter` constructor to intercept it.

During testing, I also need to list all posts imported so far and delete the entire collection to clean up and retry:

List all posts:

curl -XGET 'localhost:9200/tbc/_search?pretty'

Clear the `tbc` index:

curl -XDELETE 'localhost:9200/tbc?pretty'

I successfully imported the first nine posts now.

Post number 10, *Selecting all Walls*, fails with a `UnicodeDecodeError` error message.

Here is the script in its current state:

<pre>
#!/usr/bin/env python

from htmllib import HTMLParser, HTMLParseError
from formatter import AbstractFormatter, DumbWriter
from os import path
import elasticsearch
import json
import re
import StringIO

_tbc_dir = '/a/doc/revit/tbc/git/a/'

def get_text_from_html(html_input):
  my_stringio = StringIO.StringIO() # make an instance of this file-like string thing
  p = HTMLParser(AbstractFormatter(DumbWriter(my_stringio)))
  try: p.feed(html_input); p.close() #calling close is not usually needed, but let's play it safe
  except HTMLParseError: print ':(' #the html is badly malformed (or you found a bug)
  return my_stringio.getvalue()

def parse_index_line(line):
  nr = int(line[22:26])
  date = line[35:45]
  url = line[63:]
  assert( url.startswith('http') or url[0]==' ' )
  i = url.index('"')
  assert( 0 < i )
  title = url[i+2:]
  url = url[:i]
  if url == ' ': return -1,'','','',''
  i = title.index('"')
  assert( 0 < i )
  filename = title[i+1:]
  i = title.index('<')
  assert( 0 < i )
  title = title[:i]
  i = filename.index('"')
  assert( 0 < i )
  filename = filename[:i]
  print nr, date, url, "'"+title+"'", filename
  return nr, date, url, title, filename
  
def load_from_index():
  es = elasticsearch.Elasticsearch()

  f = open(path.join(_tbc_dir, "index.html"))
  lines = f.readlines()
  f.close()
  
  for line in lines:
    if line.startswith('<tr><td align="right">'):
      nr, date, url, title, filename = parse_index_line(line)
      if 0 < nr:
        assert(filename.endswith('.htm') or filename.endswith('.html'))
        f = open(path.join(_tbc_dir, filename))
        html = f.read()
        f.close()
        
        s = get_text_from_html(html)

        json_body = {"nr" : nr, "date" : date, "url" : url, "title" : title, "text" : s}

        es.index(index='tbc', doc_type='blogpost', body=json_body)
        
def main():
  load_from_index()

if __name__ == '__main__':
  main()
</pre>

#### <a name="8"></a>UnicodeDecodeError

UnicodeDecodeError: 'utf8' codec can't decode byte 0xa0 in position 2595: invalid start byte

As it turns out, the offending file was encoded in a Windows encoding.

I converted it to UTF-8.


#### <a name="8"></a>RevitLookup Bug Fixes

Some new enhancements have been added to our irreplaceable Revit BIM database exploration
tool [RevitLookup](https://github.com/jeremytammik/RevitLookup).

In the last few weeks, it was significantly restructured to use `Reflection` and reduce code duplication:

- [Using `Reflection` for cross-version compatibility](http://thebuildingcoder.typepad.com/blog/2017/02/revitlookup-using-reflection-for-cross-version-compatibility.html)
- [Basic clean-up of the new version](http://thebuildingcoder.typepad.com/blog/2017/02/revitlookup-with-reflection-cleanup.html)
- [Restore access to extensible storage data](http://thebuildingcoder.typepad.com/blog/2017/02/revitlookup-extensible-storage-restored.html#3)
- [Further enhancements](http://thebuildingcoder.typepad.com/blog/2017/03/revitlookup-enhancements-future-revit-and-other-api-news.html#8)

Alexander Ignatovich, [@CADBIMDeveloper](https://github.com/CADBIMDeveloper), aka Александр Игнатович,
now submitted a few new bug fixes in
his [pull request #29](https://github.com/jeremytammik/RevitLookup/pull/29/commits):

- Move `CollectorExtElement` field initialization to constructor, use linq extension methods instead of linq syntax
- Get types only from `AppDomain.CurrentDomain.BaseDirectory`, the Revit.exe directory path. 
I have a dll with a name that contains the substring "revit".
This library depends on another library in another location.
I have an `Assembly.Resolve` event subscription to load dependencies correctly.
In such case this code fails, because it can't be aware of correct paths to load referenced libraries.
- Fix bug in getting `Application.Documents` when more than one document is opened.
The `Close` method must not be called &ndash; it sucessfully closes non-active documents and fails to get information about them.

Many thanks to Alexander for these improvements!

I integrated them 
into [RevitLookup release 2017.0.0.19](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.19).


#### <a name="9"></a>RevitLookup Icons

Just a few hours after Alexander's bug fixes,
Ehsan [@eirannejad](https://github.com/eirannejad) Iran-Nejad chipped in with some further important improvements in
his [pull request #30](https://github.com/jeremytammik/RevitLookup/pull/30/commits):

- Added and updated icon package  …
    - Added Icon for RevitLookup button in Revit UI
    - Added icon to RevitLookup forms
    - Revised Icons for RevitLookup menu bar
- Added exception handling
    - `Path.GetDirectoryName` throws `System.ArgumentException if the assembly `Location` is null.

Many thanks to Ehsan for these improvements!

I integrated them 
into [RevitLookup release 2017.0.0.20](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.20).

The most up-to-date version is always provided in the master branch of 
the [RevitLookup GitHub repository](https://github.com/jeremytammik/RevitLookup).

If you would like to access any part of the functionality that was removed when switching to the `Reflection` based approach, please grab it
from [release 2017.0.0.13](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.13) or earlier.

I am also happy to restore any other code that was removed and that you would like preserved.
Simply create a pull request for that, explain your need and motivation, and I will gladly merge it back again.
