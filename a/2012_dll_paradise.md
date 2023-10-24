<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


twitter:

 with the #RevitAPI @AutodeskRevit #BIM @DynamoBIM @AutodeskAPS

&ndash; ...

linkedin:



#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### DLL Paradise


####<a name="2"></a> DLL Paradise for Revit Add-ins via Named Pipe IPC

Windows applications with dependencies on external components occasionally
encounter [DLL hell\(https://duckduckgo.com/?q=dll+hell) due to conflicting dependencies.
The Building Coder occasionally discussed
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

 a new solution for


- roman blog post -- ipc for revit add-ins
DLL Hell -- past discussions
DLL Paradise -- roman
Just wrote a new article about Revit and plugins inter-processor communication
Post: https://www.linkedin.com/feed/update/urn:li:activity:7120385512464388096/
ENU version: https://github.com/atomatiq/InterprocessCommunication
CIS version: https://github.com/Nice3point/InterprocessCommunication
Please be aware that Revit API is working at moving away from .NET 4.8 as we speak:
https://thebuildingcoder.typepad.com/blog/2023/08/15-years-polygon-areas-and-net-core.html#3
As usual, you are one step ahead and mention .NET 7…





####<a name="2"></a> Interprocess Communication: Strategies and Best Practices

<p align="center">
  <img src="https://github.com/Nice3point/InterprocessCommunication/assets/20504884/21d38cc0-9dfe-46af-959d-8deffaf91b3c" />
</p>

We all know how challenging it is to maintain large programs and keep up with progress.
Developers of plugins for Revit understand this better than anyone else.
We have to write our programs in .NET Framework 4.8 and forgo modern and fast libraries.
Ultimately, this affects users who are forced to use outdated software.

In such scenarios, splitting the application into multiple processes using Named Pipes appears to be an excellent solution due to its performance and reliability.
In this article, we discuss how to create and use Named Pipes to communicate between the Revit application running on .NET 4.8 and its plugin running on .NET 7.

####<a name="2"></a> Table of Contents

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

####<a name="2"></a> Introduction to Using Named Pipes for Communication Between Applications on Different .NET Versions

In the world of application development, there is often a need to ensure data exchange between different applications, especially in cases where they operate on different versions of .NET or different languages.
Splitting a single application into multiple processes must be justified.
What is simpler, calling a function directly, or exchanging messages? Obviously, the former.

So what are the benefits of doing this?

- Resolving Dependency Conflicts

  With each passing year, the size of Revit plugins is growing exponentially, and dependencies are also increasing at a geometric rate.
  Plugins might use incompatible versions of a single library, leading to program crashes. Process isolation solves this problem.

- Performance

  The performance measurements for sorting and mathematical calculations on different .NET versions are provided below.

    ```
    BenchmarkDotNet v0.13.9, Windows 11 (10.0.22621.1702/22H2/2022Update/SunValley2)
    AMD Ryzen 5 2600X, 1 CPU, 12 logical and 6 physical cores
    .NET 7.0           : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2
    .NET Framework 4.8 : .NET Framework 4.8.1 (4.8.9139.0), X64 RyuJIT VectorSize=256
    ```
  | Method      | Runtime            | Mean           | Error        | StdDev       | Allocated |
    |------------ |------------------- |---------------:|-------------:|-------------:|----------:|
  | ListSort    | .NET 7.0           | 1,113,161.8 ns | 20,385.15 ns | 21,811.88 ns |  804753 B |
  | ListOrderBy | .NET 7.0           | 1,064,851.1 ns | 12,401.25 ns | 11,600.13 ns |  807054 B |
  | MinValue    | .NET 7.0           |       979.4 ns |      7.40 ns |      6.56 ns |         - |
  | MaxValue    | .NET 7.0           |       970.6 ns |      4.32 ns |      3.60 ns |         - |
  | ListSort    | .NET Framework 4.8 | 2,144,723.5 ns | 40,359.72 ns | 37,752.51 ns | 1101646 B |
  | ListOrderBy | .NET Framework 4.8 | 2,192,414.7 ns | 25,938.78 ns | 24,263.15 ns | 1105311 B |
  | MinValue    | .NET Framework 4.8 |    58,019.0 ns |    460.30 ns |    430.57 ns |      40 B |
  | MaxValue    | .NET Framework 4.8 |    66,053.4 ns |    610.28 ns |    541.00 ns |      41 B |

  The 68-fold difference in speed when finding the minimum value, and the complete absence of memory allocation, is impressive.

How then to write a program in the latest .NET version that will interact with an incompatible .NET framework?
Create two applications, Server and Client, without adding dependencies between each other and configure the interaction between them using a configured protocol.

Below are some of the possible ways of interaction between two applications:

1. Using WCF (Windows Communication Foundation)
2. Using sockets (TCP or UDP)
3. Using Named Pipes
4. Using operating system signals (e.g., Windows signals):

   An example from Autodesk's code, the interaction of the Project Browser plugin with the Revit backend via messages.

    ```c#
    public class DataTransmitter : IEventObserver
    {
        private void PostMessageToMainWindow(int iCmd) =>
            this.HandleOnMainThread((Action) (() =>
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
    ```

Each option has its own pros and cons. In my opinion, the most convenient for local machine interaction is Named Pipes. Let's delve into it.

####<a name="2"></a> What are Named Pipes?

Named Pipes are a mechanism for Inter-Process Communication (IPC) that enables processes to exchange data through named channels.
They provide a one-way or duplex connection between processes.
Apart from high performance, Named Pipes also offer various security levels, making them an attractive solution for many inter-process communication scenarios.

####<a name="2"></a> Interactions between applications in .NET 4.8 and .NET 7

Let's consider two applications, one containing the business logic (server), and the other one for the user interface (client).
NamedPipe is used to facilitate communication between these two processes.

The operation principle of NamedPipe involves the following steps:

1. **Creation and configuration of NamedPipe**: The server creates and configures the NamedPipe with a specific name that will be accessible to the client.
   The client needs to know this name to connect to the pipe.
2. **Waiting for connection**: The server starts to wait for the client to connect to the pipe.
   This is a blocking operation, and the server remains in a pending state until the client connects.
3. **Connecting to NamedPipe**: The client initiates a connection to the NamedPipe, specifying the name of the pipe to which it wants to connect.
4. **Data exchange**: After a successful connection, the client and server can exchange data in the form of byte streams.
   The client sends requests for executing the business logic, and the server processes these requests and sends back the results.
5. **Session termination**: After the data exchange is complete, the client and server can close the connection with NamedPipe.

#####<a name="2"></a> Server Creation

On the .NET platform, the server side is represented by the `NamedPipeServerStream` class.
The class implementation provides both asynchronous and synchronous methods for working with NamedPipe.
To avoid blocking the main thread, we will utilize asynchronous methods.

Here's an example code snippet for creating a NamedPipeServer:

```C#
public static class NamedPipeUtil
{
    /// <summary>
    /// Create a server for the current user only
    /// </summary>
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
```

The server name should not contain special characters to avoid exceptions.
To generate the pipe name, we will use a hash created from the username and the current folder, which is unique enough for the client to use this server upon connection.
You can modify this behavior or use any name within the scope of your project, especially if the client and server are in different directories.

This approach is used in the [Roslyn .NET compiler](https://github.com/dotnet/roslyn). For those who want to delve deeper into this topic, I recommend studying the source code of the project

The `PipeDirection` indicates the direction of the channel.
`PipeDirection.In` implies that the server will only receive messages, while `PipeDirection.InOut` can both receive and send messages.

#####<a name="2"></a> Client Creation

To create the client, we will use the `NamedPipeClientStream` class.
The code is almost similar to the server and may vary slightly depending on the .NET versions.
For instance, in .NET framework 4.8, the `PipeOptions.CurrentUserOnly` value does not exist, but it appears in .NET 7.

```C#
/// <summary>
/// Create a client for the current user only
/// </summary>
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
```

#####<a name="2"></a> Transmission Protocol

NamedPipe represents a stream, which allows us to write any sequence of bytes to the stream.
However, working with bytes directly might not be very convenient, especially when dealing with complex data or structures.
To simplify the interaction with data streams and structure information in a convenient format, transmission protocols are used.

Transmission protocols define the format and order of data transmission between applications.
They ensure the structuring of information to facilitate understanding and proper interpretation of data between the sender and the receiver.

In cases where we need to send a "Request to execute a specific command on the server" or a "Request to update application settings," the server must understand how to process it from the client.
Therefore, to facilitate request handling and data exchange management, we will create an `RequestType` Enum.

```C#
public enum RequestType
{
    PrintMessage,
    UpdateModel
}
```

The request itself will be represented by a class that will contain all the information about the transmitted data.

```c#
public abstract class Request
{
    public abstract RequestType Type { get; }

    protected abstract void AddRequestBody(BinaryWriter writer);

    /// <summary>
    ///     Write a Request to the given stream.
    /// </summary>
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

    /// <summary>
    /// Write a string to the Writer where the string is encoded
    /// as a length prefix (signed 32-bit integer) follows by
    /// a sequence of characters.
    /// </summary>
    protected static void WriteLengthPrefixedString(BinaryWriter writer, string value)
    {
        writer.Write(value.Length);
        writer.Write(value.ToCharArray());
    }
}
```

The class contains the basic code for writing data to the stream. `AddRequestBody()` is used by derived classes to write their own structured data.

Examples of derived classes:

```C#
/// <summary>
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
/// </summary>
public class PrintMessageRequest : Request
{
    public string Message { get; }

    public override RequestType Type => RequestType.PrintMessage;

    public PrintMessageRequest(string message)
    {
        Message = message;
    }

    protected override void AddRequestBody(BinaryWriter writer)
    {
        WriteLengthPrefixedString(writer, Message);
    }
}

/// <summary>
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
/// </summary>
public class UpdateModelRequest : Request
{
    public int Iterations { get; }
    public bool ForceUpdate { get; }
    public string ModelName { get; }

    public override RequestType Type => RequestType.UpdateModel;

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
```

By using this structure, clients can create requests of various types, each of which defines its own logic for handling data and parameters.
The `PrintMessageRequest` and `UpdateModelRequest` classes provide examples of requests that can be sent to the server to perform specific tasks.

On the server side, it is necessary to develop the corresponding logic for processing incoming requests.
To do this, the server must read data from the stream and use the received parameters to perform the necessary operations.

Example of a received request on the server side:

```c#
/// <summary>
/// Represents a request from the client. A request is as follows.
///
///  Field Name         Type                Size (bytes)
/// ----------------------------------------------------
///  RequestType       enum RequestType   4
///  RequestBody       Request subclass   variable
///
/// </summary>
public abstract class Request
{
    public enum RequestType
    {
        PrintMessage,
        UpdateModel
    }

    public abstract RequestType Type { get; }

    /// <summary>
    ///     Read a Request from the given stream.
    /// </summary>
    public static async Task<Request> ReadAsync(Stream stream)
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
            RequestType.PrintMessage => PrintMessageRequest.Create(reader),
            RequestType.UpdateModel => UpdateModelRequest.Create(reader),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// This task does not complete until we are completely done reading.
    /// </summary>
    private static async Task ReadAllAsync(Stream stream, byte[] buffer, int count)
    {
        var totalBytesRead = 0;
        do
        {
            var bytesRead = await stream.ReadAsync(buffer, totalBytesRead, count - totalBytesRead);
            if (bytesRead == 0) throw new EndOfStreamException("Reached end of stream before end of read.");
            totalBytesRead += bytesRead;
        } while (totalBytesRead < count);
    }

    /// <summary>
    /// Read a string from the Reader where the string is encoded
    /// as a length prefix (signed 32-bit integer) followed by
    /// a sequence of characters.
    /// </summary>
    protected static string ReadLengthPrefixedString(BinaryReader reader)
    {
        var length = reader.ReadInt32();
        return length < 0 ? null : new string(reader.ReadChars(length));
    }
}

/// <summary>
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
/// </summary>
public class PrintMessageRequest : Request
{
    public string Message { get; }

    public override RequestType Type => RequestType.PrintMessage;

    public PrintMessageRequest(string message)
    {
        Message = message;
    }

    protected override void AddRequestBody(BinaryWriter writer)
    {
        WriteLengthPrefixedString(writer, Message);
    }
}

/// <summary>
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
/// </summary>
public class UpdateModelRequest : Request
{
    public int Iterations { get; }
    public bool ForceUpdate { get; }
    public string ModelName { get; }

    public override RequestType Type => RequestType.UpdateModel;

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
```

The `ReadAsync()` method reads the request type from the stream and then, depending on the type, reads the corresponding data and creates an object of the corresponding request.

Implementing a data transmission protocol and structuring requests as classes enable efficient management of information exchange between the client and the server, ensuring structured and comprehensible interaction between the two parties.
However, when designing such protocols, it is essential to consider potential security risks and ensure that both ends of the interaction handle all possible scenarios correctly.

#####<a name="2"></a> Connection Management

To send messages from the UI client to the server, let's create a `ClientDispatcher` class that will handle connections, timeouts, and scheduling requests, providing an interface for client-server interaction via named pipes.

```C#
/// <summary>
///     This class manages the connections, timeout and general scheduling of requests to the server.
/// </summary>
public class ClientDispatcher
{
    private const int TimeOutNewProcess = 10000;

    private Task _connectionTask;
    private readonly NamedPipeClientStream _client = NamedPipeUtil.CreateClient(PipeDirection.Out);

    /// <summary>
    ///     Connects to server without awaiting
    /// </summary>
    public void ConnectToServer()
    {
        _connectionTask = _client.ConnectAsync(TimeOutNewProcess);
    }

    /// <summary>
    ///     Write a Request to the server.
    /// </summary>
    public async Task WriteRequestAsync(Request request)
    {
        await _connectionTask;
        await request.WriteAsync(_client);
    }
}
```

Working principle:

1. **Initialization:** the `NamedPipeClientStream` is initialized in the class constructor, used to create a client stream with a named pipe.
2. **Establishing Connection:** the `ConnectToServer` method initiates an asynchronous connection to the server.
   The operation's result is stored in a `Task`.
   `TimeOutNewProcess` is used to disconnect the client in case of unexpected exceptions.
3. **Sending Requests:** the `WriteRequestAsync` method is designed for asynchronously sending a Request object through the established connection.
   The request will be sent only after the connection is established.

To receive messages by the server, we will create a `ServerDispatcher` class to manage the connection and read requests.

```C#
/// <summary>
///     This class manages the connections, timeout and general scheduling of the client requests.
/// </summary>
public class ServerDispatcher
{
    private readonly NamedPipeServerStream _server = NamedPipeUtil.CreateServer(PipeDirection.In);

    /// <summary>
    ///     This function will accept and process new requests until the client disconnects from the server
    /// </summary>
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
```

Working principle:

1. **Initialization:** the `NamedPipeServerStream` is initialized in the class constructor, used to create a server stream with a named pipe.
2. **Listening for Connections:** The `ListenAndDispatchConnections()` method asynchronously waits for a client connection.
   After processing the requests, it closes the named pipe and releases resources.
3. **Handling Requests:** The `ListenAndDispatchConnectionsCoreAsync()` method handles requests until the client is disconnected.
   Depending on the type of request, corresponding data processing occurs, such as displaying the message content in the console or updating the model.

An example of sending a request from the UI to the server:

```C#

/// <summary>
///     Programme entry point
/// </summary>
public sealed partial class App
{
    public static ClientDispatcher ClientDispatcher { get; }

    static App()
    {
        ClientDispatcher = new ClientDispatcher();
        ClientDispatcher.ConnectToServer();
    }
}

/// <summary>
///     WPF view business logic
/// </summary>
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
```

The complete code example is available in the repository, and you can run it on your machine by following a few steps:

- Run "Build Solution."
- Run "Run OneWay/Backend."

The application will automatically launch the Server and Client, and you will see the full output of the messages transmitted via the NamedPipe in the IDE console.

#####<a name="2"></a> Two-Way Communication

There are often situations where the usual one-way data transmission from the client to the server is not sufficient.
In such cases, it is necessary to handle errors or send results in response.
To enable more complex interaction between the client and the server, developers have to resort to the use of two-way data transmission, which allows for the exchange of information in both directions.

Similar to requests, to efficiently handle responses, it is also necessary to define an enumeration for response types.
This will enable the client to interpret the received data correctly.

```C#
public enum ResponseType
{
    // The update request completed on the server and the results are contained in the message.
    UpdateCompleted,

    // The request was rejected by the server.
    Rejected
}
```

Efficient handling of responses will require creating a new class named `Response`.
Functionally, it does not differ from the Request class.
However, unlike Request, which can be read on the server, Response will be written to the stream.

```C#
/// <summary>
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
/// </summary>
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

    /// <summary>
    ///     Write a Response to the stream.
    /// </summary>
    public async Task WriteAsync(Stream outStream)
    {
        // Same as request class from client
    }

    /// <summary>
    /// Write a string to the Writer where the string is encoded
    /// as a length prefix (signed 32-bit integer) follows by
    /// a sequence of characters.
    /// </summary>
    protected static void WriteLengthPrefixedString(BinaryWriter writer, string value)
    {
        // Same as request class from client
    }
}
```

You can find derivative classes in the project repository: [PipeProtocol](https://github.com/atomatiq/InterprocessCommunication/blob/main/TwoWay/Backend/Server/PipeProtocol.cs)

To enable the server to send responses to the client, we need to modify the `ServerDispatcher` class.
This will allow writing responses to the stream after executing a task.

Additionally, let's change the pipe direction to bidirectional:

```C#
_server = NamedPipeUtil.CreateServer(PipeDirection.InOut);

/// <summary>
///     Write a Response to the client.
/// </summary>
public async Task WriteResponseAsync(Response response) => await response.WriteAsync(_server);
```

To demonstrate the operation, let's add a 2-second delay, emulating a heavy task, in the `ListenAndDispatchConnectionsCoreAsync()` method.

```C#
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
```

Currently, the client does not handle responses from the server.
Let's address this. Let's create a `Response` class in the client that will handle the received responses.

```C#
/// <summary>
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
/// </summary>
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

    /// <summary>
    ///     Read a Request from the given stream.
    /// </summary>
    public static async Task<Response> ReadAsync(Stream stream)
    {
        // Same as request class from server
    }

    /// <summary>
    /// This task does not complete until we are completely done reading.
    /// </summary>
    private static async Task ReadAllAsync(Stream stream, byte[] buffer, int count)
    {
        // Same as request class from server
    }

    /// <summary>
    /// Read a string from the Reader where the string is encoded
    /// as a length prefix (signed 32-bit integer) followed by
    /// a sequence of characters.
    /// </summary>
    protected static string ReadLengthPrefixedString(BinaryReader reader)
    {
        // Same as request class from server
    }
}
```

Furthermore, we'll update the `ClientDispatcher` class to handle responses from the server.
To do this, we'll add a new method and change the direction to bidirectional.

```C#
_client = NamedPipeUtil.CreateClient(PipeDirection.InOut);

/// <summary>
///     Read a Response from the server.
/// </summary>
public async Task<Response> ReadResponseAsync() => await Response.ReadAsync(_client);
```

We'll also add response handling to the ViewModel, where we'll simply display it as a message.

```C#
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
```

These changes will allow for more efficient organization of the interaction between the client and the server, ensuring a more complete and reliable handling of requests and responses.

#####<a name="2"></a> Implementation for Revit plug-in

<p align="center">
  <img src="https://github.com/Nice3point/InterprocessCommunication/assets/20504884/09e0dee3-d4bd-4858-87eb-6bf6766b8dde" />
</p>

<p align="center">Technology evolves, Revit never changes © Confucius</p>

Currently, Revit is using .NET Framework 4.8.
However, to enhance the plugin user interface, let's consider upgrading to .NET 7.
It is important to note that the backend of the plugin will interact only with the outdated framework of Revit and will act as a server.

Let's create a mechanism of interaction that allows the client to send requests for the deletion of model elements and subsequently receive responses regarding the deletion results.
To implement this functionality, we will use bidirectional data transfer between the server and the client.

The first step in our development process will be to enable the plugin to automatically close upon Revit's closure.
To accomplish this, we have written a method that sends the ID of the current process to the client.
This will help the client to automatically close its process upon the closure of the parent Revit process.

Here is the code for sending the ID of the current process to the client:

```C#
private static void RunClient(string clientName)
{
    var startInfo = new ProcessStartInfo
    {
        FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!.AppendPath(clientName),
        Arguments = Process.GetCurrentProcess().Id.ToString()
    };

    Process.Start(startInfo);
}
```

And here is the code for the client, which facilitates the closure of its process upon the closure of the parent Revit process:

```C#
protected override void OnStartup(StartupEventArgs args)
{
    ParseCommandArguments(args.Args);
}

private void ParseCommandArguments(string[] args)
{
    var ownerPid = args[0];
    var ownerProcess = Process.GetProcessById(int.Parse(ownerPid));
    ownerProcess.EnableRaisingEvents = true;
    ownerProcess.Exited += (_, _) => Shutdown();
}
```

Additionally, we require a method that will handle the deletion of selected model elements:

```C#
public static ICollection<ElementId> DeleteSelectedElements()
{
    var transaction = new Transaction(Document);
    transaction.Start("Delete elements");

    var selectedIds = UiDocument.Selection.GetElementIds();
    var deletedIds = Document.Delete(selectedIds);

    transaction.Commit();
    return deletedIds;
}
```

Let's also update the method `ListenAndDispatchConnectionsCoreAsync()` to handle incoming connections:

```C#
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
        var deletedIds = await Application.AsyncEventHandler.RaiseAsync(_ => RevitApi.DeleteSelectedElements());
        await WriteResponseAsync(new DeletionCompletedResponse(deletedIds.Count));
    }
    catch (Exception exception)
    {
        await WriteResponseAsync(new RejectedResponse(exception.Message));
    }
}
```

And finally, the updated ViewModel code:

```C#
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
```

####<a name="2"></a> Installing .NET Runtime during plugin installation

Not every user may have the latest version of .NET Runtime installed on their local machine, so we need to make some changes to the plugin installer.

If you are using the [Nice3point.RevitTemplates](https://github.com/Nice3point/RevitTemplates), making these adjustments will be effortless.
The templates use the WixSharp library, which enables the creation of `.msi` files directly in C#.

To add custom actions and install .NET Runtime, we will create a `CustomAction`:

```C#
public static class RuntimeActions
{
    /// <summary>
    ///     Add-in client .NET version
    /// </summary>
    private const string DotnetRuntimeVersion = "7";

    /// <summary>
    ///     Direct download link
    /// </summary>
    private const string DotnetRuntimeUrl = $"https://aka.ms/dotnet/{DotnetRuntimeVersion}.0/windowsdesktop-runtime-win-x64.exe";

    /// <summary>
    ///     Installing the .NET runtime after installing software
    /// </summary>
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
                0 => ActionResult.Success,
                1602 => ActionResult.UserExit,
                1618 => ActionResult.Success,
                _ => ActionResult.Failure
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
                .Where(line => line.Contains("Microsoft.WindowsDesktop.App"))
                .Any(line => line.Contains($"{DotnetRuntimeVersion}."));
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
```

This code checks whether the required version of .NET is installed on the local machine, and if not, it downloads and installs it.
The installation process updates the `Status` of the current progress of downloading and unpacking the Runtime.

Finally, we need to connect the `CustomAction` to the WixSharp project. To do this, we initialize the `Actions` property:

```C#
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
```

####<a name="2"></a> Conclusion

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





<pre class="prettyprint">
</pre>



<center>
<img src="img/.png" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic">Campus rendering</p>
</center>


####<a name="3"></a>

####<a name="4"></a>



