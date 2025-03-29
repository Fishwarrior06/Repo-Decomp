using System.Reflection;
using HarmonyLib;

namespace REPO_Rebalanced.patches;

[HarmonyPatch(typeof(ShopManager), "Awake")]
public static class ChangeShopValueCurve
{

    // Reflecting these fields
    /*
       internal float itemValueMultiplier = 4f;
       internal float upgradeValueIncrease = 0.5f;
       internal float healthPackValueIncrease = 0.05f;
       internal float crystalValueIncrease = 0.2f;
     */
    private static readonly (string name, float defaultValue)[] ShopSettings = 
    {
        ("itemValueMultiplier", 4f),
        ("upgradeValueIncrease", 0.5f),
        ("healthPackValueIncrease", 0.05f),
        ("crystalValueIncrease", 0.2f)
    };
    static void Postfix(ShopManager __instance)
    {
        foreach (var setting in ShopSettings)
        {
            var configEntry = Plugin.Config.Bind("Shop", setting.name, setting.defaultValue);
            typeof(ShopManager)
                .GetField(setting.name, BindingFlags.Instance | BindingFlags.NonPublic)?
                .SetValue(__instance, configEntry.Value);
        }
    }
}