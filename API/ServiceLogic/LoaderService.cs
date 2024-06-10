using System.Reflection;
using ILoaders;
using IServiceLogic;
using Microsoft.Extensions.Configuration;

namespace ServiceLogic;

public class LoaderService : ILoaderService
{
    #region Properties and Constructor
    
    private readonly IConfiguration _appConfiguration;
    private List<ILoader> _loaders = new List<ILoader>();

    public LoaderService(IConfiguration appConfiguration)
    {
        _appConfiguration = appConfiguration;
        UpdateLoadersList();
        MonitorLoadersFolder();
    }
    
    #endregion
    
    #region Folder Monitoring

    public void MonitorLoadersFolder()
    {
        string loadersPath = _appConfiguration["AppSettings:LoadersPath"];
        
        FileSystemWatcher watcher = new FileSystemWatcher(loadersPath, "*.dll");
        watcher.Created += OnFileModified;
        watcher.Changed += OnFileModified;
        watcher.Deleted += OnFileModified;
        watcher.Renamed += OnFileModified;
        watcher.EnableRaisingEvents = true;
    }

    private void OnFileModified(object sender, FileSystemEventArgs e)
    {
        UpdateLoadersList();
    }
    
    #endregion
    
    #region Get All Loaders
    
    public List<ILoader> GetAllLoaders()
    {
        return _loaders;
    }
    
    #endregion
    
    #region Update Loaders List and Valid Loader

    public void UpdateLoadersList()
    {
        try
        {
            string relativePath = _appConfiguration["AppSettings:LoadersPath"];
            
            string absolutPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);
            if (string.IsNullOrEmpty(absolutPath))
            {
                throw new Exception("Loaders path not found in appsettings");
            }

            if (!Directory.Exists(absolutPath))
            {
                throw new Exception("Loaders path not found");
            }

            foreach (var file in Directory.GetFiles(absolutPath, "*.dll"))
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (IsValidLoader(type))
                        {
                            ILoader loader = (ILoader)Activator.CreateInstance(type);
                            _loaders.Add(loader);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Error loading loader" + file);
                }
            }


        }
        catch
        {
            throw new Exception("Error updating loaders list");
        }

    }

    private bool IsValidLoader(Type type)
    {
        return type.GetInterfaces().Contains(typeof(ILoader)) && !type.IsInterface && !type.IsAbstract && typeof(ILoader).IsAssignableFrom(type);
    }
    
    #endregion
}