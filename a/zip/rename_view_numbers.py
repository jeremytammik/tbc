# these commands get executed in the current scope
from Autodesk.Revit.UI.Selection import ObjectType
from Autodesk.Revit.UI import TaskDialog
import clr
import traceback
import operator
import math
clr.AddReference('RevitAPI') 
clr.AddReference('RevitAPIUI') 
from Autodesk.Revit.DB import * 

app = __revit__.Application
doc = __revit__.ActiveUIDocument.Document
uidoc = __revit__.ActiveUIDocument
SHEET = doc.ActiveView 
LOGFILE =  "T:\Malcolm-Chris\REVIT\PYTHON REVIT SHELL\log.txt"
RAW_WRITE = True
def is_array(var):
    return isinstance(var, (list, tuple))

def log(textArr,is_raw=False):
	global LOGFILE
	filename =  LOGFILE
	if (not is_array(textArr)):
		textArr = [textArr]
	target = open(filename, 'a+')
	target.write("\n")
	for i in textArr:
		if (not is_raw):
			target.write(repr(i))
		else:
			target.write(i)
	target.close()

def clearLog():
	global LOGFILE
	filename =  LOGFILE
	target = open(filename, 'w+')
	target.write("")
	target.close()

def transpose(lis):
	return map(list, zip(*lis))

def dicViewer(dic):
	list = []
	for key,val in dic.items():
		list.append(["key:"+str(key),val])
	return list

def getPtList(dic):
	list = []
	for key,val in dic.items():
		list.append(map(lambda x: float(x), key.split(",")))
	return list

def seq(start, stop, step=1):
    n = int(round((stop - start)/float(step)))
    if n > 1:
        return([start + step*i for i in range(n+1)])
    else:
        return([])

def listToDic(list):
	dic = {}
	for i in list:
		i[0] = i[0].replace("key:","")
		dic[i[0]] = i[1]
	return dic

def xyToPoint(xyPt):
	return [xyPt[0],xyPt[1],0]
	#return Point.ByCoordinates(xyPt[0],xyPt[1],0)
	
def getClosest(x,n):
	return 	math.floor(x / n) * n;

def closestNode(coord,points):
    """Return closest point to coord from points"""
    dists = [(pow(point[0] - coord[0], 2) + pow(point[1] - coord[1], 2), point) for point in points]             
    # list of (dist, point) tuples
    nearest = min(dists)
    return nearest[1]  # return point only

def getNewDetailViewNumber(pts,offsetX, offsetY, stepX, stepY,gridPtsDic):
	newPts = []
	notClosestPts = []
	newPtsPts = [] 
	detailViewNumber = []
	for pt in pts:
		approxPt = ([round(getClosest(pt[0], stepX)+offsetX,2),round(getClosest(pt[1], stepY)+offsetY,2)])
		log(["approxPt",approxPt])
		notClosestPts.append(xyToPoint(approxPt))
		closestPt = closestNode(approxPt, getPtList(gridPtsDic))

		newPtsPts.append(xyToPoint(closestPt))
		newPts.append(closestPt)
		detailViewNumber.append(gridPtsDic[",".join(map(lambda x: str(x), closestPt))])
	#Assign your output to the OUT variable.
	return {
	"newPts":newPts,
	"detailViewNumber":detailViewNumber,
	"notClosestPts":notClosestPts, 
	"newPtsPts":newPtsPts
	}

