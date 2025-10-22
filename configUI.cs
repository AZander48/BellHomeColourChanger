using BepInEx.Configuration;
using UnityEngine;
using BepInEx;
using System.Reflection;
using System.Linq;
using GlobalSettings;
using System;
using System.Runtime.CompilerServices;

namespace BellHomeColourChanger
{
    public class ConfigUI
    {
        private readonly BepInEx.Logging.ManualLogSource logger;
        private readonly BellHomeColourChanger bellHomeColourChanger;
        

        public ConfigEntry<bool> debugMode = null!;
        public ConfigEntry<GlobalEnums.BellhomePaintColours> colour = null!;

        public ConfigUI(BepInEx.Logging.ManualLogSource logger, BellHomeColourChanger bellHomeColourChanger)
        {
            this.logger = logger;
            this.bellHomeColourChanger = bellHomeColourChanger;
        }

        public void InitializeConfigUI(BepInEx.Configuration.ConfigFile config)
        {
            debugMode = config.Bind("Debug", "DebugMode", false);
            colour = config.Bind
            (
                "General",
                "Bell Color",
                GlobalEnums.BellhomePaintColours.None,
                "Choose the color for your bell"
            );
            colour.SettingChanged += (sender, args) =>
            {
                bellHomeColourChanger.SetBellColor(colour.Value);
            };
        }
    }
}