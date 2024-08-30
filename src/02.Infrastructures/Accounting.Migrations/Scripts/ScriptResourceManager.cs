namespace Accounting.Migrations.Scripts;

public class ScriptResourceManager
{
    public static string Read(string name)
    {
        var assembly = typeof(ScriptResourceManager).Assembly;
        var resourcesBasePath = typeof(ScriptResourceManager).Namespace;

        var resoucePath = $"{resourcesBasePath}.{name}";
        using var stream = assembly.GetManifestResourceStream(resoucePath);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}