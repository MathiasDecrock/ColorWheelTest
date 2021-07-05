using BepInEx;
using UnityEngine;
using Jotunn.Utils;
using System.Reflection;
using System.IO;
using Jotunn.Managers;
using Jotunn.Entities;
using Jotunn.Configs;
using UnityEngine.UI;

namespace ColorWheel
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class ColorWheel : BaseUnityPlugin
    {
        public const string PluginGUID = "com.jotunn.jotunnmodstub";
        public const string PluginName = "ColorWheel";
        public const string PluginVersion = "0.0.1";
        private static GameObject menu;
        private static GameObject customcanvas;
        private GameObject customlelz;
        public System.Type testBehaviour;

        private void Awake()
        {
            LoadAssets();
            PrefabManager.OnPrefabsRegistered += LoadSomethingFromGame;
            
        }

#if DEBUG
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F6))
            { // Set a breakpoint here to break on F6 key press
                if (menu is null)
                {
                    menu = Instantiate(customcanvas);
                    menu.name = "guildoverlay";
                    menu.transform.SetSiblingIndex(menu.transform.GetSiblingIndex() - 4);
                }
                else
                {
                    menu.SetActive(!menu.activeSelf);
                }
            }
        }
#endif
        public void LoadAssets()
        {
            AssetBundle assetBundle = AssetUtils.LoadAssetBundleFromResources("colorwheel", typeof(ColorWheel).Assembly);
            customcanvas = assetBundle.LoadAsset<GameObject>("ColorCanvas");
            PrefabManager.Instance.AddPrefab(customcanvas);
        }

        private void LoadSomethingFromGame()
        {
            customlelz = PrefabManager.Instance.CreateEmptyPrefab("LelzCube", true);
            customlelz.AddMonoBehaviour(CustomMonoBehavioursNames.ColorPickerExampleScript);
            customlelz.AddComponent<Piece>();
            var CP = new CustomPiece(customlelz,
                new PieceConfig
                {
                    Category = "lelz",
                    Name = "lelz",
                    Enabled = true,
                    Icon = PrefabManager.Cache.GetPrefab<GameObject>("piece_workbench").GetComponent<Piece>().m_icon,
                    CraftingStation = "piece_workbench",
                    PieceTable = "Hammer",
                    Description = "lelz",
                    Requirements = new RequirementConfig[]
                    {
                        new RequirementConfig {Amount = 1, Item = "Wood", Recover = false}
                    }
                });

            var button = customcanvas.transform.Find("Button").gameObject.GetComponentInChildren<Button>();
            button.onClick.AddListener(ColorPickerExampleScript.ChooseColorButtonClick);
            PieceManager.Instance.AddPiece(CP);

        }

        public static class CustomMonoBehavioursNames
        {
            public static string ColorPickerExampleScript = nameof(ColorPickerExampleScript);
            public static string ColorPicker = nameof(ColorPicker);
        }
    }
}