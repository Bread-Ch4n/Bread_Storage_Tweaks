using MelonLoader.Utils;

namespace Bread_Storage_Tweaks;

public static class Utils
{
    public static readonly string PreferencePath =
        Path.Combine(MelonEnvironment.UserDataDirectory, "Bread_Storage_Tweaks");

    public static int MaxSlotAmount;
}

public class StorageEntityValues
{
    public int RowAmount;
    public int SlotAmount;

    public StorageEntityValues(int slotAmount, int rowAmount = 1)
    {
        RowAmount = rowAmount;
        SlotAmount = slotAmount;
    }
}