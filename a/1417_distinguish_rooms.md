<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- Diane Christoforo RE: How to distinguish between "Redundant" and "NotEnclosed" Rooms?

How to Distinguish Redundant Rooms #revitAPI #3dwebcoder @AutodeskRevit #adsk #aec #bim

A couple of interesting Revit API issues were resolved during my recent absence. Let's start with this question raised by Miroslav Schonauer and resolved by Diane Christoforo: Using the terminology as shown in Schedules, I need to report all 'Not Placed', 'Redundant' and 'Not Enclosed' rooms...

-->

### How to Distinguish Redundant Rooms

A couple of interesting Revit API issues were resolved during my recent absence.

Let's start with this question raised by Miroslav Schonauer and resolved by Diane Christoforo:

**Question:** Using the terminology as shown in Schedules, I need to report all 'Not Placed', 'Redundant' and 'Not Enclosed' rooms.

I cannot rely on any particular schedule being present in the model.

So far, my research shows that:

- All of these 3 'failure' cases have `.Area` as 0.
- 'Not Placed' additionally has `.Location` as null.

The only remaining issue is how to distinguish between 'Redundant' and 'Not Enclosed' rooms.

Do you have any ideas how can I do this comprehensively and deterministically from the room properties and params?

BTW, I'm aware that an alternative approach would be via the Failure API. I can see, e.g., the enumeration value  `BuiltInFailures.RoomFailures.RoomNotEnclosed`.

After just reading about it and looking into some samples, I worry about this approach and think that it is possible to 'override' the failures, which may prevent me from determining whether the rooms really are Not Enclosed or Redundant.

Here is sample code illustrating the approach I have been able to develop so far:

<pre class="code">
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Distinguish 'Not Placed', 'Redundant' and 'Not Enclosed' rooms.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">void</span> DistinguishRooms(
&nbsp; &nbsp; <span class="teal">Document</span> doc,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="teal">StringBuilder</span> sb,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">int</span> numErr,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">int</span> numWarn )
&nbsp; {
&nbsp; &nbsp; sb = <span class="blue">new</span> <span class="teal">StringBuilder</span>();
&nbsp;
&nbsp; &nbsp; <span class="teal">FilteredElementCollector</span> rooms
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc );
&nbsp;
&nbsp; &nbsp; rooms.WherePasses( <span class="blue">new</span> <span class="teal">RoomFilter</span>() );
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Room</span> r <span class="blue">in</span> rooms )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; sb.AppendFormat( <span class="maroon">&quot;\r\n&nbsp; Room {0}:'{1}': &quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; r.Id.ToString(), r.Name );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( r.Area &gt; 0 ) <span class="green">// OK if having Area</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; sb.AppendFormat( <span class="maroon">&quot;OK (A={0}[ft3])&quot;</span>, r.Area );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">else</span> <span class="blue">if</span>( <span class="blue">null</span> == r.Location ) <span class="green">// Unplaced if no Location</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; sb.AppendFormat( <span class="maroon">&quot;UnPlaced (Location is null)&quot;</span> );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; sb.AppendFormat( <span class="maroon">&quot;NotEnclosed or Redundant &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;- how to distinguish?&quot;</span> );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
</pre>

Any ideas if this is possible?

**Answer:** Rooms that are redundant still have normal boundaries.

So you can call `Room.GetBoundarySegments` to test.

The list will be empty for a non-enclosed or unplaced room.

**Response:** Thank you, Diane!

Here is the final solution implementing your suggestion:

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="blue">enum</span> <span class="teal">RoomState</span>
&nbsp; {
&nbsp; &nbsp; Unknown,
&nbsp; &nbsp; Placed,
&nbsp; &nbsp; NotPlaced,
&nbsp; &nbsp; NotEnclosed,
&nbsp; &nbsp; Redundant
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Distinguish 'Not Placed',&nbsp; 'Redundant' </span>
&nbsp; <span class="gray">///</span><span class="green"> and 'Not Enclosed' rooms.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="teal">RoomState</span> DistinguishRoom( <span class="teal">Room</span> room )
&nbsp; {
&nbsp; &nbsp; <span class="teal">RoomState</span> res = <span class="teal">RoomState</span>.Unknown;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( room.Area &gt; 0 )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="green">// Placed if having Area</span>
&nbsp;
&nbsp; &nbsp; &nbsp; res = <span class="teal">RoomState</span>.Placed;
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">else</span> <span class="blue">if</span>( <span class="blue">null</span> == room.Location )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="green">// No Area and No Location =&gt; Unplaced</span>
&nbsp;
&nbsp; &nbsp; &nbsp; res = <span class="teal">RoomState</span>.NotPlaced;
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="green">// must be Redundant or NotEnclosed</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">SpatialElementBoundaryOptions</span> opt
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">SpatialElementBoundaryOptions</span>();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">IList</span>&lt;<span class="teal">IList</span>&lt;<span class="teal">BoundarySegment</span>&gt;&gt; segs
&nbsp; &nbsp; &nbsp; &nbsp; = room.GetBoundarySegments( opt );
&nbsp;
&nbsp; &nbsp; &nbsp; res = ( <span class="blue">null</span> == segs || segs.Count == 0 )
&nbsp; &nbsp; &nbsp; &nbsp; ? <span class="teal">RoomState</span>.NotEnclosed
&nbsp; &nbsp; &nbsp; &nbsp; : <span class="teal">RoomState</span>.Redundant;
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> res;
&nbsp; }
</pre>

Jeremy adds: I added Miro's test methods as `DistinguishRoomsDraft` and `DistinguishRoom`
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2016.0.126.8](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2016.0.126.8) in
the module [CmdListAllRooms.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdListAllRooms.cs#L30-L110).

Many thanks to Miro and Diane for the interesting question, research and answer.

I actually cleaned up both the draft and final methods before publishing them, with the two following strong recommendations
on [performance](#2) and [exception handling](#3):

#### <a name="2"></a>Performance

Looking at your code, I notice an important inefficiency:

<pre>
  foreach( Element elRoom in fecRooms.ToElements() )
  {
    Room r = elRoom as Room;
</pre>

You can save yourself the duplication of the entire list of rooms by eliminating the call to `ToElements`, and the cast is not needed either:

<pre>
  foreach( Room r in fecRooms )
</pre>

I hope this helps... systematically!

I hope you can eliminate a thousand calls to `ToElements` all over all your projects.

I discussed this and other similar issues many times here in the past, e.g.
for [FindElement and filtered element collector optimisation](http://thebuildingcoder.typepad.com/blog/2012/09/findelement-and-collector-optimisation.html)
and
[ToElementIds performance](http://thebuildingcoder.typepad.com/blog/2012/12/toelementids-performance.html).


#### <a name="3"></a>Exception Handling

I eliminated a catch-all exception handler from Miro's solution.

Are you aware of the strong recommendation against never, ever, catching all exceptions?

Using a `catch` with no specific exceptions listed is very strongly discouraged, cf. e.g. these articles
on [C# catching all exceptions](http://stackoverflow.com/questions/315948/c-catching-all-exceptions)
and [why <code>catch(Exception)</code> and empty <code>catch</code> is bad](http://blogs.msdn.com/b/dotnet/archive/2009/02/19/why-catch-exception-empty-catch-is-bad.aspx).

Here is another more extensive discussion
on [exception handling in C# with the "Do Not Catch Exceptions That You Cannot Handle" rule in mind](http://www.codeproject.com/Articles/7557/Exception-Handling-in-C-with-the-quot-Do-Not-Catch).

