<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Scaling a bitmap for the #RevitAPI external application large and small image icons @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/scaleribbonicon

Every time I created a ribbon button, I was faced with the task of creating appropriately scaled icons for it to populate the <code>PushButton</code> large and small image icon properties <code>LargeImage</code> and <code>Image</code>.
They seem to expect a 32 x 32 and 16 x 16 icon, respectively.
I finally solved that once and for all by implementing a couple of methods to perform automatic bitmap scaling
&ndash; BitmapImageToBitmap &ndash; convert a <code>BitmapImage</code> to <code>Bitmap</code>
&ndash; BitmapToBitmapSource &ndash; convert a <code>Bitmap</code> to a <code>BitmapSource</code>
&ndash; ResizeImage &ndash; resize an image to the specified width and height
&ndash; ScaledIcon &ndash; scale down large icon to desired size for Revit ribbon button
&ndash; Usage sample &ndash; putting them together...

--->

### Scaling a Bitmap for the Large and Small Image Icons

Every time I created a ribbon button, I was faced with the task of creating appropriately scaled icons for it to populate the `PushButton` large and small image icon properties `LargeImage` and `Image`.

They seem to expect a 32 x 32 and 16 x 16 icon, respectively.

I finally solved that once and for all by implementing a couple of methods to perform automatic bitmap scaling:

