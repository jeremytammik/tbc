<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<style>
table, th, td {
  border: 1px solid black;
  border-collapse: collapse;
}
th, td {
  padding-left: 1em;
  padding-right: 1em;
  text-align:right;
}
</style>

</head>

<!---

- aps intro:
  We have a new overview video answering the question “What is Autodesk Platform Services?” This gives a quick explanation of what APS is, how it fits into the Autodesk Platform, and shows some of the most popular applications of APS in use with our customers and partners. You can find the video on YouTube here: https://youtu.be/RrAel5Mx7-0?si=lk3C2qLjUHX8PALi

- another open source multimodal model
  Fuyu-8B: A Multimodal Architecture for AI Agents
  https://www.adept.ai/blog/fuyu-8b
  Can be run offline on a laptop CPU


twitter:

@AutodeskAPS overview, and DLL paradise for @AutodeskRevit #RevitAPI add-ins via named pipe IPC, interprocess communication strategies and best practices communicating between .NET 4.8 and .NET 7 #BIM @DynamoBIM @AutodeskAPS https://autode.sk/dllparadise

APS overview
&ndash; DLL paradise for Revit add-ins via named pipe IPC
&ndash; Interprocess communication strategies and best practices
&ndash; Using named pipes to communicate between different .NET versions
&ndash; Interactions between .NET 4.8 and .NET 7
&ndash; Server / client transmission protocol
&ndash; Connection management
&ndash; Two-way communication...

linkedin:

Autodesk Platform Services APS overview, and DLL paradise for #Revit #API add-ins via named pipe IPC, interprocess communication strategies and best practices communicating between .NET 4.8 and .NET 7

https://autode.sk/dllparadise

- DLL paradise for Revit add-ins via named pipe IPC
- Interprocess communication strategies and best practices
- Using named pipes to communicate between different .NET versions
- Interactions between .NET 4.8 and .NET 7
- Server / client transmission protocol
- Connection management
- Two-way communication...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### DLL Paradise and a Fall

One huge article explaining how you can address DLL hell today, and a bunch of little notes to decorate it:

