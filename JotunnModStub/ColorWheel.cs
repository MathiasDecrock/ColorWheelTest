using BepInEx;
using UnityEngine;
using Jotunn.Utils;
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
        private GameObject CustomCube;
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
                    var button = customcanvas.transform.Find("ChooseColor").GetComponent<Button>();
                    Jotunn.Logger.LogInfo($"Button found at {button}");
                    button.onClick.AddListener(() =>
                    {
                        Debug.Log("button pressed");
                        ColorPickerExampleScript.ChooseColorButtonClick();
                    });
                    menu = Instantiate(customcanvas);
                    menu.name = "ColorWheel";
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
            CustomCube = assetBundle.LoadAsset<GameObject>("ColorCube");
            CustomCube.AddComponent<Piece>();
            
            var button2 = customcanvas.transform.Find("ChooseGradient").gameObject;
            button2.SetActive(false);

            PrefabManager.Instance.AddPrefab(customcanvas);
        }

        private void LoadSomethingFromGame()
        {
            customlelz = PrefabManager.Instance.CreateEmptyPrefab("LelzCube", true);
            customlelz.AddMonoBehaviour(CustomMonoBehavioursNames.ColorPickerExampleScript);
            customlelz.AddMonoBehaviour(CustomMonoBehavioursNames.GradientPickerExample);
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

            var thing = new CustomPiece(CustomCube, new PieceConfig
            {
                Category = "lelz",
                Name = "lelz2",
                Enabled = true,
                Icon = PrefabManager.Cache.GetPrefab<GameObject>("forge").GetComponent<Piece>().m_icon,
                CraftingStation = "piece_workbench",
                PieceTable = "Hammer",
                Description = "lelz2",
                Requirements = new RequirementConfig[]
                   {
                        new RequirementConfig {Amount = 1, Item = "Wood", Recover = false}
                   }
            });


            
            PieceManager.Instance.AddPiece(thing);
            PieceManager.Instance.AddPiece(CP);

        }

        public static class CustomMonoBehavioursNames
        {
            public static string ColorPickerExampleScript = nameof(ColorPickerExampleScript);
            public static string ColorPicker = nameof(ColorPicker);
            public static string GradientPickerExample = nameof(GradientPickerExample);
        }
    }
}