using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

namespace REPO_Rebalanced;
[HarmonyPatch(typeof(ShopManager), "GetAllItemsFromStatsManager")]
public class ShopManagerPatch
{
    static void Postfix(ShopManager __instance)
    {
        var shotgun = StatsManager.instance.itemDictionary.Values
            .FirstOrDefault(item => item.itemAssetName == "Item Gun Shotgun");

        if (shotgun == null)
        {
            Plugin.Logger.LogWarning("No se encontró la shotgun en itemDictionary.");
            return;
        }

        // Agregar la shotgun dos veces para que pueda aparecer hasta 2 veces
        __instance.potentialItems.Add(shotgun);
        __instance.potentialItems.Add(shotgun);

        Plugin.Logger.LogInfo("Se agregaron 2 instancias de la shotgun a la tienda.");
    }
}