def getPtGrid(startPt, endPt):
	
	coords = {}

	# CHANGE THIS IF GRID IS DIFFERENT! 
	detailGrid = [
		[30 ,25 ,20 ,15 ,10,5],
		[29 ,24 ,19 ,14 ,9 ,4],
		[28 ,23 ,18 ,13 ,8 ,3],
		[27 ,22 ,17 ,12 ,7 ,2],
		[26 ,21 ,16 ,11 ,6 ,1]]

	xDiv = len(detailGrid[0])
	yDiv = len(detailGrid)

	detailGridFlat = reduce(operator.add, transpose(detailGrid))
	count = 0 
	stepX = round((endPt.X-startPt.X)/(xDiv),2)
	stepY = round((endPt.Y-startPt.Y)/(yDiv),2)
	for i in seq(startPt.X, endPt.X-stepX, stepX):
		for j in seq(startPt.Y, endPt.Y-stepY,  stepY):
			coords[str(round(i,2))+","+str(round(j,2))]= detailGridFlat[count]
			count=count+1

	#Assign your output to the OUT variable.
	return { "coords": coords,
		"offsetX": startPt.X,
		"offsetY": endPt.Y,
		"stepX": stepX,
		"stepY": stepY
		}
	
def elementFromId(id):
	log(["id:", id])
	global doc
	return doc.GetElement(id)

def setParam(el, paramName, value):
	param = getParam(el, paramName, True)
	if not param.IsReadOnly:
		param.Set(value)
	else:
		log(["Coult not edit ",paramName," on ",el, ". It is either readonly or not user editable. // Read Only: ",param.IsReadOnly])

def getParam(el, paramName, asParamObject=False):
	params = getParameters(el, asParamObject)
	return params[paramName]

def getParameters(el,asParamObject=False):
	parameters = el.Parameters
	params = {}
	for param in parameters:
		if (asParamObject==False):
			params[param.Definition.Name] = param.AsString()
		else:
			params[param.Definition.Name] = param
	return params

def elementFromReference(ref):
	global doc
	id = ref.ElementId
	return doc.GetElement(id)

def getPointsFromViewports(viewport):
	outline = viewport.GetLabelOutline()
	return [outline.MaximumPoint.X,outline.MinimumPoint.Y,0]

def pickObject():
    
    __window__.Hide()
    TaskDialog.Show ("Select Objects", "Select the line representing the grid bounds after closing this dialog.")
    picked = uidoc.Selection.PickObject(ObjectType.Element)
    #__window__.Topmost = True
    #__window__.Show()
    return picked


# clear log file
clearLog()
t = Transaction(doc, 'Rename Detail Numbers')
t.Start()
 #<------------- the stuff ------------>
#lets get the guide curve
try:
	
	bbCrvRef = pickObject()
	#log(bbCrvRef)
	bbCrv = elementFromReference(bbCrvRef).GeometryCurve
	#log(bbCrv)
	pts = [bbCrv.GetEndPoint(0),bbCrv.GetEndPoint(1)]
	log(pts)
	ptGridData = getPtGrid(pts[0],pts[1])
	log(ptGridData)
	
	viewports = map(lambda x: elementFromId(x), SHEET.GetAllViewports())
	titleBlockPts = map(lambda x: getPointsFromViewports(x) ,viewports)
	log(titleBlockPts)
	detailViewNumberData = getNewDetailViewNumber(titleBlockPts,ptGridData['offsetX'], ptGridData['offsetY'],ptGridData['stepX'], ptGridData['stepY'],ptGridData['coords'])
	log(detailViewNumberData)
	log(map(lambda x: getParameters(x) ,viewports))
	log(map(lambda x: getParam(x,"Detail Number") ,viewports))
	log("Hello")

	# <---- Make unique numbers	
	for i, viewport in enumerate(viewports):
		paramName = "Detail Number"
		currentVal = getParam(viewport,"Detail Number")
		setParam(viewport, paramName,currentVal+"x")
	t.Commit()
	
	'''
	# <---- Do the thang 	
	t2 = Transaction(doc, 'Rename Detail Numbers')
	t2.Start()
	for i, viewport in enumerate(viewports):
		setParam(viewport, "Detail Number",detailViewNumberData[i])
	 t2.Commit()
	'''
	
except SyntaxError, e:
	log(["Error!\n---------\n", traceback.format_exc()],RAW_WRITE)

except Exception, e:
	log(["Error!\n---------\n", traceback.format_exc()],RAW_WRITE)

 #<------------- end of the stuff ------------>

__window__.Close()