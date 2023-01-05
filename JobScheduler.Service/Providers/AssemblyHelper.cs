namespace JobScheduler.Service.Providers;
public class AssemblyHelper
{
    public T GetInstanceOf<T>(string fullClassName)
    {
        var assembly = typeof(T).Assembly;
        Type type = assembly.GetExportedTypes().FirstOrDefault(t => t.FullName.Equals(fullClassName));

        if (type != null)
        {
            var instance = (T)Activator.CreateInstance(type);
            return instance;
        }
        return default(T);
    }
}
