<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

Design a Better Future with Forma’s Suite of Sustainability Solutions
https://blogs.autodesk.com/forma/2023/05/08/sustainability-solutions/


- View's Workset Editable - Change from No to Yes with the API?
  https://forums.autodesk.com/t5/revit-api-forum/view-s-workset-editable-change-from-no-to-yes-with-the-api/m-p/11997681#M71740
  [Q] Can anyone tell me if it's possible to change a view's workset status from "No" to "Yes" (editable) using the Revit API (Revit 2023 or earlier). See image blow.
I'm not asking to have anyone do my work for me. I just want to know if it's possible and maybe toss me a hint or two? I've searched the internet and I've found several similar posts saying it wasn't possible but they were for much older versions of Revit.
Thanks in advance!
view_workset_editable.jpg
[A]
explanation by RPT and solution by Jonas Hoyer

- Selection.SetReferences
  https://autodesk.slack.com/archives/C0SR6NAP8/p1685354466881299
  Shen Wang
  A customer raised a Revit API question about PickObjects (ObjectType.LinkedElement).
  After he gets the graphic element in the linked file via PickObjects(ObjectType.LinkedElement), how to make the linked graphic element selected in the current view, just like to achieve the effect of TAB button to select the linked element.
  Martha Hirstoaga
  Hello, since Revit 2023 you can use Autodesk.Revit.UI.Selection.SetReferences(IList<Reference> references), which  selects the given references. Reference can be an element or a subelement in the host or a linked document.
  Shen Wang
  If the customer is using the version less than 2023, is the above variable still available? Otherwise, does he have any other choice before 2023? Since he is still using 2020.
  Martha Hirstoaga
  In Revit Pre-2023, only Autodesk.Revit.UI.Selection.SetElementIds(ICollection<ElementId> elementIds) was available, being able to select only elements in the host document. I am not aware of any other way to select an element  from other document in pre-2023 Revit.

- WallType naming best practices
  https://forums.autodesk.com/t5/revit-api-forum/is-it-possible-to-change-the-walltype-name-through-api/m-p/11990210

twitter:

 in the @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS

&ndash;
...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

###

####<a name="2"></a> Forma Sustainability Flyer

