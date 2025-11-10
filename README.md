# Blaise Questionnaire Data Tool

A Windows Forms-based tool designed to simplify loading data into a Blaise questionnaire in our cloud-based environment.

## Git

**Never commit your `App.config` file with actual connection details.** To prevent sensitive information from being added to Git, you can use the following command to add all files while ignoring the `App.config` file:

```bash
git add . ':!app.config'
```

## Setup

**Note:** This tool requires Visual Studio on Windows due to the .NET Framework-based Blaise API.

The Blaise API is hosted on a private NuGet feed in our Azure DevOps repository, so you’ll need to set this up manually in Visual Studio.

### 1. Adding the Azure DevOps NuGet Feed

To access the Blaise API, add our Azure DevOps NuGet feed in Visual Studio:

1. Go to Tools > Options > NuGet Package Manager > Package Sources.
1. Click the Add (+) button to add a new source.
1. Enter a name for the source and the feed URL:

```
https://pkgs.dev.azure.com/<ORG>/<PROJ>/_packaging/<FEED>/nuget/v3/index.json
```

Replace `<ORG>`, `<PROJ>`, and `<FEED>` with the appropriate values.

### 2. Configuring Your NuGet Personal Access Token (PAT)

To authenticate with the NuGet feed, you’ll need to add your PAT to your NuGet configuration file:

1. Open your NuGet configuration file:

```
%appdata%\NuGet\NuGet.Config
```

2. Add the following section:

```
<packageSourceCredentials>
   <<SOURCE_NAME>>
      <add key="Username" value="<USERNAME>" />
      <add key="ClearTextPassword" value="<PAT>" />
   </<SOURCE_NAME>>
</packageSourceCredentials>
```

Replace `<SOURCE_NAME>`, `<USERNAME>`, and `<PAT>` with the appropriate values.

## Usage

### Prerequisites

- Install the gcloud CLI and ensure you're authenticated to the correct GCP project
- Have a Blaise user account with appropriate permissions

### Connecting to Blaise

There are two methods to connect to Blaise:

#### Option 1: Via Nginx Reverse Proxy (Recommended)

The simplest method requiring only the hostname:

1. Find the hostname in GCP Console: **Monitoring > Uptime Checks** (look for the hostname containing `web`)
2. Use this hostname with HTTPS binding

**Note:** This method uses HTTPS binding for sandboxes but may differ and require extra steps, e.g. IP allowlist, for formal environments.

#### Option 2: Via IAP Tunnels

Connect through the management VM using localhost:

1. Open two terminal windows and start the IAP tunnels:

   ```bash
   gcloud start-iap-tunnel blaise-gusty-mgmt 8031 --local-host-port=localhost:8031
   ```

   ```bash
   gcloud start-iap-tunnel blaise-gusty-mgmt 8033 --local-host-port=localhost:8033
   ```

2. Use `localhost` as your connection hostname with HTTP binding

**Note:** This method uses HTTP binding. If you have Blaise 5 installed locally, you may need to stop the service first.

### Creating Cases

1. Launch the application in Visual Studio to open the Windows Forms UI
2. Enter your Blaise connection details (see [Connecting to Blaise](#connecting-to-blaise)) and click **Connect**
3. Select your desired server park and questionnaire
4. Specify the start primary key and the number of cases to create
5. Click **Create** to generate cases

**Default Method: Dummy Sample Data**

Cases are populated with dummy sample data as defined in `CaseDataModel.cs`. This file shows which fields will be populated.

**Alternative Method: JSON Files**

You can also create cases from JSON files, allowing you to populate any fields you like:

- Example files and common scenarios are available in the `CaseFiles` folder
- Please commit any useful JSON files you create for future use
- If a field doesn't exist in the questionnaire, the tool will skip it without errors

**Important:** Existing case data is cleared each time new data is loaded.
