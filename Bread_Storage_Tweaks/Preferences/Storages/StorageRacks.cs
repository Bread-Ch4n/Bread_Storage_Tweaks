using Il2CppScheduleOne.Storage;
using MelonLoader;

namespace Bread_Storage_Tweaks.Preferences.Storages;

public static class StorageRacks
{
    private static MelonPreferences_Category? _storageRackCategory;
    private static MelonPreferences_Entry<StorageEntityValues>? _large;
    private static MelonPreferences_Entry<StorageEntityValues>? _medium;
    private static MelonPreferences_Entry<StorageEntityValues>? _small;

    public static void Init()
    {
        _storageRackCategory = MelonPreferences.CreateCategory("Storage Racks");
        _storageRackCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Storage_Racks.cfg"), true, false);

        _large = _storageRackCategory.CreateEntry("Large Storage", new StorageEntityValues(8));
        _medium = _storageRackCategory.CreateEntry("Medium Storage", new StorageEntityValues(6));
        _small = _storageRackCategory.CreateEntry("Small Storage", new StorageEntityValues(4));

        Utils.MaxSlotAmount = Math.Max(Utils.MaxSlotAmount, ((List<MelonPreferences_Entry<StorageEntityValues>>)
        [
            _large!, _medium!, _small!
        ]).Max(x => x.Value.SlotAmount));

        _storageRackCategory.SaveToFile(false);
    }

    public static StorageEntityValues? GetValues(StorageEntity storageEntity) =>
        storageEntity.StorageEntityName switch
        {
            "Large Storage Rack" => _large!.Value,
            "Medium Storage Rack" => _medium!.Value,
            "Small Storage Rack" => _small!.Value,
            _ => null
        };
}