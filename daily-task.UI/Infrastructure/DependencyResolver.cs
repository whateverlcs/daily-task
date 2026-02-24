using Microsoft.Extensions.DependencyInjection;

namespace daily_task.UI.Infrastructure
{
    public static class DependencyResolver
    {
        private static IServiceProvider _serviceProvider;

        public static void SetServiceProvider(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public static T GetService<T>() where T : notnull
            => _serviceProvider.GetRequiredService<T>();
    }
}