using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System;


namespace REPO_Rebalanced;
[HarmonyPatch(typeof(ShopManager), "GetAllItemsFromStatsManager")]
public class ShopManagerPatch
    {
        static void Postfix(ShopManager __instance)
        {
            // Itera por todos los ítems en la tienda
            foreach (var item in StatsManager.instance.itemDictionary.Values)
            {
                // Si el nombre del ítem es "shotgun", lo agregamos manualmente a la tienda
                if (item.itemAssetName == "Item Gun Shotgun")
                {
                    Plugin.Logger.LogInfo("Agregando shotgun extra a la tienda!");

                    // Generamos un número aleatorio entre 0 y 2 usando System.Random
                    int shotgunCount = new Random().Next(0, 3); // 0, 1 o 2 shotguns

                    // Añadir el número aleatorio de shotguns a la tienda
                    for (int i = 0; i < shotgunCount; i++)
                    {
                        __instance.potentialItems.Add(item);
                    }
                }
            }
        }
    }