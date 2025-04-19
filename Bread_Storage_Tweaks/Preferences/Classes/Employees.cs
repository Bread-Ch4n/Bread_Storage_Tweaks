using MelonLoader;

namespace Bread_Storage_Tweaks.Preferences.Classes;

public static class Employees
{
    private static MelonPreferences_Category? _employeesSlotAmountCategory;
    public static MelonPreferences_Entry<int>? BotanistSlotAmount;
    public static MelonPreferences_Entry<int>? ChemistSlotAmount;
    public static MelonPreferences_Entry<int>? CleanerSlotAmount;
    public static MelonPreferences_Entry<int>? HandlerSlotAmount;


    private static MelonPreferences_Category? _employeesRowAmountCategory;
    public static MelonPreferences_Entry<int>? BotanistRowAmount;
    public static MelonPreferences_Entry<int>? ChemistRowAmount;
    public static MelonPreferences_Entry<int>? CleanerRowAmount;
    public static MelonPreferences_Entry<int>? HandlerRowAmount;

    public static void Init()
    {
        _employeesSlotAmountCategory = MelonPreferences.CreateCategory("Employee Slots");
        _employeesSlotAmountCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Employees.cfg"));

        BotanistSlotAmount = _employeesSlotAmountCategory.CreateEntry("Botanist Slot Amount", 5,
            description:
            "Do not make any values smaller or remove the mod. You will loose the items from the extra slots.\nThe max slot value is 20!");
        ChemistSlotAmount = _employeesSlotAmountCategory.CreateEntry("Chemist Slot Amount", 5);
        CleanerSlotAmount = _employeesSlotAmountCategory.CreateEntry("Cleaner Slot Amount", 5);
        HandlerSlotAmount = _employeesSlotAmountCategory.CreateEntry("Handler Slot Amount", 5);

        List<MelonPreferences_Entry<int>> sa =
        [
            HandlerSlotAmount, ChemistSlotAmount, BotanistSlotAmount, CleanerSlotAmount
        ];
        foreach (var saEntry in sa.Where(saEntry => saEntry.Value > 20)) saEntry.Value = 20;

        _employeesRowAmountCategory = MelonPreferences.CreateCategory("Employee Rows");
        _employeesRowAmountCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Employees.cfg"));

        BotanistRowAmount = _employeesRowAmountCategory.CreateEntry("Botanist Amount", 1);
        ChemistRowAmount = _employeesRowAmountCategory.CreateEntry("Chemist Row Amount", 1);
        CleanerRowAmount = _employeesRowAmountCategory.CreateEntry("Cleaner Row Amount", 1);
        HandlerRowAmount = _employeesRowAmountCategory.CreateEntry("Handler Row Amount", 1);
    }
}