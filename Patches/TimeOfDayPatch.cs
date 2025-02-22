using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using HarmonyLib;
using HarmonyLib.Tools;
using UnityEngine;

namespace MapsStinger.Patches
{
    [HarmonyPatch(typeof(TimeOfDay))]
    internal class TimeOfDayPatch

    {
        private const string modGUID = "Oignion.MapsStinger";
        internal ManualLogSource mls;

        [HarmonyPatch("PlayTimeMusicDelayed")]
        [HarmonyPrefix]
        static void OverrideAudio(TimeOfDay __instance)
        {
            if (StartOfRound.Instance.currentLevelID == 7)
            {
                __instance.TimeOfDayMusic.clip = MapsStinger.SoundFX[1];
                MapsStinger.mls.LogInfo("Dine Music");
            }
            else
            {
                if (StartOfRound.Instance.currentLevelID == 6) 
                {
                    __instance.TimeOfDayMusic.clip = MapsStinger.SoundFX[2];
                    MapsStinger.mls.LogInfo("Rend Music");
                }
                else 
                    {
                        __instance.TimeOfDayMusic.clip = TimeOfDay.Instance.TimeOfDayMusic.clip;
                    MapsStinger.mls.LogInfo("Base music");
                    }
                    
            }
            
        }
    }
}
