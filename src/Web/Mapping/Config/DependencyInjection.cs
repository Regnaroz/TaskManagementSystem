using Mapster;
using MapsterMapper;
using System.Reflection;

namespace SampleProject.Web.Mapping.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        //get mapping setting
        var config = TypeAdapterConfig.GlobalSettings;
        //get all classes that Inhirtis from IRegister and update the config
        config.Scan(Assembly.GetExecutingAssembly());

        //setting the Config Ignore case firstName == FirstName
        config.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
        //register the new scanend config
        services.AddSingleton(config);
        //register the IMapper with ServiceMapper (ServiceMapper : Mapper)
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}
