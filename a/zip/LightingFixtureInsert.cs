using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Autodesk.Revit;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;

namespace ElectricalTools
{
    class InsertLights
    {
        UIDocument mRevitDoc;
        StreamReader mFileReader;
        ICollection<Element> mFamilySymbolsList;
        ICollection<Element> mReferencePlaneList;
        Document mDoc;

        // ---------------------------------------------------------------------------------------------------------

        public InsertLights()
        {
            mFileReader = null;
        }

        // ---------------------------------------------------------------------------------------------------------

        public void run(ExternalCommandData cmdData, ElementSet elements)
        {
            mRevitDoc = cmdData.Application.ActiveUIDocument;
            mDoc = mRevitDoc.Document;

            if (openFile())
            {
                // --- Get a list of all FamilySymbols
                FilteredElementCollector familySymbolsCollector = new FilteredElementCollector(mDoc);
                mFamilySymbolsList = familySymbolsCollector.WhereElementIsElementType().ToElements();

                // --- Get a list of all Reference Planes
                FilteredElementCollector refPlaneCollector = new FilteredElementCollector(mDoc);
                ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_CLines);
                mReferencePlaneList = refPlaneCollector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
                
                string str;

                str = mFileReader.ReadLine();

                int row = 0;

                // --- Read through each line of the file
                while (str != null)
                {
                    // --- Ignore blank lines
                    if (str.CompareTo("") == 0)
                        continue;

                    // --- Split the string at every tab
                    string[] words = str.Split('\t');

                    if (words != null)
                    {
                        int numWords = words.Length;

                        if (numWords > 0)
                        {
                            string familyName = TidyString(words[0]);

                            // --- Ignore the first row if the first cell is "Family"
                            if ((row > 0) || (!stringsMatchIgnoreCase(familyName,"FAMILY")))
                            {
                                string familyType = numWords > 1 ? TidyString(words[1]) : null;
                                string position = numWords > 2 ? TidyString(words[2]) : null;
                                string host = numWords > 3 ? TidyString(words[3]) : null;
                                insertLight(familyName, familyType, position, host);
                            }
                        }
                    }

                    row++;
                    str = mFileReader.ReadLine();
                }

                mFileReader.Close();
            }
        }
        
        // ---------------------------------------------------------------------------------------------------------
        
        private FamilySymbol findFamilySymbol(string familyName, string symbolName)
        {
            FamilySymbol fs = null;

            foreach (Element e in mFamilySymbolsList)
            {
                fs = e as FamilySymbol;

                if (fs != null)
                {
                    Category category = fs.Category;
                    if ((fs.Family.Name.CompareTo(familyName)==0) && (category.Name.CompareTo("Lighting Fixtures") == 0))
                    {
                        string FamilySymbolName = fs.Name;
                        if (fs.Name.CompareTo(symbolName) == 0)
                        {
                            return fs;
                        }
                    }
                }
            }

            return null;
        }


        // ---------------------------------------------------------------------------------------------------------

        private ReferencePlane findReferencePlaneByName(string name)
        {
            foreach (Element e in mReferencePlaneList)
            {
                ReferencePlane rp = e as ReferencePlane;
                if ((rp != null) && (rp.Name.CompareTo(name)==0))
                    return rp;
            }
            return null;
        }

        // ---------------------------------------------------------------------------------------------------------
        
