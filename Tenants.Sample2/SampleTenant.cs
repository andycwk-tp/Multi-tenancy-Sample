namespace MultiTenancy.Tenants.Sample2
{
  using MultiTenancy.Core;

  /// <summary>
    /// Tenant for "Sample2" Tenant
    /// </summary>
    public class SampleTenant : AbstractApplicationTenant
    {
        /// <summary>
        /// Initializes a new instance of the SampleTenant class
        /// </summary>
        public SampleTenant()
        {
            // setup view engine
            ViewEngine = DetermineViewEngine();

            // setup dependency container
            DependencyContainer = AssemblySettings.FormContainer(config => config.For<IApplicationTenant>().Singleton().Use(this));

            ApplicationName = "Sample 2";
            MinifyJavaScript = true;
            MinifyCSS = true;
            EnabledFeatures = null;
            UrlPaths = new[] { "http://localhost:3455/" };
        }
    }
}