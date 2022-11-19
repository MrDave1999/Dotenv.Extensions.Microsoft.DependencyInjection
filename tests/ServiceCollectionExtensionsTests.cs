namespace DotEnv.Core.Extensions.Microsoft.DependencyInjection.Tests;

[TestClass]
public class ServiceCollectionExtensionsTests
{
    private const string Summaries       = "SUMMARIES";
    private const string Expected        = "Cool";
    private const string ConfigEnvPath   = "env_files/config.env";

    [TestMethod]
    public void AddDotEnvWithDefaultEnvFileName()
    {
        var services = new ServiceCollection();
        SetEnvironmentVariable(Summaries, null);

        services.AddDotEnv();

        var reader = services.BuildServiceProvider().GetRequiredService<IEnvReader>();
        Assert.AreEqual(Expected, actual: reader[Summaries]);
    }

    [TestMethod]
    public void AddDotEnvWithCustomConfiguration()
    {
        var services = new ServiceCollection();
        SetEnvironmentVariable(Summaries, null);

        services.AddDotEnv(ConfigEnvPath);

        var reader = services.BuildServiceProvider().GetRequiredService<IEnvReader>();
        Assert.AreEqual(Expected, actual: reader[Summaries]);
    }

    [TestMethod]
    public void AddDotEnvGenericWithDefaultEnvFileName()
    {
        var services = new ServiceCollection();
        SetEnvironmentVariable(Summaries, null);

        services.AddDotEnv<AppSettings>();

        var settings = services.BuildServiceProvider().GetRequiredService<AppSettings>();
        Assert.AreEqual(Expected, actual: settings.Summaries);
    }

    [TestMethod]
    public void AddDotEnvGenericWithCustomConfiguration()
    {
        var services = new ServiceCollection();
        SetEnvironmentVariable(Summaries, null);

        services.AddDotEnv<AppSettings>(ConfigEnvPath);

        var settings = services.BuildServiceProvider().GetRequiredService<AppSettings>();
        Assert.AreEqual(Expected, actual: settings.Summaries);
    }

    [TestMethod]
    public void AddCustomEnvWithCustomConfiguration()
    {
        var services = new ServiceCollection();
        Env.CurrentEnvironment = null;

        services.AddCustomEnv(
            basePath: "env_files/environment/dev", 
            environmentName: "dev"
        );

        var reader = services.BuildServiceProvider().GetRequiredService<IEnvReader>();
        Assert.AreEqual(expected: "1", actual: reader["DEV_ENV"]);
        Assert.AreEqual(expected: "1", actual: reader["DEV_ENV_DEV"]);
        Assert.AreEqual(expected: "1", actual: reader["DEV_ENV_DEV_LOCAL"]);
        Assert.AreEqual(expected: "1", actual: reader["DEV_ENV_LOCAL"]);
    }

    [TestMethod]
    public void AddCustomEnvGenericWithCustomConfiguration()
    {
        var services = new ServiceCollection();
        Env.CurrentEnvironment = null;

        services.AddCustomEnv<SettingsProduction>(
            basePath: "env_files/environment/production", 
            environmentName: "production"
        );

        var settings = services.BuildServiceProvider().GetRequiredService<SettingsProduction>();
        Assert.AreEqual(expected: "1", actual: settings.ProdEnv);
        Assert.AreEqual(expected: "1", actual: settings.ProdEnvProd);
        Assert.AreEqual(expected: "1", actual: settings.ProdEnvProdLocal);
        Assert.AreEqual(expected: "1", actual: settings.ProdEnvLocal);
    }
}