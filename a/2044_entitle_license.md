<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
-->

<!-- https://prismjs.com -->
<link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>
<style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style>
</head>

<!---

- Licencing Plugins - What Revit subscriptions have to be considered?
  https://forums.autodesk.com/t5/revit-api-forum/licencing-plugins-what-revit-subscriptions-have-to-be-considered/m-p/12766353#M78805

- Entitlement Revit API - My update
  https://forums.autodesk.com/t5/revit-api-forum/entitlement-revit-api-my-update/td-p/12761235

twitter:

Revit API discussions on @AutodeskRevit #RevitAPI add-in licensing and multi-version use of the AppStore Entitlement API #BIM @DynamoBIM https://autode.sk/entitlement

Two illuminating posts from the Revit API discussion forum on licensing and the entitlement API
&ndash; Multi-version Revit entitlement API
&ndash; Add-in licensing...

linkedin:

Revit API discussions on #RevitAPI add-in licensing and multi-version use of the AppStore Entitlement API:

https://autode.sk/entitlement

- Multi-version Revit entitlement API
- Add-in licensing...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Entitlement API for Revit, Licensing for Add-Ins

Today we highlight two illuminating posts from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) on
licensing and the entitlement API:

- [Multi-version Revit entitlement API](#2)
- [Add-in licensing](#3)

####<a name="2"></a> Multi-Version Revit Entitlement API

Julian [W7k](https://w7k.pl/) Wandzilak shared
his [Entitlement Revit API update](https://forums.autodesk.com/t5/revit-api-forum/entitlement-revit-api-my-update/td-p/12761235):

> I am recently updating all my paid apps to 2025.
Thanks to this forum, everything went rather smoothly.
Except I had some weird problems with my `CheckOnline` method based the AEC DevBlog article on
the [Entitlement API for Revit Exchange Store Apps](https://adndevblog.typepad.com/aec/2015/04/entitlement-api-for-revit-exchange-store-apps.html).

> For some reason in 2025 I had some problems with RestSharp; while checking the licence in the `OnApplicationInitialized` method, the process was abruptly stopped, weirdly enough, without even throwing errors.

> Update helped, but as RestSharp changed, I started having problems with older Revit versions.
I decided to rewrite it completely, and this time use the default `HttpClient`:

<pre><code class="language-cs">public static bool CheckOnline( string appId, string userId )
{
  Uri uri = new Uri( $"https://apps.exchange.autodesk.com/webservices/checkentitlement/?userid={userId}&appid={appId}" );

  bool isValid = false;

  try
  {
    HttpClient myHttpClient = new HttpClient();

    Task&lt;HttpResponseMessage&gt; task
      = Task.Run(() =&gt; myHttpClient.GetAsync(uri));

    task.Wait();
    HttpResponseMessage response = task.Result;

    Task&lt;string&gt; readTask = response.Content.ReadAsStringAsync();
    string text = readTask.Result;

    EntitlementResponse entitlementResponse
      = JsonConvert.DeserializeObject&lt;EntitlementResponse&gt;(text);

    isValid = entitlementResponse.IsValid;
  }
  catch
  {
    return false;
  }
  return isValid;
}

[Serializable]
public class EntitlementResponse
{
  public string UserId { get; set; }
  public string AppId { get; set; }
  public bool IsValid { get; set; }
  public string Message { get; set; }
}
</code></pre>

Here is the code.
I hope it might be useful to someone.
Works for me in all versions from Revit 2020 through Revit 2025.
Many thanks to Julian for sharing this.

####<a name="3"></a> Add-In Licensing

A wealth of ideas and information about how to add licensing to your own add-in is shared by Julian, Luiz Henrique [@ricaun](https://ricaun.com/) Cassettari, Barry Newcombe and others in the discussion
on [licencing plugins &ndash; what Revit subscriptions have to be considered?](https://forums.autodesk.com/t5/revit-api-forum/licencing-plugins-what-revit-subscriptions-have-to-be-considered/m-p/12766353)

**Question:**
I have built a server-based licensing mechanism for my plugins.
It depends on the Autodesk ID (human unrecognizable) and the username.
So, this will only work if a user is logged in with their Autodesk account.
I´m used to that because I worked with the cloud services for a few years now.
But I also remember working in another company where logging in was not necessary and user names could be set manually.
And so I would like to know:

- What different subscriptions do i have to consider?
- What options do i have to bind users to their licence?

For users that are not logged in with their Autodesk account I only see the option to bind the licence to the machine (cpu / mainboard) and not to the user himself.

**Answer:**
I think you must be logged in to your Autodesk Account to use Revit.
The last version working without it was 2019 (The last perpetual license).

Personally, I am using Autodesk AppStore and the entitlement tools provided by Autodesk.
It works well &ndash; except:
To me it looks that Autodesk breaks EU laws about online marketplaces...
There is no way of adding floating licenses for services with subscription fee.
So, you can’t give someone 10 licenses and let them manage them.
Also, if a user gets a license on PayPal, no one can do anything with it &ndash;
Right now when my users change their employer, I simply add them a new license to their new email.

**Response:**
Uh, an amazing amount of tools you built there on [W7k](https://w7k.pl/)!
And it´s a nice homepage with descriptions for all the tools.
How long have you been working on that?

Can you explain a little how it works when licensing with the appstore?
Do you have to setup anything in the code to make it work? I assume the payment management will all be done by autodesk? I understand the problem with floating licenses and isn´t it also the case that monthly payment is not possible?

I more and more like the idea of using the Autodesk ID for my purposes. The problem is in some companies the users don´t have Autodesk accounts, the Autodesk Licence works differently there. But I think that in general most Revit users have an Autodesk account and if not they should create one.
I can check the active licenses for my plugins only once a month or even once a year, so the users are not forced to log in every day. I think this solution would be user-friendly enough.

The only way to bypass my licensing system would be to just create a new Autodesk account every month to start a new 30 day trial. I could additionally create a machine identifier from the cpu/mainboard ids to check if someone is using 30 day trials repeatedly...

I can also implement floating licenses. Only disadvantage with my self-built solution is that I also have to manage payment manually myself.

**Answer:**
The response is long, so I decided to split it by topics:

My tools:
Thanks. It is my way of making the AEC industry slightly less stupid. Or at least less confident about wasting peoples time
It is hard to say. It started as my side project (plus a way of learning c#) and then I decided to make a product out of it. I started Drafter a year ago but was not working constantly on it. I am working on my plugins when I don’t have anything to do in my freelancing schedule or I need something to work faster &ndash; so it’s hard to say how much time it took. Plus, right now, I am in the process of publishing the third one
Probably making documentation and all the icons was the worst So I am super happy that you like it!

- Autodesk AppStore Code in the AEC DevBlog article on
the [Entitlement API for Revit Exchange Store Apps](https://adndevblog.typepad.com/aec/2015/04/entitlement-api-for-revit-exchange-store-apps.html)

In a nutshell, it is connecting with Autodesk App Store and gives you back information about a licence from there. On top of that, I decided that my tools create a local licence file (Documents folder &ndash; not a tool folder as it messes up uninstaller). So, each time you run the tool, it can check the licence without having to connect to the server. I would share the code, but first it needs some refactoring.

Named users:
I believe that nowadays you can’t use Revit without named users, but I might be wrong about it.
Looking for an Autodesk licence? We’re retiring licences based on serial numbers and assigning each subscription to a named user. These new plans provide a range of capabilities for organizations of every size.
Maintenance plans will retire on May 7, 2021 and multi-user subscriptions will retire on August 7, 2022. If you are still on a maintenance plan or multi-user subscription, choose a trade-in offer.
From [Autodesk licensing options](https://www.autodesk.com/ca-en/licensing/overview).

Creating New Autodesk Account:
I was thinking about it too. In the end I decided to make my tools as good as possible for someone who bought them &ndash; I can imagine situation when someone is leaving the office and new person must use their old computer.
It is dot net, dll and it heavily uses API, so there are the worst things people can do to them pretty easily . Frankly speaking, I am making some videos this weekend where I will show how to code some of my scripts.
That’s why I added 30-day trial &ndash; it gives me time to assign proper licences to the people working in the offices which bought the tools from me. I see some free riders who use them without buying them on some weird emails ($14 seriously ) but previously they were wasting my time asking for free licences to be able to show the tool in their offices &ndash; coincidentally they always want only a one licence just for them.

Autodesk App Store Payment:
Sadly, this is where the things become problematic. (it is not a legal advice but as I see it) Autodesk App Store allows us to make payments via PayPal, but it is a very old way of making payments. So, let’s start with some tax information:
You are in Europe, so there are two ways of adding VAT:
To companies &ndash; inverted 0% &ndash; you need their VAT ID, and they need to pay VAT in their own country later &ndash; great as it is neutral for them,
To customers &ndash; according to your local regulations (23% in Poland) up to some amount and above there is EU system to pay it in each separate country according to its regulations.
Similar situation is if you sell outside of EU, but it becomes more problematic as you need to check the situation in each country.
So, why I said that Autodesk probably breaks the laws here? As an online market they should collect all this information for you and help with adding a correct VAT & Sales tax &ndash; In some countries they can be even the ones who should pay it in your name.
Sadly, right now, the only thing I get after transaction is PayPal account name + email address of PayPal account + Revit email address. So, I am sending an email asking about other information which is a stupid waste of time.

I already raised some of the issues with the Autodesk AppStore Team in January and got some promising info:

> Thank you for reaching out to us.
Since the Drafter app is subscription based, and for the subscription based apps Users will have to log in to store with their respective account and subscribe to get entitled to use the app, the autorenewal happen for the same account for every renewal period along with the entitlement.
Thus, multi-purchase option for the subscription-based apps is not available.
Yes, you can assign (entitle) each account from the backend itself.
Thank you for your help and explanations! It really clarifies it for me so I can tell client what to do!
It would be great to have an option to allow buyers to manage their subscriptions &ndash; Is there a place I could raise the idea of this improvement?
Thank you for the acknowledging.
And yes, we agree with you to have the option of multi-purchase of subscription based app.
Our engineering team is aware of this, but as the entitlement API is directly dependent with the PayPal subscription, thus it was good to keep it so.
However, this improvement request will be considered and possibly be implemented in the near future or next store update, as we have received lot of such requests.

**Response:**
Thank you for sharing this insight into your work.
I think I will then use the Autodesk Appstore just for publishing but not for licencing.
I´ll also share some code snippets here and for sure will report about my experiences as soon my system is ready.

**Answer 2:**
The last Autodesk DevCon included some conversations about improving the Autodesk App Store, and the [APS roadmap](https://aps.autodesk.com/aps-roadmap) includes items about the App Store.

About the licensing, I created my entitlement API using the Autodesk UserId to identify the user connected inside Revit; without an account connected, the plugin does not work.

I have some plugins in the App Store with Trial or Free, and the payment is done outside the Autodesk App Store on my website using a platform that manages taxes depending on the region where the user is buying the license.

Is kinda simple to recreate a simple API using the Entitlement API as a reference to validate your plugin/user.

<pre><code class="language-cs">public class EntitlementResponse
{
  public string UserId { get; set; }
  public string AppId { get; set; }
  public bool IsValid { get; set; }
  public string Message { get; set; }
}
</code></pre>

Here is a simple sample command and
the [AppEntitlement class to work with Autodesk AppStore: Entitlement API](https://gist.github.com/ricaun/e86e63c42bd1a5add1f8d08d6fa84aff).

**Response:**
So, from you experience you would say users are comfortable with the Autodesk ID check on every Revit startup? Because then I would just do the same and skip implementing an additional mechanism with a local file and just tell the users being logged in is necessary.
If someone has the patience and would like to explain, I still don´t get how the appstore works.
How is it possible that you use the appstore licencing mechanism with an external payment service?
How does the appstore know if a user has paid and has an active license?

**Answer:**
Yes, by default you need to have an account connected to use Revit.
The plugin checks if the user is connected, and, if that is the case, checks online to validate the license and create a temp file to enable work offline for some time.

I don't use the default Autodesk App Store licensing mechanism, because is not flexible.
I kinda don't need to have the plugins in App Store, all the payments and licenses are managed by my API system.

**Response:**
Thank you for the explanation!
I now found out how the Autodesk licensing is called that even made me start this thread: Flexnet license.
I have worked in a company with 100 Revit users where logging in to an Autodesk account was not necessary.
But if you had no connection to the company server you got this message:

<center>
<img src="img/flexnet_license_1.png" alt="Flexnet license" title="Flexnet license" width="300"/> <!-- Pixel Height: 430 Pixel Width: 496 -->
</center>

**Answer:**
I wonder what Revit version of the flexnet license works without a user connected, I suppose the newer Revit version you need to have an account.
Without an Autodesk account connected most of the plugins in the Autodesk App Store just not gonna work.

 **Answer 2:**
I was thinking about making my own page with selling options, but right now being seen on the market is probably the biggest source of costumers for me. Especially after recently Drafter got marked as one of the most popular paid apps for Revit. It also helps customers trust me as they are downloading the product from Autodesk App Store. So I see plenty of benefits of being here
Still, I will probably end up with a cheaper version on my webpage which will collects correct info as per your page and more expensive on an Autodesk App Store. So far I prefer to spend my free time coding something cool 😉

How the Autodesk app store works (if you use entitlement class provided by Autodesk and their system)

- You submit the app &ndash; after revision by Autodesk, the app is being published on the market.
- Someone wants to buy your app:
    - On PayPal &ndash; the process is automated with problems mentioned above. Customers are redirected to PayPal, pay there, and then the system brings them back to the page informing the Autodesk App Store about the successful transaction.
    - You have a possibility to simply add new users:
    this is how it looks for my Drafter. Autodesk doesn't care why you are adding these and what are you doing with them &ndash; You can add or delete them however you like (they are marked below as Published Added):

<center>
<img src="img/autodesk_appstore.jpg" alt="Autodesk AppStore" title="Autodesk AppStore" width="600"/> <!-- Pixel Height: 579 Pixel Width: 1,000 -->
</center>

So far I hit 2 problems with it.

- People need to log into the market.
- They need to agree to the market rules &ndash; to do it they need to download something, really.
It is another reason why I made trial for Drafter &ndash; before I had to tell people to download randomly something free.
- Only after that, you will be able to add them a licence.
- After that, they will be able to download your app and install it.
- While starting the app, the correctly implemented entitlement class will try to connect with market server with `userId` and `AppId`. In return, you will get an error, true or false &ndash; it is up to you what you want to do with it or how you're going to manage problems. Personally, after checking, I am creating a hashed licence file with result.  Depending on result, I am freezing and blocking the possibility of using my tools. Also, each command will double-check the licence from my files. That is more or less what's going on there.
- Users can normally use the app. If they log out, they will be logged out by Autodesk and Revit will stop working.
- Each time they start Revit, the process will repeat from point 5. You can implement some code to stay offline, so you don't have to check online every time, as it makes Revit start slowly.

**Answer 3:**
In your case, it does not make sense to add an option to your website, and yes adding the plugin to the App Store is a great way to get exposure, I have some plugins in there as well.

That image with an option to Add Download Entitlement is new, never saw any tutorial with that image about the Entitlement API.

If you use PayPal as a payment and use Entitlement API with the @Julian.Wandzilak examination, this is the way.

**Answer from AppStore:**
Thank you for sharing your concerns. Yes, we are aware of the issues and concerns over Taxation. Our App Store Engineering team is working on these things and will share the details once available.
Could you please try to implement IPN mechanism for your apps to get more information on the users? Please go through following links:

- [Autodesk App Store: IPN notification format](https://damassets.autodesk.net/content/dam/autodesk/www/adn/pdf/instant-payment-notification-ipn-format.pdf)
- [GitHub source code for Exchange-IPNListener-Sample](https://github.com/ADN-DevTech/Exchange-IPNListener-Sample)

Please feel free to contact us via [appsubmissions@autodesk.com](appsubmissions@autodesk.com) for any queries related to App Store.

**Answer:**
I can confirm that larger organisations use flexnet licencing always. My discipline is structural engineering and I have worked for a lot of global engineering firms such as URS, AECOM, Jacobs, Arup (1000s of Revit users) and smaller national consultancies (more than 10 users, less than 100) and all have used flexnet licencing. I am developing my own plugins at the moment and see the single sign on licence as a constraint on sales to these bigger organisations who use flexnet. Plus I have worked in secure industries where security and internet access can be limited, again if you need to check your licence every time you log in this would cause issues. Another concern I have with trying to sell to the bigger organisations is that many of them cant purchase software for such a small value as £10 , I found out trying to purchase software from the app store through a global organisation two things:

- They often have a minimum purchase order value, i.e., they cant write a cheque or make a payment for less than £200
and so a local manager was going to purchase the app on his credit card and claim the money back.
- When the IT dept looked at the App and it didnt have a valid signed security certificate (you know the message that pops up about your app and says 'the publisher of this add in could not be verified') they would not sanction its use on company systems and we could not purchase the app.

From this I have come to the following strategy:

- Sell a single sign-on version of the app for a reasonable price on the app store, say £40, licence controlled by the app store REST call.
- Sell a multi-seat version of the app, say 8 seats for £250 on the app store, for big organisations, This app has no security or licence control and licence control is based on the 'trust system' of these large organisations who often employ dedicated software to manage their licences deployed.

I went through the process of getting a certiport digital security certificate to sign my app DLL with so as to be able to sell to larger organisations. Once signed, the message only pops up once at the start, if the plug in is signed.

These organisations do use certain plug in such as rushforth tools, who sell an enterprise version on their own website, so a licencing system for flexnet can be implemented; there are pre-purchased licencing software solutions I have seen which can be purchased off the shelf to do this like QLM (quick licence manager) by soraco, but obviously have a cost associated with them.

**Response:**
Autodesk is planning to add some [Multi-user license](https://portal.productboard.com/autodeskforge/20-autodesk-platform-services-roadmap/c/202-multi-user-license-management).
The question is if using flexnet licencing in a modern Revit is possible to run without a user connected. I don't think that is possible.
Today I only have a single user license in my plugins, I can swap to another user manually. A floating license is a thing that I am planning to create, would be really handy to enable a floating license to all the users with the same domain email.

**Answer:**
Here are snapshots of the flexnet licence; notice there is no user signed in to Autodesk and Revit fully runs, the bottom right photograph is when you have not connected to the server that is hosting the flexnet licence.

<center>
<img src="img/flexnet_license_2.jpg" alt="Flexnet license" title="Flexnet license" width="496"/> <!-- Pixel Height: 430 Pixel Width: 496 -->
</center>

I think the idea of using the domain is the right way to go I think Rushforth tools use this approach. The below text is from the licencing section of Rushforth website, see enterprise licences at the bottom.

Summary:

- For individual licenses, install and click the activation button to complete an individual activation request. Check your spam folder or send a follow up email if your registration key is not sent within 24 hours of sending your activation request email.
- For Site licenses, all users can input the same key or that process can be automated by deploying a Windows registry key.

Installation:

- The .exe installer is a per-user installer and does not require any admin install privileges. Don't "Run as Admin" or it may not appear on the current user's profile.
- The .msi installer is a per-machine installer and will require admin install privileges.
- Both installers have a -quiet switch for silent installation by deployment software.

Single-User Licenses:

- Purchase
    - Purchase the quantity of licenses needed for your team.
    - All licenses purchased are attributed to the company email domain name (unless using a personal email).
    - Each single user license authorizes you to register two computers for the same user. A unique registration code must be requested from each computer. A separate license should be purchased for each user that will use the tools.
- Notify Users
    - You can immediately notify users that they can install and activate the software.
    - Each user license request will be counted toward the total number purchased for the company on a first come, first served basis. If you would like to submit a list of authorized users via email to control who can request activations, that can be accommodated as well.
- Click Activate
    - Your user(s) can immediately install the tools and click the Licensing/Activation button on the RF Tools ribbon in Revit to generate and send in an individual activation request code via email to get back a machine-specific registration key. As long as the user email address domain matches the purchasing company email address domain, the activation will be attributed to the purchase.

Enterprise Site License:

- Install the tools on an unlimited number of computers on a network. A single network license key code can be used to activate any number of computers in any number of offices around the world that share a common network domain name. This license code can be deployed as a windows registry key to all users as well. Site license also includes authorization to request individual activation codes for up to 20 non-network connected computers such as personal laptops or home computers. Previously purchased single-user licenses can be applied toward the purchase of an Enterprise Site License of the same version.
- To receive the Network License Key: Have one of your users click the 'Licensing/Activation' button on the RF Tools tab in Revit (after they have installed the tools). Click the Enterprise Site License button to request your Network License Key. This will prepare an email with the name of the domain to be registered. Once verified, you will receive a reply with a license key that will work for all computers connected to the same domain name.

Many thanks to all of you for sharing this.
