using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace REPO_Rebalanced
{
    [BepInPlugin("com.fishyorch.rebalanced", "REPO Rebalanced", "0.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo($"Plugin REPO Rebalanced is loaded!");

            // Aplica todos los Harmony patches de este assembly
            Harmony harmony = new Harmony("com.fishyorch.rebalanced");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}