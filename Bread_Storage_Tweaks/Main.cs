using Bread_Storage_Tweaks.Preferences;
using Bread_Storage_Tweaks.Preferences.Storages;
using HarmonyLib;
using Il2CppScheduleOne.ItemFramework;
using Il2CppScheduleOne.NPCs;
using Il2CppScheduleOne.Storage;
using Il2CppScheduleOne.UI;
using Il2CppScheduleOne.UI.Phone.Delivery;
using MelonLoader;
using UnityEngine;
using Object = UnityEngine.Object;

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

            StorageEntityValues? values = null;

            if (Cars.GetValues(__instance) != null) values = Cars.GetValues(__instance);
            else if (Closets.GetValues(__instance) != null) values = Closets.GetValues(__instance);
            else if (Extra.GetValues(__instance) != null) values = Extra.GetValues(__instance);
            else if (StorageRacks.GetValues(__instance) != null) values = StorageRacks.GetValues(__instance);

            values ??= new StorageEntityValues(__instance.SlotCount, __instance.DisplayRowCount);

            __instance.SlotCount = values.SlotAmount;
            __instance.DisplayRowCount = values.RowAmount;
        }

        [HarmonyPatch(typeof(NPCInventory), "Awake")]
        [HarmonyPrefix]
        private static void NPCSlotPatch(NPCInventory __instance)
        {
            if (__instance == null) return;

            StorageEntityValues? values = null;

            if (Employees.GetValues(__instance) != null) values = Employees.GetValues(__instance);

            values ??= new StorageEntityValues(__instance.SlotCount);

            __instance.SlotCount = values.SlotAmount;
        }

        [HarmonyPatch(typeof(StorageMenu), "Open", typeof(IItemSlotOwner), typeof(string), typeof(string))]
        [HarmonyPrefix]
        private static bool NPCInvRowPatch(StorageMenu __instance, IItemSlotOwner owner, string title,
            string subtitle)
        {
            var npcInventory = owner.TryCast<NPCInventory>();
            if (npcInventory == null) return true;

            __instance.IsOpen = true;
            __instance.OpenedStorageEntity = null;

            StorageEntityValues? values = null;

            if (Employees.GetValues(npcInventory) != null) values = Employees.GetValues(npcInventory);

            values ??= new StorageEntityValues(npcInventory.SlotCount, __instance.SlotGridLayout.constraintCount);

            __instance.SlotGridLayout.constraintCount = values.RowAmount;

            __instance.Open(title, subtitle, owner);
            return false;
        }

        [HarmonyPatch(typeof(StorageMenu), "Awake")]
        [HarmonyPrefix]
        private static void StorageEntityPatch(StorageMenu __instance)
        {
            if (Utils.MaxSlotAmount <= 20) return;
            var slotsTransform = __instance.Container.gameObject.transform.Find("Slots");
            if (slotsTransform == null) return;

            var slotsUIsList = new List<ItemSlotUI>(__instance.SlotsUIs);

            while (slotsTransform.childCount < Utils.MaxSlotAmount)
                try
                {
                    var clonedSlot = Object.Instantiate(slotsTransform.GetChild(0).gameObject, slotsTransform, true);
                    clonedSlot.name = clonedSlot.name.Replace("Clone", $"Extra-[{slotsTransform.childCount - 1}]");
                    clonedSlot.SetActive(true);
                    clonedSlot.transform.localScale = Vector3.one;

                    slotsUIsList.Add(clonedSlot.GetComponent<ItemSlotUI>() ?? clonedSlot.AddComponent<ItemSlotUI>());
                }
                catch (Exception e)
                {
                    MelonLogger.Error(e);
                }

            __instance.SlotsUIs = slotsUIsList.ToArray();
        }

        [HarmonyPatch(typeof(DeliveryShop), "WillCartFitInVehicle")]
        [HarmonyPrefix]
        private static bool DeliveryVanPatch(DeliveryShop __instance, ref bool __result)
        {
            var totalStacks = 0;

            foreach (var entry in __instance.listingEntries)
            {
                if (entry.SelectedQuantity == 0)
                    continue;

                var quantity = entry.SelectedQuantity;
                var stackLimit = entry.MatchingListing.Item.StackLimit;

                totalStacks += (quantity + stackLimit - 1) / stackLimit;
            }

            __result = totalStacks <= Cars._deliveryVan!.Value.SlotAmount;
            return false;
        }
    }
}