- [What are the Autodesk Platform Services?](#1)
- [DLL paradise for Revit add-ins via named pipe IPC](#2)
- [Interprocess communication: strategies and best practices](#3)
    - [Table of contents](#3.1)
    - [Using named pipes to communicate between different .NET versions](#3.2)
    - [What are named pipes?](#3.3)
    - [Interactions between applications in .NET 4.8 and .NET 7](#3.4)
    - [Server creation](#3.5)
    - [Client creation](#3.6)
    - [Transmission protocol](#3.7)
    - [Connection management](#3.8)
    - [Two-way communication](#3.9)
    - [Implementation for Revit plug-in](#3.10)
    - [Installing .NET runtime during plugin installation](#3.11)
    - [Conclusion](#3.12)
- [Fuyu-8B multimodal architecture for AI agents](#4)
- [How open source wins](#6)
- [HTTP/3](#5)

By the way, I am writing this from my hospital bed.
I had a 6-metre fall from a ladder onto earth last weekend and broke my right hipbone, both front and back, plus some other less important bits and pieces.
Now I am waiting for an operastion to get it all screwed back together again and hope that will provide a stable basis for a speedy recovery.

<center>
<img src="img/2023-10-21_jeremy_in_hospital.jpg" alt="Jeremy in hospital" title="Jeremy in hospital" width="600"/>
</center>

####<a name="1"></a> What are the Autodesk Platform Services?

We have a new two-and-a-half-minute overview YouTube video answering
the question [What is Autodesk Platform Services?](https://youtu.be/RrAel5Mx7-0?feature=shared) to
give a quick explanation of what APS is, how it fits into the Autodesk Platform, and shows some of the most popular applications of APS in use with our customers and partners.


####<a name="2"></a> DLL Paradise for Revit Add-ins via Named Pipe IPC

Windows applications integrating external components occasionally
encounter [DLL hell](https://duckduckgo.com/?q=dll+hell) due to conflicting dependencies.
The Building Coder discussed
some [specific and even some pretty generic solutions](https://www.google.com/search?q=dll+hell&as_sitesearch=thebuildingcoder.typepad.com).
Once again, Roman [Nice3point](https://github.com/Nice3point) Karpovich
of [atomatiq](https://www.linkedin.com/company/atomatiq/), aka Роман Карпович,
principal maintainer of RevitLookup, comes to the rescue, sharing a new article about Revit and add-in inter-processor communication:

Dive into the world of inter-process communication and discover how to establish seamless communication for running a Revit plugin on .NET 7.

Learn about the essence of Named Pipes:

- Inter-process communication across .NET 4.8 and .NET 7
- Server setup and client creation
- Efficient data transmission protocols
- Connection management strategies
- Two-way data transfer capabilities
- Implementation of a Revit plugin

Want to know how it works? Check out the full GitHub article
on [Interprocess Communication: Strategies and Best Practices](https://github.com/atomatiq/InterprocessCommunication):

- [LinkedIn post](https://www.linkedin.com/feed/update/urn:li:activity:7120385512464388096/)
- [ENU version](https://github.com/atomatiq/InterprocessCommunication)
- [CIS version](https://github.com/Nice3point/InterprocessCommunication)

Please be aware that the Revit development team is looking at
possible [options for moving the Revit API forward from .NET 4.8](https://thebuildingcoder.typepad.com/blog/2023/08/15-years-polygon-areas-and-net-core.html#3) as
we speak.
With the approach described here, you can move ahead today and address other DLL conflicts as well.

####<a name="3"></a> Interprocess Communication: Strategies and Best Practices

We all know how challenging it is to maintain large programs and keep up with progress.
Developers of plugins for Revit understand this better than anyone else.
We have to write our programs in .NET Framework 4.8 and forgo modern and fast libraries.
Ultimately, this affects users who are forced to use outdated software.

In such scenarios, splitting the application into multiple processes using Named Pipes appears to be an excellent solution due to its performance and reliability.
In this article, we discuss how to create and use Named Pipes to communicate between the Revit application running on .NET 4.8 and its plugin running on .NET 7.

<!--

####<a name="3.1"></a> Table of Contents

* [Introduction to Using Named Pipes for Communication Between Applications on Different .NET Versions](#introduction-to-using-named-pipes-for-communication-between-applications-on-different-net-versions)
* [What are Named Pipes?](#what-are-named-pipes)
* [Interactions between applications in .NET 4.8 and .NET 7](#interactions-between-applications-in-net-48-and-net-7)
    * [Server Creation](#server-creation)
    * [Client Creation](#client-creation)
    * [Transmission Protocol](#transmission-protocol)
    * [Connection Management](#connection-management)
    * [Two-Way Communication](#two-way-communication)
    * [Implementation for Revit plug-in](#implementation-for-revit-plug-in)
* [Installing .NET Runtime during plugin installation](#installing-net-runtime-during-plugin-installation)
* [Conclusion](#conclusion)

-->

####<a name="3.2"></a> Using Named Pipes to Communicate Between Different .NET Versions

In the world of application development, there is often a need to ensure data exchange between different applications, especially in cases where they operate on different versions of .NET or different languages.
Splitting a single application into multiple processes must be justified.
What is simpler, calling a function directly, or exchanging messages? Obviously, the former.

So what are the benefits of doing this?

- Resolving Dependency Conflicts

With each passing year, the size of Revit plugins is growing exponentially, and dependencies are also increasing at a geometric rate.
Plugins might use incompatible versions of a single library, leading to program crashes. Process isolation solves this problem.

- Performance

Here are a few performance measurements for sorting and mathematical calculations on different .NET versions:

- BenchmarkDotNet v0.13.9, Windows 11 (10.0.22621.1702/22H2/2022Update/SunValley2)
- AMD Ryzen 5 2600X, 1 CPU, 12 logical and 6 physical cores
- .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2
- .NET Framework 4.8.1 (4.8.9139.0), X64 RyuJIT VectorSize=256

<center>
<table>
<tr><th> Method</th><th>.NET</th><th>Mean ns</th><th>Error ns</th><th>SthDev ns</th><th>Bytes</th></tr>
<tr><td> ListSort   </td><td>7.0</td><td>1,113,161</td><td>20,385</td><td>21,811</td><td> 804753</td></tr>
<tr><td> ListOrderBy</td><td>7.0</td><td>1,064,851</td><td>12,401</td><td>11,600</td><td> 807054</td></tr>
<tr><td> MinValue   </td><td>7.0</td><td>      979</td><td>     7</td><td>     6</td><td>       </td></tr>
<tr><td> MaxValue   </td><td>7.0</td><td>      970</td><td>     4</td><td>     3</td><td>       </td></tr>
<tr><td> ListSort   </td><td>4.8</td><td>2,144,723</td><td>40,359</td><td>37,752</td><td>1101646</td></tr>
<tr><td> ListOrderBy</td><td>4.8</td><td>2,192,414</td><td>25,938</td><td>24,263</td><td>1105311</td></tr>
<tr><td> MinValue   </td><td>4.8</td><td>   58,019</td><td>   460</td><td>   430</td><td>     40</td></tr>
<tr><td> MaxValue   </td><td>4.8</td><td>   66,053</td><td>   610</td><td>   541</td><td>     41</td></tr>
</table>
</center>

The 68-fold difference in speed when finding the minimum value, and the complete absence of memory allocation, is impressive.

How then to write a program in the latest .NET version that will interact with an incompatible .NET framework?
Create two applications, Server and Client, without adding dependencies between each other and configure the interaction between them using a configured protocol.

Here are some possible ways of interaction between two applications:

-  Using WCF (Windows Communication Foundation)
-  Using sockets (TCP or UDP)
-  Using Named Pipes
-  Using operating system signals (e.g., Windows signals):

An example of the latter from Autodesk's code, the interaction of the Project Browser plugin with the Revit backend via messages.

<pre class="prettyprint">
public class DataTransmitter : IEventObserver
{
  private void PostMessageToMainWindow(int iCmd) =&gt;
    this.HandleOnMainThread((Action) (() =&gt;
      Win32Api.PostMessage(Application.UIApp.getUIApplication().MainWindowHandle, 273U, new IntPtr(iCmd), IntPtr.Zero)));

  public void HandleShortCut(string key, bool ctrlPressed)
  {
    string lower = key.ToLower();
    switch (PrivateImplementationDetails.ComputeStringHash(lower))
    {
    case 388133425:
      if (!(lower == "f2")) break;
      this.PostMessageToMainWindow(DataTransmitter.ID_RENAME);
      break;
    case 1740784714:
      if (!(lower == "delete")) break;
      this.PostMessageToMainWindow(DataTransmitter.ID_DELETE);
      break;
    case 3447633555:
      if (!(lower == "contextmenu")) break;
      this.PostMessageToMainWindow(DataTransmitter.ID_PROJECTBROWSER_CONTEXT_MENU_POP);
      break;
    case 3859557458:
      if (!(lower == "c") || !ctrlPressed) break;
      this.PostMessageToMainWindow(DataTransmitter.ID_COPY);
      break;
    case 4077666505:
      if (!(lower == "v") || !ctrlPressed) break;
      this.PostMessageToMainWindow(DataTransmitter.ID_PASTE);
      break;
    case 4228665076:
      if (!(lower == "y") || !ctrlPressed) break;
      this.PostMessageToMainWindow(DataTransmitter.ID_REDO);
      break;
    case 4278997933:
      if (!(lower == "z") || !ctrlPressed) break;
      this.PostMessageToMainWindow(DataTransmitter.ID_UNDO);
      break;
    }
  }
}
</pre>

Each option has its own pros and cons. In my opinion, the most convenient for local machine interaction is Named Pipes. Let's delve into it.

####<a name="3.3"></a> What are Named Pipes?

Named Pipes are a mechanism for Inter-Process Communication (IPC) that enables processes to exchange data through named channels.
They provide a one-way or duplex connection between processes.
Apart from high performance, Named Pipes also offer various security levels, making them an attractive solution for many inter-process communication scenarios.

####<a name="3.4"></a> Interactions between applications in .NET 4.8 and .NET 7

Let's consider two applications, one containing the business logic (server), and the other one for the user interface (client).
NamedPipe is used to facilitate communication between these two processes.

The operation principle of NamedPipe involves the following steps:

-  **Creation and configuration of NamedPipe**: The server creates and configures the NamedPipe with a specific name that will be accessible to the client.
   The client needs to know this name to connect to the pipe.
-  **Waiting for connection**: The server starts to wait for the client to connect to the pipe.
   This is a blocking operation, and the server remains in a pending state until the client connects.
-  **Connecting to NamedPipe**: The client initiates a connection to the NamedPipe, specifying the name of the pipe to which it wants to connect.
-  **Data exchange**: After a successful connection, the client and server can exchange data in the form of byte streams.
   The client sends requests for executing the business logic, and the server processes these requests and sends back the results.
-  **Session termination**: After the data exchange is complete, the client and server can close the connection with NamedPipe.

#####<a name="3.5"></a> Server Creation

On the .NET platform, the server side is represented by the `NamedPipeServerStream` class.
The class implementation provides both asynchronous and synchronous methods for working with NamedPipe.
To avoid blocking the main thread, we will utilize asynchronous methods.

Here's an example code snippet for creating a NamedPipeServer:

<pre class="prettyprint">
public static class NamedPipeUtil
{
  /// &lt;summary&gt;
  /// Create a server for the current user only
  /// &lt;/summary&gt;
  public static NamedPipeServerStream CreateServer(PipeDirection? pipeDirection = null)
  {
    const PipeOptions pipeOptions = PipeOptions.Asynchronous | PipeOptions.WriteThrough;
    return new NamedPipeServerStream(
      GetPipeName(),
      pipeDirection ?? PipeDirection.InOut,
      NamedPipeServerStream.MaxAllowedServerInstances,
      PipeTransmissionMode.Byte,
      pipeOptions);
  }

  private static string GetPipeName()
  {
    var serverDirectory = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
    var pipeNameInput = $"{Environment.UserName}.{serverDirectory}";
    var hash = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(pipeNameInput));

    return Convert.ToBase64String(hash)
      .Replace("/", "_")
      .Replace("=", string.Empty);
  }
}
</pre>

The server name should not contain special characters to avoid exceptions.
To generate the pipe name, we will use a hash created from the username and the current folder, which is unique enough for the client to use this server upon connection.
You can modify this behavior or use any name within the scope of your project, especially if the client and server are in different directories.

This approach is used in the [Roslyn .NET compiler](https://github.com/dotnet/roslyn). For those who want to delve deeper into this topic, I recommend studying the source code of the project

The `PipeDirection` indicates the direction of the channel.
`PipeDirection.In` implies that the server will only receive messages, while `PipeDirection.InOut` can both receive and send messages.

#####<a name="3.6"></a> Client Creation

To create the client, we will use the `NamedPipeClientStream` class.
The code is almost similar to the server and may vary slightly depending on the .NET versions.
For instance, in .NET framework 4.8, the `PipeOptions.CurrentUserOnly` value does not exist, but it appears in .NET 7.

<pre class="prettyprint">
/// &lt;summary&gt;
/// Create a client for the current user only
/// &lt;/summary&gt;
public static NamedPipeClientStream CreateClient(PipeDirection? pipeDirection = null)
{
  const PipeOptions pipeOptions = PipeOptions.Asynchronous | PipeOptions.WriteThrough | PipeOptions.CurrentUserOnly;
  return new NamedPipeClientStream(".",
    GetPipeName(),
    pipeDirection ?? PipeDirection.Out,
    pipeOptions);
}

private static string GetPipeName()
{
  var clientDirectory = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
  var pipeNameInput = $"{System.Environment.UserName}.{clientDirectory}";
  var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(pipeNameInput));

  return Convert.ToBase64String(bytes)
    .Replace("/", "_")
    .Replace("=", string.Empty);
}
</pre>

#####<a name="3.7"></a> Transmission Protocol

NamedPipe represents a stream, which allows us to write any sequence of bytes to the stream.
However, working with bytes directly might not be very convenient, especially when dealing with complex data or structures.
To simplify the interaction with data streams and structure information in a convenient format, transmission protocols are used.

Transmission protocols define the format and order of data transmission between applications.
They ensure the structuring of information to facilitate understanding and proper interpretation of data between the sender and the receiver.

In cases where we need to send a "Request to execute a specific command on the server" or a "Request to update application settings," the server must understand how to process it from the client.
Therefore, to facilitate request handling and data exchange management, we will create an `RequestType` Enum.

<pre class="prettyprint">
public enum RequestType
{
    PrintMessage,
    UpdateModel
}
</pre>

The request itself will be represented by a class that will contain all the information about the transmitted data.

<pre class="prettyprint">
public abstract class Request
{
  public abstract RequestType Type { get; }

  protected abstract void AddRequestBody(BinaryWriter writer);

  /// &lt;summary&gt;
  ///   Write a Request to the given stream.
  /// &lt;/summary&gt;
  public async Task WriteAsync(Stream outStream)
  {
    using var memoryStream = new MemoryStream();
    using var writer = new BinaryWriter(memoryStream, Encoding.Unicode);

    writer.Write((int) Type);
    AddRequestBody(writer);
    writer.Flush();

    // Write the length of the request
    var length = checked((int) memoryStream.Length);

    // There is no way to know the number of bytes written to
    // the pipe stream. We just have to assume all of them are written
    await outStream.WriteAsync(BitConverter.GetBytes(length), 0, 4);
    memoryStream.Position = 0;
    await memoryStream.CopyToAsync(outStream, length);
  }

  /// &lt;summary&gt;
  /// Write a string to the Writer where the string is encoded
  /// as a length prefix (signed 32-bit integer) follows by
  /// a sequence of characters.
  /// &lt;/summary&gt;
  protected static void WriteLengthPrefixedString(BinaryWriter writer, string value)
  {
    writer.Write(value.Length);
    writer.Write(value.ToCharArray());
  }
}
</pre>

The class contains the basic code for writing data to the stream. `AddRequestBody()` is used by derived classes to write their own structured data.

Examples of derived classes:

<pre class="prettyprint">
/// &lt;summary&gt;
/// Represents a Request from the client. A Request is as follows.
///
///  Field Name         Type            Size (bytes)
/// --------------------------------------------------
///  RequestType        Integer         4
///  Message            String          Variable
///
/// Strings are encoded via a character count prefix as a
/// 32-bit integer, followed by an array of characters.
///
/// &lt;/summary&gt;
public class PrintMessageRequest : Request
{
  public string Message { get; }

  public override RequestType Type =&gt; RequestType.PrintMessage;

  public PrintMessageRequest(string message)
  {
    Message = message;
  }

  protected override void AddRequestBody(BinaryWriter writer)
  {
    WriteLengthPrefixedString(writer, Message);
  }
}

/// &lt;summary&gt;
/// Represents a Request from the client. A Request is as follows.
///
///  Field Name         Type            Size (bytes)
/// --------------------------------------------------
///  ResponseType       Integer         4
///  Iterations         Integer         4
///  ForceUpdate        Boolean         1
///  ModelName          String          Variable
///
/// Strings are encoded via a character count prefix as a
/// 32-bit integer, followed by an array of characters.
///
/// &lt;/summary&gt;
public class UpdateModelRequest : Request
{
  public int Iterations { get; }
  public bool ForceUpdate { get; }
  public string ModelName { get; }

  public override RequestType Type =&gt; RequestType.UpdateModel;

  public UpdateModelRequest(string modelName, int iterations, bool forceUpdate)
  {
    Iterations = iterations;
    ForceUpdate = forceUpdate;
    ModelName = modelName;
  }

  protected override void AddRequestBody(BinaryWriter writer)
  {
    writer.Write(Iterations);
    writer.Write(ForceUpdate);
    WriteLengthPrefixedString(writer, ModelName);
  }
}
</pre>

By using this structure, clients can create requests of various types, each of which defines its own logic for handling data and parameters.
The `PrintMessageRequest` and `UpdateModelRequest` classes provide examples of requests that can be sent to the server to perform specific tasks.

On the server side, it is necessary to develop the corresponding logic for processing incoming requests.
To do this, the server must read data from the stream and use the received parameters to perform the necessary operations.

Example of a received request on the server side:

<pre class="prettyprint">
/// &lt;summary&gt;
/// Represents a request from the client. A request is as follows.
///
///  Field Name         Type                Size (bytes)
/// ----------------------------------------------------
///  RequestType       enum RequestType   4
///  RequestBody       Request subclass   variable
///
/// &lt;/summary&gt;
public abstract class Request
{
  public enum RequestType
  {
    PrintMessage,
    UpdateModel
  }

  public abstract RequestType Type { get; }

  /// &lt;summary&gt;
  ///   Read a Request from the given stream.
  /// &lt;/summary&gt;
  public static async Task&lt;Request&gt; ReadAsync(Stream stream)
  {
    var lengthBuffer = new byte[4];
    await ReadAllAsync(stream, lengthBuffer, 4).ConfigureAwait(false);
    var length = BitConverter.ToUInt32(lengthBuffer, 0);

    var requestBuffer = new byte[length];
    await ReadAllAsync(stream, requestBuffer, requestBuffer.Length);

    using var reader = new BinaryReader(new MemoryStream(requestBuffer), Encoding.Unicode);

    var requestType = (RequestType) reader.ReadInt32();
    return requestType switch
    {
      RequestType.PrintMessage =&gt; PrintMessageRequest.Create(reader),
      RequestType.UpdateModel =&gt; UpdateModelRequest.Create(reader),
      _ =&gt; throw new ArgumentOutOfRangeException()
    };
  }

  /// &lt;summary&gt;
  /// This task does not complete until we are completely done reading.
  /// &lt;/summary&gt;
  private static async Task ReadAllAsync(Stream stream, byte[] buffer, int count)
  {
    var totalBytesRead = 0;
    do
    {
      var bytesRead = await stream.ReadAsync(buffer, totalBytesRead, count - totalBytesRead);
      if (bytesRead == 0) throw new EndOfStreamException("Reached end of stream before end of read.");
      totalBytesRead += bytesRead;
    } while (totalBytesRead &lt; count);
  }

  /// &lt;summary&gt;
  /// Read a string from the Reader where the string is encoded
  /// as a length prefix (signed 32-bit integer) followed by
  /// a sequence of characters.
  /// &lt;/summary&gt;
  protected static string ReadLengthPrefixedString(BinaryReader reader)
  {
    var length = reader.ReadInt32();
    return length &lt; 0 ? null : new string(reader.ReadChars(length));
  }
}

/// &lt;summary&gt;
/// Represents a Request from the client. A Request is as follows.
///
///  Field Name         Type            Size (bytes)
/// --------------------------------------------------
///  RequestType        Integer         4
///  Message            String          Variable
///
/// Strings are encoded via a character count prefix as a
/// 32-bit integer, followed by an array of characters.
///
/// &lt;/summary&gt;
public class PrintMessageRequest : Request
{
  public string Message { get; }

  public override RequestType Type =&gt; RequestType.PrintMessage;

  public PrintMessageRequest(string message)
  {
    Message = message;
  }

  protected override void AddRequestBody(BinaryWriter writer)
  {
    WriteLengthPrefixedString(writer, Message);
  }
}

/// &lt;summary&gt;
/// Represents a Request from the client. A Request is as follows.
///
///  Field Name         Type            Size (bytes)
/// --------------------------------------------------
///  RequestType        Integer         4
///  Iterations         Integer         4
///  ForceUpdate        Boolean         1
///  ModelName          String          Variable
///
/// Strings are encoded via a character count prefix as a
/// 32-bit integer, followed by an array of characters.
///
/// &lt;/summary&gt;
public class UpdateModelRequest : Request
{
  public int Iterations { get; }
  public bool ForceUpdate { get; }
  public string ModelName { get; }

  public override RequestType Type =&gt; RequestType.UpdateModel;

  public UpdateModelRequest(string modelName, int iterations, bool forceUpdate)
  {
    Iterations = iterations;
    ForceUpdate = forceUpdate;
    ModelName = modelName;
  }

  protected override void AddRequestBody(BinaryWriter writer)
  {
    writer.Write(Iterations);
    writer.Write(ForceUpdate);
    WriteLengthPrefixedString(writer, ModelName);
  }
}
</pre>

The `ReadAsync()` method reads the request type from the stream and then, depending on the type, reads the corresponding data and creates an object of the corresponding request.

Implementing a data transmission protocol and structuring requests as classes enable efficient management of information exchange between the client and the server, ensuring structured and comprehensible interaction between the two parties.
However, when designing such protocols, it is essential to consider potential security risks and ensure that both ends of the interaction handle all possible scenarios correctly.

#####<a name="3.8"></a> Connection Management

To send messages from the UI client to the server, let's create a `ClientDispatcher` class that will handle connections, timeouts, and scheduling requests, providing an interface for client-server interaction via named pipes.

<pre class="prettyprint">
/// &lt;summary&gt;
///     This class manages the connections, timeout and general scheduling of requests to the server.
/// &lt;/summary&gt;
public class ClientDispatcher
{
  private const int TimeOutNewProcess = 10000;

  private Task _connectionTask;
  private readonly NamedPipeClientStream _client = NamedPipeUtil.CreateClient(PipeDirection.Out);

  /// &lt;summary&gt;
  ///   Connects to server without awaiting
  /// &lt;/summary&gt;
  public void ConnectToServer()
  {
    _connectionTask = _client.ConnectAsync(TimeOutNewProcess);
  }

  /// &lt;summary&gt;
  ///   Write a Request to the server.
  /// &lt;/summary&gt;
  public async Task WriteRequestAsync(Request request)
  {
    await _connectionTask;
    await request.WriteAsync(_client);
  }
}
</pre>

Working principle:

-  **Initialization:** the `NamedPipeClientStream` is initialized in the class constructor, used to create a client stream with a named pipe.
-  **Establishing Connection:** the `ConnectToServer` method initiates an asynchronous connection to the server.
   The operation's result is stored in a `Task`.
   `TimeOutNewProcess` is used to disconnect the client in case of unexpected exceptions.
-  **Sending Requests:** the `WriteRequestAsync` method is designed for asynchronously sending a Request object through the established connection.
   The request will be sent only after the connection is established.

To receive messages by the server, we will create a `ServerDispatcher` class to manage the connection and read requests.

<pre class="prettyprint">
/// &lt;summary&gt;
///     This class manages the connections, timeout and general scheduling of the client requests.
/// &lt;/summary&gt;
public class ServerDispatcher
{
  private readonly NamedPipeServerStream _server = NamedPipeUtil.CreateServer(PipeDirection.In);

  /// &lt;summary&gt;
  ///   This function will accept and process new requests until the client disconnects from the server
  /// &lt;/summary&gt;
  public async Task ListenAndDispatchConnections()
  {
    try
    {
      await _server.WaitForConnectionAsync();
      await ListenAndDispatchConnectionsCoreAsync();
    }
    finally
    {
      _server.Close();
    }
  }

  private async Task ListenAndDispatchConnectionsCoreAsync()
  {
    while (_server.IsConnected)
    {
      try
      {
        var request = await Request.ReadAsync(_server);
        if (request.Type == Request.RequestType.PrintMessage)
        {
          var printRequest = (PrintMessageRequest) request;
          Console.WriteLine($"Message from client: {printRequest.Message}");
        }
        else if (request.Type == Request.RequestType.UpdateModel)
        {
          var printRequest = (UpdateModelRequest) request;
          Console.WriteLine($"The {printRequest.ModelName} model has been {(printRequest.ForceUpdate ? "forcibly" : string.Empty)} updated {printRequest.Iterations} times");
        }
      }
      catch (EndOfStreamException)
      {
        return; //Pipe disconnected
      }
    }
  }
}
</pre>

Working principle:

-  **Initialization:** the `NamedPipeServerStream` is initialized in the class constructor, used to create a server stream with a named pipe.
-  **Listening for Connections:** The `ListenAndDispatchConnections()` method asynchronously waits for a client connection.
   After processing the requests, it closes the named pipe and releases resources.
-  **Handling Requests:** The `ListenAndDispatchConnectionsCoreAsync()` method handles requests until the client is disconnected.
   Depending on the type of request, corresponding data processing occurs, such as displaying the message content in the console or updating the model.

An example of sending a request from the UI to the server:

<pre class="prettyprint">
/// &lt;summary&gt;
///   Programme entry point
/// &lt;/summary&gt;
public sealed partial class App
{
  public static ClientDispatcher ClientDispatcher { get; }

  static App()
  {
    ClientDispatcher = new ClientDispatcher();
    ClientDispatcher.ConnectToServer();
  }
}

/// &lt;summary&gt;
///   WPF view business logic
/// &lt;/summary&gt;
public partial class MainViewModel : ObservableObject
{
  [ObservableProperty] private string _message = string.Empty;

  [RelayCommand]
  private async Task SendMessageAsync()
  {
    var request = new PrintMessageRequest(Message);
    await App.ClientDispatcher.WriteRequestAsync(request);
  }

  [RelayCommand]
  private async Task UpdateModelAsync()
  {
    var request = new UpdateModelRequest(AppDomain.CurrentDomain.FriendlyName, 666, true);
    await App.ClientDispatcher.WriteRequestAsync(request);
  }
}
</pre>

The complete code example is available in the repository, and you can run it on your machine by following a few steps:

- Run "Build Solution."
- Run "Run OneWay/Backend."

The application will automatically launch the Server and Client, and you will see the full output of the messages transmitted via the NamedPipe in the IDE console.

#####<a name="3.9"></a> Two-Way Communication

There are often situations where the usual one-way data transmission from the client to the server is not sufficient.
In such cases, it is necessary to handle errors or send results in response.
To enable more complex interaction between the client and the server, developers have to resort to the use of two-way data transmission, which allows for the exchange of information in both directions.

Similar to requests, to efficiently handle responses, it is also necessary to define an enumeration for response types.
This will enable the client to interpret the received data correctly.

<pre class="prettyprint">
public enum ResponseType
{
  // The update request completed on the server and the results are contained in the message.
  UpdateCompleted,

  // The request was rejected by the server.
  Rejected
}
</pre>

Efficient handling of responses will require creating a new class named `Response`.
Functionally, it does not differ from the Request class.
However, unlike Request, which can be read on the server, Response will be written to the stream.

<pre class="prettyprint">
/// &lt;summary&gt;
/// Base class for all possible responses to a request.
/// The ResponseType enum should list all possible response types
/// and ReadResponse creates the appropriate response subclass based
/// on the response type sent by the client.
/// The format of a response is:
///
/// Field Name       Field Type          Size (bytes)
/// -------------------------------------------------
/// ResponseType     enum ResponseType   4
/// ResponseBody     Response subclass   variable
/// &lt;/summary&gt;
public abstract class Response
{
  public enum ResponseType
  {
    // The update request completed on the server and the results are contained in the message.
    UpdateCompleted,

    // The request was rejected by the server.
    Rejected
  }

  public abstract ResponseType Type { get; }

  protected abstract void AddResponseBody(BinaryWriter writer);

  /// &lt;summary&gt;
  ///   Write a Response to the stream.
  /// &lt;/summary&gt;
  public async Task WriteAsync(Stream outStream)
  {
    // Same as request class from client
  }

  /// &lt;summary&gt;
  /// Write a string to the Writer where the string is encoded
  /// as a length prefix (signed 32-bit integer) follows by
  /// a sequence of characters.
  /// &lt;/summary&gt;
  protected static void WriteLengthPrefixedString(BinaryWriter writer, string value)
  {
    // Same as request class from client
  }
}
</pre>

You can find derivative classes in the project repository: [PipeProtocol](https://github.com/atomatiq/InterprocessCommunication/blob/main/TwoWay/Backend/Server/PipeProtocol.cs)

To enable the server to send responses to the client, we need to modify the `ServerDispatcher` class.
This will allow writing responses to the stream after executing a task.

Additionally, let's change the pipe direction to bidirectional:

<pre class="prettyprint">
_server = NamedPipeUtil.CreateServer(PipeDirection.InOut);

/// &lt;summary&gt;
///     Write a Response to the client.
/// &lt;/summary&gt;
public async Task WriteResponseAsync(Response response) =&gt; await response.WriteAsync(_server);
</pre>

To demonstrate the operation, let's add a 2-second delay, emulating a heavy task, in the `ListenAndDispatchConnectionsCoreAsync()` method.

<pre class="prettyprint">
private async Task ListenAndDispatchConnectionsCoreAsync()
{
  while (_server.IsConnected)
  {
    try
    {
      var request = await Request.ReadAsync(_server);

      // ...
      if (request.Type == Request.RequestType.UpdateModel)
      {
        var printRequest = (UpdateModelRequest) request;

        await Task.Delay(TimeSpan.FromSeconds(2));
        await WriteResponseAsync(new UpdateCompletedResponse(changes: 69, version: "2.1.7"));
      }
    }
    catch (EndOfStreamException)
    {
      return; //Pipe disconnected
    }
  }
}
</pre>

Currently, the client does not handle responses from the server.
Let's address this. Let's create a `Response` class in the client that will handle the received responses.

<pre class="prettyprint">
/// &lt;summary&gt;
/// Base class for all possible responses to a request.
/// The ResponseType enum should list all possible response types
/// and ReadResponse creates the appropriate response subclass based
/// on the response type sent by the client.
/// The format of a response is:
///
/// Field Name       Field Type          Size (bytes)
/// -------------------------------------------------
/// ResponseType     enum ResponseType   4
/// ResponseBody     Response subclass   variable
///
/// &lt;/summary&gt;
public abstract class Response
{
  public enum ResponseType
  {
    // The update request completed on the server and the results are contained in the message.
    UpdateCompleted,

    // The request was rejected by the server.
    Rejected
  }

  public abstract ResponseType Type { get; }

  /// &lt;summary&gt;
  ///   Read a Request from the given stream.
  /// &lt;/summary&gt;
  public static async Task&lt;Response&gt; ReadAsync(Stream stream)
  {
    // Same as request class from server
  }

  /// &lt;summary&gt;
  /// This task does not complete until we are completely done reading.
  /// &lt;/summary&gt;
  private static async Task ReadAllAsync(Stream stream, byte[] buffer, int count)
  {
    // Same as request class from server
  }

  /// &lt;summary&gt;
  /// Read a string from the Reader where the string is encoded
  /// as a length prefix (signed 32-bit integer) followed by
  /// a sequence of characters.
  /// &lt;/summary&gt;
  protected static string ReadLengthPrefixedString(BinaryReader reader)
  {
    // Same as request class from server
  }
}
</pre>

Furthermore, we'll update the `ClientDispatcher` class to handle responses from the server.
To do this, we'll add a new method and change the direction to bidirectional.

<pre class="prettyprint">
_client = NamedPipeUtil.CreateClient(PipeDirection.InOut);

/// &lt;summary&gt;
///     Read a Response from the server.
/// &lt;/summary&gt;
public async Task&lt;Response&gt; ReadResponseAsync() =&gt; await Response.ReadAsync(_client);
</pre>

We'll also add response handling to the ViewModel, where we'll simply display it as a message.

<pre class="prettyprint">
[RelayCommand]
private async Task UpdateModelAsync()
{
  var request = new UpdateModelRequest(AppDomain.CurrentDomain.FriendlyName, 666, true);
  await App.ClientDispatcher.WriteRequestAsync(request);

  var response = await App.ClientDispatcher.ReadResponseAsync();
  if (response.Type == Response.ResponseType.UpdateCompleted)
  {
    var completedResponse = (UpdateCompletedResponse) response;

    MessageBox.Show($"{completedResponse.Changes} elements successfully updated to version {completedResponse.Version}");
  }
  else if (response.Type == Response.ResponseType.Rejected)
  {
    MessageBox.Show("Update failed");
  }
}
</pre>

These changes will allow for more efficient organization of the interaction between the client and the server, ensuring a more complete and reliable handling of requests and responses.

#####<a name="3.10"></a> Implementation for Revit plug-in

<center>
<img src="img/2023_revit_technology.jpg" alt="Revit technology 2023" title="Revit technology 2023" width="500"/>
<p style="font-size: 80%; font-style:italic">Technology evolves, Revit never changes © Confucius</p>
</center>

Currently, Revit is using .NET Framework 4.8.
However, to enhance the plugin user interface, let's consider upgrading to .NET 7.
It is important to note that the backend of the plugin will interact only with the outdated framework of Revit and will act as a server.

Let's create a mechanism of interaction that allows the client to send requests for the deletion of model elements and subsequently receive responses regarding the deletion results.
To implement this functionality, we will use bidirectional data transfer between the server and the client.

The first step in our development process will be to enable the plugin to automatically close upon Revit's closure.
To accomplish this, we have written a method that sends the ID of the current process to the client.
This will help the client to automatically close its process upon the closure of the parent Revit process.

Here is the code for sending the ID of the current process to the client:

<pre class="prettyprint">
private static void RunClient(string clientName)
{
  var startInfo = new ProcessStartInfo
  {
    FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!.AppendPath(clientName),
    Arguments = Process.GetCurrentProcess().Id.ToString()
  };

  Process.Start(startInfo);
}
</pre>

And here is the code for the client, which facilitates the closure of its process upon the closure of the parent Revit process:

<pre class="prettyprint">
protected override void OnStartup(StartupEventArgs args)
{
  ParseCommandArguments(args.Args);
}

private void ParseCommandArguments(string[] args)
{
  var ownerPid = args[0];
  var ownerProcess = Process.GetProcessById(int.Parse(ownerPid));
  ownerProcess.EnableRaisingEvents = true;
  ownerProcess.Exited += (_, _) =&gt; Shutdown();
}
</pre>

Additionally, we require a method that will handle the deletion of selected model elements:

<pre class="prettyprint">
public static ICollection&lt;ElementId&gt; DeleteSelectedElements()
{
  var transaction = new Transaction(Document);
  transaction.Start("Delete elements");

  var selectedIds = UiDocument.Selection.GetElementIds();
  var deletedIds = Document.Delete(selectedIds);

  transaction.Commit();
  return deletedIds;
}
</pre>

Let's also update the method `ListenAndDispatchConnectionsCoreAsync()` to handle incoming connections:

<pre class="prettyprint">
private async Task ListenAndDispatchConnectionsCoreAsync()
{
  while (_server.IsConnected)
  {
    try
    {
      var request = await Request.ReadAsync(_server);
      if (request.Type == Request.RequestType.DeleteElements)
      {
        await ProcessDeleteElementsAsync();
      }
    }
    catch (EndOfStreamException)
    {
      return; //Pipe disconnected
    }
  }
}

private async Task ProcessDeleteElementsAsync()
{
  try
  {
    var deletedIds = await Application.AsyncEventHandler.RaiseAsync(_ =&gt; RevitApi.DeleteSelectedElements());
    await WriteResponseAsync(new DeletionCompletedResponse(deletedIds.Count));
  }
  catch (Exception exception)
  {
    await WriteResponseAsync(new RejectedResponse(exception.Message));
  }
}
</pre>

And finally, the updated ViewModel code:

<pre class="prettyprint">
[RelayCommand]
private async Task DeleteElementsAsync()
{
  var request = new DeleteElementsRequest();
  await App.ClientDispatcher.WriteRequestAsync(request);

  var response = await App.ClientDispatcher.ReadResponseAsync();
  if (response.Type == Response.ResponseType.Success)
  {
    var completedResponse = (DeletionCompletedResponse) response;
    MessageBox.Show($"{completedResponse.Changes} elements successfully deleted");
  }
  else if (response.Type == Response.ResponseType.Rejected)
  {
    var rejectedResponse = (RejectedResponse) response;
    MessageBox.Show($"Deletion failed\n{rejectedResponse.Reason}");
  }
}
</pre>

####<a name="3.11"></a> Installing .NET Runtime during plugin installation

Not every user may have the latest version of .NET Runtime installed on their local machine, so we need to make some changes to the plugin installer.

If you are using the [Nice3point.RevitTemplates](https://github.com/Nice3point/RevitTemplates), making these adjustments will be effortless.
The templates use the WixSharp library, which enables the creation of `.msi` files directly in C#.

To add custom actions and install .NET Runtime, we will create a `CustomAction`:

<pre class="prettyprint">
public static class RuntimeActions
{
  /// &lt;summary&gt;
  ///   Add-in client .NET version
  /// &lt;/summary&gt;
  private const string DotnetRuntimeVersion = "7";

  /// &lt;summary&gt;
  ///   Direct download link
  /// &lt;/summary&gt;
  private const string DotnetRuntimeUrl = $"https://aka.ms/dotnet/{DotnetRuntimeVersion}.0/windowsdesktop-runtime-win-x64.exe";

  /// &lt;summary&gt;
  ///   Installing the .NET runtime after installing software
  /// &lt;/summary&gt;
  [CustomAction]
  public static ActionResult InstallDotnet(Session session)
  {
    try
    {
      var isRuntimeInstalled = CheckDotnetInstallation();
      if (isRuntimeInstalled) return ActionResult.Success;

      var destinationPath = Path.Combine(Path.GetTempPath(), "windowsdesktop-runtime-win-x64.exe");

      UpdateStatus(session, "Downloading .NET runtime");
      DownloadRuntime(destinationPath);

      UpdateStatus(session, "Installing .NET runtime");
      var status = InstallRuntime(destinationPath);

      var result = status switch
      {
        0 =&gt; ActionResult.Success,
        1602 =&gt; ActionResult.UserExit,
        1618 =&gt; ActionResult.Success,
        _ =&gt; ActionResult.Failure
      };

      File.Delete(destinationPath);
      return result;
    }
    catch (Exception exception)
    {
      session.Log("Error downloading and installing DotNet: " + exception.Message);
      return ActionResult.Failure;
    }
  }

  private static int InstallRuntime(string destinationPath)
  {
    var startInfo = new ProcessStartInfo(destinationPath)
    {
      Arguments = "/q",
      UseShellExecute = false
    };

    var installProcess = Process.Start(startInfo)!;
    installProcess.WaitForExit();
    return installProcess.ExitCode;
  }

  private static void DownloadRuntime(string destinationPath)
  {
    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

    using var httpClient = new HttpClient();
    var responseBytes = httpClient.GetByteArrayAsync(DotnetRuntimeUrl).Result;

    File.WriteAllBytes(destinationPath, responseBytes);
  }

  private static bool CheckDotnetInstallation()
  {
    var startInfo = new ProcessStartInfo
    {
      FileName = "dotnet",
      Arguments = "--list-runtimes",
      RedirectStandardOutput = true,
      UseShellExecute = false,
      CreateNoWindow = true
    };

    try
    {
      var process = Process.Start(startInfo)!;
      var output = process.StandardOutput.ReadToEnd();
      process.WaitForExit();

      return output.Split('\n')
        .Where(line =&gt; line.Contains("Microsoft.WindowsDesktop.App"))
        .Any(line =&gt; line.Contains($"{DotnetRuntimeVersion}."));
    }
    catch
    {
      return false;
    }
  }

  private static void UpdateStatus(Session session, string message)
  {
    var record = new Record(3);
    record[2] = message;

    session.Message(InstallMessage.ActionStart, record);
  }
}
</pre>

This code checks whether the required version of .NET is installed on the local machine, and if not, it downloads and installs it.
The installation process updates the `Status` of the current progress of downloading and unpacking the Runtime.

Finally, we need to connect the `CustomAction` to the WixSharp project. To do this, we initialize the `Actions` property:

<pre class="prettyprint">
var project = new Project
{
  Name = "Wix Installer",
  UI = WUI.WixUI_FeatureTree,
  GUID = new Guid("8F2926C8-3C6C-4D12-9E3C-7DF611CD6DDF"),
  Actions = new Action[]
  {
    new ManagedAction(RuntimeActions.InstallDotnet,
      Return.check,
      When.Before,
      Step.InstallFinalize,
      Condition.NOT_Installed)
  }
};
</pre>

####<a name="3.12"></a> Conclusion

In this article, we explored how Named Pipes, primarily used for Inter-Process Communication (IPC), can be used in scenarios requiring data exchange between applications running on different .NET versions.
Dealing with code that needs to be maintained across multiple versions, a well-considered IPC strategy can be valuable, providing key benefits such as:

- Dependency conflict resolution
- Enhancing performance
- Functional flexibility

We discussed the process of creating a server and client that interact with each other through a pre-defined protocol, as well as various ways of managing connections.

We examined an example of server responses and demonstrated the operation of both sides of the interaction.

Finally, we underscored how Named Pipes are used in the development of a plugin for Revit to provide communication between the backend operating on the legacy .NET 4.8 platform and the user interface running on the newer .NET 7 version.

Demo code for each part of this article is available on GitHub.

In certain cases, splitting applications into separate processes can not only reduce dependencies within the program but also improve the UI responsiveness.
However, let us not forget that the choice of approach requires analysis and should be based on the actual requirements and constraints of your project.

Do you need to split each plugin into multiple processes? Definitely not.

We hope that this article will help you find the best solution for your interprocess communication scenarios and give you an understanding of how to apply IPC approaches in practice.

Many thanks to Roman for his deep research and careful documentation of this important topic, in addition to all his maintenance work on RevitLookup.

####<a name="4"></a> Fuyu-8B Multimodal Architecture for AI Agents

Another open source multimodal model hit the scene,
[Fuyu-8B: A Multimodal Architecture for AI Agents](https://www.adept.ai/blog/fuyu-8b).
It can be run offline on a laptop CPU.

####<a name="6"></a> How Open Source Wins

[Open Source does not win by being cheaper](https://github.com/getlago/lago/wiki/Open-Source-does-not-win-by-being-cheaper#how-open-source-winsby-solving-an-extensibility-problem),
but by offering tranparency, extensibility and quality.

####<a name="5"></a> HTTP/3

Did you notice that you have started using HTTP/3?
I hadn't.
Learn [why HTTP/3 is eating the world](https://blog.apnic.net/2023/09/25/why-http-3-is-eating-the-world/).

