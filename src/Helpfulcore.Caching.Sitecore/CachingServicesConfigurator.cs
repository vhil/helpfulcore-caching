namespace Helpfulcore.Caching.Sitecore
{
    using Microsoft.Extensions.DependencyInjection;
    using Factory = global::Sitecore.Configuration.Factory;
    using IServicesConfigurator = global::Sitecore.DependencyInjection.IServicesConfigurator;

    public class CachingServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection container)
        {
            container.AddSingleton(provider =>
                Factory.CreateObject("helpfulcore/caching/cacheRepository", false) as ICacheRepository);
            container.AddSingleton(provider => 
                Factory.CreateObject("helpfulcore/caching/cacheProvider", false) as ISitecoreCacheProvider);
            container.AddSingleton<ICacheProvider>(provider => provider.GetService<ISitecoreCacheProvider>());
        }
    }
}
