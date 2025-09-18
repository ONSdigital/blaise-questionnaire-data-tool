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

### Adding the Azure DevOps NuGet Feed

To access the Blaise API, add our Azure DevOps NuGet feed in Visual Studio:

1. Go to Tools > Options > NuGet Package Manager > Package Sources.
1. Click the Add (+) button to add a new source.
1. Enter a name for the source and the feed URL:
```
https://pkgs.dev.azure.com/<ORG>/<PROJ>/_packaging/<FEED>/nuget/v3/index.json
```

Replace `<ORG>`, `<PROJ>`, and `<FEED>` with the appropriate values.

### Configuring Your NuGet Personal Access Token (PAT)

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

1. Launch the application in Visual Studio to open the Windows Forms UI.
1. Enter your Blaise connection details in the provided fields and click Connect.
   - You can either use our Nginx reverse proxy or open tunnels to the management VM and use localhost.
   - Note that the GCP HTTPS proxy will not work.
1. Select your desired server park and questionnaire.
1. Specify the start primary key and the number of cases you wish to create.
1. Click Create to generate cases.

The cases will be populated with dummy sample data. The `CaseDataModel.cs` file will show you what will be attempted to be populated in the data.

Cases can also be created from a JSON file, allowing you to populate any fields you like. The `CaseFiles` folder contains examples and common scenarios. Please remember to commit any useful JSON files you may create for future use.

If a field does not exist in the questionnaire, the tool will skip over it without causing errors.

**Important:** Existing case data is cleared each time new data is loaded.