AU 2022 introduced [Forma for AEC](https://thebuildingcoder.typepad.com/blog/2022/09/aps-au-and-miter-wall-join-for-full-face.html#3 without
gping into much detail.
More detail is added by
the [Forma sustainability flyer](/Users/jta/a/doc/revit/tbc/git/a/zip/forma_sustainability_flyer.pdf):

> [Design a Better Future with Forma’s Suite of Sustainability Solutions](https://blogs.autodesk.com/forma/2023/05/08/sustainability-solutions):

> Cities consume more than two-thirds of the world’s energy and account for over 70% of global carbon emissions
([source](https://unfccc.int/news/urban-climate-action-is-crucial-to-bend-the-emissions-curve#:~:text=Cities%20consume%20over%20two%2Dthirds,Asia%20and%20Sub%2DSaharan%20Africa)).
This means architects, real estate developers, and urban planners have an exceptional opportunity to mitigate the environmental impact of our cities by designing buildings and communities with sustainable outcomes in mind.

> Sustainable outcomes are best achieved through a proactive, data-driven approach that starts at the earliest stages of design before it becomes costly and difficult to make changes.
Autodesk Forma’s powerful suite of real-time analyses equips design teams with the quick, visual insights needed to prioritize sustainability from day one of a project.

####<a name="2"></a> RevitLookup Enhanced Member Support Optiobs


@jeremytammik We need to discuss which options should be enabled by default


Enhanced member support #168
https://github.com/jeremytammik/RevitLookup/pull/168

# Summary of the Pull Request

**Description:**

Fields, events support
![изображение](https://github.com/jeremytammik/RevitLookup/assets/20504884/ae8cdea7-ec76-4618-b72a-fbbb9361225e)

Private members support
![изображение](https://github.com/jeremytammik/RevitLookup/assets/20504884/8f945edb-6365-4d28-89a8-b8a8d2b0e33e)

Settings update
![изображение](https://github.com/jeremytammik/RevitLookup/assets/20504884/550b593c-2039-4ec6-a665-ac0fe0e65215)

## Quality Checklist

- [x] My code follows the style guidelines of this project
- [x] I have performed a self-review of my own code
- [ ] I have made corresponding changes to the documentation
- [x] My changes generate no new warnings


@jeremytammik in theory, we could do a release) and then do a survey


That sounds like an effective approach to me. Then, I can mention on the blog to please take a look and respond within a given time frame, for example. How long? Or leave it open?


If you do a blog mention it's not obvious where users can vote) Let's maybe gather feedback on the new release and then release the poll on GitHub


yes, better. then i'll just mention the new release and ask people to provide feedback on it.

https://github.com/jeremytammik/RevitLookup/pull/169
Release 2024.0.6 #169



Cleanup
6ef136a
@Nice3point
Update Nuke
112ff8f
@Nice3point
Fix GetMaterialIds
f371559
@Nice3point
DefinitionGroup support
a2acf7c
@Nice3point
Disable Show for ElementType
6d7e4f7
@Nice3point
Demo project restore
0b0259a
@Nice3point
Nuke update
3b93574
@Nice3point
Material area, volume support
1a3d6b8
@Nice3point
Static members support
0760578
@Nice3point
Update Changelog.md
f8fdc6f
@Nice3point
Update nuget packages
298f227
@Nice3point
Update Codeowners
b3cd202
@Nice3point
FamilyInstance rooms support
8572c8f
@Nice3point
Update Changelog.md
4d8afe4
@Nice3point
Merge remote-tracking branch 'origin/master' into dev
f0160ae
@Nice3point
Separate UI thread
3f07edc
@Nice3point
CleanUp
0658f26
@Nice3point
Merge branch 'dev' into dev_threads
25f571b
@Nice3point
Remove debug RenderOptions
c8f2c45
@Nice3point
Fix initialisation render mode
561a2f9
@Nice3point
Separate thread for UI (#166)
adc82da
@Nice3point
Update Build.Installer
d3a2ce9
@Nice3point
Merge remote-tracking branch 'origin/dev' into dev
1e83db1
@Nice3point
Fix context menu dispatcher
a45712f
@Nice3point
Icons support
610ddfe
@Nice3point
Icons support (#167)
706a43c
@Nice3point
Merge remote-tracking branch 'origin/dev_icons' into dev
1fb7112
@Nice3point
Pages virtualization
5dc922d
@Nice3point
Category redirection support
f57949c
@Nice3point
Rework metadata builder
0540675
Nice3point added 12 commits 16 hours ago
@Nice3point
Fields, events support
d09cffa
@Nice3point
Fix R24 compability
77297de
@Nice3point
Bump version
9cd52ce
@Nice3point
Update Contributing.md
84501bd
@Nice3point
Update Changelog.md
9586242
@Nice3point
Update Changelog.md
6473f88
@Nice3point
Update icon
6336f68
@Nice3point
Disable transition by default
1df9e4a
@Nice3point
Enhanced member support (#168)
7667116
@Nice3point
Icons update
127fb42
@Nice3point
Merge branch 'dev_members' into dev
1637ada
@Nice3point
Update Changelog.md



2024.0.6

Repository: jeremytammik/RevitLookup · Tag: 2024.0.6 · Commit: 80ddf15 · Released by: github-actions[bot]

Features

User interface

Icons

Introducing a collection of new icons for properties, methods, fields, and events, ensuring a visually appealing representation
image
https://github-production-user-asset-6210df.s3.amazonaws.com/20504884/242402267-ffbba475-e240-4928-bf02-68d8f75cbc4c.png

Enhanced Performance with Separate UI Thread

The RevitLookup user interface now operates in a dedicated thread, independent of Revit's workload. This architectural improvement significantly enhances smoothness and responsiveness

New Additional Setting Options

Introducing a range of new setting options that expand customization capabilities and provide users with greater control over the tool's behavior

Core

Class fields

Introducing support for displaying class fields, enabling a comprehensive understanding of the class structure
image
https://github-production-user-asset-6210df.s3.amazonaws.com/20504884/242403456-a4304fd4-4537-4bd2-8d90-88f46137a55a.png


Class events

Introducing support for displaying class events, facilitating better comprehension of event-driven programming within the class
image
https://github-production-user-asset-6210df.s3.amazonaws.com/20504884/242403960-3b7ae347-e7bc-4642-89a0-99cd089f0abe.png

Class private members

Empowering developers with the ability to visualize and access class private fields, properties, methods, and events, providing a complete overview of the class implementation
image
https://github-production-user-asset-6210df.s3.amazonaws.com/20504884/242406752-4c6e4459-cf2f-4d35-9b03-fe0b259b3c9a.png

Improvements

ElementId Redirection to Category

Implemented a helpful feature that automatically redirects ElementId to Category, whenever applicable. This simplifies navigation and enhances the user experience

Content Virtualization

Applied content virtualization to the dashboard and settings page, optimizing performance by efficiently managing large amounts of data and dynamically loading content as needed. This results in a smoother and more efficient user interaction

—
This release has 10 assets:

RevitLookup-2021.2.6-MultiUser.msi
RevitLookup-2021.2.6-SingleUser.msi
RevitLookup-2022.2.6-MultiUser.msi
RevitLookup-2022.2.6-SingleUser.msi
RevitLookup-2023.2.6-MultiUser.msi
RevitLookup-2023.2.6-SingleUser.msi
RevitLookup-2024.0.6-MultiUser.msi
RevitLookup-2024.0.6-SingleUser.msi
Source code (zip)
Source code (tar.gz)
Visit the release page to download them.

https://github.com/jeremytammik/RevitLookup/releases




####<a name="2"></a> Modify View Workset Editable

explanation by RPT and solution by Jonas Hoyer

A nice explanation and succinct solution
by Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
and [Jonas Hoyer](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/8174024)
on [view's workset editable &ndash; change from No to Yes with the API?](https://forums.autodesk.com/t5/revit-api-forum/view-s-workset-editable-change-from-no-to-yes-with-the-api/m-p/11997681):

**Question:** Can anyone tell me if it's possible to change a view's workset status from "No" to "Yes" (editable) using the Revit API (Revit 2023 or earlier), in this form:

<center>
<img src="img/view_workset_editable.jpg" alt="View workset editable" title="View workset editable" width="313"/> <!-- Pixel Height: 354 Pixel Width: 313 -->
</center>

I searched the Internet and found several similar posts saying it wasn't possible, but they were for much older versions of Revit.
Thanks in advance!

**Explanation:** When you take ownership of a workset, it becomes editable to that user 'Yes'; when you relinquish the workset it reverts to 'No' (which doesn't actually mean you can't edit it).

[WorksharingUtils CheckoutWorksets](https://www.revitapidocs.com/2023/39b55560-c85b-bebc-e825-b76b5ba313a7.htm) allows
the user to take ownership of the workset and make them editable to that user.

[RelinquishOwnership](https://www.revitapidocs.com/2023/09f4e163-cb8f-de87-d641-3ba667adf4e0.htm) allows
user to decide via RelinquishOptions what is relinquished (given back) including various workset types.

I believe the 'editable' indicator is just a function of if the current user has taken ownership of a certain workset or not, i.e., they can take ownership of a workset for whatever time period to be sole editor of it until they relinquish it.
The 'Yes/No' is only meaningful when you are the owner of it.
The interface is probably outdated and contains bad terminology to a certain extent.
The default is 'No (Non Editable)' meaning everyone can edit items on a workset via borrowing elements (so 'non editable' means you can edit it and so can everyone else).

I believe the only other time it is 'yes' is when you've just created a workset but are yet to synchronise it to central.
You can't make it 'non editable' available to all users until after you've synchronised it (because it doesn't yet exist in the central file).
So, under that circumstance, you would have to synchronise then relinquish.
Similarly, if you wanted to rename a workset you would have to make it 'editable' first.

So, in summary, the 'Editable/Non Editable' aspect should be read as relating to if current user can solely edit the workset or not rather than just being a borrower of elements on a workset.

The 'Owner' column exists separately from the 'Yes/No' one because some types of workset you decide to be the editor/owner of (user created ones) whereas when you create a view the ownership of the view workset is inherently you until synchronised.

Have you tried the [WorsharingUtils](https://www.revitapidocs.com/2023/ae7857a0-4e9b-f9c1-84c7-8b250af68068.htm) class for the View worksets in question?

Under certain circumstances described above you will not be able to influence this 'editable/non editable' aspect via the API for a single user because it is related to how two or more people are interacting in a workshared environment e.g if someone else owns the workset then you can only ask they relinquish it.

Choosing to take ownership of entire worksets is generally the old way of working before element borrowing was used.
Mostly, people take ownership just to do infrequent things such as change workset names but that isn't applicable to View worksets.
They may also take ownership to restrict access but again not for views.

**Solution:** To set the editable status of the view workset to `yes` you have to check out the workset of the view by using
the [CheckoutWorksets](https://www.revitapidocs.com/2023/97f0d4eb-ad2a-ca9d-a896-5144bd68c5a5.htm) method
of the WorksharingUtils class.
You can retrieve the `WorksetId` of the view with the `ELEM_PARTITION_PARAM` `BuiltInParameter`.
Then, you can construct a `WorksetId` with the returned integer and use that to checkout the view's workset.
That will set the editable status to yes.
Hope this helps!

Many thanks to Richard and Jonas for the explanation and solution!

####<a name="3"></a> Selection SetReferences

FYI, a quick pointer to a relatively new API method enabling sub-element highlighting:

**Question:** After retrieving an element in a linked file via `PickObjects` with a 'LinkedElement' 'ObjectType', how can I highlight the linked element selected in the current view to achieve the effect of using the TAB key to select the linked element in the UI?

**Answer:** Since Revit 2023, you can use the Selection [`SetReferences` method](https://www.revitapidocs.com/2023/813a9d31-bc4f-1ebc-9a7b-69a2a99d22ac.htm) to highlight the references passed in to it.
A reference can be an element or a subelement in the host or a linked document.

**Question:** How about previous releases of Revit?

**Answer:** In Revit pre-2023, you only
have [SetElementIds](https://www.revitapidocs.com/2023/813a9d31-bc4f-1ebc-9a7b-69a2a99d22ac.htm).
That only enables you to select top-level elements in the host document.
I am not aware of any other way to select an element from another document in pre-2023 Revit.

####<a name="4"></a> WallType Naming Best Practices

A discussion on best practices naming `WallType` elements in the thread
asking [is it possible to change the `WallType` name through API?](https://forums.autodesk.com/t5/revit-api-forum/is-it-possible-to-change-the-walltype-name-through-api/m-p/11990210)

<!---

**Question:** Is this possible?
I am trying to change the WallType for all walls in the selection like this:

<pre class="prettyprint">
Selection selection = uidoc.Selection;
ElementSet collection = selection.Elements;

List&lt;WallType&Gt;§ type=new List&lt;Walltype&Gt;§();

int index =0;
Transaction trans = new Transaction(doc);
trans.Start("Wall Type");
foreach (Element collectedElem in collection)
{
  if (collectedElem.GetType().Name == "Wall")
  {
    Wall wall = null;
    wall = (Wall) collectedElem;

    string wallValue = collectedElem.Name.ToUpper()
      + ":" + collectedElem.Level.Name;

    // excelWallInfo is a string list loaded from external excel file.
    // Each entry is in the format WalltypeName+":"+Level+":"+NewName

    foreach (string entry in excelWallInfo)
    {
      if (entry.Contains(wallValue) == true)
      {
        string[] entryElements = entry.Split(':');
        string newWallTypeName = entryElements[2];
        if (newWallTypeName == "None" || newWallTypeName == null)
        {
          newWallTypeName = "EmptyName";
        }

        type.Add((WallType)wall.WallType.Duplicate(newWallTypeName)); //This line has problem in the second iteration.

        TaskDialog.Show("Duplicated Element Type", type[index].Name);
          wall.WallType = type[index];
          ++index;
      }
    }
  }
  trans.Commit();
}
</pre>

The above code stopped in the second iteration.
The first change is successful.
However, the second iteration throws an exception.

I don't just want to change the WallType.Name, since it will change the name of all walls of the same type.
I am trying to give each wall a new name.

**Answer:** I see several issues with your code.
Before addressing those, though, are you absolutely sure that you know what you are doing?
What you are trying to achieve sounds rather questionable to me.
Do you have an in-depth understanding of the underlying Revit BIM paradigm and best practices?

Anyway, if you really want to achieve what you say, I have the following suggestions:

I recommend [encapsulating the transaction instantiation in a C# 'using' statement](https://thebuildingcoder.typepad.com/blog/2012/04/using-using-automagically-disposes-and-rolls-back.html).

Furthermore, it will probably clarify and simplify things if you separate the different steps of your operation, instead of lumping them all into one single big iteration, e.g.:

- Iterate over the selection and collect the walls that need updating. Close the iteration.
- Iterate over excelWallInfo and determine the required wall types. Close the iteration.
- Open a transaction.
- Duplicate the required wall types.
- Iterate over the walls that need modifying and do the dirty deed.
- Commit the transaction.

--->

**Question:** I am thinking in develop a script that changes all walls' types' names based on properties of the wall types.

I sometimes see a name of the type that mentions 30cm Thickness and does not match the real thickness of the wall type, so my script should generate the wall type name based on the function (exterior, interior , etc.), the material of each layer of the wall and thickness and the total wall thickness.

For example `InteriorWall_Concrete-100` for a one layer interior wall and 100mm thickness, or

- ExteriorWall_Concrete-100_Bricks-100_200

Is that against best practices?

That plugin would avoid having different naming systems and ensure that the name of the type matches the real properties... sounds a crazy thing for you? or super complicated task?

**Answer 1:** For me, as a programmer, that makes perfect sense and is perfectly feasible.
However, I am not a BIM expert and cannot advise you on best practices.
I would suggest that you also raise that question in the generic architectural forum.

**Answer 2:** I might be able to add a bit of info for you;
my primary job is Revit tech (and support) in an Architectural firm (I just do a little coding when needed to get things working the way we need them to) and I have worked as CAD/BIM Manager at a couple of firms over the many years and I can confirm:

That is an excellent idea and will greatly benefit those working in the model(s).

**Answer 3:** This is one of the fundamental things we all want to do at some point: reduce what a type is to a string value of the main things that define it. The idea works fine until there is a certain minor detail which distinguishes two types. In the end, human beings use Revit models not robots. So there is only a certain extent to which you can codify such things and it still be readable and fit on screen where it is read. What features are most important to represent and in which order, that is always the endless discussion.


<!---

Hi @jeffreybo.liu ,

I can't follow the code completely some context is missing, but from what I think it does I would not recommend it.

Couple of things with the code / logic:

1. It tries to Duplicate a WallType X each time with a Wall Type Named Y, this fails because Wall Type Y already exists.

You should first retrieve all walltypes and if Wall Type Y already exists it can't be recreated!

Imagine creating a second walltype "EmptyName", that won't work either.

2. Why create a new Walltype altogether?, it's based on it's type properties, then just rename it.

do check if the name already exists, .... what action to take if it already exist??

3. Level, what does this have to do with the Type name of a wall, from what I see the Level is stripped from the new name, so 2 walls with identical types but on different levels would have different names (?) in the ExcelWallInfo but would be renamed identicall, again each name can only exist once in project.

4. The ExcelWallInfo list, how is this built, seems it doesn't take the layers, thickness, interior into account

5. Model Groups; walls in groups can't have their walltype changed by API etc...

6. Can't read the code clearly, (use the forums Code insert button), looks that the commit happens each iteration of the selected walls, call the Commit after all walls are processed.

+ If all this could be corrected you would end up with a lot of WallTypes....and what about the description or other text fields?

If I would need to force a naming structure just iterate the WallTypes in the project and base it's name on the properties as total width (as in you're other post), with maybe a translation dictionary if certain materials(names) are used in layers.

This is one of the fundamental things we all want to do at some point: reduce what a type is to a string value of the main things that define it. The idea works fine until there is a certain minor detail which distinguishes two types. In the end human beings use Revit models not robots. So there is only a certain extent to which you can codify such things and it still be readable and fit on screen where it is read. What features are most important to represent and in which order, that is always the endless discussion.

Regarding original post from 2014 by others the issue is likely related to:

- Name cannot contain any of the following characters:
<br/>(:01:&lt;&gt; ?'~
or any of the non-printable characters.

/Users/jta/a/doc/revit/tbc/git/a/img/invalid_symbol_characters.png
Pixel Height: 203
Pixel Width: 357

They are also using the name from GetType rather than Revit parameter value or API property i.e. not using something written specifically for RevitAPI but instead depending on a general programming feature (things inherited from object).

<pre class="prettyprint">

</pre>

--->
