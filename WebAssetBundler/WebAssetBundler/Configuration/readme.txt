```
    <bundler>
        <styleSheets>
            <group name="Test1" combine="true" compress="true" version="1.1">
                <add source="~/Script/file1.css"/>
                <add source="~/Script/file2.css"/>
                <add source="~/Script/file3.css"/>
            </group>
        </styleSheets>
        <scripts>
            <group name="Test1" combine="true" compress="true" version="1.1">
                <add source="~/Content/file1.js"/>
                <add source="~/Content/file1.js"/>
                <add source="~/Content/file1.js"/>
            </group>
        </scripts>
    </bundler>
```

**Note:** Groups are combined and compressed by default.   
**Note:** There is no version by default.

### Using Shared Groups to Your Project

First you will need to add a new section to the **Web.config** of the project. (Note: The config file is in the root, do not use the config in the Views folder).

```
<section name="bundler" type="WebAssetBundler.Web.Mvc.SharedGroupConfigurationSection, WebAssetBundler" />
```

This line will need to be added to the **configSections** element in the **Web.config** file. Here is a full example:

```
<configSections>
    <section name="bundler" type="WebAssetBundler.Web.Mvc.SharedGroupConfigurationSection, WebAssetBundler" />      
</configSections>
```

**Note:** The above should be added under the configuration element.   
You can now start adding the configuration elements to the Web.config file. The **bundler** element should be added under the configuration element. Alternatively you can create a separate configuration file to hold the configuration.

### Configuration In Its Own File

- Ensure you have added the config section to the Web.config file. See above.

- Add this to the configuration section in your Web.config file.

```
<bundler configSource="Bundler.config"/>
```

- Create a new file called Bundler.config in your application root.

- In the Bundler.config file add the full configuration structure. Example:

```
<bundler>
   <styleSheets>
       ...
   </styleSheets>
   <scripts>
       ...
   </scripts>
</bundler>
```