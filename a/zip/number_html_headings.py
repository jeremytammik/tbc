#!/usr/bin/python
#
# number_html_headings.py - add numbering to HTML headings
#
# Copyright (C) 2017-04-25 by Jeremy Tammik, Autodesk Inc. 
#

filename = 'Revit_Platform_API_Changes_and_Additions_2018_03.htm'

filepath = '/a/doc/revit/tbc/git/a/zip/' + filename

nlevels = 10 # max heading level to number

ih = [] # current count for each heading level

for i in range(nlevels):
  ih.append(0)
  
def filereadlines( filename ):
  f = open( filename )
  data = f.readlines()
  f.close()
  return data

if __name__ == '__main__':

  def main():
    lines = filereadlines( filepath )
    for line in lines:
      line = line.strip()
      a = ''
      if line.startswith('<h'):
        level = int(line[2])
        ih[level-1] = ih[level-1] + 1
        a = '.'.join( [str(ih[l]) for l in range(level)] )
        a = '<a name="' + a + '"></a>'
      print a + line

main()

