using Bread_Storage_Tweaks.Preferences;
using Bread_Storage_Tweaks.Preferences.Classes;
using HarmonyLib;
using Il2CppScheduleOne.Economy;
using Il2CppScheduleOne.ItemFramework;
using Il2CppScheduleOne.NPCs;
using Il2CppScheduleOne.Storage;
using Il2CppScheduleOne.UI;
using MelonLoader;

namespace Bread_Storage_Tweaks;

public class Main : MelonMod
{
    public override void OnInitializeMelon()
    {
        PreferenceManager.Init();

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

            __instance.SlotCount = entityName switch
            {
                "Briefcase" => Extra.BriefcaseSa!.Value,
                "Large Storage Rack" => StorageRacks.LargeRsa!.Value,
                "Medium Storage Rack" => StorageRacks.MediumRsa!.Value,
                "Small Storage Rack" => StorageRacks.SmallRsa!.Value,
                "Delivery Bay" => Extra.DeliveryBaySa!.Value,
                "Dead Drop" => Extra.DeadDropSa!.Value,
                "Safe" => Extra.SafeSa!.Value,
                "Trunk" => Cars.GetSA(__instance),
                _ => __instance.SlotCount
            };

            __instance.DisplayRowCount = entityName switch
            {
                "Briefcase" => Extra.BriefcaseRa!.Value,
                "Large Storage Rack" => StorageRacks.LargeRra!.Value,
                "Medium Storage Rack" => StorageRacks.MediumRra!.Value,
                "Small Storage Rack" => StorageRacks.SmallRra!.Value,
                "Delivery Bay" => Extra.DeliveryBayRa!.Value,
                "Dead Drop" => Extra.DeadDropRa!.Value,
                "Safe" => Extra.SafeRa!.Value,
                "Trunk" => Cars.GetRA(__instance),
                _ => __instance.DisplayRowCount
            };
        }

        [HarmonyPatch(typeof(StorageMenu), "Open", typeof(IItemSlotOwner), typeof(string), typeof(string))]
        [HarmonyPrefix]
        private static bool DealerInvRowPatch(StorageMenu __instance, IItemSlotOwner owner, string title,
            string subtitle)
        {
            var npcInventory = owner.TryCast<NPCInventory>();
            if (npcInventory == null || npcInventory.GetComponentInParent<Dealer>() == null) return true;

            __instance.IsOpen = true;
            __instance.OpenedStorageEntity = null;
            __instance.SlotGridLayout.constraintCount = Extra.DealerRa!.Value;
            __instance.Open(title, subtitle, owner);

            return false;
        }

        [HarmonyPatch(typeof(NPCInventory), "Awake")]
        [HarmonyPrefix]
        private static void DealerInvPatch(NPCInventory __instance) => _ = __instance.GetComponentInParent<Dealer>()
            ? __instance.SlotCount = Extra.DealerSa!.Value
            : (object)null!;
    }
}