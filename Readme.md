🌩️ PowerOverInternet
===

[![GitHub Actions](https://img.shields.io/github/workflow/status/Aldaviva/PowerOverInternet/.NET?logo=github)](https://github.com/Aldaviva/PowerOverInternet/actions/workflows/dotnet.yml) [![Testspace](https://img.shields.io/testspace/tests/Aldaviva/Aldaviva:PowerOverInternet/master?passed_label=passing&failed_label=failing&logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCA4NTkgODYxIj48cGF0aCBkPSJtNTk4IDUxMy05NCA5NCAyOCAyNyA5NC05NC0yOC0yN3pNMzA2IDIyNmwtOTQgOTQgMjggMjggOTQtOTQtMjgtMjh6bS00NiAyODctMjcgMjcgOTQgOTQgMjctMjctOTQtOTR6bTI5My0yODctMjcgMjggOTQgOTQgMjctMjgtOTQtOTR6TTQzMiA4NjFjNDEuMzMgMCA3Ni44My0xNC42NyAxMDYuNS00NFM1ODMgNzUyIDU4MyA3MTBjMC00MS4zMy0xNC44My03Ni44My00NC41LTEwNi41UzQ3My4zMyA1NTkgNDMyIDU1OWMtNDIgMC03Ny42NyAxNC44My0xMDcgNDQuNXMtNDQgNjUuMTctNDQgMTA2LjVjMCA0MiAxNC42NyA3Ny42NyA0NCAxMDdzNjUgNDQgMTA3IDQ0em0wLTU1OWM0MS4zMyAwIDc2LjgzLTE0LjgzIDEwNi41LTQ0LjVTNTgzIDE5Mi4zMyA1ODMgMTUxYzAtNDItMTQuODMtNzcuNjctNDQuNS0xMDdTNDczLjMzIDAgNDMyIDBjLTQyIDAtNzcuNjcgMTQuNjctMTA3IDQ0cy00NCA2NS00NCAxMDdjMCA0MS4zMyAxNC42NyA3Ni44MyA0NCAxMDYuNVMzOTAgMzAyIDQzMiAzMDJ6bTI3NiAyODJjNDIgMCA3Ny42Ny0xNC44MyAxMDctNDQuNXM0NC02NS4xNyA0NC0xMDYuNWMwLTQyLTE0LjY3LTc3LjY3LTQ0LTEwN3MtNjUtNDQtMTA3LTQ0Yy00MS4zMyAwLTc2LjY3IDE0LjY3LTEwNiA0NHMtNDQgNjUtNDQgMTA3YzAgNDEuMzMgMTQuNjcgNzYuODMgNDQgMTA2LjVTNjY2LjY3IDU4NCA3MDggNTg0em0tNTU3IDBjNDIgMCA3Ny42Ny0xNC44MyAxMDctNDQuNXM0NC02NS4xNyA0NC0xMDYuNWMwLTQyLTE0LjY3LTc3LjY3LTQ0LTEwN3MtNjUtNDQtMTA3LTQ0Yy00MS4zMyAwLTc2LjgzIDE0LjY3LTEwNi41IDQ0UzAgMzkxIDAgNDMzYzAgNDEuMzMgMTQuODMgNzYuODMgNDQuNSAxMDYuNVMxMDkuNjcgNTg0IDE1MSA1ODR6IiBmaWxsPSIjZmZmIi8%2BPC9zdmc%2B)](https://aldaviva.testspace.com/spaces/192613) [![Coveralls](https://img.shields.io/coveralls/github/Aldaviva/PowerOverInternet?logo=coveralls)](https://coveralls.io/github/Aldaviva/PowerOverInternet?branch=master)

*Web service with an HTTP API for turning smart power outlets on and off*

<!-- MarkdownTOC autolink="true" bracket="round" autoanchor="true" levels="1,2" bullets="1.,-,-,-" -->

1. [Prerequisites](#prerequisites)
1. [Installation](#installation)
1. [Usage](#usage)
1. [API Reference](#api-reference)

<!-- /MarkdownTOC -->

<a id="prerequisites"></a>
## Prerequisites

- [ASP.NET Core Runtime 6 or later](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Kasa smart outlet](https://www.kasasmart.com/us/products/smart-plugs)
    - Verified with [EP10](https://www.kasasmart.com/us/products/smart-plugs/kasa-smart-plug-mini-ep10) and [KP125](https://www.kasasmart.com/us/products/smart-plugs/kasa-smart-plug-slim-energy-monitoring-kp125)
    - Probably [compatible](https://github.com/Aldaviva/Kasa#prerequisites) with most other models

<a id="installation"></a>
## Installation

For additional hosting and deployment scenarios not covered in this section, such as `HTTP.sys` and Windows services, refer to [Host and deploy ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/?view=aspnetcore-6.0).

### IIS

1. Install the [.NET Core Hosting Bundle](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/hosting-bundle?view=aspnetcore-6.0) for IIS.
1. Copy the build files from the [latest release of this project](https://github.com/Aldaviva/PowerOverInternet/releases/latest) to a folder on your server.
1. In IIS Manager, add a new Website to your server's Sites.
1. Choose a name and binding for the site.
1. Choose that folder as the site's physical path.

For more information, refer to [Host ASP.NET Core on Windows with IIS](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-6.0) and [Publish an ASP.NET Core app to IIS](https://learn.microsoft.com/en-us/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-6.0&tabs=netcore-cli).


![Add Website](https://raw.githubusercontent.com/Aldaviva/PowerOverInternet/master/.github/images/iis-new-site.png)

### Standalone

1. Copy the build files from the [latest release of this project](https://github.com/Aldaviva/PowerOverInternet/releases/latest) to a folder on your server.
1. In that folder, run `PowerOverInternet.exe`.

```ps1
> .\PowerOverInternet.exe


info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Program Files\PowerOverInternet\
```

<a id="usage"></a>
## Usage

### Turn on an outlet

```http
PUT /power?outletHostname=192.168.1.100&turnOn=true
Host: myserver.com
```

### Turn off an outlet

```http
PUT /power?outletHostname=192.168.1.100&turnOn=false
Host: myserver.com
```

### Turn off an outlet after 30 seconds

```http
PUT /power?outletHostname=192.168.1.100&turnOn=false&delaySec=30
Host: myserver.com
```

### Shut down and power off Cisco endpoint using macro

This [Cisco macro](https://gist.github.com/Aldaviva/bccd766099e2d7807da086feacf2c18a#file-shutdown-button-js-L22) cuts power to the endpoint after a confirmation prompt and a 33 second graceful shutdown delay.

```js
await xapi.Command.HttpClient.Put({
    Url: "https://thor.aldaviva.com:8444/power?outletHostname=sx20.outlets.aldaviva.com&turnOn=false&delaySec=33" //it takes the SX20 about 28 seconds to shut down
}, "");
```

### Phone charger control

This [Automate](https://llamalab.com/automate/) flow turns off a phone charger when the phone is ≥80% charged, and turns it back on when it's ≤20% charged.

[**Download flow**](https://raw.githubusercontent.com/Aldaviva/PowerOverInternet/master/.github/files/charging-limiter.flo)

![Automate flow](https://raw.githubusercontent.com/Aldaviva/PowerOverInternet/master/.github/images/charging-limiter.png)

<a id="api-reference"></a>
## API Reference

### `PUT /power`

|Parameter|Required|Location|Example|Description|
|---|---|---|---|---|
|`outletHostname`|🛑&nbsp;required|❔&nbsp;query|`192.168.1.100`|The IP address or FQDN of your smart outlet, visible in router DHCP list or [`nmap`](https://nmap.org/) scan.|
|`turnOn`|🛑&nbsp;required|❔&nbsp;query|`true`|`true` to turn the outlet on, or `false` to turn it off.|
|`delaySec`|🟢&nbsp;optional|❔&nbsp;query|`30`|Number of seconds to wait after receiving the request before changing the outlet's power state. Defaults to `0` if omitted.|

**Response status:** `204 No Content`

The response is sent immediately, not after waiting for `delaySec` seconds.
