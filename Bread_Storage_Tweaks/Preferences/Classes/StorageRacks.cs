using MelonLoader;

namespace Bread_Storage_Tweaks.Preferences.Classes;

public static class StorageRacks
{
    private static MelonPreferences_Category? _storageRackSlotAmountCategory;
    public static MelonPreferences_Entry<int>? LargeRackSlotAmount;
    public static MelonPreferences_Entry<int>? MediumRackSlotAmount;
    public static MelonPreferences_Entry<int>? SmallRackSlotAmount;

    private static MelonPreferences_Category? _storageRackRowAmountCategory;
    public static MelonPreferences_Entry<int>? LargeRackRowAmount;
    public static MelonPreferences_Entry<int>? MediumRackRowAmount;
    public static MelonPreferences_Entry<int>? SmallRackRowAmount;

    public static void Init()
    {
        _storageRackSlotAmountCategory = MelonPreferences.CreateCategory("Storage Rack Slots");
        _storageRackSlotAmountCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Storage_Racks.cfg"));

        LargeRackSlotAmount = _storageRackSlotAmountCategory.CreateEntry("Large Storage Rack Slot Amount", 8);
        MediumRackSlotAmount = _storageRackSlotAmountCategory.CreateEntry("Medium Storage Rack Slot Amount", 6);
        SmallRackSlotAmount = _storageRackSlotAmountCategory.CreateEntry("Small Storage Rack Slot Amount", 4);

        _storageRackRowAmountCategory = MelonPreferences.CreateCategory("Storage Rack Rows");
        _storageRackRowAmountCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Storage_Racks.cfg"));

        LargeRackRowAmount = _storageRackRowAmountCategory.CreateEntry("Large Storage Rack Row Amount", 2);
        MediumRackRowAmount = _storageRackRowAmountCategory.CreateEntry("Medium Storage Rack Row Amount",
            2
        );
        SmallRackRowAmount = _storageRackRowAmountCategory.CreateEntry("Small Storage Rack Row Amount", 2);

        Utils.MaxSlotAmount = Math.Max(Utils.MaxSlotAmount, ((List<MelonPreferences_Entry<int>>)
        [
            LargeRackSlotAmount!, MediumRackSlotAmount!, SmallRackSlotAmount!
        ]).Max(x => x.Value));
    }
}