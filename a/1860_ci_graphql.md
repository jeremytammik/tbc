<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

New RevitLookup CI system using GitLab YAML and a GraphQL interface for Revit and the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://bit.ly/revitlookupci

Let's start the week with a look at the new RevitLookup CI system and a GraphQL interface for Revit
&ndash; RevitLookup CI on GitLab
&ndash; GitLab CI YAML
&ndash; GraphQL for Revit
&ndash; The Tamm Tree...

linkedin:

New RevitLookup CI system using GitLab YAML and a GraphQL interface for Revit and the #RevitAPI

https://bit.ly/revitlookupci

Let's start the week with a look at the new RevitLookup CI system and a GraphQL interface for Revit:

- RevitLookup CI on GitLab
- GitLab CI YAML
- GraphQL for Revit
- The Tamm Tree...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### RevitLookup Continuous Integration and GraphQL

Let's start the week with a look at the new RevitLookup CI system and a GraphQL interface for Revit:

- [RevitLookup CI on GitLab](#2)
- [GitLab CI YAML](#3)
- [GraphQL for Revit](#4)
- [The Tamm Tree](#5)

Before that, here is another version of
the [Pareto 80-20 principle](https://en.wikipedia.org/wiki/Pareto_principle),
especially targeted at coding:

<blockquote>
<p><i>The first 90% of the code accounts for the first 90% of the development time.
The remaining 10% of the code accounts for the other 90% of the development time.</i></p>
<p style="text-align: right; font-style: italic">&ndash; Cargill's Rule</p>
</blockquote>

#### <a name="2"></a>RevitLookup CI on GitLab

Three years back, Peter Hirn of [Build Informed GmbH](https://www.buildinformed.com) very kindly set up a
public [CI system](https://en.wikipedia.org/wiki/Continuous_integration) for RevitLookup automatically
creating [RevitLookup builds](https://thebuildingcoder.typepad.com/blog/2017/04/forgefader-ui-lookup-builds-purge-and-room-instances.html#3).

In April this year, however, Matt [@WspDev](https://github.com/WspDev) Taylor
raised [issue #59 &ndash; continuous integration no longer working](https://github.com/jeremytammik/RevitLookup/issues/59),
caused by Harry Mattison's preceding [pull request #58 &ndash; solution changes for multi-release building](https://github.com/jeremytammik/RevitLookup/pull/58),
detailed in the discussion on [support for multi-release building](https://thebuildingcoder.typepad.com/blog/2020/04/revitlookup-2021-with-multi-release-support.html#4).

After some attempts to address that issue, not much happened until last week.

Now, Peter solved it cleanly and radically by moving the CI pipeline to a different platform
in [pull request #61 &ndash; move CI to `gitlab.com`](https://github.com/jeremytammik/RevitLookup/pull/61):

> I moved the CI to [gitlab.com](https://about.gitlab.com).
The entire pipeline to build and publish RevitLookup is now defined in `.gitlab-ci.yml`.

> You can check out the progress and output of the CI
at [gitlab.com/buildinformed-public/revitlookup/-/pipelines](https://gitlab.com/buildinformed-public/revitlookup/-/pipelines)

> Once you accept this merge request, the build should be triggered after max. 30 (?) minutes.

> As you can see in the build stage, I'm still building against Revit 2019 API and force the framework version to v4.7.2.
If there is a problem, developers can try to fix it by editing this file or contact us
via [email to info@buildinformed.com](mailto:info@buildinformed.com).

The first release built using the new pipeline
is [RevitLookup 2021.0.0.3](https://github.com/jeremytammik/RevitLookup/releases/tag/2021.0.0.3).

Ever so many thanks to Peter for this great solution!

#### <a name="3"></a>GitLab CI YAML

The GitLab blurb states:

> GitLab is a complete DevOps platform

> With GitLab, you get a complete CI/CD toolchain in a single application. One interface. One conversation. One permission model. Thousands of features...

I find it pretty interesting just taking a look into the file defining the CI pipeline, `.gitlab-ci.yml`:

<pre class="code">
stages:
  - download-revit-api
  - build
  - sign
  - publish

download-revit-api:
  stage: download-revit-api
  image: alpine:3.12
  variables:
    REVIT_API_URL: https://bimag.b-cdn.net/revit/2019/RevitAPI.dll
    REVIT_API_CHECKSUM: da1fbac1e5afb210478cbb508e0677679e994da8c9e13b56c43d5e122dabd6e7
    REVIT_APIUI_URL: https://bimag.b-cdn.net/revit/2019/RevitAPIUI.dll
    REVIT_APIUI_CHECKSUM: d29a755922997310a215afa83fa44ea91aafdd6feddce722e5feed98d2ae9f73
  script:
    - apk add --update --no-cache curl
    - mkdir revit-api
    - |
        echo "${REVIT_API_CHECKSUM}  -" > /tmp/checksum \
        && curl -L "${REVIT_API_URL}" \
        | tee revit-api/RevitAPI.dll \
        | sha256sum -c /tmp/checksum
    - |
        echo "${REVIT_APIUI_CHECKSUM}  -" > /tmp/checksum \
        && curl -L "${REVIT_APIUI_URL}" \
        | tee revit-api/RevitAPIUI.dll \
        | sha256sum -c /tmp/checksum
  artifacts:
    paths:
      - revit-api/
    expire_in: 1 day

build:
  stage: build
  tags:
    - shared-windows
    - windows
    - windows-1809
  dependencies:
    - download-revit-api
  variables:
    MSBUILD: 'C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe'
  script:
    - '& "$MSBUILD" -p:TargetFrameworkVersion=v4.7.2 -p:Configuration=2021 -p:ReferencePath="../revit-api" -p:PostBuildEvent= CS/RevitLookup.sln'
    - New-Item -Force -ItemType Directory -Path artifacts | Out-Null
    - Copy-Item "CS/bin/2021/RevitLookup.dll" artifacts/
    - Copy-Item "CS/RevitLookup.addin" artifacts/
  artifacts:
    paths:
      - artifacts/
    expire_in: 1 day

sign:
  stage: sign
  image: debian:buster-slim
  dependencies:
    - build
  variables:
    ASSEMBLY: artifacts/RevitLookup.dll
    TIMESERVER: http://timestamp.verisign.com/scripts/timstamp.dll
    DEBIAN_FRONTEND: noninteractive
  script:
    - apt-get update && apt-get install -y --no-install-recommends osslsigncode
    - mkdir -p .secrets && mount -t ramfs -o size=1M ramfs .secrets/
    - echo "${CODESIGN_CERTIFICATE}" | base64 --decode > .secrets/authenticode.spc
    - echo "${CODESIGN_KEY}" | base64 --decode > .secrets/authenticode.key
    - osslsigncode -h sha1 -spc .secrets/authenticode.spc -key .secrets/authenticode.key -t ${TIMESERVER} -in "${ASSEMBLY}" -out "${ASSEMBLY}-sha1"
    - osslsigncode -nest -h sha2 -spc .secrets/authenticode.spc -key .secrets/authenticode.key -t ${TIMESERVER} -in "${ASSEMBLY}-sha1" -out "${ASSEMBLY}"
    - rm "${ASSEMBLY}-sha1"
  after_script:
    - rmdir .secrets
  artifacts:
    paths:
      - artifacts/
    expire_in: 1 day
  only:
    variables:
      - $CODESIGN_CERTIFICATE != null
      - $CODESIGN_KEY != null

publish:
  stage: publish
  image: alpine:3.12
  dependencies:
    - sign
  before_script:
    - |
        if [ -n "${CI_COMMIT_TAG}" ]; then
            export RELEASE_NAME="${CI_COMMIT_TAG}"
            export S3_FOLDER="releases"
        else
            export RELEASE_NAME="${CI_COMMIT_BRANCH}-$(date --utc -Iseconds)"
            export S3_FOLDER="current"
        fi
    - export ZIP="${RELEASE_NAME}.7z"

    - apk add --update --no-cache curl jq py-pip p7zip
    - pip install awscli
    - eval $(aws ecr get-login --no-include-email --region $AWS_REGION | sed 's|https://||')
  script:
    - 7z a "${ZIP}" ./artifacts/*
    - aws s3 cp "${ZIP}" "s3://${S3_BUCKET_NAME}/${S3_FOLDER}/${ZIP}"
  only:
    variables:
      - $AWS_ACCESS_KEY_ID != null
      - $AWS_SECRET_ACCESS_KEY != null
      - $AWS_REGION != null
      - $S3_BUCKET_NAME != null
</pre>

<center>
<img src="img/gitlab_functionality.png" alt="GitLab functionality" title="GitLab functionality" width="800"/> <!-- 1125 -->
</center>

####<a name="4"></a> GraphQL for Revit

[Gregor Vilkner](https://www.microdesk.com/consulting-team/gregor-vilkner)
of [Microdesk](https://www.microdesk.com) implemented a
[GraphQL](https://graphql.org) endpoint
for Revit that can also be accessed remotely using azure service bus.

> No middle men, no storing stuff in the cloud.

> Was wondering if you wanted to check this out:

> [Revit2GraphQL GitHub repository](https://github.com/gregorvilkner/Revit2GraphQL)

<center>
<img src="img/BIMrx_marconi.jpg" alt="BIMrx Marconi" title="BIMrx Marconi" width="800"/> <!-- 1125 -->
</center>

We did a quick Q and A on this:

Q: I looked your repo and it looks perfectly OK and straightforward.

A: Thanks.
 
Q: I and others have done similar things in the past, communicating with Revit, meseems.

A: Great (ancient) word: meseems! I had to look that up.
 
Q: Not with GraphQL specifically.

A: GraphQL is awesome, and very hot right now. People get hooked on graph stuff: once you start you’ll want to use it everywhere. The 'one endpoint only' thing. The way you can structure single queries that would otherwise be batch-jobs of dozens of calls. Traversal has so many applications in BIM: duct runs, electrical distribution, path finding between spaces (doors as relationships, rooms as nodes), load take downs for columns.
 
Q: What do you want to share about this beyond what you write in the repo readme?

A: I had done a bunch of work using graph databases and GraphQL at my last job. After recently re-surfacing in the AECO space, BIM feels like a very good candidate for a graph db approach. It’s got mountains of extremely diverse datasets and especially relationships of all sorts play a super important role. Both, GraphQL and graph dbs center around this 'entrance point and traverse' concept. That really hits home for me with BIM: different folks have different entrance points and traversal needs: structural, rooms/spaces, MEP systems… We have a GraphQL endpoint for Autodesk Fabrication as well – and it’s super useful there too for reporting, QA/QC applications, interoperability.
 
Q: How does your effort compare with something you would do with Forge?

A: Forge rocks – at Microdesk we’re all big fans. But we’re still a bit off with manipulating content in Forge. Excited to see how much momentum and adoption design automation will pick up. In a discussion with some of our FM guys, however, they totally liked the idea of having a GraphQL endpoint for Forge. You can interactively explore a dataset – that’s a big deal, because it takes the need to write code out of the equation. One person would 'discover' and 'arrive' at just the right set of queries to do the trick and someone else could implement it. Btw, this is how GraphQL evolved inside Facebook when it was originally developed back in 2012.
 
Q: How is the stability if you start processing large jobs?

A: Query execution is basically as fast as the Revit API allows. Queries are executed sequentially – that’s how Revit likes it. Over-fetching is a topic in GraphQL. Since you can build large traversals in a query you could easily overwhelm the resolver and payload size. For instance: all families in a model, with all instances, and all parameters – it’s super easy to build this query, but one should obviously tweak, adjust, test, use name filters, … But returning 100k rows of JSON is no big deal at all.
 
Q: What is the purpose, and how does it differ from a Forge approach?

A: Using mutations we can write back into the model. The Marconi approach gets you a 'phone-nr' for a unique session – so it’s very immediate, real-time, and hopefully lends itself to collaborative applications. Also, one could put a resolver against Forge – and then the exact same queries could be made either against a Revit model on a desktop, or a Forge model in the cloud. A user in a Marconi client application would not (need to) know if the other end is Revit or Forge.

We have seen with Dynamo, how much 'algorithmic' and 'generative' stuff gets brought to the game by power users that aren’t familiar with coding and the Revit API in particular. Plus, there’s a ton of little this and that to keep in mind when coding against the Revit API: tasks, sessions, transactions, being in the wrong thread… A GraphQL endpoint takes all that away. You could bring that endpoint with a bunch of Revit models to a hackathon and over 24 hours have kids build mobile apps and dashboards against Revit data – folks that have never operated Revit, or learned to code against the Revit API. And they could use anything they want as long as it can make HTTP requests.
 
Q: Do you have any demos that specifically highlight the use of the GraphQL in conjunction with the BIM?

A: Yes, we’ll have to shoot some YouTube vids. Hopefully soon.
 
... did I mention there are `TammTreeNodes` in there?
To traverse mechanical systems using some of the building coders fine demo code.
That’s waiting for a good round of MEP refactoring.
But for now, duct tape will have to do.
 
Many thanks to Gregor for implementing, sharing and explaining this powerful tool.

####<a name="5"></a> The Tamm Tree

Tamm really is a tree, in fact, an oak, in Estonian.

[Tamm is also a common Estonian surname](https://en.wikipedia.org/wiki/Tamm_(surname)).

My surname Tammik means 'oak grove'.
It exists as a German surname as well, Eichenhain, and is also quite common in Sweden, as Eklund.

So, the TammTree makes perfect sense in another dimension as well.
