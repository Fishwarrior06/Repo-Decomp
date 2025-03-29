using HarmonyLib;
using System.Collections.Generic;

[HarmonyPatch(typeof(ShopManager), "GetAllItemsFromStatsManager")]
public class ShopManagerPatch
{
    static void Postfix(ShopManager __instance)
    {
        // Itera por todos los ítems en la tienda
        foreach (var item in StatsManager.instance.itemDictionary.Values)
        {
            // Si el nombre del ítem es "shotgun", lo agregamos manualmente a la tienda
            if (item.itemAssetName == "shotgun")
            {
                Plugin.Logger.LogInfo("Agregando shotgun extra a la tienda!");

                // Asegúrate de agregarlo a la lista adecuada
                __instance.potentialItems.Add(item); 
            }
        }
    }
}