using MelonLoader;

namespace Bread_Storage_Tweaks.Preferences.Classes;

public static class Extra
{
    private static MelonPreferences_Category? _extraSaCategory;
    public static MelonPreferences_Entry<int>? BriefcaseSa;
    public static MelonPreferences_Entry<int>? DealerSa;
    public static MelonPreferences_Entry<int>? DeliveryBaySa;
    public static MelonPreferences_Entry<int>? DeadDropSa;
    public static MelonPreferences_Entry<int>? SafeSa;


    private static MelonPreferences_Category? _extraRCategory;
    public static MelonPreferences_Entry<int>? BriefcaseRa;
    public static MelonPreferences_Entry<int>? DealerRa;
    public static MelonPreferences_Entry<int>? DeliveryBayRa;
    public static MelonPreferences_Entry<int>? DeadDropRa;
    public static MelonPreferences_Entry<int>? SafeRa;

    public static void Init()
    {
        _extraSaCategory = MelonPreferences.CreateCategory("Extra Slots");
        _extraSaCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Extra.cfg"));

        BriefcaseSa = _extraSaCategory.CreateEntry("Briefcase Slot Amount", 4,
            description:
            "Do not make any values smaller or remove the mod. You will loose the items from the extra slots.\nThe max slot value is 20!");
        DealerSa = _extraSaCategory.CreateEntry("Dealer Inventory Slot Amount", 5);
        DeliveryBaySa = _extraSaCategory.CreateEntry("Delivery Bay Slot Amount", 5);
        DeadDropSa = _extraSaCategory.CreateEntry("Dead Drop Slot Amount", 5);
        SafeSa = _extraSaCategory.CreateEntry("Safe Slot Amount", 10);

        List<MelonPreferences_Entry<int>> sa =
        [
            BriefcaseSa, DealerSa, DeliveryBaySa,
            DeadDropSa, SafeSa
        ];
        foreach (var saEntry in sa.Where(saEntry => saEntry.Value > 20)) saEntry.Value = 20;

        _extraRCategory = MelonPreferences.CreateCategory("Extra Rows");
        _extraRCategory.SetFilePath(Path.Combine(Utils.PreferencePath, "Extra.cfg"));

        BriefcaseRa = _extraRCategory.CreateEntry("Briefcase Row Amount", 1);
        DealerRa = _extraRCategory.CreateEntry("Dealer Inventory Row Amount", 1);
        DeliveryBayRa = _extraRCategory.CreateEntry("Delivery Bay Row Amount", 1);
        DeadDropRa = _extraRCategory.CreateEntry("Dead Drop Row Amount", 1);
        SafeRa = _extraRCategory.CreateEntry("Safe Slot Row Amount", 1);
    }
}