<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="run_prettify.js" type="text/javascript"></script> --> 
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

Import blog posts into @ElasticsearchQA #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge http://bit.ly/tbcimport_lookupicon
RevitLookup updated and with new icon #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge http://bit.ly/tbcimport_lookupicon

I started working on the question answering system Q4R4 Question Answering for Revit API.
The first step is to import The Building Coder blog posts into Elasticsearch and experiment with full-text queries on them.
Furthermore, we are proud to present yet more enhancements to the revamped version of RevitLookup
&ndash; Q4R4 sources and result presentation
&ndash; Importing <code>tbc</code> blog posts into Elasticsearch
&ndash; Listing and clearing the Elasticsearch <code>tbc</code> index
&ndash; Strip and clean up HTML for JSON document
&ndash; Q4R4 GitHub repo and <code>tbcimport.py</code> script
&ndash; RevitLookup bug fixes
&ndash; RevitLookup icons...

-->

### Q4R4 tbc Import and RevitLookup 

I started working on the question answering
system [Q4R4 *Question Answering for Revit API*](http://thebuildingcoder.typepad.com/blog/2017/03/q4r4-revit-api-question-answering-system.html).

The first step is to import The Building Coder blog posts into Elasticsearch and experiment with full-text queries on them.

Furthermore, we are proud to present yet more enhancements to the revamped version of RevitLookup:

- [Q4R4 sources and result presentation](#2)
- [Importing `tbc` blog posts into Elasticsearch](#3)
- [Listing and clearing the Elasticsearch `tbc` index](#4)
- [Strip and clean up HTML for JSON document](#5)
- [Q4R4 GitHub repo and `tbcimport.py` script](#6)
- [RevitLookup bug fixes](#7)
- [RevitLookup icons](#8)




#### <a name="2"></a>Q4R4 Question Sources and Result Presentation

One aspect of q4r4 is searching, and another is what results to present and how.

One useful approach that comes to mind might be:

Given a query, return the most relevant results separately from several different resource collections:

- The Revit API help file
- The Revit add-in developer guide
- The Revit SDK sample collection
- Revit API discussion forum threads
- The Building Coder blog posts
- Anonymised ADN case answers
- StackOverflow queries

#### <a name="3"></a>Importing `tbc` Blog Posts into Elasticsearch

As mentioned
in [the last post on q4r4](http://thebuildingcoder.typepad.com/blog/2017/03/q4r4-revit-api-question-answering-system.html),
I should start off implementing a simple but intelligent search engine without worrying about machine learning or AI in any of its forms.

I am still reading
about [Elasticsearch](https://www.elastic.co/products/elasticsearch) and
figuring out how to set up an experimental system to try this out.

<center>
<img src="img/icon-elasticsearch-bb.png" alt="Elasticsearch" width="84"/>
</center>

I started with the The Building Coder blog posts, since I have them all in handy text format, either HTML or Markdown, publicly accessible in
the [tbc GitHub repository](https://github.com/jeremytammik/tbc).

I want to import all posts' full text into Elasticsearch.

A similar topic is discussed 
in [having fun with Python and Elasticsearch, Part 1](https://bitquabit.com/post/having-fun-python-and-elasticsearch-part-1/).

I installed the [Elasticsearch Python library](https://www.elastic.co/guide/en/elasticsearch/client/python-api/current/index.html) and
implemented a module `tbcimport.py` to read
the [tbc main blog post index](http://jeremytammik.github.io/tbc/a) and open each HTML file on the local system.


#### <a name="4"></a>Listing and Clearing the Elasticsearch `tbc` Index

For testing purposes, it is useful to be able to list all posts imported so far and delete the entire collection to clean up and retry; here are two `curl` commands to achieve that:

- List all posts:

<pre>
curl -XGET 'localhost:9200/tbc/_search?pretty'
</pre>

- Clear the `tbc` index:

<pre>
curl -XDELETE 'localhost:9200/tbc?pretty'
</pre>


#### <a name="5"></a>Strip and Clean Up HTML for JSON Document

After reading the main blog post index file, I need to extract the text from the HTML contents and put it into a JSON document for Elasticsearch to imbibe.

Some useful hints for this are provided here:

- [Hitchhiker's Guide to Python &ndash; HTML scraping](http://docs.python-guide.org/en/latest/scenarios/scrape/)
- [Extracting text from HTML file using Python](http://stackoverflow.com/questions/328356/extracting-text-from-html-file-using-python)

I settled for a very simple HTML text extractor using the `htmllib` `HTMLParser`.

It initially wrote the text to standard output, but I was able to pass a file-like `StringIO` object into the `DumbWriter` constructor to intercept it.

On the first attempt, I successfully imported the first nine posts.
Post number 10, *Selecting all Walls*, failed with a `UnicodeDecodeError` error message.

<pre>
UnicodeDecodeError: 'utf8' codec can't decode byte 0xa0 in position 2595: invalid start byte
</pre>

As it turned out, the offending file was stored in a Windows encoding.
I converted it to UTF-8.

Next, I went one step further and eliminated all non-ASCII characters by adding `re.sub( r'[^\x00-\x7f]', r'', my_stringio.getvalue() )` to the result of stripping the HTML tags.

This will presumably corrupt some foreign names, expressions, and text passages.
I would not expect those passages to be of any major importance for Revit API related queries anyway.

I also added an assertion to ensure that the filenames listed in `index.html` really do exist.

A surprising number of errors were discovered and fixed in the process.

Now I have successfully imported all The Building Coder blog posts into Elasticsearch.

#### <a name="6"></a>Q4R4 GitHub Repo and `tbcimport.py` Script

I celebrated this first step by creating
the [q4r4 GitHub repository](https://github.com/jeremytammik/q4r4),
adding [tbcimport.py](https://github.com/jeremytammik/q4r4/blob/master/tbcimport.py) to it in its current functional state, 
and creating [q4r4 release 1.0.0](https://github.com/jeremytammik/q4r4/releases/tag/1.0.0).

Here is the script in its current state:

<script src="https://gist.github.com/jeremytammik/d834055f2f4943c4bbe97beb85d803cc.js"></script>

The next thing to do is to start experimenting with queries, and presumably with ways to optimise the resulting hits.


#### <a name="7"></a>RevitLookup Bug Fixes

While I am fiddling with q4r4,
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) and 
other Revit API related issues remain as vibrant as ever.

Some new enhancements were added to our irreplaceable Revit BIM database exploration
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
The `Close` method must not be called &ndash; it successfully closes non-active documents and fails to get information about them.

Many thanks to Alexander for these improvements!

I integrated them 
into [RevitLookup release 2017.0.0.19](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.19).


#### <a name="8"></a>RevitLookup Icons

Just a few hours after Alexander's bug fixes,
Ehsan [@eirannejad](https://github.com/eirannejad) Iran-Nejad chipped in with some further important improvements in
his [pull request #30](https://github.com/jeremytammik/RevitLookup/pull/30/commits):

- Added and updated icon package
    - Added icon for RevitLookup button in Revit UI
    - Added icon to RevitLookup forms
    - Revised icons for RevitLookup menu bar
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
