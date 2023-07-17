<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- roman [jeremytammik/RevitLookup] Revit api. Retrieve all parameters and categories (Discussion #183)

twitter:


&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### All Categories and Parameters



####<a name="2"></a>

Hi Jeremy, I made a new discovery for me 🙈 As far as I know it is impossible to get a list of all Builtin parameters using the public API.  We are offered an enum but we cannot get the Parameter itself from it. By exploring private unmanaged code, using reflection and pointers I managed to do it, I get all builtin parameters of revit. If you approve I can write a publication about it

rk_get_all_bips.png
Pixel Height: 915
Pixel Width: 1,469

## Summary

I recently came across a problem from my business partner who wanted to get all the built-in Revit parameters to extract metadata such as data type, units, storage type, etc. As we know, this is not possible using Autodesk's public API.

Similar situation with categories, we can't get all the built-in categories, the only available way is to get them from the document settings, but it contains a very truncated list

![изображение](https://github.com/jeremytammik/RevitLookup/assets/20504884/faae850b-818d-463d-af52-bcd832fff416)

## Solution

We can use private code using reflection and pointers, we only need a document.
The point of the method is not to get them but create it.

Example code to get all parameters:

<pre class="prettyprint">
public static List&lt;Parameter&gt; GetBuiltinParameters(Document document)
{
    const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

    var documentType = typeof(Document);
    var parameterType = typeof(Parameter);
    var assembly = Assembly.GetAssembly(parameterType);
    var aDocumentType = assembly.GetType("ADocument");
    var elementIdType = assembly.GetType("ElementId");
    var elementIdIdType = elementIdType.GetField("&lt;alignment member&gt;", bindingFlags)!;
    var getADocumentType = documentType.GetMethod("getADocument", bindingFlags)!;
    var parameterCtorType = parameterType.GetConstructor(bindingFlags, null, new[] {aDocumentType.MakePointerType(), elementIdType.MakePointerType()}, null)!;

    var builtinParameters = Enum.GetValues(typeof(BuiltInParameter));
    var parameters = new List&lt;Parameter&gt;(builtinParameters.Length);
    foreach (BuiltInParameter builtinParameter in builtinParameters)
    {
        var elementId = Activator.CreateInstance(elementIdType);
        elementIdIdType.SetValue(elementId, builtinParameter);

        var handle = GCHandle.Alloc(elementId);
        var elementIdPointer = GCHandle.ToIntPtr(handle);
        Marshal.StructureToPtr(elementId, elementIdPointer, true);

        var parameter = (Parameter) parameterCtorType.Invoke(new[] {getADocumentType.Invoke(document, null), elementIdPointer});
        parameters.Add(parameter);
        handle.Free();
    }

    return parameters;
}
```

Example code to get all categories:

<pre class="prettyprint">

public static List&lt;Category&gt; GetBuiltinCategories(Document document)
{
    const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

    var documentType = typeof(Document);
    var categoryType = typeof(Category);
    var assembly = Assembly.GetAssembly(categoryType);
    var aDocumentType = assembly.GetType("ADocument");
    var elementIdType = assembly.GetType("ElementId");
    var elementIdIdType = elementIdType.GetField("&lt;alignment member&gt;", bindingFlags)!;
    var getADocumentType = documentType.GetMethod("getADocument", bindingFlags)!;
    var categoryCtorType = categoryType.GetConstructor(bindingFlags, null, new[] {aDocumentType.MakePointerType(), elementIdType.MakePointerType()}, null)!;

    var builtInCategories = Enum.GetValues(typeof(BuiltInCategory));
    var categories = new List&lt;Category&gt;(builtInCategories.Length);
    foreach (BuiltInCategory builtInCategory in builtInCategories)
    {
        var elementId = Activator.CreateInstance(elementIdType);
        elementIdIdType.SetValue(elementId, builtInCategory);

        var handle = GCHandle.Alloc(elementId);
        var elementIdPointer = GCHandle.ToIntPtr(handle);
        Marshal.StructureToPtr(elementId, elementIdPointer, true);

        var category = (Category) categoryCtorType.Invoke(new[] {getADocumentType.Invoke(document, null), elementIdPointer});
        categories.Add(category);
        handle.Free();
    }

    return categories;
}
```

## Result

As a result, we have created all the parameters and all the categories of the entire Enum

Parameters:

![изображение](https://github.com/jeremytammik/RevitLookup/assets/20504884/2fd8e4a6-cdec-4626-be10-4fc42e9023ac)

Categories:

![изображение](https://github.com/jeremytammik/RevitLookup/assets/20504884/f9f8bdfe-fe80-4440-8092-9efab1868d0b)

## Limitations

Created parameters have no binding to the element, and consequently have no value, only metadata


**Question:**

**Answer:**

####<a name="3"></a>

<center>
<img src="img/.png" alt="" title="Materials and Visual AP" width="100"/> <!-- Pixel Height: 143 Pixel Width: 610 -->
</center>



<pre class="prettyprint">

</pre>

**Response:**


