using MelonLoader;

namespace Bread_Storage_Tweaks.Preferences.Classes;

public static class Extra
{
    private static MelonPreferences_Category? _extraSlotAmountCategory;
    public static MelonPreferences_Entry<int>? BriefcaseSlotAmount;
    public static MelonPreferences_Entry<int>? CoffeeTableSlotAmount;
    public static MelonPreferences_Entry<int>? DeadDropSlotAmount;
    public static MelonPreferences_Entry<int>? DealerSlotAmount;
    public static MelonPreferences_Entry<int>? DeliveryBaySlotAmount;
    public static MelonPreferences_Entry<int>? DeliveryVehicleSlotAmount;
    public static MelonPreferences_Entry<int>? DisplayCabinetSlotAmount;
    public static MelonPreferences_Entry<int>? SafeSlotAmount;
    public static MelonPreferences_Entry<int>? TableSlotAmount;
    public static MelonPreferences_Entry<int>? WallMountedShelfSlotAmount;

    private static MelonPreferences_Category? _extraRowAmountCategory;
    public static MelonPreferences_Entry<int>? BriefcaseRowAmount;
    public static MelonPreferences_Entry<int>? CoffeeRowAmount;
    public static MelonPreferences_Entry<int>? DeadDropRowAmount;
    public static MelonPreferences_Entry<int>? DealerRowAmount;
    public static MelonPreferences_Entry<int>? DeliveryBayRowAmount;
    public static MelonPreferences_Entry<int>? DeliveryVehicleRowAmount;
    public static MelonPreferences_Entry<int>? DisplayCabinetRowAmount;
    public static MelonPreferences_Entry<int>? SafeRowAmount;
    public static MelonPreferences_Entry<int>? TableRowAmount;
    public static MelonPreferences_Entry<int>? WallMountedShelfRowAmount;

    public static void Init()
    {
        _extraSlotAmountCategory = MelonPreferences.CreateCategory("Extra Slots");
        _extraSlotAmountCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Extra.cfg"));

        BriefcaseSlotAmount = _extraSlotAmountCategory.CreateEntry("Briefcase Slot Amount", 4);
        CoffeeTableSlotAmount = _extraSlotAmountCategory.CreateEntry("Coffee Table Slot Amount", 3);
        DeadDropSlotAmount = _extraSlotAmountCategory.CreateEntry("Dead Drop Slot Amount", 5);
        DealerSlotAmount = _extraSlotAmountCategory.CreateEntry("Dealer Inventory Slot Amount", 5);
        DeliveryBaySlotAmount = _extraSlotAmountCategory.CreateEntry("Delivery Bay Slot Amount", 5);
        DeliveryVehicleSlotAmount = _extraSlotAmountCategory.CreateEntry("Delivery Vehicle Slot Amount", 16);
        DisplayCabinetSlotAmount = _extraSlotAmountCategory.CreateEntry("Display Cabinet Slot Amount", 4);
        SafeSlotAmount = _extraSlotAmountCategory.CreateEntry("Safe Slot Amount", 10);
        TableSlotAmount = _extraSlotAmountCategory.CreateEntry("Table Slot Amount", 3);
        WallMountedShelfSlotAmount = _extraSlotAmountCategory.CreateEntry("Wall-Mounted Shelf Slot Amount", 4);

        _extraRowAmountCategory = MelonPreferences.CreateCategory("Extra Rows");
        _extraRowAmountCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Extra.cfg"));

        BriefcaseRowAmount = _extraRowAmountCategory.CreateEntry("Briefcase Row Amount", 1);
        CoffeeRowAmount = _extraRowAmountCategory.CreateEntry("Coffee Table Row Amount", 1);
        DeadDropRowAmount = _extraRowAmountCategory.CreateEntry("Dead Drop Row Amount", 1);
        DealerRowAmount = _extraRowAmountCategory.CreateEntry("Dealer Inventory Row Amount", 1);
        DeliveryBayRowAmount = _extraRowAmountCategory.CreateEntry("Delivery Bay Row Amount", 1);
        DeliveryVehicleRowAmount = _extraRowAmountCategory.CreateEntry("Delivery Vehicle Row Amount", 2);
        DisplayCabinetRowAmount = _extraRowAmountCategory.CreateEntry("Display Cabinet Row Amount", 1);
        SafeRowAmount =
            _extraRowAmountCategory.CreateEntry("Safe Row Amount", 1, oldIdentifier: "Safe Slot Row Amount");
        TableRowAmount =
            _extraRowAmountCategory.CreateEntry("Table Row Amount", 1, oldIdentifier: "Table Slot Row Amount");
        WallMountedShelfRowAmount = _extraRowAmountCategory.CreateEntry("Wall-Mounted Shelf Row Amount", 1);

        Utils.MaxSlotAmount = Math.Max(Utils.MaxSlotAmount, ((List<MelonPreferences_Entry<int>>)
        [
            BriefcaseSlotAmount!, CoffeeTableSlotAmount!, DeadDropSlotAmount!,
            DealerSlotAmount!, DeliveryBaySlotAmount!, DeliveryVehicleSlotAmount!, DisplayCabinetSlotAmount!,
            SafeSlotAmount!, TableSlotAmount!, WallMountedShelfSlotAmount!
        ]).Max(x => x.Value));
    }
}