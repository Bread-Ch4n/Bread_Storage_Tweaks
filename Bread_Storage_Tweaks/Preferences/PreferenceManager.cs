using Bread_Storage_Tweaks.Preferences.Storages;

namespace Bread_Storage_Tweaks.Preferences;

public static class PreferenceManager
{
    public static void Init()
    {
        Directory.CreateDirectory(Utils.PreferencePath);

        Cars.Init();
        Employees.Init();
        Extra.Init();
        StorageRacks.Init();
        Closets.Init();
    }
}