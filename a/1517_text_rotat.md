<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- set text note rotation
12550175 [Textnote - how to set rotation in Revit 2016]
http://forums.autodesk.com/t5/revit-api-forum/textnote-how-to-set-rotation-in-revit-2016/m-p/6800468

#RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

The Forge DevCon developer conference has been happily united with Autodesk University, text note rotation is easy, and I continued my deep learning exploration for implementing a Revit API question answering system
&ndash; Forge DevCon at AU
&ndash; Setting <code>TextNote</code> rotation
&ndash; TensorFlow and Keras
&ndash; Updating restricted Python packages
&ndash; Rules of machine learning...

#AULondon, #UI, #innovation, #RevitAPI, @AutodeskRevit bit.ly/2j7Sxkb

-->

### TextNote Rotation, Forge DevCon, TensorFlow and Keras

The Forge DevCon developer conference has been happily united with Autodesk University, text note rotation is easy, and I continued my deep learning exploration for implementing a Revit API question answering system:

- [Forge DevCon at AU](#2)
- [Setting `TextNote` rotation](#3)
- [TensorFlow and Keras](#4)
- [Updating restricted Python packages](#5)
- [Rules of machine learning](#6)


####<a name="2"></a>Forge DevCon at AU

A match made in heaven:
the [Forge DevCon](https://forge.autodesk.com/DevCon) developer
conference has been happily united
with [Autodesk University](http://au.autodesk.com/las-vegas)

A lot of synergy is gained connecting the two.

<center>
<img src="img/forge_devcon_at_au.png" alt="Forge DevCon at AU" width="500"/>
</center>

The last DevCon in 2016 was held in San Francisco and was a great success.

We plan to make it bigger and better still this year, and moving it to Las Vegas for 2017 will help.

It will take place on November 13-14,
i.e., [Monday and Tuesday of the Autodesk University week](http://adndevblog.typepad.com/cloud_and_mobile/2017/01/forge-devcon-change-of-date-and-venue.html).

In previous years, these were the days on which we hosted the DevDay and DevLab conference and open house support hackathon for application developers.

This is a good thing.

Somewhat selfishly, it means less travel for those of us coming across from Europe.

It will also make AU a more compelling, developer-centric event.

Now nobody has to choose between DevCon and DevDays + AU. It’s all together in one place.




####<a name="3"></a>Setting TextNote Rotation

Rotating a text note is very easy, both during creation and when modifying existing elements.

One little complication for existing add-ins is introduced by fact that the approach changed with
the [`TextNote` API overhaul in Revit 2016](http://thebuildingcoder.typepad.com/blog/2015/04/whats-new-in-the-revit-2016-api.html#4.02):

**Question:** In Revit 2015, I used the method:

<pre class="code">
  <span style="color:#2b91af;">TextNote</span>&nbsp;text&nbsp;=&nbsp;doc.Create.NewTextNote(&nbsp;
  &nbsp;&nbsp;view,&nbsp;origin,&nbsp;baseVec,&nbsp;upVec,&nbsp;lineWidth,
  &nbsp;&nbsp;<span style="color:#2b91af;">TextAlignFlags</span>.TEF_ALIGN_CENTER&nbsp;
  &nbsp;&nbsp;|&nbsp;<span style="color:#2b91af;">TextAlignFlags</span>.TEF_ALIGN_MIDDLE,
  &nbsp;&nbsp;strText&nbsp;);
</pre>

for creating a text note.
 
As this method has been replaced with `TextNote.Create`, I can create a text note using:

<pre class="code">
  <span style="color:#2b91af;">TextNote</span>&nbsp;text&nbsp;=&nbsp;<span style="color:#2b91af;">TextNote</span>.Create(&nbsp;doc,&nbsp;
  &nbsp;&nbsp;view.Id,&nbsp;&nbsp;origin,&nbsp;strText,&nbsp;texttypeID&nbsp;);
</pre>

But how do I set its rotation when creating it?
 
Also, how do I change the rotation of existing text notes?

**Answer:** To rotate the `TextNote` as it is placed, one of the overloads for `TextNote.Create` uses `TextNoteOptions` and its `Rotation` option:
 
<pre class="code">
  <span style="color:#2b91af;">TextNoteOptions</span>&nbsp;tno&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">TextNoteOptions</span>();
  tno.Rotation&nbsp;=&nbsp;0.5&nbsp;*&nbsp;<span style="color:#2b91af;">Math</span>.PI;&nbsp;<span style="color:green;">//&nbsp;in&nbsp;Radians</span>
  tno.TypeId&nbsp;=&nbsp;texttypeID;&nbsp;<span style="color:green;">//&nbsp;need&nbsp;to&nbsp;include&nbsp;text&nbsp;type</span>
   
  <span style="color:#2b91af;">TextNote</span>&nbsp;text&nbsp;=&nbsp;<span style="color:#2b91af;">TextNote</span>.Create(&nbsp;doc,&nbsp;
  &nbsp;&nbsp;view.Id,&nbsp;origin,&nbsp;strText,&nbsp;tno&nbsp;);
</pre>

**Response:** This is how I managed to create the rotated text aligned with existing curve based elements:
 
<pre class="code">
  <span style="color:green;">//&nbsp;get&nbsp;the&nbsp;curve&nbsp;of&nbsp;the&nbsp;element</span>
  <span style="color:#2b91af;">Curve</span>&nbsp;curve&nbsp;=&nbsp;(&nbsp;(<span style="color:#2b91af;">LocationCurve</span>)&nbsp;elem.Location&nbsp;)
  &nbsp;&nbsp;.Curve;
   
  <span style="color:green;">//&nbsp;get&nbsp;start&nbsp;and&nbsp;end&nbsp;point&nbsp;of&nbsp;pipe</span>
  <span style="color:#2b91af;">XYZ</span>&nbsp;PipeEnd&nbsp;=&nbsp;curve.GetEndPoint(&nbsp;1&nbsp;);
  <span style="color:#2b91af;">XYZ</span>&nbsp;PipeStart&nbsp;=&nbsp;curve.GetEndPoint(&nbsp;0&nbsp;);
   
  <span style="color:green;">//&nbsp;get&nbsp;vector&nbsp;representing&nbsp;the&nbsp;pipe</span>
  <span style="color:#2b91af;">XYZ</span>&nbsp;v&nbsp;=&nbsp;PipeEnd&nbsp;-&nbsp;PipeStart;
   
  <span style="color:blue;">double</span>&nbsp;angle&nbsp;=&nbsp;<span style="color:#2b91af;">Math</span>.Atan2(&nbsp;v.Y,&nbsp;v.X&nbsp;);

  <span style="color:blue;">var</span>&nbsp;testOptions&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">TextNoteOptions</span>()
  {
  &nbsp;&nbsp;HorizontalAlignment&nbsp;=&nbsp;<span style="color:#2b91af;">HorizontalTextAlignment</span>.Center,
  &nbsp;&nbsp;Rotation&nbsp;=&nbsp;angle,
  &nbsp;&nbsp;TypeId&nbsp;=&nbsp;texttypeID
  };
   
  <span style="color:blue;">var</span>&nbsp;text&nbsp;=&nbsp;<span style="color:#2b91af;">TextNote</span>.Create(&nbsp;doc,&nbsp;view.Id,
  &nbsp;&nbsp;origin,&nbsp;strText,&nbsp;testOptions&nbsp;);
</pre>

<center>
<img src="img/rotate_text.png" alt="Rotate text" width="377"/>
</center>



####<a name="4"></a>TensorFlow and Keras

Continuing my superficial browsing of various open source possibilities on which to base a question answering system for the Revit API add-in programming, I now bumped
into [TensorFlow](https://en.wikipedia.org/wiki/TensorFlow)
([web site](https://www.tensorflow.org/),
[GitHub repo](https://github.com/tensorflow/tensorflow)).

It converts the learning matter to vectors and computes similarities, learning best matches by assigning weight using various algorithms.

Here are a bunch of interesting TensorFlow tutorial and other related texts that I would all like to work through in greater depth anon:

- [MNIST tutorial for beginners](https://www.tensorflow.org/tutorials/mnist/beginners/index) and [advanced](https://www.tensorflow.org/tutorials/mnist/pros/index)
- [Vector Representations of Words](https://www.tensorflow.org/tutorials/word2vec/)
- [Recurrent Neural Networks](https://www.tensorflow.org/tutorials/recurrent/)
- [Sequence-to-Sequence Models](https://www.tensorflow.org/tutorials/seq2seq/)
- [TensorKart: self-driving MarioKart with TensorFlow](http://kevinhughes.ca/blog/tensor-kart)

They also led to further papers and the [Keras](https://keras.io) system built on top of it:

- [SyntaxNet](https://www.tensorflow.org/tutorials/syntaxnet/) is a neural-network Natural Language Processing framework for TensorFlow.
- [Question Answering Using Deep Learning](https://cs224d.stanford.edu/reports/StrohMathur.pdf) &ndash; neural network variants are becoming the dominant architecture for many NLP tasks. This project applies several deep learning approaches to question answering, with a focus on the bAbI dataset, including TensorFlow.
- [Keras: Deep Learning library for Theano and TensorFlow](https://keras.io/) &ndash; paper on [Deep Language Modelling for Question Answering using Keras](http://benjaminbolte.com/blog/2016/keras-language-modeling.html) with [GitHub repo](https://github.com/fchollet/keras/blob/master/examples/babi_rnn.py)
- [Question answering on the Facebook bAbi dataset using recurrent neural networks and 175 lines of Python + Keras](http://smerity.com/articles/2015/keras_qa.html)
- [The Facebook bAbI tasks](https://research.fb.com/downloads/babi/)

I decided to install these packages, which led to an interesting problem that took a while to sort out and seems worthwhile documenting for future reference.


####<a name="5"></a>Updating Restricted Python Packages

TensorFlow and Keras are Python based.

Installation is very straightforward and easy.

TensorFlow requires several other base packages: `six`, `funcsigs`, `pbr`, `mock`, `numpy`, `protobuf`.

Unfortunately, on my system, some of these were out of date and the system refused to allow me to update them.

For instance, calling `$ pip install tensorflow` led to a conflict due to the existence of prior versions of `six` and `numpy`.

After some research, I determined that I could resolve the issues by simply deleting the associated files.

Unfortunately, this is not permitted by the system, as described in the question
on [why `chown` reports `operation not permitted` on OS X](http://superuser.com/questions/279235/why-does-chown-report-operation-not-permitted-on-os-x).

The short description of the solution for this is given there:

> System Integrity Protection (rootless) ... boot into Recovery Mode (Cmd-R), run `csrutil disable`, then restart; to reenable, use `csrutil enable`.

It is explained in greater detail in the StackOverflow question
on [restricted folder and files in OS X El Capitan](http://stackoverflow.com/questions/30768087/restricted-folder-files-in-os-x-el-capitan):

To temporarily disable SIP (System Integrity Protection):

- Reboot
- As soon as you hear the "Mac sound" on the grey screen, press Cmd+R to enter Recovery mode
- Open Utilities &gt; Terminal
- Run the command `csrutil disable`
- Reboot, you'll land in the normal OS with SIP disabled
- Do all the changes you'd like to do
- Reboot again
- As soon as you hear the "Mac sound" on the grey screen, press Cmd+R to enter Recovery mode
- Enable SIP with `csrutil enable`
- Reboot again
- Done

On my system, the problematic packages live in the folder

- `System/Library/Frameworks/Python.framework/Versions/2.7/Extras/lib/python`

You can examine their restricted status using `ls`:

<pre>
&#36; &#108;s -lhdO num*
drwxr-xr-x  36 root  wheel  restricted             1.2K May 25  2016 numpy
-rw-r--r--   1 root  wheel  restricted,compressed  1.6K Aug  1  2015 numpy-1.8.0rc1-py2.7.egg-info
</pre>

By temporarily disabling SIP, I was finally able to move them out of the way and continue normal installation:

<pre>
&#36; sudo -H pip install tensorflow
</pre>

With TensorFlow in place, I successfully ran the standard Python install in the `keras` folder and immediately executed one of its interesting examples:

<pre>
/a/src/deep_learning &#36; cd kears
/a/src/deep_learning/keras &#36; sudo python setup.py install
/a/src/deep_learning/keras &#36; cd examples
/a/src/deep_learning/keras/examples &#36; python babi_rnn.py
</pre>


<!----
  problem installing due to prior package 'six'
  /System/Library/Frameworks/Python.framework/Versions/2.7/Extras/lib/python
  /System/Library/Frameworks/Python.framework/Versions/2.7/Extras/lib/python/six-1.4.1-py2.7.egg-info
  http://superuser.com/questions/214004/how-to-add-user-to-a-group-from-mac-os-x-command-line
  sudo dseditgroup -o edit -a tammikj -t user wheel
  http://superuser.com/questions/279235/why-does-chown-report-operation-not-permitted-on-os-x
  sudo chmod -N file        # Remove ACLs from file
  sudo chmod ugo+rw file    # Give everyone read-write permission to file
  sudo chflags nouchg file  # Clear the user immutable flag from file
  start up with Cmd-S for single user mode 
  # root ... read-only file system
  System Integrity Protection (rootless) ... boot into Recovery Mode (Cmd-R), run csrutil disable, then restart (to reenable, use csrutil enable).
  finally got six removed
  $ sudo -H pip install tensorflow
  Installing collected packages: six, funcsigs, pbr, mock, numpy, protobuf, tensorflow

System/Library/Frameworks/Python.framework/Versions/2.7/Extras/lib/python $ ls -d num*
numpy				numpy-1.8.0rc1-py2.7.egg-info
/System/Library/Frameworks/Python.framework/Versions/2.7/Extras/lib/python $ sudo mv numpy numpy-1.8.0rc1-py2.7.egg-info /j/tmp/python_package_backup/numpy
Password:
mv: rename numpy to /j/tmp/python_package_backup/numpy/numpy: Operation not permitted
mv: rename numpy-1.8.0rc1-py2.7.egg-info to /j/tmp/python_package_backup/numpy/numpy-1.8.0rc1-py2.7.egg-info: Operation not permitted

System/Library/Frameworks/Python.framework/Versions/2.7/Extras/lib/python $ ls -lhdO num*
drwxr-xr-x  36 root  wheel  restricted             1.2K May 25  2016 numpy
-rw-r--r--   1 root  wheel  restricted,compressed  1.6K Aug  1  2015 numpy-1.8.0rc1-py2.7.egg-info

Cmd-R

csrutil disable

/System/Library/Frameworks/Python.framework/Versions/2.7/Extras/lib/python $ sudo mv numpy numpy-1.8.0rc1-py2.7.egg-info /j/tmp/python_package_backup/
Password:

Cmd-R

csrutil enable

Users/tammikj $ sudo -H pip install tensorflow
Password:

/a/src/deep_learning/keras $ sudo python setup.py install

a/src/deep_learning/keras/examples $ python babi_rnn.py
Using TensorFlow backend.
RNN / Embed / Sent / Query = <class 'keras.layers.recurrent.LSTM'>, 50, 100, 100
Downloading data from https://s3.amazonaws.com/text-datasets/babi_tasks_1-20_v1-2.tar.gz
vocab = [u'.', u'?', u'Daniel', u'John', u'Mary', u'Sandra', u'Where', u'apple', u'back', u'bathroom', u'bedroom', u'discarded', u'down', u'dropped', u'football', u'garden', u'got', u'grabbed', u'hallway', u'is', u'journeyed', u'kitchen', u'left', u'milk', u'moved', u'office', u'picked', u'put', u'the', u'there', u'to', u'took', u'travelled', u'up', u'went']
X.shape = (1000, 552)
Xq.shape = (1000, 5)
Y.shape = (1000, 36)
story_maxlen, query_maxlen = 552, 5
Build model...
Training
Train on 950 samples, validate on 50 samples
Epoch 1/40
544/950 [================>.............] - ETA: 13s - loss: 3.2671 - acc: 0.0460

Epoch 1/40
950/950 [==============================] - 32s - loss: 2.9663 - acc: 0.1200 - val_loss: 2.2771 - val_acc: 0.3000
Epoch 2/40
288/950 [========>.....................] - ETA: 23s - loss: 2.2930 - acc: 0.2049

Epoch 40/40
950/950 [==============================] - 29s - loss: 1.6535 - acc: 0.3474 - val_loss: 1.6400 - val_acc: 0.4000
1000/1000 [==============================] - 5s
Test loss / test accuracy = 1.6902 / 0.3270


- single-user mode
  Shut down your Mac.
  Press the power button to start up your Mac.
  Immediately hold down the following keys:
  Hold down Command-S for single-user mode.
  Hold down Command-V for verbose mode.

--->

Keras is up and running now.

What shall I do next?

Here are two descriptions of successful experiments implementing minimal question answering systems with it that I am taking a deeper look at:

- [Deep Language Modelling for Question Answering using Keras](http://benjaminbolte.com/blog/2016/keras-language-modeling.html)
  ([^](/a/doc/deep_learning/keras/Deep_Language_Modeling_for_Question_Answering_using_Keras.pdf)),
  [GitHub repo](https://github.com/codekansas/keras-language-modeling)
- [Question Answering Using Deep Learning](https://cs224d.stanford.edu/reports/StrohMathur.pdf)
  ([^](/a/doc/deep_learning/keras/Question_Answering_Using_Deep_Learning_by_Stroh_Mathur.pdf)),
  [GitHub repo](https://github.com/priyank87/memn2n)
  
Time for me to get going putting together some Revit API related tests?


####<a name="6"></a>Rules of Machine Learning

By the way, before doing anything else, I and everybody else interested in machine learning ought to read and ponder
Martin Zinkevich's 43 [Rules of Machine Learning](http://martin.zinkevich.org/rules_of_ml/rules_of_ml.pdf),
split up into useful consecutive phases:

- Before Machine Learning
- Phase I: Your First Pipeline
    - Monitoring
    - Your First Objective
- Phase II: Feature Engineering
    - Human Analysis of the System
    - Training-­Serving Skew
- Phase III: Slowed Growth, Optimization Refinement, and Complex Models

To quote Martin:

The document is arranged in four parts:

- The first part should help you understand whether the time is right for building a machine learning system.
- The second part is about deploying your first pipeline.
- The third part is about launching and iterating while adding new features to your pipeline, how to evaluate models and training-­serving skew.
- The final part is about what to do when you reach a plateau.
- Afterwards, there is a list of related work and an appendix with some background on the systems commonly used as examples in this document.

It is strongly geared towards ranking.
