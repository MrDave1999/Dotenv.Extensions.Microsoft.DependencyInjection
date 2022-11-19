using DotEnv.Core;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        private const string DefaultEnvFileName = ".env";

        /// <summary>
        /// This registers <see cref="IEnvReader" /> as a singleton.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="provider">The environment variables provider.</param>
        /// <returns>An instance that allows access to the environment variables.</returns>
        private static IEnvReader AddEnvReader(this IServiceCollection services, IEnvironmentVariablesProvider provider)
        {
            var reader = new EnvReader(provider);
            services.AddSingleton<IEnvReader>(reader);
            return reader;
        }

        /// <summary>
        /// Adds the environment vars using a service.
        /// This registers <see cref="IEnvReader" /> as a singleton and calls the <see cref="EnvLoader.Load"/> method.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <exception cref="ArgumentNullException"><c>services</c> is <c>null</c>.</exception>
        /// <returns>An instance that allows access to the environment variables.</returns>
        public static IEnvReader AddDotEnv(this IServiceCollection services)
            => services.AddDotEnv(DefaultEnvFileName);

        /// <summary>
        /// Adds the environment vars using a service.
        /// This registers <see cref="IEnvReader" /> as a singleton and calls the <see cref="EnvLoader.Load"/> method.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="paths">The .env files paths to add.</param>
        /// <exception cref="ArgumentNullException"><c>services</c>, or <c>paths</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The length of the <c>paths</c> list is zero.</exception>
        /// <returns>An instance that allows access to the environment variables.</returns>
        public static IEnvReader AddDotEnv(this IServiceCollection services, params string[] paths)
        {
            _ = services ?? throw new ArgumentNullException(nameof(services));
            var envVars = new EnvLoader()
                              .AddEnvFiles(paths)
                              .Load();

            return services.AddEnvReader(envVars);
        }

        /// <summary>
        /// This registers <typeparamref name="TSettings" /> as a singleton.
        /// </summary>
        /// <typeparam name="TSettings">The type of the new instance to bind.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <param name="provider">The environment variables provider.</param>
        /// <returns>An instance that allows access to the environment variables.</returns>
        private static TSettings AddAppSettings<TSettings>(this IServiceCollection services, IEnvironmentVariablesProvider provider)
            where TSettings : class, new()
        {
            var settings = new EnvBinder(provider).Bind<TSettings>();
            services.AddSingleton(settings);
            return settings;
        }

        /// <summary>
        /// Adds the environment vars using a service.
        /// This registers <typeparamref name="TSettings" /> as a singleton and calls the <see cref="EnvLoader.Load"/> method.
        /// </summary>
        /// <typeparam name="TSettings">The type of the new instance to bind.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <exception cref="ArgumentNullException"><c>services</c> is <c>null</c>.</exception>
        /// <returns>An instance that allows access to the environment variables.</returns>
        public static TSettings AddDotEnv<TSettings>(this IServiceCollection services) where TSettings : class, new()
            => services.AddDotEnv<TSettings>(DefaultEnvFileName);

        /// <summary>
        /// Adds the environment vars using a service.
        /// This registers <typeparamref name="TSettings" /> as a singleton and calls the <see cref="EnvLoader.Load"/> method.
        /// </summary>
        /// <typeparam name="TSettings">The type of the new instance to bind.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <param name="paths">The .env files paths to add.</param>
        /// <exception cref="ArgumentNullException"><c>services</c>, or <c>paths</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The length of the <c>paths</c> list is zero.</exception>
        /// <returns>An instance that allows access to the environment variables.</returns>
        public static TSettings AddDotEnv<TSettings>(this IServiceCollection services, params string[] paths) 
            where TSettings : class, new()
        {
            _ = services ?? throw new ArgumentNullException(nameof(services));
            var envVars = new EnvLoader()
                              .AddEnvFiles(paths)
                              .Load();

            return services.AddAppSettings<TSettings>(envVars);
        }

        /// <summary>
        /// Adds the environment vars based on the environment (development, test, staging or production).
        /// This registers <see cref="IEnvReader" /> as a singleton and calls the <see cref="EnvLoader.LoadEnv"/> method.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="basePath">The base path where the .env files will be located.</param>
        /// <param name="environmentName">The name of the environment.</param>
        /// <exception cref="ArgumentNullException"><c>services</c> is <c>null</c>.</exception>
        /// <returns>An instance that allows access to the environment variables.</returns>
        public static IEnvReader AddCustomEnv(this IServiceCollection services, string basePath = null, string environmentName = null)
        {
            _ = services ?? throw new ArgumentNullException(nameof(services));
            var loader = new EnvLoader();
            if (basePath is not null)
                loader.SetBasePath(basePath);
            if (environmentName is not null)
                loader.SetEnvironmentName(environmentName);

            var envVars = loader.LoadEnv();
            return services.AddEnvReader(envVars);
        }

        /// <summary>
        /// Adds the environment vars based on the environment (development, test, staging or production).
        /// This registers <typeparamref name="TSettings" /> as a singleton and calls the <see cref="EnvLoader.LoadEnv"/> method.
        /// </summary>
        /// <typeparam name="TSettings">The type of the new instance to bind.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <param name="basePath">The base path where the .env files will be located.</param>
        /// <param name="environmentName">The name of the environment.</param>
        /// <exception cref="ArgumentNullException"><c>services</c> is <c>null</c>.</exception>
        /// <returns>An instance that allows access to the environment variables.</returns>
        public static TSettings AddCustomEnv<TSettings>(this IServiceCollection services, string basePath = null, string environmentName = null) 
            where TSettings : class, new()
        {
            _ = services ?? throw new ArgumentNullException(nameof(services));
            var loader = new EnvLoader();
            if (basePath is not null)
                loader.SetBasePath(basePath);
            if (environmentName is not null)
                loader.SetEnvironmentName(environmentName);

            var envVars = loader.LoadEnv();
            return services.AddAppSettings<TSettings>(envVars);
        }
    }
}
