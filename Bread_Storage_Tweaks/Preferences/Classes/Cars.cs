using Il2CppScheduleOne.Storage;
using MelonLoader;

namespace Bread_Storage_Tweaks.Preferences.Classes;

public static class Cars
{
    private static MelonPreferences_Category? _carSaCategory;
    public static MelonPreferences_Entry<int>? ShitBoxSa;
    public static MelonPreferences_Entry<int>? Suvsa;
    public static MelonPreferences_Entry<int>? SedanSa;
    public static MelonPreferences_Entry<int>? PickupSa;
    public static MelonPreferences_Entry<int>? CoupeSa;
    public static MelonPreferences_Entry<int>? VanSa;


    private static MelonPreferences_Category? _carRCategory;
    public static MelonPreferences_Entry<int>? ShitBoxRa;
    public static MelonPreferences_Entry<int>? Suvra;
    public static MelonPreferences_Entry<int>? SedanRa;
    public static MelonPreferences_Entry<int>? PickupRa;
    public static MelonPreferences_Entry<int>? CoupeRa;
    public static MelonPreferences_Entry<int>? VanRa;


    public static void Init()
    {
        _carSaCategory = MelonPreferences.CreateCategory("Car Slots");
        _carSaCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Cars.cfg"));

        ShitBoxSa = _carSaCategory.CreateEntry("Shitbox Slot Amount", 5,
            description:
            "Do not make any values smaller or remove the mod. You will loose the items from the extra slots.\nThe max slot value is 20!");
        Suvsa = _carSaCategory.CreateEntry("SUV Slot Amount", 5);
        SedanSa = _carSaCategory.CreateEntry("Sedan Slot Amount", 5);
        PickupSa = _carSaCategory.CreateEntry("Pickup Slot Amount", 8);
        CoupeSa = _carSaCategory.CreateEntry("Coupe Slot Amount", 4);
        VanSa = _carSaCategory.CreateEntry("Van Slot Amount", 16);

        List<MelonPreferences_Entry<int>> sa =
        [
            ShitBoxSa, Suvsa, SedanSa,
            PickupSa, CoupeSa, VanSa
        ];
        foreach (var saEntry in sa.Where(saEntry => saEntry.Value > 20)) saEntry.Value = 20;

        _carRCategory = MelonPreferences.CreateCategory("Car Rows");
        _carRCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Cars.cfg"));

        ShitBoxRa = _carRCategory.CreateEntry("Shitbox Row Amount", 1);
        Suvra = _carRCategory.CreateEntry("SUV Row Amount", 1);
        SedanRa = _carRCategory.CreateEntry("Sedan Amount", 1);
        PickupRa = _carRCategory.CreateEntry("Pickup Row Amount", 2);
        CoupeRa = _carRCategory.CreateEntry("Coupe Row Amount", 1);
        VanRa = _carRCategory.CreateEntry("Van Row Amount", 2);
    }

    public static int GetSA(StorageEntity storageEntity) =>
        storageEntity.name switch
        {
            _ when storageEntity.name.Contains("Shitbox") => ShitBoxSa!.Value,
            _ when storageEntity.name.Contains("SUV") => Suvsa!.Value,
            _ when storageEntity.name.Contains("Sedan") => SedanSa!.Value,
            _ when storageEntity.name.Contains("Pickup") => PickupSa!.Value,
            _ when storageEntity.name.Contains("Coupe") => CoupeSa!.Value,
            _ when storageEntity.name.Contains("Van") => VanSa!.Value,
            _ => storageEntity.SlotCount
        };

    public static int GetRA(StorageEntity storageEntity) =>
        storageEntity.name switch
        {
            _ when storageEntity.name.Contains("Shitbox") => ShitBoxRa!.Value,
            _ when storageEntity.name.Contains("SUV") => Suvra!.Value,
            _ when storageEntity.name.Contains("Sedan") => SedanRa!.Value,
            _ when storageEntity.name.Contains("Pickup") => PickupRa!.Value,
            _ when storageEntity.name.Contains("Coupe") => CoupeRa!.Value,
            _ when storageEntity.name.Contains("Van") => VanRa!.Value,
            _ => storageEntity.DisplayRowCount
        };
}