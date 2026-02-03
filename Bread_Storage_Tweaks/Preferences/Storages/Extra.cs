using Il2CppScheduleOne.Storage;
using MelonLoader;

namespace Bread_Storage_Tweaks.Preferences.Storages;

public static class Extra
{
    private static MelonPreferences_Category? _extraCategory;
    private static MelonPreferences_Entry<StorageEntityValues>? _briefcase;
    private static MelonPreferences_Entry<StorageEntityValues>? _locker;
    private static MelonPreferences_Entry<StorageEntityValues>? _coffeeTable;
    private static MelonPreferences_Entry<StorageEntityValues>? _deadDrop;
    private static MelonPreferences_Entry<StorageEntityValues>? _deliveryBay;
    private static MelonPreferences_Entry<StorageEntityValues>? _displayCabinet;
    private static MelonPreferences_Entry<StorageEntityValues>? _safe;
    private static MelonPreferences_Entry<StorageEntityValues>? _table;
    private static MelonPreferences_Entry<StorageEntityValues>? _wallMountedShelf;

    public static void Init()
    {
        _extraCategory = MelonPreferences.CreateCategory("Extra Slots");
        _extraCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Extra.cfg"), true, false);

        _briefcase = _extraCategory.CreateEntry("Briefcase", new StorageEntityValues(4));
        _locker = _extraCategory.CreateEntry("Locker", new StorageEntityValues(6));
        _coffeeTable = _extraCategory.CreateEntry("Coffee Table", new StorageEntityValues(3));
        _deadDrop = _extraCategory.CreateEntry("Dead Drop", new StorageEntityValues(5));
        _deliveryBay = _extraCategory.CreateEntry("Delivery Bay", new StorageEntityValues(5));
        _displayCabinet = _extraCategory.CreateEntry("Display Cabinet", new StorageEntityValues(4));
        _safe = _extraCategory.CreateEntry("Safe", new StorageEntityValues(10));
        _table = _extraCategory.CreateEntry("Table", new StorageEntityValues(3));
        _wallMountedShelf = _extraCategory.CreateEntry("Wall-Mounted Shelf", new StorageEntityValues(4));

        Utils.MaxSlotAmount = Math.Max(Utils.MaxSlotAmount, ((List<MelonPreferences_Entry<StorageEntityValues>>)
        [
            _briefcase!, _locker!, _coffeeTable!,
            _deadDrop!, _deliveryBay!, _displayCabinet!,
            _safe!, _table!, _wallMountedShelf!
        ]).Max(x => x.Value.SlotAmount));

        _extraCategory.SaveToFile(false);
    }

    public static StorageEntityValues? GetValues(StorageEntity storageEntity) =>
        storageEntity.StorageEntityName switch
        {
            "Briefcase" => _briefcase!.Value,
            "Locker" => _locker!.Value,
            "Coffee Table" => _coffeeTable!.Value,
            "Dead Drop" => _deadDrop!.Value,
            "Delivery Bay" => _deliveryBay!.Value,
            "Display Cabinet" => _displayCabinet!.Value,
            "Safe" => _safe!.Value,
            "Table" => _table!.Value,
            "Wall-Mounted Shelf" => _wallMountedShelf!.Value,
            _ => null
        };
}