🌩️ PowerOverInternet
===

[![Build status](https://img.shields.io/github/workflow/status/Aldaviva/PowerOverInternet/.NET?logo=github)](https://github.com/Aldaviva/PowerOverInternet/actions/workflows/dotnet.yml)

*Web service with an HTTP API for turning smart power outlets on and off*

<!-- MarkdownTOC autolink="true" bracket="round" autoanchor="true" levels="1,2" bullets="1.,-,-,-" -->

1. [Prerequisites](#prerequisites)
1. [Installation](#installation)
1. [Usage](#usage)
1. [API Reference](#api-reference)

<!-- /MarkdownTOC -->

<a id="prerequisites"></a>
## Prerequisites

- [.NET 6 or later](https://dotnet.microsoft.com/en-us/download)
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

[**Download**](https://raw.githubusercontent.com/Aldaviva/PowerOverInternet/master/.github/files/charging-limiter.flo)

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
