using Microsoft.Extensions.Configuration;

namespace CleanArch.Infra.Shell;

public class ConnectionHelper
{
    public static string GetDbConnection(string connectionString)
    {
        var str = Directory.GetCurrentDirectory();
        var index = str.IndexOf("\\bin");
        str = index > 0 ? str.Substring(0, index) : str;
        var path = Path.Combine(str, "..", "Shared");
        var builder = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(path, "sharedSettings.json"), true);

        var strConnection = builder.Build().GetConnectionString(connectionString);

        return strConnection;
    }
}