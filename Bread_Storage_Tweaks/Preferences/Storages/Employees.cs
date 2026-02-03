using Il2CppScheduleOne.Economy;
using Il2CppScheduleOne.Employees;
using Il2CppScheduleOne.NPCs;
using MelonLoader;

namespace Bread_Storage_Tweaks.Preferences.Storages;

public static class Employees
{
    private static MelonPreferences_Category? _employeeCategory;

    private static MelonPreferences_Entry<StorageEntityValues>? _botanist;
    private static MelonPreferences_Entry<StorageEntityValues>? _chemist;
    private static MelonPreferences_Entry<StorageEntityValues>? _cleaner;
    private static MelonPreferences_Entry<StorageEntityValues>? _handler;
    private static MelonPreferences_Entry<StorageEntityValues>? _dealer;

    public static void Init()
    {
        _employeeCategory = MelonPreferences.CreateCategory("Employees");
        _employeeCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Employees.cfg"), true, false);

        _botanist = _employeeCategory.CreateEntry("Botanist", new StorageEntityValues(5));
        _chemist = _employeeCategory.CreateEntry("Chemist", new StorageEntityValues(5));
        _cleaner = _employeeCategory.CreateEntry("Cleaner", new StorageEntityValues(5));
        _handler = _employeeCategory.CreateEntry("Handler", new StorageEntityValues(5));
        _dealer = _employeeCategory.CreateEntry("Dealer", new StorageEntityValues(5));

        Utils.MaxSlotAmount = Math.Max(Utils.MaxSlotAmount,
            ((List<MelonPreferences_Entry<StorageEntityValues>>)
                [_botanist!, _chemist!, _cleaner!, _handler!, _dealer!]).Max(x => x.Value.SlotAmount));

        _employeeCategory.SaveToFile(false);
    }

    public static StorageEntityValues? GetValues(NPCInventory npc)
    {
        if (npc.GetComponentInParent<Botanist>())
            return _botanist!.Value;
        if (npc.GetComponentInParent<Chemist>())
            return _chemist!.Value;
        if (npc.GetComponentInParent<Cleaner>())
            return _cleaner!.Value;
        if (npc.GetComponentInParent<Packager>())
            return _handler!.Value;
        if (npc.GetComponentInParent<Dealer>())
            return _dealer!.Value;
        return null;
    }
}