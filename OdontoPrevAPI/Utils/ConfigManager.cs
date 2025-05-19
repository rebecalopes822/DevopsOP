namespace OdontoPrevAPI.Utils;

public sealed class ConfigManager
{
    private static readonly Lazy<ConfigManager> _instance = new(() => new ConfigManager());

    public static ConfigManager Instance => _instance.Value;

    public string ConnectionString { get; }

    private ConfigManager()
    {
        ConnectionString = "User Id=rm553764;Password=fiap24;Data Source=oracle.fiap.com.br:1521/orcl;";
    }
}
