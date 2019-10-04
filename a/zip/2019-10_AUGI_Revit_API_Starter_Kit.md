# Revit API Starter Kit

by David Raynor, Application Specialist (BIM Manager) for Revit MEPS at Dewberry

We all have some tedious, mundane tasks we would like to automate. Perhaps it is the dream button you have had on the wish list for years. Or it’s a company standard that just isn’t user friendly. Either way, you don’t want to spend your precious time on tasks you can do while mentally checked out. As an employer, you don’t want your employees unengaged in what they are doing when they could be performing valuable work. You also don’t want them to feel unvalued, being asked to spend their time on menial tasks. Automation provides a win-win for the team. But where to start?

This article is intended to be a starter kit for programming. You’ll find lots of links throughout to help you along your way. I know this may be coming to you in a printed format, I like it printed myself, but for what we are trying to do here, I’d recommend looking up this article on AUGI’s website so the hyperlinks will work. I’m not going to pretend to teach you everything in one article. Rather, I want to give you the tools that will help you along the way.

First let’s discuss the various flavors of programming with Revit®. For many tasks you don’t need a full blown programmed add-in. Dynamo is all the rage these days, but it is not without its own drawbacks. There are also macros, with pros and cons, and then there are full blown add-ins that take a bit more to get going, but with many advantages the others lack.

## DYNAMO

Dynamo (Figure 1) is one way to get the power of programming without having to code at all. It is called visual programming. It now comes free from Autodesk and is installed with Revit. It comes with a large supply of commands, known as nodes, that can get you going pretty well. Soon after you start playing with Dynamo you’ll discover other people are generously donating their efforts in creating packages of new nodes. You’ll get hooked on these nodes and build some truly awesome graphs (collections of nodes and connecting wires to do a task).

Then just as different versions of Revit get released, different corresponding versions of the API get released, and so do different versions of these nodes. Keeping everyone in your organization current on the nodes and the graphs created for them can be challenging. I will often use Dynamo for tasks unique to a project—things I don’t expect to need again on future projects. That’s not at all to say you can’t save your work and reuse it later. I do that all the time. If a graph is robust enough, you can send it to users and they can run it in Dynamo Player. It is a user interface that hides the nodes and wires of the graph by giving users a Run button. It even allows for user input for specific variables.

One awesome resource in the Dynamo realm is John Pierson, aka sixtysecondrevit. He offers tremendous support to the Dynamo community, maintains the Rhythm package of nodes, and a blog at: https://www.sixtysecondrevit.com/.

There are a lot of free tutorials on Dynamo. If you are looking for a video training package, BIM After Dark by the The Revit Kid offers a Dynamo class titled DIY Dynamo. It costs around $250.

