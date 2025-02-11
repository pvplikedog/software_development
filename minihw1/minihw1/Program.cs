namespace hw1;

using Microsoft.Extensions.DependencyInjection;

class Program
{
    public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<VetClinic>();
            services.AddSingleton<Zoo>();
            services.AddSingleton<Menu>();
            var serviceProvider = services.BuildServiceProvider();
            
            var menu = serviceProvider.GetService<Menu>();
            menu.Handle();
        }
}