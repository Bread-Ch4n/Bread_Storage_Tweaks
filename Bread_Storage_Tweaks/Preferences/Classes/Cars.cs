using Il2CppScheduleOne.Delivery;
using Il2CppScheduleOne.Storage;
using MelonLoader;

namespace Bread_Storage_Tweaks.Preferences.Classes;

public static class Cars
{
    private static MelonPreferences_Category? _carSlotAmountCategory;
    private static MelonPreferences_Entry<int>? _coupeSlotAmount;
    private static MelonPreferences_Entry<int>? _pickupSlotAmount;
    private static MelonPreferences_Entry<int>? _sedanSlotAmount;
    private static MelonPreferences_Entry<int>? _suvSlotAmount;
    private static MelonPreferences_Entry<int>? _vanSlotAmount;
    public static MelonPreferences_Entry<int>? ShitBoxSlotAmount;

    private static MelonPreferences_Category? _carRowAmountCategory;
    private static MelonPreferences_Entry<int>? _coupeRowAmount;
    private static MelonPreferences_Entry<int>? _pickupRowAmount;
    private static MelonPreferences_Entry<int>? _sedanRowAmount;
    private static MelonPreferences_Entry<int>? _suvRowAmount;
    private static MelonPreferences_Entry<int>? _vanRowAmount;
    public static MelonPreferences_Entry<int>? ShitBoxRowAmount;


    public static void Init()
    {
        _carSlotAmountCategory = MelonPreferences.CreateCategory("Car Slots");
        _carSlotAmountCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Cars.cfg"));

        ShitBoxSlotAmount = _carSlotAmountCategory.CreateEntry("Shitbox Slot Amount", 5);
        _coupeSlotAmount = _carSlotAmountCategory.CreateEntry("Coupe Slot Amount", 4);
        _pickupSlotAmount = _carSlotAmountCategory.CreateEntry("Pickup Slot Amount", 8);
        _sedanSlotAmount = _carSlotAmountCategory.CreateEntry("Sedan Slot Amount", 5);
        _suvSlotAmount = _carSlotAmountCategory.CreateEntry("SUV Slot Amount", 5);
        _vanSlotAmount = _carSlotAmountCategory.CreateEntry("Van Slot Amount", 16);

        _carRowAmountCategory = MelonPreferences.CreateCategory("Car Rows");
        _carRowAmountCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Cars.cfg"));

        ShitBoxRowAmount = _carRowAmountCategory.CreateEntry("Shitbox Row Amount", 1);
        _coupeRowAmount = _carRowAmountCategory.CreateEntry("Coupe Row Amount", 1);
        _pickupRowAmount = _carRowAmountCategory.CreateEntry("Pickup Row Amount", 2);
        _sedanRowAmount = _carRowAmountCategory.CreateEntry("Sedan Amount", 1);
        _suvRowAmount = _carRowAmountCategory.CreateEntry("SUV Row Amount", 1);
        _vanRowAmount = _carRowAmountCategory.CreateEntry("Van Row Amount", 2);

        Utils.MaxSlotAmount = Math.Max(Utils.MaxSlotAmount,
            ((List<MelonPreferences_Entry<int>>)
            [
                ShitBoxSlotAmount!,
                _coupeSlotAmount!,
                _pickupSlotAmount!,
                _sedanSlotAmount!,
                _suvSlotAmount!,
                _vanSlotAmount!
            ]).Max(x => x.Value));
    }

    public static int GetSA(StorageEntity storageEntity) =>
        storageEntity.name switch
        {
            _ when storageEntity.name.Contains("Shitbox") => ShitBoxSlotAmount!.Value,
            _ when storageEntity.name.Contains("SUV") => _suvSlotAmount!.Value,
            _ when storageEntity.name.Contains("Sedan") => _sedanSlotAmount!.Value,
            _ when storageEntity.name.Contains("Pickup") => _pickupSlotAmount!.Value,
            _ when storageEntity.name.Contains("Coupe") => _coupeSlotAmount!.Value,
            _ when storageEntity.name.Contains("Van") => _vanSlotAmount!.Value,
            _ when storageEntity.GetComponentInParent<DeliveryVehicle>() => Extra.DeliveryVehicleSlotAmount!.Value,
            _ => storageEntity.SlotCount
        };

    public static int GetRA(StorageEntity storageEntity) =>
        storageEntity.name switch
        {
            _ when storageEntity.name.Contains("Shitbox") => ShitBoxRowAmount!.Value,
            _ when storageEntity.name.Contains("SUV") => _suvRowAmount!.Value,
            _ when storageEntity.name.Contains("Sedan") => _sedanRowAmount!.Value,
            _ when storageEntity.name.Contains("Pickup") => _pickupRowAmount!.Value,
            _ when storageEntity.name.Contains("Coupe") => _coupeRowAmount!.Value,
            _ when storageEntity.name.Contains("Van") => _vanRowAmount!.Value,
            _ when storageEntity.GetComponentInParent<DeliveryVehicle>() => Extra.DeliveryVehicleRowAmount!.Value,
            _ => storageEntity.DisplayRowCount
        };
}