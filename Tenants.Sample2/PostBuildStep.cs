﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace MultiTenancy.Tenants.Sample2
{
  using System.Reflection;
  using System.Web.Mvc;
  using Spark;
  using Spark.FileSystem;

  [RunInstaller(true)]
  public partial class PostBuildStep : System.Configuration.Install.Installer
  {
    public PostBuildStep()
    {
      InitializeComponent();
    }

    private void precompileInstaller1_DescribeBatch(object sender, Spark.Web.Mvc.Install.DescribeBatchEventArgs e)
    {
      // Add controllers from assemblies. 
      // NOTE: This will change to reflect the override model.
      /*foreach (var viewSetting in AssemblySettings.AssemblyViewPaths)
          e.Batch.FromAssembly(viewSetting.Item1);*/


      foreach (var controller in AssemblySettings.FormContainer().GetAllInstances<IController>())
        e.Batch.For(controller.GetType());
    }

    /// <summary>
    /// Gets the spark settings used for view generation
    /// </summary>
    private ISparkSettings Settings
    {
      get
      {
        var settings = new SparkSettings().SetAutomaticEncoding(true)
                                          .SetDefaultLanguage(LanguageType.CSharp);

#if DEBUG
        settings.SetDebug(true);
#else
                settings.SetDebug(false);
#endif

        // add embedded view folders from settings
        foreach (var viewSetting in AssemblySettings.AssemblyViewPaths)
          settings.AddViewFolder(ViewFolderType.EmbeddedResource, EmbeddedFor(viewSetting.Item1, viewSetting.Item2));

        return settings;
      }
    }

    private IDictionary<string, string> EmbeddedFor(Assembly assembly, string pathToResources)
    {
      // new EmbeddedViewFolder(assembly: ____, resourcePath: ____);
      return new Dictionary<string, string>
            { 
                {"assembly", assembly.FullName },
                {"resourcePath", pathToResources}
            };
    }
  }
}
