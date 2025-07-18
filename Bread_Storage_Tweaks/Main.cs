﻿using Bread_Storage_Tweaks.Preferences;
using Bread_Storage_Tweaks.Preferences.Classes;
using HarmonyLib;
using Il2CppScheduleOne.Economy;
using Il2CppScheduleOne.Employees;
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

            __instance.SlotCount = entityName switch
            {
                "Briefcase" => Extra.BriefcaseSlotAmount!.Value,
                "Coffee Table" => Extra.CoffeeTableSlotAmount!.Value,
                "Dead Drop" => Extra.DeadDropSlotAmount!.Value,
                "Delivery Bay" => Extra.DeliveryBaySlotAmount!.Value,
                "Display Cabinet" => Extra.DisplayCabinetSlotAmount!.Value,
                "Large Storage Rack" => StorageRacks.LargeRackSlotAmount!.Value,
                "Locker" => Extra.LockerSlotAmount!.Value,
                "Medium Storage Rack" => StorageRacks.MediumRackSlotAmount!.Value,
                "Safe" => Extra.SafeSlotAmount!.Value,
                "Shitbox Trunk" => Cars.ShitBoxSlotAmount!.Value,
                "Small Storage Rack" => StorageRacks.SmallRackSlotAmount!.Value,
                "Table" => Extra.TableSlotAmount!.Value,
                "Trunk" => Cars.GetSA(__instance),
                "Wall-Mounted Shelf" => Extra.WallMountedShelfSlotAmount!.Value,
                _ => __instance.SlotCount
            };

            __instance.DisplayRowCount = entityName switch
            {
                "Briefcase" => Extra.BriefcaseRowAmount!.Value,
                "Coffee Table" => Extra.CoffeeRowAmount!.Value,
                "Dead Drop" => Extra.DeadDropRowAmount!.Value,
                "Delivery Bay" => Extra.DeliveryBayRowAmount!.Value,
                "Display Cabinet" => Extra.DisplayCabinetRowAmount!.Value,
                "Large Storage Rack" => StorageRacks.LargeRackRowAmount!.Value,
                "Locker" => Extra.LockerRowAmount!.Value,
                "Medium Storage Rack" => StorageRacks.MediumRackRowAmount!.Value,
                "Safe" => Extra.SafeRowAmount!.Value,
                "Shitbox Trunk" => Cars.ShitBoxRowAmount!.Value,
                "Small Storage Rack" => StorageRacks.SmallRackRowAmount!.Value,
                "Table" => Extra.TableRowAmount!.Value,
                "Trunk" => Cars.GetRA(__instance),
                "Wall-Mounted Shelf" => Extra.WallMountedShelfRowAmount!.Value,
                _ => __instance.DisplayRowCount
            };
        }

        [HarmonyPatch(typeof(NPCInventory), "Awake")]
        [HarmonyPrefix]
        private static void NPCSlotPatch(NPCInventory __instance)
        {
            if (__instance == null) return;

            if (__instance.GetComponentInParent<Botanist>())
                __instance.SlotCount = Employees.BotanistSlotAmount!.Value;
            else if (__instance.GetComponentInParent<Chemist>())
                __instance.SlotCount = Employees.ChemistSlotAmount!.Value;
            else if (__instance.GetComponentInParent<Cleaner>())
                __instance.SlotCount = Employees.CleanerSlotAmount!.Value;
            else if (__instance.GetComponentInParent<Dealer>())
                __instance.SlotCount = Extra.DealerSlotAmount!.Value;
            else if (__instance.GetComponentInParent<Packager>())
                __instance.SlotCount = Employees.HandlerSlotAmount!.Value;
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

            if (npcInventory.GetComponentInParent<Botanist>())
                __instance.SlotGridLayout.constraintCount = Employees.BotanistRowAmount!.Value;
            else if (npcInventory.GetComponentInParent<Chemist>())
                __instance.SlotGridLayout.constraintCount = Employees.ChemistRowAmount!.Value;
            else if (npcInventory.GetComponentInParent<Cleaner>())
                __instance.SlotGridLayout.constraintCount = Employees.CleanerRowAmount!.Value;
            else if (npcInventory.GetComponentInParent<Dealer>())
                __instance.SlotGridLayout.constraintCount = Extra.DealerRowAmount!.Value;
            else if (npcInventory.GetComponentInParent<Packager>())
                __instance.SlotGridLayout.constraintCount = Employees.HandlerRowAmount!.Value;
            else __instance.SlotGridLayout.constraintCount = __instance.SlotGridLayout.constraintCount;

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

            __result = totalStacks <= Extra.DeliveryVehicleSlotAmount!.Value;
            return false;

            // var num1 = 0;
            // foreach (var listingEntry in __instance.listingEntries)
            //     if (listingEntry.SelectedQuantity != 0)
            //     {
            //         var num2 = listingEntry.SelectedQuantity;
            //         var stackLimit = listingEntry.MatchingListing.Item.StackLimit;
            //         while (num2 > 0)
            //         {
            //             if (num2 > stackLimit)
            //                 num2 -= stackLimit;
            //             else
            //                 num2 = 0;
            //             ++num1;
            //         }
            //     }
            //
            // __result = num1 <= Extra.DeliveryVehicleSlotAmount!.Value;
            // return false;
        }
    }
}