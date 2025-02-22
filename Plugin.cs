using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MapsStinger.Patches;
using UnityEngine;

namespace MapsStinger
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class MapsStinger : BaseUnityPlugin
    {
        private const string modGUID = "Oignion.MapsStinger";
        private const string modName = "MapsStinger";
        private const string modVersion = "1.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static MapsStinger Instance;
        internal static List<AudioClip> SoundFX;
        internal static AssetBundle Bundle;

        internal new static ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("It loaded");

            harmony.PatchAll(typeof(TimeOfDayPatch));

            mls = Logger;

            SoundFX = new List<AudioClip>();
            string FolderLocation = Instance.Info.Location;
            FolderLocation = FolderLocation.TrimEnd("MapsStinger.dll".ToCharArray());
            Bundle = AssetBundle.LoadFromFile(FolderLocation + "asset");
            if(Bundle != null)
            {
                mls.LogInfo("asset bundle loaded");
                SoundFX = Bundle.LoadAllAssets<AudioClip>().ToList();
            }
            else
            {
                mls.LogError("Uh the asset bundle haven't loaded");
            }

        }
    }
}
