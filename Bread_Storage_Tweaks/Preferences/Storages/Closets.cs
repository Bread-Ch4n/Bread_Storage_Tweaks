using Il2CppScheduleOne.Storage;
using MelonLoader;

namespace Bread_Storage_Tweaks.Preferences.Storages;

public static class Closets
{
    private static MelonPreferences_Category? _closetCategory;
    private static MelonPreferences_Entry<StorageEntityValues>? _hugeCloset;
    private static MelonPreferences_Entry<StorageEntityValues>? _largeCloset;
    private static MelonPreferences_Entry<StorageEntityValues>? _mediumCloset;
    private static MelonPreferences_Entry<StorageEntityValues>? _smallCloset;


    public static void Init()
    {
        _closetCategory = MelonPreferences.CreateCategory("Closets");
        _closetCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Closets.cfg"), true, false);

        _hugeCloset = _closetCategory.CreateEntry("Huge Closet", new StorageEntityValues(20, 2));
        _largeCloset = _closetCategory.CreateEntry("Large Closet", new StorageEntityValues(12, 2));
        _mediumCloset = _closetCategory.CreateEntry("Medium Closet", new StorageEntityValues(8, 2));
        _smallCloset = _closetCategory.CreateEntry("Small Closet", new StorageEntityValues(6));

        Utils.MaxSlotAmount = Math.Max(Utils.MaxSlotAmount, ((List<MelonPreferences_Entry<StorageEntityValues>>)
        [
            _hugeCloset!, _largeCloset!, _mediumCloset!, _smallCloset!
        ]).Max(x => x.Value.SlotAmount));

        _closetCategory.SaveToFile(false);
    }

    public static StorageEntityValues? GetValues(StorageEntity storageEntity) =>
        storageEntity.name switch
        {
            _ when storageEntity.name.Contains("HugeStorageCloset") => _hugeCloset!.Value,
            _ when storageEntity.name.Contains("LargeStorageCloset") => _largeCloset!.Value,
            _ when storageEntity.name.Contains("MediumStorageCloset") => _mediumCloset!.Value,
            _ when storageEntity.name.Contains("SmallStorageCloset") => _smallCloset!.Value,
            _ => null
        };
}