- [BitmapImageToBitmap](#3) &ndash; convert a `BitmapImage` to `Bitmap`
- [BitmapToBitmapSource](#4) &ndash; convert a `Bitmap` to a `BitmapSource`
- [ResizeImage](#5) &ndash; resize an image to the specified width and height
- [ScaledIcon](#6) &ndash; return a scaled down icon of desired size for Revit ribbon button
- [Usage sample](#7) &ndash; putting them together

Here they are one by one:

####<a name="3"></a>BitmapImageToBitmap

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Convert&nbsp;a&nbsp;BitmapImage&nbsp;to&nbsp;Bitmap</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">Bitmap</span>&nbsp;BitmapImageToBitmap(&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">BitmapImage</span>&nbsp;bitmapImage&nbsp;)
{
&nbsp;&nbsp;<span style="color:green;">//BitmapImage&nbsp;bitmapImage&nbsp;=&nbsp;new&nbsp;BitmapImage(</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;new&nbsp;Uri(&quot;../Images/test.png&quot;,&nbsp;UriKind.Relative));</span>
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">MemoryStream</span>&nbsp;outStream&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">MemoryStream</span>()&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BitmapEncoder</span>&nbsp;enc&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BmpBitmapEncoder</span>();
&nbsp;&nbsp;&nbsp;&nbsp;enc.Frames.Add(&nbsp;<span style="color:#2b91af;">BitmapFrame</span>.Create(&nbsp;bitmapImage&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;enc.Save(&nbsp;outStream&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Bitmap</span>&nbsp;bitmap&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Bitmap</span>(&nbsp;outStream&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Bitmap</span>(&nbsp;bitmap&nbsp;);
&nbsp;&nbsp;}
}
</pre>


####<a name="4"></a>BitmapToBitmapSource

<pre class="code">
[System.Runtime.InteropServices.<span style="color:#2b91af;">DllImport</span>(&nbsp;<span style="color:#a31515;">&quot;gdi32.dll&quot;</span>&nbsp;)]
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">extern</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;DeleteObject(&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;hObject&nbsp;);
 
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Convert&nbsp;a&nbsp;Bitmap&nbsp;to&nbsp;a&nbsp;BitmapSource</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">BitmapSource</span>&nbsp;BitmapToBitmapSource(&nbsp;<span style="color:#2b91af;">Bitmap</span>&nbsp;bitmap&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;hBitmap&nbsp;=&nbsp;bitmap.GetHbitmap();
 
&nbsp;&nbsp;<span style="color:#2b91af;">BitmapSource</span>&nbsp;retval;
 
&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;retval&nbsp;=&nbsp;<span style="color:#2b91af;">Imaging</span>.CreateBitmapSourceFromHBitmap(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;hBitmap,&nbsp;<span style="color:#2b91af;">IntPtr</span>.Zero,&nbsp;<span style="color:#2b91af;">Int32Rect</span>.Empty,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BitmapSizeOptions</span>.FromEmptyOptions()&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">finally</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;DeleteObject(&nbsp;hBitmap&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;retval;
}
</pre>


####<a name="5"></a>ResizeImage

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Resize&nbsp;the&nbsp;image&nbsp;to&nbsp;the&nbsp;specified&nbsp;width&nbsp;and&nbsp;height.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>image<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;image&nbsp;to&nbsp;resize.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>width<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;width&nbsp;to&nbsp;resize&nbsp;to.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>height<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;height&nbsp;to&nbsp;resize&nbsp;to.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;resized&nbsp;image.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">Bitmap</span>&nbsp;ResizeImage(
&nbsp;&nbsp;<span style="color:#2b91af;">Image</span>&nbsp;image,&nbsp;
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;width,
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;height&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;destRect&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;System.Drawing.<span style="color:#2b91af;">Rectangle</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;0,&nbsp;0,&nbsp;width,&nbsp;height&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;destImage&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Bitmap</span>(&nbsp;width,&nbsp;height&nbsp;);
 
&nbsp;&nbsp;destImage.SetResolution(&nbsp;image.HorizontalResolution,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;image.VerticalResolution&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;g&nbsp;=&nbsp;<span style="color:#2b91af;">Graphics</span>.FromImage(&nbsp;destImage&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;g.CompositingMode&nbsp;=&nbsp;<span style="color:#2b91af;">CompositingMode</span>.SourceCopy;
&nbsp;&nbsp;&nbsp;&nbsp;g.CompositingQuality&nbsp;=&nbsp;<span style="color:#2b91af;">CompositingQuality</span>.HighQuality;
&nbsp;&nbsp;&nbsp;&nbsp;g.InterpolationMode&nbsp;=&nbsp;<span style="color:#2b91af;">InterpolationMode</span>.HighQualityBicubic;
&nbsp;&nbsp;&nbsp;&nbsp;g.SmoothingMode&nbsp;=&nbsp;<span style="color:#2b91af;">SmoothingMode</span>.HighQuality;
&nbsp;&nbsp;&nbsp;&nbsp;g.PixelOffsetMode&nbsp;=&nbsp;<span style="color:#2b91af;">PixelOffsetMode</span>.HighQuality;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;wrapMode&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ImageAttributes</span>()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;wrapMode.SetWrapMode(&nbsp;<span style="color:#2b91af;">WrapMode</span>.TileFlipXY&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;g.DrawImage(&nbsp;image,&nbsp;destRect,&nbsp;0,&nbsp;0,&nbsp;image.Width,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;image.Height,&nbsp;<span style="color:#2b91af;">GraphicsUnit</span>.Pixel,&nbsp;wrapMode&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;destImage;
}
</pre>


####<a name="6"></a>ScaledIcon

`ScaledIcon` simply calls the three helper methods defined above to return a scaled version of the input image:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Scale&nbsp;down&nbsp;large&nbsp;icon&nbsp;to&nbsp;desired&nbsp;size&nbsp;for&nbsp;Revit&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;ribbon&nbsp;button,&nbsp;e.g.,&nbsp;32&nbsp;x&nbsp;32&nbsp;or&nbsp;16&nbsp;x&nbsp;16</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">BitmapSource</span>&nbsp;ScaledIcon(&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">BitmapImage</span>&nbsp;large_icon,
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;w,
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;h&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;BitmapToBitmapSource(&nbsp;ResizeImage(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;BitmapImageToBitmap(&nbsp;large_icon&nbsp;),&nbsp;w,&nbsp;h&nbsp;)&nbsp;);
}
</pre>


####<a name="7"></a>Usage Sample

Within the external application `PopulatePanel` method, simply read the embedded resource icon image and apply `ScaledIcon` to it to populate the large and small image properties with appropriately scaled images:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">BitmapImage</span>&nbsp;bmi&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BitmapImage</span>(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Uri</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;icons/cmdx.png&quot;</span>,&nbsp;<span style="color:#2b91af;">UriKind</span>.Relative&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">PushButton</span>&nbsp;pb&nbsp;=&nbsp;p.AddItem(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PushButtonData</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Command&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Command&quot;</span>,&nbsp;path,&nbsp;<span style="color:#a31515;">&quot;CmdX&quot;</span>&nbsp;)&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">PushButton</span>;
 
&nbsp;&nbsp;pb.ToolTip&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Do&nbsp;something&nbsp;fantastic&quot;</span>;
&nbsp;&nbsp;pb.LargeImage&nbsp;=&nbsp;ScaledIcon(&nbsp;bmi,&nbsp;32,&nbsp;32&nbsp;);
&nbsp;&nbsp;pb.Image&nbsp;=&nbsp;ScaledIcon(&nbsp;bmi,&nbsp;16,&nbsp;16&nbsp;);
</pre>

<center>
<img src="img/roomedit_2014_embedded_icon_resource.png" alt="Embedded icon resource" width="220"/>
</center>