- [diydynamo.com](http://diydynamo.com/)

Figure 1

## MACROS

Macros are a powerful tool, but they have some hefty drawbacks, too. Macros can reside either in the active project file or within the application—you have to pick one. Macros saved within the project file can be used by any user who opens that file. Macros saved in the application are saved to the user’s Revit configuration. These macros can be used on any model file, but only by the user who created the macro, which makes sharing them among many users cumbersome. All in one project is easy, and that project can be the company template—then everyone gets all the macros. However, they also get a pop-up every time they open Revit asking if they want to enable macros. This gets old fast. There is a setting under Revit options that will allow you to always enable macros, but you might want to run that by your firm’s security team first.

I will often use application macros for repeatable tasks I perform, but not necessarily ones I expect others to need to use. For example, I maintain the project templates. If I need to make a change across all of them, I have a macro I edit each time to automate that change across all the various view templates. I rarely ever use the project- based macros due to the annoying pop-up everyone gets. But if I were to use it, it would be to solve a project-specific problem and I would be comfortable with its use by others on the project.

Macros are a good gateway into learning to program before diving into a class on programming and installing specialized software to learn. Harry Mattson at Boost Your BIM blog has a great online course. You’ll pay about $150 for the Udemy class, but it does a great job of explaining the commands and how to use them from the macro environment. This environment is a lightweight programming editor and compiler known as Sharp Develop (Figure 2). It is very similar to Visual Studio, which we will discuss later.

Another great resource is Michael Kilkelly at ArchSmarter. He has a great seven-step starter for writing macros.

- [archsmarter.com/first-revit-macro](https://archsmarter.com/first-revit-macro/)
- [www.udemy.com](https://www.udemy.com/)
- [boostyourbim.wordpress.com](https://boostyourbim.wordpress.com/)

## ADD-INS

Add-ins are a bit more work upfront. They are just as powerful as Dynamo and Macros, but more rigid. Users are not able to open up the hood and edit what is happening. Add-ins offer the ability to add your own buttons to the ribbon, though this is not required. Because of the environment they are created in, they have better tools for debugging and figuring out why things are going awry. They are also much easier to share and maintain across a large company.

### Minimum Requirements

There are just two elements required for Revit to acknowledge and implement an add-in. First, it needs to read a “.add-in” manifest file. This file must be stored in one of two places.

- C:\ProgramData\Autodesk\Revit\Addins\2019

or

- C:\Users\userName\AppData\Roaming\Autodesk\Revit\Addins\2019

This file tells Revit that an add-in exists and where to find the second requirement, a “.dll” file. This .dll file is the output of compiling your code in Visual Studio. When you run your code via Visual Studio, it is compiling it and creating this .dll file in the“bin” folder of your project. Compiling is the process of converting the written code from C# into machine language the computer understands.

### Programming

If someone remembers their programming classes from college, that’s great, but not required. There are free resources available to learn programming. Here you’ll need to decide which language to learn. My recommendation would first be C sharp, aka C#. The reason is all of the supporting documentation, including sample code, is provided in C#. That makes using copy and paste much easier if it’s the same language you understand. That’s not the only one though. Visual Basic and Python are excellent choices, but may require more translation, which could be troublesome for a beginner.

You don’t need to enroll in the local community college, though that would be a great start. On the frugal side of things, Microsoft offers a free online Fundamentals for Beginners C# Programming course. All it requires is a free Microsoft account.

This course gave me a good understanding of the functions and grammar of C#. It also talked me through downloading and installing Microsoft’s Integrated Development Environment (IDE), called Visual Studio, called Visual Studio (Figure 3).

Visual Studio comes in various flavors ranging from free to several hundred dollars. I recommend the use of the Visual Studio Community Edition, free for individuals for both free or paid apps. If your company has more than 250 PCs or generates over $1 million in revenue, then you’ll need to be honest and pay the $500 for the Professional version or the Enterprise Edition.

- [docs.microsoft.com/en-us/visualstudio/get-started/csharp/?view=vs-2019](https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/?view=vs-2019)
- [visualstudio.microsoft.com/vs/community](https://visualstudio.microsoft.com/vs/community/)

If you are creating your own add-in using Visual Studio I’d highly advise you to download and install the Revit Add-in Project template for Visual Studio. Just like Revit has project templates for new projects, Visual Studio has templates for programming new projects.

This template is stored on Github. Let me explain what Github is. When multiple programmers are working on a single project, they will use Version Control, which is a way to track what changes have been made to the files in a project, and by who, as well as being a convenient centralized storage location. It is really very similar to a Revit workshare-enabled project. There is a master repository you can think of as a central file. Individuals download a local copy, called a branch, which is a copy of the master. They will edit the code in their branch, adding or editing features until they have a fully functional tool. Then they will merge their branch back to the master, or sync their local copy with the central file. This way the master is always a functioning version of the software, with the latest features being added to it in a functional state.

Github is one of many similar services that use a form of version control called Git, thus the name Github. Many users will make their work public on these sites, giving you access to their master branches. This is called an open source project. You can choose to contribute to these projects with your own branches, or download versions for yourself. Be sure to read and honor the terms of use.

- [github.com/jeremytammik/VisualStudioRevitAddinWizard](https://github.com/jeremytammik/VisualStudioRevitAddinWizard)

While you are hanging out on Github, be sure to download Revit Lookup, a must have add-in for peeking under the hood at the database that is driving Revit. It allows you to select an item in Revit and explore the properties of that element, hidden from the users’ view, but exposed in the API. You can view items such as the x,y,z coordinate, or to find out if a parameter is an integer value or a string acting like an integer. If you want to see what the compiler sees, this is the tool you need.

- [github.com/jeremytammik/RevitLookup](https://github.com/jeremytammik/RevitLookup)

There is a huge programming community outside of Revit API work and many are very active on a blog called Stack Overflow. You can find answers to all kinds of programming questions there.

- [stackoverflow.com](https://stackoverflow.com/)

That covers the basic tools you’ll need in your programming toolkit. Now we get started focusing on the tools for working with Revit. You’ll need to download the Revit Software Developers Kit, aka the Revit SDK. This kit contains many shiny gems, including the documentation for the Automated Programmable Interface, or API. Don’t be intimidated by all the acronyms. Just as the buttons and icons on the screen are the Graphical User Interface or GUI, the computer can send the same commands to Revit without going through the GUI. For almost any command you have access to with the GUI, there is a counterpart for the API. The Documentation just tells you how to use it and what information it’s expecting.

Another great resource for the documentation is the website Revit API Docs. One advantage to this website is it searches all versions of the API, whereas the documentation only covers one specific version. Just as a new version of Revit is released every year, so a version of the API is also released. Some years they change how the tools in the API work to be more concise, optimize performance, or enable new features. This means one year’s command may not work, or at least not the same way as in previous years. The older commands will have been depreciated over a year, allowing you to transition to the new command, and then finally unsupported the next year. Just like Microsoft offers free training on learning a programming language, Autodesk offers a few free training examples, also accessed via the Revit SDK website. Then there is the community that supports users just like you, learning to use and write code. Jeremy Tammik, aka The Building Coder, maintains an incredible blog with tons of sample code.

- [thebuildingcoder.typepad.com](https://thebuildingcoder.typepad.com/)
- [www.revitapidocs.com](https://www.revitapidocs.com/)

## PROGRAMMING SPECIAL TOPICS

### Visual Studio Settings

This topic alone could drive you mad. Visual Studio supports and works with countless programs and not all have the same requirements. Therefore, there are some specific settings you have to get right for everything to work.

- [forums.autodesk.com/t5/revit-api-forum/advice-on-debugging-c-in-visual-studio/td-p/6496811](https://forums.autodesk.com/t5/revit-api-forum/advice-on-debugging-c-in-visual-studio/td-p/6496811)

### Breakpoints

As you start your program it is invaluable to peek at what is happening and step through the program, watching how variables are read and manipulated. Setting specific points at which to stop is done using a Breakpoint.

- [docs.microsoft.com/en-us/visualstudio/debugger/using-breakpoints?view=vs-2019](https://docs.microsoft.com/en-us/visualstudio/debugger/using-breakpoints?view=vs-2019)

### Transactions

Through the API you can query and gather all the information you need, display it, or export it any way you like. If you plan to make a change to the Revit file in any way it must be done within a transaction block. A block is just code contained within a set of brackets. Transaction blocks not only enable the document to be modified, but also show up as an undo-able action from the user interface. Transactions can be grouped together while others can happen in quick succession. Sometimes it’s necessary to perform a transaction to determine some piece of information, save that information to a variable in your code, and then undo the transaction. Whatever the task may be, you’ll need to be familiar with transactions.

- [knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/Revit-API/files/GUID-BECA30DB-23B4-4E71-BE24-DC4DD176E52D-htm.html](https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/Revit-API/files/GUID-BECA30DB-23B4-4E71-BE24-DC4DD176E52D-htm.html)

### Try/Catch Blocks

Think of this as a safety net. You are trying to do something, but if it fails, catch it, and tell me why it failed. This is just good practice and will save you time troubleshooting. Also, if one part of a program fails, it can swallow the error and proceed, allowing you to skip over some problematic situations.

- [docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-catch](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-catch)

### Multi-Version Support

Every version of Revit has a corresponding API.dll file that must be included in the Visual Studio References. Also, every tool you create will compile to create a “.dll” file. To keep from going mad with all the different versions, you’ll want to follow Konrad Sobon’s guide on setting up Multi-Version Targeting. This will likely be a topic to investigate after you have developed your first add-in. With next year’s release of Revit, you realize you may need to copy all that work into a new Visual Studio project for the next year’s Revit version. This technique allows you to keep all your source code in one project, and build to multiple versions.

- [archi-lab.net/how-to-maintain-revit-plug-ins-for-multiple-versions](https://archi-lab.net/how-to-maintain-revit-plug-ins-for-multiple-versions/)

### Keep With it

These are the tools and resources I use regularly. When I started on this journey I had never written a program. It had been years since I had any programming training in college and that knowledge was long gone. I was determined to automate a task that wasted hours of time per week and had no end in sight. I was very motivated, to say the least.

I didn’t succeed initially. The first successful run was months after I started. I also recruited a friend to help me over a few humps. Don’t be afraid to ask for help when you hit a roadblock. It was worth it many times over. Not just the value of the tool I had created, but the skill I had developed. I have found programming to be one of the most addictive activities I have ever started. Maybe I need to get out more! Then again, I get to use my words to write programs that create models that then get built in real life. Simply put, I use my words to build buildings. You can’t get much closer to magic than that.

### Author

David Raynor, Application Specialist (BIM Manager) for Revit MEPS at Dewberry.

*Currently, David Raynor works at Dewberry as an Application Specialist (BIM Manager) for Revit MEPS.
He develops content for all trades and fre- quently travels to various offices teach-ing staff the Dewberry way of working in Revit.
To help with consistency and efficiency, he maintains and supports all Revit API coding efforts.
When not working, he can be found wandering the mountains, camping, kayaking, or skiing.
He lives just south of Raleigh, North Carolina, with his family of three amazing kids and a supportive, understanding, wonderful wife.*

### Attribution

Text copied from
the [AUGIWorld October 2019 issue](https://issuu.com/augi/docs/aw201910hr),
page 20: [Revit MEP &ndash; Revit API Starter Kit](https://issuu.com/augi/docs/aw201910hr/20),
(C) 2019 by [AUGI](https://www.augi.com), the Autodesk User Group International.
