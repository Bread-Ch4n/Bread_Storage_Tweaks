using Bread_Storage_Tweaks.Preferences.Classes;

namespace Bread_Storage_Tweaks.Preferences;

public static class PreferenceManager
{
    public static void Init()
    {
        Directory.CreateDirectory(Utils.PreferencePath);

        StorageRacks.Init();
        Extra.Init();
        Cars.Init();
    }
}