<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- pick room in either
  https://forums.autodesk.com/t5/revit-api-forum/pickobject-to-select-room-in-current-model-or-linked-models/m-p/9624169

- How to get info that Revit custom export of a view is canceled
https://stackoverflow.com/questions/62794859/how-to-get-info-that-revit-custom-export-of-a-view-is-canceled
Q: I have used Revit custom export of a model for exporting a 3D View based on IExportContext. It works fine. But I found that the export process can be Canceled (see screen below) Custom export canceling Then appears a dialog Dialog screen I have 2 questions:
custom_export_cancel_button.png
1. How to get info that exporting was canceled?
2. Why the name of operation is Printing (see Dialog screen shot)
custom_export_cancel_printing.png
A: 
1. Implement and handle the [`IExportContext`
`IsCanceled` method](https://www.revitapidocs.com/2020/31f0b662-81a1-89b8-ab2a-0de99af3b753.htm).
2. Because the custom export is in fact a printing or exporting context, cf. the [`CustomExporter` documentation](https://www.revitapidocs.com/2020/d2437433-9183-cbb1-1c67-dedd86db5b5a.htm): *The Export method of this class triggers standard rendering or exporting process in Revit, but instead of displaying the result on screen or printer, the output is channeled through the given custom context that handles processing of the geometric as well as non-geometric information*.

- Seeking feedback from developers regarding Revit API (Derived Analytical Model)
  https://forums.autodesk.com/t5/revit-api-forum/seeking-feedback-from-developers-regarding-revit-api-derived/m-p/9615258
  Are you a developer who interacts with the Revit API regarding the Derived Analytical Model? If so, please take 5 minutes to provide your input. We would like to hear from you. Our goal is to get a better understanding what functions developers are using and how they are using them. Please feel free to forward this on to a developer you work with. Please click here to provide input: https://autodeskfeedback.az1.qualtrics.com/jfe/form/SV_9Xe2uafQg3X9xXf

- Revit API is single threaded; add-in can use multiple threads
Q: I have a quick question here: does Revit addin run as a child process of Revit or the addins run in the same process of Revit?
Also, where in the code do we start the execution of an addin?
A: I believe that it is the same process. In fact, you have to be careful to keep execution of API from the addin code on the main thread of Revit.
As to where execution begins, an addin is packaged as an application class and there are startup/shutdown methods. If you need to go lower than that, you need to find where the association of guid-to-dll from the .addin file is used.
Also the same thread (main):
Here it is:
https://git.autodesk.com/revit/revit/blob/f646bd8157f522092b98519596f3aa8e08e6179e/Source/API/AddInManager/ExternalDBApplicationManager.cpp#L216
"You have to be careful to keep execution of API from the addin code on the main thread of Revit."
Q: Why do we need to be careful to keep the execution in the main thread?
A: You cannot call Revit API from multiple threads, because most of Revit code is a critical section.
Weird corruptions and crashes; it's totally against the way revit runs
It can be quite easy to accidentally let some code call Revit API from a non-main thread. I think it can happen when you have timers and UI containers that update asynchronously and need to update something in Revit.
Here is an expalantion of [](https://thebuildingcoder.typepad.com/blog/2014/11/the-revit-api-is-never-ever-thread-safe.html).
R: Okay, I understand the thread-safety and critical section ideas.
I think process and thread are different concept.
Processes are typically independent of each other, While threads exist as the subset of a process
Does it mean: Revit addin runs in the main thread of Revit, so addin and Revit are in the same process, they share the memory? this is the question we want to know.
A: Yes
Although, no, that is not entirely true. Revit addins can be multi-threaded. The Revit API can only be correctly accessed from the main thread.
That is one reason we have ExternalEvents (which give addins a chance to safely call Revit API methods when another thread says they need to)
Q: Is it entirely true that Revit and the addin share the same memory?
A: The way I understand it is that an addin can spawn threads for computation or UI purposes, but calls to RevitAPI should be made from the same thread, the one that calls those Startup/OnDocumentOpen methods, etc. Edit: ExternalEvents appear to be a special case.
The Revit process owns the main thread which executes both code inside of Revit and the .NET code. You are able to see a callstack that starts in the addin and goes all the way down to Revit.
Past the managed/unmanaged transition. In fact, there can be several such transitions.
Q: Is it entirely true that Revit and the addin share the same memory?
Could you rephrase?
A: What is being attempted or prevented?
Here is some example of cross thread data access:
https://git.autodesk.com/badicst/hack-itree-addin/blob/572e3050f49cde7854bfc929380e9273b79a7646/CameraUtils.cs#L26
https://thebuildingcoder.typepad.com/blog/2011/06/no-multithreading-in-revit.html
Q: We are trying to understand the exact relationship between Revit and its addins, so we ask experts if an addin can snoop on Revit's memory and find secrets like service credentials.
A: I think that can be phrased as a general question for security experts: if you call code in a managed assembly, can it see memory inside of the caller? I think it is possible, but .NET is rather sophisticated about tracking trust level of the assemblies.
R: yes - i like that rephrase
R: thank you! you got what exactly we want to know!
A: I can't add much, particularly not much about how managed code is restricted..... at a low level, any process like the one that is running Revit.exe has a single address space. All threads and all DLLs that exist in the process "see" the same data. Yes, a given thread of execution can transition between native and managed, which, at  higher level, each present a different world to the executing code .
We know that the way physical memory is mapped into address space cannot be changing very much on these transitions, or when switching among threads, because remapping so often would be too slow.
Security? Native code, in particular, could be trampling on anything at any time. It's the Wild Wild West. Exploits like Spectre feed on that freedom. Addins are simply DLLs, but well-behaved addins are limited by the managed architecture and, well, correctness.
The so-called main or UI thread of Revit is usually in charge. That's the one your addin code is running in by default
Managed code running within an Addin is permitted to create managed side threads. There are a lot of things the code can make happen in these threads, including ending up in native code, but for correctness, such should not be calling arbitrary Revit APIs. (edited)

- a beautiful [Beginner’s Guide To Abstraction](https://jesseduffield.com/beginners-guide-to-abstraction/)
  by Jesse Duffield in a [Pursuit of Laziness](https://jesseduffield.com)
  Experienced programmers know it all, but beatifully put for a less experienced coder.



twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Pick Room in Project or Link

Ending this hot and exciting  week

####<a name="2"></a> Are You Using the Derived Analytical Model?

If so, please provide feedback to 

- Seeking feedback from developers regarding Revit API (Derived Analytical Model)
  https://forums.autodesk.com/t5/revit-api-forum/seeking-feedback-from-developers-regarding-revit-api-derived/m-p/9615258
  Are you a developer who interacts with the Revit API regarding the Derived Analytical Model? If so, please take 5 minutes to provide your input. We would like to hear from you. Our goal is to get a better understanding what functions developers are using and how they are using them. Please feel free to forward this on to a developer you work with. Please click here to provide input: https://autodeskfeedback.az1.qualtrics.com/jfe/form/SV_9Xe2uafQg3X9xXf


####<a name="3"></a> 


####<a name="4"></a> 

<center>
<img src="img/" alt="" title="" width="100"/>
</center>

####<a name="5"></a> 


