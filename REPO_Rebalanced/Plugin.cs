using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using System.Reflection;
using System.IO;
using UnityEngine;

namespace REPO_Rebalanced
{
    [BepInPlugin("com.fishyorch.rebalanced", "REPO Rebalanced", "0.0.2")]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        internal new static ConfigFile Config;

        private static readonly (string name, float defaultValue)[] ItemSpawnSettings =
        {
            ("Item Drone Battery", 0.3f),
            ("Item Drone Feather", 0.3f),
            ("Item Drone Indestructible", 0.3f),
            ("Item Drone Torque", 0.3f),
            ("Item Drone Zero Gravity", 0.5f),
            ("Item Extraction Tracker", 0.5f),
            ("Item Gun Handgun", 0.6f),
            ("Item Gun Shotgun", 0.3f),
            ("Item Gun Tranq", 0.5f),
            ("Item Melee Baseball Bat", 0.7f),
            ("Item Melee Frying Pan", 0.6f),
            ("Item Melee Sledge Hammer", 0.5f),
            ("Item Melee Sword", 0.5f)
        };

        public static System.Collections.Generic.List<ItemSpawnInfo> ItemSpawnInfos = new System.Collections.Generic.List<ItemSpawnInfo>();

        private void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo($"Plugin REPO Rebalanced is loaded!");

            Config = new ConfigFile(Path.Combine(Paths.ConfigPath, "REPO_Rebalanced.cfg"), true);

            foreach (var setting in ItemSpawnSettings)
            {
                var configEntry = Config.Bind("ItemSpawnProbabilities", setting.name, setting.defaultValue, $"Probabilidad de aparición para {setting.name}");
                ItemSpawnInfos.Add(new ItemSpawnInfo(setting.name, configEntry.Value));
            }

            Harmony harmony = new Harmony("com.fishyorch.rebalanced");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

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
}