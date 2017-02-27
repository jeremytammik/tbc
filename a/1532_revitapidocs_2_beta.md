<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

 #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

Gui Talarico created an extremely useful online version of the contents of the Revit API help file RevitAPI.chm.
It sports significant advantages over the locally hosted Windows help file
&ndash; Online access from anywhere, any platform, OS, device
&ndash; Share links to specific topics for discussion with peers
&ndash; Covers and compares between multiple versions of the Revit API;
During the expansion cover Revit 2017.1, Gui implemented a number of other significant enhancements...

-->

### Revit API Docs 2.0 Beta

As we noted back
in [August](http://thebuildingcoder.typepad.com/blog/2016/08/online-revit-api-docs-and-convex-hull.html#2)
and [October](http://thebuildingcoder.typepad.com/blog/2016/10/token-expiry-and-online-revit-api-docs.html#2) last
year, Gui Talarico created an extremely useful online version of the contents of the Revit API help file *RevitAPI.chm*.

It sports several significant advantages over the locally hosted Windows help file:

- Online access from anywhere, any platform, OS, device.
- Share links to specific topics for discussion with peers.
- Covers and compares between multiple versions of the Revit API.

During the expansion to integrate the new information to cover Revit 2017.1, Gui implemented a number of other significant enhancements as well:

- [Revit 2017.1 API added](http://beta.revitapidocs.com/2017.1/).
- New parsers written from the ground up. This creates cleaner and more consistent html mark-ups from the original CHM content.
- Content is compared across yearly versions, and when identical, only one copy is being stored and served (storing only 25k html files vs +80k, previously).
- Smarter nav-bar now also indicates if the doc was updated/removed across years, [for example the WallFoundation class](http://beta.revitapidocs.com/2016/29a6e040-a36e-2a0c-5339-c69aa7776301.htm) &ndash; note the coloured bars and tooltips under the year numbers.
- Result filtering: search result page now has a text box user can use to further filter results; [try it out](http://beta.revitapidocs.com/2016/b0a5f22c-6951-c3af-cd29-1f28f574035d.htm?query=wall).
- [Python Examples](http://beta.revitapidocs.com/2016/b0a5f22c-6951-c3af-cd29-1f28f574035d.htm) &ndash; adds Python snippets if one is found (beta).
- Ajax loading: when possible, content is loaded asynchronously, for a more seamless navigation.
- Language switcher: allows user to only see code from the selected language.
- Copy code &ndash; [code examples](http://beta.revitapidocs.com/2016/29a6e040-a36e-2a0c-5339-c69aa7776301.htm) now have a `Copy` button which copies to the clipboard.
- Implemented [Handlebars JS](http://handlebarsjs.com) templating engine for more maintainable front-end code.
- All API documentation pages are now fully editable; just click `Edit` in the upper-right corner, to [edit and send a pull request](https://github.com/gtalarico/revitapidocs/edit/dev/new_html_parser/app/templates/api_docs/2016/29a6e040-a36e-2a0c-5339-c69aa7776301.htm).
- Many other bug fixes and improvements behind the scenes.

These updates are currently available for testing at **[beta.revitapidocs.com](http://beta.revitapidocs.com/)** and
will be pushed to the main server once they have been battle tested.

Please report any [issues or bugs on the 2.0 milestone](https://github.com/gtalarico/revitapidocs/milestone/7).

Thank you for participating, and many thanks to Gui for creating and maintaining this wonderful tool for the entire community!

For more information, please refer to Gui's own [full description of Revit API Docs 2.0](http://thebar.cc/revit-api-docs-2-0).

<center>
<img src="img/revitapidocs_2_beta.png" alt="Revit API Docs 2.0 Beta" width="975"/>
</center>