        private void insertLight(string familyName, string familyType, string position, string host)
        {
            if (familyType == null)
                return;

            if (position == null)
                return;

            string[] words = position.Split(',');

            string xStr = words.Length > 0 ? TidyString(words[0]) : null;
            string yStr = words.Length > 1 ? TidyString(words[1]) : null;

            double x = 0;
            double y = 0;

            try
            {
                x = Convert.ToDouble(xStr);
            }
            catch
            {
                MessageBox.Show("Unable to convert " + xStr + " to a double", "Electrical Tools");
            }

            try
            {
                y = Convert.ToDouble(yStr);
            }
            catch
            {
                MessageBox.Show("Unable to convert " + yStr + " to a double", "Electrical Tools");
            }

            // MessageBox.Show("Insert: " + familyName + "  " + familyType + " X: " + x.ToString() + " Y: " + y.ToString() + " on: " + host);

            // --- Convert the mm values into feet
            x = mmToFeet(x);
            y = mmToFeet(y);

            FamilySymbol fs = findFamilySymbol(familyName, familyType);

            if (fs == null)
            {
                MessageBox.Show("Can't find Family Symbol: " + familyType);
                return;
            }

            ReferencePlane rp = findReferencePlaneByName(host);

            if (rp == null)
            {
                MessageBox.Show("Can't find Reference Plane: " + host);
                return;
            }

            XYZ pt = new XYZ(x, y, 0);

            XYZ refDir = new XYZ(rp.Normal.Z, rp.Normal.X, rp.Normal.Y);

            Transaction trans = new Transaction(mRevitDoc.Document, "Insert Light");
            trans.Start();

            mRevitDoc.Document.Create.NewFamilyInstance(rp.Reference, pt, refDir, fs);

            trans.Commit();

            /*
            System.Collections.IEnumerator iter;
            Autodesk.Revit.UI.Selection.Selection sel;
            sel = mRevitDoc.Selection;

            ElementSet elemSet;
            elemSet = sel.Elements;

            Transaction trans = new Transaction(mRevitDoc.Document, "Lights Camera Action!");
            trans.Start();

            // --- Check elements have been selected
            if (0 == elemSet.Size)
            {
                MessageBox.Show("Select the reference plane and a light", "Electrical Tools");
                return;
            }

            iter = elemSet.ForwardIterator();

            Autodesk.Revit.DB.Element element;

            ReferencePlane mReferencePlane = null;
            FamilyInstance mFamilyInstance = null;
            FamilySymbol mFamilySymbol = null;

            while (iter.MoveNext())
            {
                element = (Autodesk.Revit.DB.Element)iter.Current;

                ReferencePlane rp = element as ReferencePlane;

                FamilyInstance fi = element as FamilyInstance;

                if (rp != null)
                {
                    //mReferencePlane = rp;
                }
                else if (fi != null)
                {
                    mFamilyInstance = fi;
                    mFamilySymbol = fi.Symbol;
                }
            }
            //
            //615738
            mReferencePlane = mRevitDoc.Document.get_Element(new ElementId(617385)) as ReferencePlane;

            XYZ pt = new XYZ(0, 0, 0);
            XYZ refDir = new XYZ(0, 0, 0);

            mRevitDoc.Document.Create.NewFamilyInstance(mReferencePlane.Reference, pt, refDir, mFamilySymbol);

            trans.Commit();
            */
        }


        // ---------------------------------------------------------------------------------------------------------

        private bool stringsMatchIgnoreCase(string str1, string str2)
        {
            string str1Upper = str1.ToUpper();
            string str2Upper = str2.ToUpper();

            if (str1Upper.CompareTo(str2Upper) == 0)
                return true;

            return false;
        }

        // ---------------------------------------------------------------------------------------------------------
        
        private double mmToFeet(double mmValue)
        {
            return mmValue / 304.8;
        }

        // ---------------------------------------------------------------------------------------------------------

        // --- Function to remove leading and trailing spaces. Also removes apostrephes (')
        // --- at the start of strings (used for explicit expressions in Excel)
        private string TidyString(string stringIn)
        {
            if (stringIn == null)
                return null;

            // --- Ignore empty strings
            if (stringIn.CompareTo("") == 0)
                return stringIn;

            // --- Remove leading and trailing spaces
            string stringOut = stringIn.Trim();

            // --- Knock off a leading apostrphe (used in Excel)
            if (stringOut[0] == '\'')
                stringOut = stringOut.Substring(1);

            // --- Knock off a leading speech mark (used in Excel)
            if (stringOut[0] == '\"')
                stringOut = stringOut.Substring(1);

            // --- Knock off a trailing speech mark (used in Excel)
            if (stringOut[stringOut.Length - 1] == '\"')
                stringOut = stringOut.Substring(0, stringOut.Length - 1);

            return stringOut;
        }

        // ---------------------------------------------------------------------------------------------------------

        private bool openFile()
        {
            string filename = "";

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Select Input File";

            dlg.Filter = "Text (*.txt)|*.txt";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filename = dlg.FileName;
            }
            else
            {
                return false;
            }

            mFileReader = new StreamReader(filename);

            if (mFileReader == null)
                return false;

            return true;
        }
    }
}
