<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Named Guid Storage for Project Identification #revitAPI #3dwebcoder @AutodeskRevit #adsk #aec #bim

I want to continue working on the TrackChangesCloud project asap.
So far, it only consists of the Revit add-in to determine and list the changes made to the BIM.
The interesting part will be to store the results in a cloud database for analysis and reporting.
A prerequisite for that is a reliable way to identify Revit project documents.
I already explored that topic when starting to implement the FireRatingCloud sample...

-->

### Named Guid Storage for Project Identification

I want to continue working on
the [TrackChangesCloud](https://github.com/jeremytammik/TrackChangesCloud) project asap.

So far, it only consists of the Revit add-in to determine and list the changes made to the BIM.

The interesting part will be to store the results in a cloud database for analysis and reporting.

A prerequisite for that is a reliable way to identify Revit project documents.

I already explored that topic
when [starting to implement](http://thebuildingcoder.typepad.com/blog/2015/07/firerating-and-the-revit-python-shell-in-the-cloud-as-web-servers.html#4)
the [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud) sample, writing
about [implementing Mongo database relationships](http://the3dwebcoder.typepad.com/blog/2015/07/implementing-mongo-database-relationships.html)
and [identifying a RVT project](http://the3dwebcoder.typepad.com/blog/2015/07/implementing-mongo-database-relationships.html#2).

I was not completely happy with that solution, and have not heard of any really perfect way to achieve what I want, so I decided to try a different tack this time:

Simply create my own Guid for the current Revit project and use that to identify it globally forever after.

Actually, I decided for a slightly more generic approach, supporting 'named Guid storage'.

I define an extensible storage schema named `JtNamedGuidStorageSchema` that just stores one single Guid object.

To create a new project identifier, I create a GUID and store it in an extensible storage entity with that schema on a Revit `DataStorage` element with a specific element name, e.g., `TrackChanges_project_identifier`.

To search for an existing project identifier, I can filter for all data storage elements with extensible storage entities containing data matching our specific schema and with the given element name.

One danger in this approach is that an existing project that already defines its own identifier might be copied to one or more follow-up projects so that its identifier is retained and reused.

Ah well, I guess I will live with that.


#### <a name="2"></a>JtNamedGuidStorage Implementation Class

Here is the new `JtNamedGuidStorage` class that I just implemented and added
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples):


<pre class="code">
<span class="blue">class</span> <span class="teal">JtNamedGuidStorage</span>
{
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> The extensible storage schema, </span>
&nbsp; <span class="gray">///</span><span class="green"> containing one single Guid field.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">class</span> <span class="teal">JtNamedGuidStorageSchema</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">public</span> <span class="blue">readonly</span> <span class="blue">static</span> <span class="teal">Guid</span> SchemaGuid = <span class="blue">new</span> <span class="teal">Guid</span>(
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;{5F374308-9C59-42AE-ACC3-A77EF45EC146}&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; &nbsp; <span class="gray">///</span><span class="green"> Retrieve our extensible storage schema </span>
&nbsp; &nbsp; <span class="gray">///</span><span class="green"> or optionally create a new one if it does</span>
&nbsp; &nbsp; <span class="gray">///</span><span class="green"> not yet exist.</span>
&nbsp; &nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; &nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="teal">Schema</span> GetSchema(
&nbsp; &nbsp; &nbsp; <span class="blue">bool</span> create = <span class="blue">true</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Schema</span> schema = <span class="teal">Schema</span>.Lookup( SchemaGuid );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( create &amp;&amp; <span class="blue">null</span> == schema )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">SchemaBuilder</span> schemaBuilder =
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">SchemaBuilder</span>( SchemaGuid );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; schemaBuilder.SetSchemaName(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;JtNamedGuidStorage&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; schemaBuilder.AddSimpleField(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Guid&quot;</span>, <span class="blue">typeof</span>( <span class="teal">Guid</span> ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; schema = schemaBuilder.Finish();
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> schema;
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Retrieve an existing named Guid </span>
&nbsp; <span class="gray">///</span><span class="green"> in the specified Revit document or</span>
&nbsp; <span class="gray">///</span><span class="green"> optionally create and return a new</span>
&nbsp; <span class="gray">///</span><span class="green"> one if it does not yet exist.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">bool</span> Get(
&nbsp; &nbsp; <span class="teal">Document</span> doc,
&nbsp; &nbsp; <span class="blue">string</span> name,
&nbsp; &nbsp; <span class="blue">out</span> <span class="teal">Guid</span> guid,
&nbsp; &nbsp; <span class="blue">bool</span> create = <span class="blue">true</span> )
&nbsp; {
&nbsp; &nbsp; <span class="blue">bool</span> rc = <span class="blue">false</span>;
&nbsp;
&nbsp; &nbsp; guid = <span class="teal">Guid</span>.Empty;
&nbsp;
&nbsp; &nbsp; <span class="green">// Retrieve a DataStorage element with our</span>
&nbsp; &nbsp; <span class="green">// extensible storage entity attached to it</span>
&nbsp; &nbsp; <span class="green">// and the specified element name.</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">ExtensibleStorageFilter</span> f
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">ExtensibleStorageFilter</span>(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">JtNamedGuidStorageSchema</span>.SchemaGuid );
&nbsp;
&nbsp; &nbsp; <span class="teal">DataStorage</span> dataStorage
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">DataStorage</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; .WherePasses( f )
&nbsp; &nbsp; &nbsp; &nbsp; .Where&lt;<span class="teal">Element</span>&gt;( e =&gt; name.Equals( e.Name ) )
&nbsp; &nbsp; &nbsp; &nbsp; .FirstOrDefault&lt;<span class="teal">Element</span>&gt;() <span class="blue">as</span> <span class="teal">DataStorage</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( dataStorage == <span class="blue">null</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( create )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">using</span>( <span class="teal">Transaction</span> t = <span class="blue">new</span> <span class="teal">Transaction</span>(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc, <span class="maroon">&quot;Create named Guid storage&quot;</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; t.Start();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Create named data storage element</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dataStorage = <span class="teal">DataStorage</span>.Create( doc );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dataStorage.Name = name;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Create entity to store the Guid data</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Entity</span> entity = <span class="blue">new</span> <span class="teal">Entity</span>(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">JtNamedGuidStorageSchema</span>.GetSchema() );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; entity.Set( <span class="maroon">&quot;Guid&quot;</span>, guid = <span class="teal">Guid</span>.NewGuid() );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Set entity to the data storage element</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dataStorage.SetEntity( entity );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; t.Commit();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; rc = <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="green">// Retrieve entity from the data storage element.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Entity</span> entity = dataStorage.GetEntity(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">JtNamedGuidStorageSchema</span>.GetSchema( <span class="blue">false</span> ) );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Assert( entity.IsValid(),
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected a valid extensible storage entity&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( entity.IsValid() )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; guid = entity.Get&lt;<span class="teal">Guid</span>&gt;( <span class="maroon">&quot;Guid&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; rc = <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> rc;
&nbsp; }
}
</pre>


#### <a name="3"></a>CmdNamedGuidStorage Test Command

As you can see, the implementation defines one single public entry point `Get`.

By default, it retrieves an existing named Guid from the given Revit document, and creates a new one if none already exists.

The creation can be suppressed by passing a `false` value for the `create` argument.

I exercise it in the trivial external command `CmdNamedGuidStorage` as follows:

<pre class="code">
&nbsp; <span class="teal">Result</span> rslt = <span class="teal">Result</span>.Failed;
&nbsp;
&nbsp; <span class="blue">string</span> name = <span class="maroon">&quot;TrackChanges_project_identifier&quot;</span>;
&nbsp; <span class="teal">Guid</span> named_guid;
&nbsp;
&nbsp; <span class="blue">bool</span> rc = <span class="teal">JtNamedGuidStorage</span>.Get( doc,
&nbsp; &nbsp; name, <span class="blue">out</span> named_guid, <span class="blue">false</span> );
&nbsp;
&nbsp; <span class="blue">if</span>( rc )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Util</span>.InfoMsg( <span class="blue">string</span>.Format(
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;This document already has a project &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;identifier: {0} = {1}&quot;</span>,
&nbsp; &nbsp; &nbsp; name, named_guid.ToString() ) );
&nbsp;
&nbsp; &nbsp; rslt = <span class="teal">Result</span>.Succeeded;
&nbsp; }
&nbsp; <span class="blue">else</span>
&nbsp; {
&nbsp; &nbsp; rc = <span class="teal">JtNamedGuidStorage</span>.Get( doc,
&nbsp; &nbsp; &nbsp; name, <span class="blue">out</span> named_guid, <span class="blue">true</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( rc )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Util</span>.InfoMsg( <span class="blue">string</span>.Format(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Created a new project identifier &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;for this document: {0} = {1}&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; name, named_guid.ToString() ) );
&nbsp;
&nbsp; &nbsp; &nbsp; rslt = <span class="teal">Result</span>.Succeeded;
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Util</span>.ErrorMsg( <span class="maroon">&quot;Something went wrong&quot;</span> );
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="blue">return</span> rslt;
</pre>

Here is the message box displayed and logged on the Visual Studio debug output console by the test command on creating a new named Guid storage:

<center>
<img src="img/named_guid_storage_01.png" alt="Retrieving a named Guid from extensible storage on a DataStorage element" width="345">
</center>

A similar message is generated on retrieving an existing identifier:

<pre>
This document already has a project identifier: TrackChanges_project_identifier = 4223cc56-e6ae-4ab9-92da-1da69c72bd10
</pre>

The new functionality discussed above is included
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2016.0.127.1](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2016.0.127.1).

I look forward to hearing what you think of it.
Thank you in advance for any comments you may have.

Now I am excited to get going with the TrackChangesCloud project again, after spending a lot of time last week answering Revit cases
and [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) questions.

