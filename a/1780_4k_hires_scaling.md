<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---


twitter:

Scaling an add-in for a 4K high resolution screen in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/scaleaddin4k

As high-resolution monitors grow ever more common, an important question arises on handling add-in scaling for 4K high resolution screens
&ndash; Problem adapting a Revit add-in for 4K displays
&ndash; Application properties for stand-alone apps
&ndash; Application manifest 
&ndash; Separate UI component with IPC 
&ndash; Two solutions for adjusting Revit for 4K displays
&ndash; Method 1 &ndash; Run hires monitor in low resolution
&ndash; Method 2 &ndash; Adjust how Revit handles 4k displays
&ndash; Discussion of the results...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Scaling an Add-In for a 4K High Resolution Screen

As high-resolution monitors grow ever more common, an important question arises on handling add-in scaling for 4K high resolution screens.

Below, CoderBoy shares some questions and answers on this topic:

- [Problem adapting a Revit add-in for 4K displays](#2)
- [Application properties for stand-alone apps](#3)
- [Application manifest suggestion](#4)
- [Separate UI component with IPC suggestion](#5)
- [Solutions documented](#6)
- [Adjusting Revit for 4K displays](#7)
    - [Method 1 &ndash; Run hires monitor in low resolution](#7.1)
    - [Method 2 &ndash; Adjust how Revit handles 4k displays](#7.2)
- [Discussion of the results](#8)

####<a name="2"></a> Problem Adapting a Revit Add-In for 4K Displays

We are in need of your help.
We have been writing commercial Revit add-ins for over ten years now.
We followed the examples from Autodesk, which were all done using Windows Forms.
We even built our add-in template code from these examples.
The sample code in the Revit SDK also still uses Windows Forms.
 
However, we have been getting more and more complaints from our customers that our add-ins don’t work properly when they run 4K monitors, because they have to scale up the monitors just to be able to read text on them; the native resolution is too high for the size of these desktop monitors to allow text to be readable at native scale.
For example, most users change the scale to 150% or even 200% in order to be able to read text.
When this happens, our Windows Forms user interfaces get kind of jumbled up.
Scrolling doesn’t work right, buttons get pushed off the edge of the window, etc.
At least some of our add-ins essentially become unusable.

####<a name="3"></a> Application Properties for Stand-Alone Apps

A workaround to this problem for our add-ins is to have whichever monitor is the primary monitor be scaled to 100% zoom.
Then, the add-ins work fine on any screen, but some customers are now balking at that.
 
In my research on this subject, you can get Windows Forms applications to work well on 4K monitors by changing some of the application properties, for example like this:

<pre class="code">
using System.Runtime.InteropServices;
  [DllImport("Shcore.dll")]
  static extern int SetProcessDpiAwareness(
    int PROCESS_DPI_AWARENESS);
    
  private enum DpiAwareness {
    None = 0,
    SystemAware = 1,
    PerMonitorAware = 2 }

  SetProcessDpiAwareness(
    (int) DpiAwareness.PerMonitorAware );
</pre>

While these things work well for standalone executables we write, I believe they must be done at the executable level, and as add-in creators for Revit we do not have control over Revit’s executable environment.
So, very sadly, as far as I can tell, these tricks don’t work for our Revit add-ins.
 
We have a rather breath-taking amount of code in our Revit add-ins, the significant majority of which is in the user interfaces.
To have to rewrite the user interfaces for all of our add-ins to use WPF would be crippling, to say the least.
 
Can you please advise us on what we would need to do to modify our existing add-ins so they will scale and work correctly on scaled-up 4K monitors in a Revit environment?
For example, are there configuration settings for Revit which control the executable environment that we can advise our customers to change?
Or, are there simple modifications to our code that we would need to implement in order for the WinForms engine on which they depend to run properly in a scaled-up 4K environment under Revit?
 
What have other long-term add-in creators done with their legacy code to solve this problem, short of a complete rewrite of their add-ins, which would not be feasible for us?
 
We really need your help with this!
Thank you very much for any advice you can offer.


####<a name="4"></a> Application Manifest Suggestion

In addition to setting DPI awareness in the code, it is also possible to use an application manifest.
I think that this is worth investigating, although it may not be very straightforward for an add-in DLL.
 
Here are some links:

- [Application manifests](https://docs.microsoft.com/en-us/windows/win32/sbscs/application-manifests)
- [DPI awareness and manifests](https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/mt846517(v%3Dvs.85))
- [Registry key](https://support.microsoft.com/en-us/help/912949/some-third-party-applications-that-use-external-manifest-files-stop-wo) (for older Windows?)


####<a name="5"></a> Separate UI Component with IPC Suggestion

I am convinced that this can be solved without rewriting all your UI code.
 
For instance, if all else fails, simply disconnect your UI completely from your Revit add-in, run it in a separate process and use IPC to pass the information back and forth, as discussed and successfully implemented in
these [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) threads:
 
- [Use IPC for disentanglement](https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html)
- [Using a geometry viewer in a Revit add-in to preview results](https://forums.autodesk.com/t5/revit-api-forum/using-a-geometry-viewer-in-a-revit-addin-to-preview-results/m-p/8868232)
 
This assumes, of course, that you have not unnecessarily mixed up your UI code with Revit API stuff.

**Response:** That is an extremely interesting idea!
 
I’m sure some of our oldest apps do embed the Revit code in with the UI.
So, those would need some work to separate out.
But that would still be better and much faster than a whole rewrite.
 
Thank you so very much!

####<a name="6"></a> Solutions Documented

I did some testing, and we only have a few of our add-ins (or reusable components) that are in pretty bad shape as far as WinForms in Revit on 4K goes.
Very fortunately, most of our add-ins seem to work reasonably well!
Some work, but require making their window large, so are awkward but functional, but a few things are downright unusable.
Of course, it’s the more complicated ones that have the worst problems, but I guess that is to be expected.
So, the scope of the problem isn’t as big as we feared it might be.
 
However, we think we found at least a short-term workaround for our WinForms on 4K display problems in all of our add-ins!
 
It turns out you can instruct Windows itself how to handle the scaling on a per-application basis, by modifying the launch icon properties.
 
Here is a solution document
on [adjusting Revit for 4K displays](zip/coderboy_Adjusting_Revit_for_4K_Displays.pdf) that
describes these simple settings changes:

####<a name="7"></a> Adjusting Revit for 4K Displays

Some add-ins for Revit may not appear correctly when using a 4K display, especially in cases where the primary screen is not set to 100% scaling.

There are 2 approaches which may quickly resolve this:

1. Tell the 4K monitor to run at 1920 x 1080 (or any other resolution) at **100% scaling**
2. Adjust how only Revit is launched and handles 4K displays

####<a name="7.1"></a> Method 1 &ndash; Run Hires Monitor in Low Resolution

Right-click on the desktop and select Display settings:

<center>
<img src="img/4k_solution_1_1.jpg" alt="Display settings" width="257">
</center>

Click on the representation of the 4K monitor (click the “Identify” button if you’re not sure):

<center>
<img src="img/4k_solution_1_2.jpg" alt="Display settings" width="410">
</center>

Set the Display resolution you like and, most importantly, set the scaling to 100% --

<center>
<img src="img/4k_solution_1_3.jpg" alt="Display settings" width="464">
</center>

You must log out and log back in for this change to take effect.

####<a name="7.2"></a> Method 2 &ndash; Adjust how Revit Handles 4K Displays

Adjustments can be made to how Revit is launched that should correct these display issues.

With no Revit sessions running, right-click on the icon that launches Revit and select Properties:

<center>
<img src="img/4k_solution_2_1.jpg" alt="Taskbar icon" width="355">
<p style="font-size: 80%; font-style:italic">Taskbar icon</p>
</center>

<center>
<img src="img/4k_solution_2_2.jpg" alt="Desktop icon" width="329">
<p style="font-size: 80%; font-style:italic">Desktop icon</p>
</center>

On the next screen, select the Compatibility tab and click on “Change settings for all users” button at the bottom:

<center>
<img src="img/4k_solution_2_3.jpg" alt="“Change settings" width="298">
</center>

Near the bottom of the next screen, click on the “Change high DPI settings” button:

<center>
<img src="img/4k_solution_2_4.jpg" alt="“Change settings" width="293">
</center>
  
On the last screen, check the “Override high DPI scaling behavior. Scaling performed by:” checkbox and select **System**:

<center>
<img src="img/4k_solution_2_5.jpg" alt="“Change settings" width="274">
</center>

Click OK on each dialog to save the settings.
 
####<a name="8"></a> Discussion of the Results

We haven’t observed any ill effects in Revit in our limited testing, but if you know of a reason for a user NOT to make these changes to their Revit start-up icon, please let us know.

One can copy the original start-up icon, modify the properties of the copy for a problematic add-in, and go back to the regular icon for normal use.

We haven’t heard any complaints other than the text being small on PDF exports.

I ran a test, and it’s true, as seen in the following two images.

“Default” is with no scaling adjustment of Revit when it’s launched:

<center>
<img src="img/4k_default.jpg" alt="4K default" width="620">
</center>

“Adjusted” is with the scaling adjustment:

<center>
<img src="img/4k_adjusted.jpg" alt="4K adjusted" width="620">
</center>

Much of the text is small.
 
Most actual Revit end users are happy to just set their screen resolution to 1920 x 1080 at 100% scaling.

That’s obviously the best solution to the issue.

Some people say, “we have 4K, we should use it at 4K”, however, who don’t want to do that.

Most of our apps are fine on 4K (80%?), but I’ve been spending some time trying to adjust our troublesome code to work better on 4K without a rewrite.

I’ve had a little success, but there are still some real stumpers too, simple designs that just don’t want to cooperate.  

To clarify 'default' versus 'adjusted':

The “Default” PDF is with no scaling adjustment on the icon definition for how Revit is launched.  It’s what would appear if you just installed Revit for the first time on your 4K display and launched it immediately with no changes.  The text size on the PDFs comes out correctly.
 
The “Adjusted” PDF was generated by following Method 2 and doing nothing else. 
 
If you look at the “Adjusted” PDF, the text in the bubbles is very small.

<center>
<img src="img/4k_adjusted_cropped.jpg" alt="4K adjusted detail" width="248">
</center>

On screen, the text looks correctly sized, but, in a PDF, it comes out small.

In the “Default” PDF, the text in the bubbles is normal sized, and comes out the same size as seen on-screen:

<center>
<img src="img/4k_default_cropped.jpg" alt="4K default detail" width="398">
</center>
 
I think the best solution is Method 1. It incurs no issues at all.

Second-best is Method 2 plus setting two icons to launch Revit, as described above: One for “normal” on-screen use (applying Method 2) and one just for generating PDFs, which has no special scaling applied in the icon properties.  

Many thanks to CoderBoy for the in-depth research and precise and complete documentation of the solution!
