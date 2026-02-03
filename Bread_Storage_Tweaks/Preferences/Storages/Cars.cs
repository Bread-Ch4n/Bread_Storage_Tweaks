using Il2CppScheduleOne.Delivery;
using Il2CppScheduleOne.Storage;
using MelonLoader;

namespace Bread_Storage_Tweaks.Preferences.Storages;

public static class Cars
{
    private static MelonPreferences_Category? _carCategory;

    private static MelonPreferences_Entry<StorageEntityValues>? _coupe;
    private static MelonPreferences_Entry<StorageEntityValues>? _hotBox;
    private static MelonPreferences_Entry<StorageEntityValues>? _pickup;
    private static MelonPreferences_Entry<StorageEntityValues>? _sedan;
    private static MelonPreferences_Entry<StorageEntityValues>? _shitBox;
    private static MelonPreferences_Entry<StorageEntityValues>? _suv;
    private static MelonPreferences_Entry<StorageEntityValues>? _van;
    public static MelonPreferences_Entry<StorageEntityValues>? _deliveryVan;

    public static void Init()
    {
        _carCategory = MelonPreferences.CreateCategory("Cars");
        _carCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Cars.cfg"), true, false);

        _coupe = _carCategory.CreateEntry("Coupe", new StorageEntityValues(4));
        _hotBox = _carCategory.CreateEntry("Hotbox", new StorageEntityValues(5));
        _pickup = _carCategory.CreateEntry("Pickup", new StorageEntityValues(8));
        _sedan = _carCategory.CreateEntry("Sedan", new StorageEntityValues(5));
        _shitBox = _carCategory.CreateEntry("Shitbox", new StorageEntityValues(5));
        _suv = _carCategory.CreateEntry("SUV", new StorageEntityValues(5));
        _van = _carCategory.CreateEntry("Van", new StorageEntityValues(16, 2));
        _deliveryVan = _carCategory.CreateEntry("Delivery Van", new StorageEntityValues(16, 2));

        Utils.MaxSlotAmount = Math.Max(Utils.MaxSlotAmount,
            ((List<MelonPreferences_Entry<StorageEntityValues>>)
            [
                _coupe!,
                _hotBox!,
                _pickup!,
                _sedan!,
                _shitBox!,
                _suv!,
                _van!,
                _deliveryVan!
            ]).Max(x => x.Value.SlotAmount));

        _carCategory.SaveToFile(false);
    }

    public static StorageEntityValues? GetValues(StorageEntity storageEntity) =>
        storageEntity.name switch
        {
            _ when storageEntity.name.Contains("Shitbox") => _shitBox!.Value,
            _ when storageEntity.name.Contains("BoxSUV") => _hotBox!.Value,
            _ when storageEntity.name.Contains("SUV") => _suv!.Value,
            _ when storageEntity.name.Contains("Sedan") => _sedan!.Value,
            _ when storageEntity.name.Contains("Pickup") => _pickup!.Value,
            _ when storageEntity.name.Contains("Coupe") => _coupe!.Value,
            _ when storageEntity.name.Contains("Van") => _van!.Value,
            _ when storageEntity.GetComponentInParent<DeliveryVehicle>() => _deliveryVan!.Value,
            _ => null
        };
}