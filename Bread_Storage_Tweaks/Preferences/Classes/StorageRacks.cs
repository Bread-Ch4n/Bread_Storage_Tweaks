using MelonLoader;

namespace Bread_Storage_Tweaks.Preferences.Classes;

public static class StorageRacks
{
    private static MelonPreferences_Category? _storageRackSaCategory;
    public static MelonPreferences_Entry<int>? LargeRsa;
    public static MelonPreferences_Entry<int>? MediumRsa;
    public static MelonPreferences_Entry<int>? SmallRsa;


    private static MelonPreferences_Category? _storageRackRCategory;
    public static MelonPreferences_Entry<int>? LargeRra;
    public static MelonPreferences_Entry<int>? MediumRra;
    public static MelonPreferences_Entry<int>? SmallRra;

    public static void Init()
    {
        _storageRackSaCategory = MelonPreferences.CreateCategory("Storage Rack Slots");
        _storageRackSaCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Storage_Racks.cfg"));

        LargeRsa = _storageRackSaCategory.CreateEntry("Large Storage Rack Slot Amount", 8,
            description:
            "Do not make any values smaller or remove the mod. You will loose the items from the extra slots.\nThe max slot value is 20!");
        MediumRsa = _storageRackSaCategory.CreateEntry("Medium Storage Rack Slot Amount", 6);
        SmallRsa = _storageRackSaCategory.CreateEntry("Small Storage Rack Slot Amount", 4);

        List<MelonPreferences_Entry<int>> sa =
        [
            LargeRsa, MediumRsa, SmallRsa
        ];
        foreach (var saEntry in sa.Where(saEntry => saEntry.Value > 20)) saEntry.Value = 20;

        _storageRackRCategory = MelonPreferences.CreateCategory("Storage Rack Rows");
        _storageRackRCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Storage_Racks.cfg"));

        LargeRra = _storageRackRCategory.CreateEntry("Large Storage Rack Row Amount", 2);
        MediumRra = _storageRackRCategory.CreateEntry("Medium Storage Rack Row Amount",
            2
        );
        SmallRra = _storageRackRCategory.CreateEntry("Small Storage Rack Row Amount", 2);
    }
}