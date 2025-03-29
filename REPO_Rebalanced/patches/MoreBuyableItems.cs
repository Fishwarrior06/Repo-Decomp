using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace REPO_Rebalanced
{
    // Clase auxiliar para mantener la información del ítem y su probabilidad de aparición
    public class ItemSpawnInfo
    {
        public string ItemName { get; set; }
        public float SpawnChance { get; set; }

        public ItemSpawnInfo(string itemName, float spawnChance)
        {
            ItemName = itemName;
            SpawnChance = spawnChance;
        }
    }

    [HarmonyPatch(typeof(ShopManager), "GetAllItemsFromStatsManager")]
    public class ShopManagerPatch
    {
        // Declarar la lista como estática para que sea accesible desde otras clases
        public static List<ItemSpawnInfo> itemSpawnInfos = new List<ItemSpawnInfo>
        {
            new ItemSpawnInfo("Item Drone Battery", 0.3f),
            new ItemSpawnInfo("Item Drone Feather", 0.3f),
            new ItemSpawnInfo("Item Drone Indestructible", 0.3f),
            new ItemSpawnInfo("Item Drone Torque", 0.3f),
            new ItemSpawnInfo("Item Drone Zero Gravity", 0.5f),
            new ItemSpawnInfo("Item Extraction Tracker", 0.5f),
            new ItemSpawnInfo("Item Gun Handgun", 0.6f),
            new ItemSpawnInfo("Item Gun Shotgun", 0.3f),
            new ItemSpawnInfo("Item Gun Tranq", 0.5f),
            new ItemSpawnInfo("Item Melee Baseball Bat", 0.7f),
            new ItemSpawnInfo("Item Melee Frying Pan", 0.6f),
            new ItemSpawnInfo("Item Melee Sledge Hammer", 0.5f),
            new ItemSpawnInfo("Item Melee Sword", 0.5f)
        };

        static void Postfix(ShopManager __instance)
        {
            System.Random rng = new System.Random();

            foreach (var item in StatsManager.instance.itemDictionary.Values)
            {
                var itemInfo = itemSpawnInfos.FirstOrDefault(info => info.ItemName == item.itemAssetName);
                
                if (itemInfo != null)
                {
                    Plugin.Logger.LogInfo($"Evaluando aparición de {item.itemAssetName}...");
                    if (rng.NextDouble() < itemInfo.SpawnChance)
                    {
                        Plugin.Logger.LogInfo($"¡Agregando {item.itemAssetName} a la tienda!");
                        __instance.potentialItems.Add(item);
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(ChatManager), "OnChatMessageReceived")]
    public class ChatManagerPatch
    {
        static void Postfix(string message)
        {
            if (message.StartsWith("/setprobability"))
            {
                string[] args = message.Split(' ');

                if (args.Length == 3)
                {
                    string itemName = args[1];
                    if (float.TryParse(args[2], out float newProbability) && newProbability >= 0 && newProbability <= 1)
                    {
                        ModifyItemProbability(itemName, newProbability);
                    }
                    else
                    {
                        Plugin.Logger.LogInfo("La probabilidad debe estar entre 0 y 1.");
                    }
                }
                else
                {
                    Plugin.Logger.LogInfo("Uso correcto: /setprobability <itemName> <probabilidad>");
                }
            }
        }

        private static void ModifyItemProbability(string itemName, float newProbability)
        {
            // Acceder a la lista a través de ShopManagerPatch
            var itemSpawnInfo = ShopManagerPatch.itemSpawnInfos.FirstOrDefault(info => info.ItemName == itemName);

            if (itemSpawnInfo != null)
            {
                itemSpawnInfo.SpawnChance = newProbability;
                Plugin.Logger.LogInfo($"Probabilidad de {itemName} actualizada a {newProbability * 100}%.");
            }
            else
            {
                Plugin.Logger.LogInfo($"Ítem {itemName} no encontrado.");
            }
        }
    }
}