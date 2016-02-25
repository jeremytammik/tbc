#!/usr/bin/python
# -*- coding: iso-8859-15 -*-
#
# pycolorize.py - massage colorised HTML source code copied from Visual Studio
#
# jeremy tammik, autodesk inc, 2009-02-05
#
# History:
#
# 2009-02-05 initial version
# 2009-05-22 updated to support Visual Studio 2008
# 2011-04-19 updated to support Visual Studio 2010 and CopySourceAsHtml 3.0
# 2013-05-21 migrated to mac os x unix, cf. http://coffeeghost.net/src/pyperclip.py
#
# read a block of text from a file or the windows clipboard
# replace cb[12345] by the appropriate colour
# remove the style and pre tags
#
# example of original text block:
#
# <style type="text/css">
# .cf { font-family: Courier New; font-size: 10pt; color: black; background: white; }
# .cl { margin: 0px; }
# .gray { color: gray; }
# .green { color: green; }
# .blue { color: blue; }
# .teal { color: teal; }
# .maroon { color: maroon; }
# </style>
# <div class="cf">
# <pre class="cl"><span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span></pre>
# <pre class="cl"><span class="gray">///</span><span class="green"> Return the specified double parameter </span></pre>
# <pre class="cl"><span class="gray">///</span><span class="green"> value for the given wall.</span></pre>
# <pre class="cl"><span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span></pre>
# <pre class="cl"><span class="blue">double</span> GetWallParameter( </pre>
# <pre class="cl">&nbsp; <span class="teal">Wall</span> wall,</pre>
# <pre class="cl">&nbsp; <span class="teal">BuiltInParameter</span> bip )</pre>
# <pre class="cl">{</pre>
# <pre class="cl">&nbsp; <span class="teal">Parameter</span> p = wall.get_Parameter( bip );</pre>
# <pre class="cl">&nbsp;</pre>
# <pre class="cl">&nbsp; <span class="teal">Debug</span>.Assert( <span class="blue">null</span> != p, </pre>
# <pre class="cl">&nbsp; &nbsp; <span class="maroon">"expected wall to have "</span></pre>
# <pre class="cl">&nbsp; &nbsp; + <span class="maroon">"HOST_AREA_COMPUTED and "</span></pre>
# <pre class="cl">&nbsp; &nbsp; + <span class="maroon">"HOST_VOLUME_COMPUTED parameters"</span> );</pre>
# <pre class="cl">&nbsp;</pre>
# <pre class="cl">&nbsp; <span class="blue">return</span> p.AsDouble();</pre>
# <pre class="cl">}</pre>
# </div>
#
# example of resulting text block:
#
# <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
# <span class="gray">///</span><span class="green"> Return the specified double parameter </span>
# <span class="gray">///</span><span class="green"> value for the given wall.</span>
# <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
# <span class="blue">double</span> GetWallParameter(
# &nbsp; <span class="teal">Wall</span> wall,
# &nbsp; <span class="teal">BuiltInParameter</span> bip )
# {
# &nbsp; <span class="teal">Parameter</span> p = wall.get_Parameter( bip );
# &nbsp;
# &nbsp; <span class="teal">Debug</span>.Assert( <span class="blue">null</span> != p,
# &nbsp; &nbsp; <span class="maroon">"expected wall to have "</span>
# &nbsp; &nbsp; + <span class="maroon">"HOST_AREA_COMPUTED and "</span>
# &nbsp; &nbsp; + <span class="maroon">"HOST_VOLUME_COMPUTED parameters"</span> );
# &nbsp;
# &nbsp; <span class="blue">return</span> p.AsDouble();
# }
#
# 2009-05-22:
#
# example of original text block:
#
# <style type="text/css">
# .cf { font-family: Courier New; font-size: 10pt; color: black; background: white; }
# .cl { margin: 0px; }
# .cb1 { color: blue; }
# .cb2 { color: green; }
# .cb3 { color: #2b91af; }
# .cb4 { color: #a31515; }
# </style>
# <div class="cf">
# <pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; <span class="cb1">int</span> n = imports.Count;</pre>
# <pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; <span class="cb2">// test</span></pre>
# <pre class="cl">&nbsp;</pre>
# <pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; <span class="cb3">Debug</span>.Print(</pre>
# <pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="cb4">&quot;Family '{0}' contains {1} import instance{2}{3}&quot;</span>,</pre>
# <pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; key, n, <span class="cb3">Util</span>.PluralSuffix( n ),</pre>
# <pre class="cl">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="cb3">Util</span>.DotOrColon( n ) );</pre>
# </div>
#
# example of resulting text block:
#
# &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">int</span> n = imports.Count;
# &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// test</span>
# &nbsp;
# &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Print(
# &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Family '{0}' contains {1} import instance{2}{3}&quot;</span>,
# &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; key, n, <span class="teal">Util</span>.PluralSuffix( n ),
# &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Util</span>.DotOrColon( n ) );
#
import os, re

color_map = { '#2b91af' : 'teal', '#a31515' : 'maroon' }

def getTextMac():
  outf = os.popen('pbpaste', 'r')
  content = outf.read()
  outf.close()
  return content

def setTextMac(text):
  outf = os.popen('pbcopy', 'w')
  outf.write(text)
  outf.close()

def getTextWin():
  w.OpenClipboard()
  d = w.GetClipboardData( win32con.CF_TEXT )
  w.CloseClipboard()
  return d

def setTextWin( aType, aString ):
  w.OpenClipboard()
  w.EmptyClipboard()
  w.SetClipboardData( aType, aString )
  w.CloseClipboard()

_regexColor = re.compile( '\.(cb[1-9]) \{ color\: ([#0-9a-z]+); \}' )
_regexStyle = re.compile( '(<style type="text/css">.*</style>\s*<div class="cf">\s*)', re.DOTALL )
_regexEnd = re.compile( '(</pre>\s*</div>)', re.DOTALL )
#_regexPreEnd = re.compile( '(</pre>$)' )

def replace_cb_by_color( s ):
  "Search for '.cb1 { color: blue; }' and globally replace cb[1-9] by the appropriate colour."
  m = _regexColor.search( s )
  if m:
    #print 'match found'
    a = m.groups()
    #print a
    if 2 == len( a ):
      color = a[1]
      #print color
      if color_map.has_key( color ): color = color_map[color]
      #print color
      return True, s.replace( a[0], color )
  #else:
  #  print 'no match found'

  return False, s

def main():
  'Convert Visual Studio CopySourceAsHtml colour styles to a more compact form.'

  s = getTextMac()
  #s = '''<style type="text/css"> ... '''
  #print s

  go = True
  while go: go, s = replace_cb_by_color( s )
  #print s

  m = _regexStyle.match( s )
  #print m

  if m:
    s = s.replace( m.group( 1 ), '' )
    #print s
    s = s.replace( '<pre class="cl">', '' )
    #print s
    m = _regexEnd.search( s )
    #print m

  if m:
    s = s.replace( m.group( 1 ), '' )
    #print s
    s = s.strip().replace( '</pre>', '' )
    #print s

  setTextMac( s )
  #print s

if __name__ == '__main__':
  main()
