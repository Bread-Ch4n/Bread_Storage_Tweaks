using HarmonyLib;
using Il2CppScheduleOne.Economy;
using Il2CppScheduleOne.ItemFramework;
using Il2CppScheduleOne.NPCs;
using Il2CppScheduleOne.Storage;
using Il2CppScheduleOne.UI;
using MelonLoader;
using MelonLoader.Utils;

namespace Bread_Storage_Tweaks;

public class Main : MelonMod
{
    private static readonly string PreferencePath = Path.Combine(MelonEnvironment.UserDataDirectory, "Bread_Storage_Tweaks");
    private static MelonPreferences_Category? _storageCategory;

    private static MelonPreferences_Entry<int>? _storageBriefcaseSlotAmount;
    private static MelonPreferences_Entry<int>? _storageLargeStorageRackSlotAmount;
    private static MelonPreferences_Entry<int>? _storageMediumStorageRackSlotAmount;
    private static MelonPreferences_Entry<int>? _storageSmallStorageRackSlotAmount;
    private static MelonPreferences_Entry<int>? _storageDealerSlotAmount;

    private static MelonPreferences_Entry<int>? _storageBriefcaseRowAmount;
    private static MelonPreferences_Entry<int>? _storageLargeStorageRackRowAmount;
    private static MelonPreferences_Entry<int>? _storageMediumStorageRackRowAmount;
    private static MelonPreferences_Entry<int>? _storageSmallStorageRackRowAmount;
    private static MelonPreferences_Entry<int>? _storageDealerRowAmount;

    public override void OnInitializeMelon()
    {
        Directory.CreateDirectory(PreferencePath);

        _storageCategory = MelonPreferences.CreateCategory("Storage Tweaks");
        _storageCategory.SetFilePath(Path.Combine(PreferencePath, "Storage.cfg"));

        _storageBriefcaseSlotAmount = _storageCategory.CreateEntry("Briefcase Inventory Slot Amount", 4, description:"Do not make any values smaller or remove the mod. You will loose the items from the extra slots.\nThe max slot value is 20!");
        _storageLargeStorageRackSlotAmount = _storageCategory.CreateEntry("Large Storage Rack Inventory Slot Amount", 8);
        _storageMediumStorageRackSlotAmount = _storageCategory.CreateEntry("Medium Storage Rack Inventory Slot Amount",
            6);
        _storageSmallStorageRackSlotAmount = _storageCategory.CreateEntry("Small Storage Rack Inventory Slot Amount", 4);
        _storageDealerSlotAmount = _storageCategory.CreateEntry("Dealer Inventory Slot Amount", 5);

        if (_storageBriefcaseSlotAmount.Value > 20) _storageBriefcaseSlotAmount.Value = 20;
        if (_storageLargeStorageRackSlotAmount.Value > 20) _storageLargeStorageRackSlotAmount.Value = 20;
        if (_storageMediumStorageRackSlotAmount.Value > 20) _storageMediumStorageRackSlotAmount.Value = 20;
        if (_storageSmallStorageRackSlotAmount.Value > 20) _storageSmallStorageRackSlotAmount.Value = 20;
        if (_storageDealerSlotAmount.Value > 20) _storageDealerSlotAmount.Value = 20;

        _storageBriefcaseRowAmount = _storageCategory.CreateEntry("Briefcase Inventory Row Amount", 1);
        _storageLargeStorageRackRowAmount = _storageCategory.CreateEntry("Large Storage Rack Inventory Row Amount", 2);
        _storageMediumStorageRackRowAmount = _storageCategory.CreateEntry("Medium Storage Rack Inventory Row Amount",
            2
            );
        _storageSmallStorageRackRowAmount = _storageCategory.CreateEntry("Small Storage Rack Inventory Row Amount", 2);
        _storageDealerRowAmount = _storageCategory.CreateEntry("Dealer Inventory Row Amount", 1);

        var h = new HarmonyLib.Harmony("Bread_Storage_Tweaks");

       h.PatchAll(typeof(StoragePatches));
    }

    private class StoragePatches
    {
        [HarmonyPatch(typeof(StorageEntity), "Awake")]
        [HarmonyPrefix]
        private static void StoragePatch(StorageEntity __instance)
        {
            if (__instance == null) return;

            var entityName = __instance.StorageEntityName;
            if (string.IsNullOrEmpty(entityName)) return;

            __instance.DisplayRowCount = entityName switch
            {
                "Briefcase" => _storageBriefcaseRowAmount!.Value,
                "Large Storage Rack" => _storageLargeStorageRackRowAmount!.Value,
                "Medium Storage Rack" => _storageMediumStorageRackRowAmount!.Value,
                "Small Storage Rack" => _storageSmallStorageRackRowAmount!.Value,
                _ => __instance.DisplayRowCount
            };

            __instance.SlotCount = entityName switch
            {
                "Briefcase" => _storageBriefcaseSlotAmount!.Value,
                "Large Storage Rack" => _storageLargeStorageRackSlotAmount!.Value,
                "Medium Storage Rack" => _storageMediumStorageRackSlotAmount!.Value,
                "Small Storage Rack" => _storageSmallStorageRackSlotAmount!.Value,
                _ => __instance.SlotCount
            };
        }

        [HarmonyPatch(typeof(StorageMenu), "Open", typeof(IItemSlotOwner), typeof(string), typeof(string))]
        [HarmonyPrefix]
        private static bool DealerInvRowPatch(StorageMenu __instance, IItemSlotOwner owner, string title, string subtitle)
        {
            var npcInventory = owner.TryCast<NPCInventory>();
            if (npcInventory == null || npcInventory.GetComponentInParent<Dealer>() == null) return true;

            __instance.IsOpen = true;
            __instance.OpenedStorageEntity = null;
            __instance.SlotGridLayout.constraintCount = _storageDealerRowAmount!.Value;
            __instance.Open(title, subtitle, owner);

            return false;
        }

        [HarmonyPatch(typeof(NPCInventory), "Awake")]
        [HarmonyPrefix]
        private static void DealerInvPatch(NPCInventory __instance) => _ = __instance.GetComponentInParent<Dealer>()
            ? __instance.SlotCount = _storageDealerSlotAmount!.Value
            : (object)null!;

    }
}
