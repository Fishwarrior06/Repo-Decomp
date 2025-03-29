﻿using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace REPO_Rebalanced;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    
    public static string BuildGUID => Assembly.GetExecutingAssembly().ManifestModule.ModuleVersionId.ToString();

    private void Awake()
    {
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        
        new Harmony("patch.repo_rebalanced").PatchAll();
    }
    
    private void OnGUI()
    {
        // Remove when releasing the mod
        GUI.Label(new Rect(10, Screen.height - 20, 400, 40), $"Rebalanced ID: {BuildGUID}");
    }
}
