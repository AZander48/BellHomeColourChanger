using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Microsoft.CodeAnalysis;
using UnityEngine;


namespace BellHomeColourChanger;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class BellHomeColourChanger : BaseUnityPlugin
{
    // Use the inherited Logger property instead of hiding it
    private Harmony harmony = null!;

    // Make Logger accessible to managers
    public new BepInEx.Logging.ManualLogSource Logger => base.Logger;
    // Manager instances
    public ConfigUI configUI = null!;

    public static BellHomeColourChanger Instance { get; private set; } = null!;

    // Cache the field info for better performance
    private static FieldInfo? _belltownHouseColourField;

    private void Awake()
    {
        try
        {
            // Put your initialization logic here
            Logger.LogInfo($"=== {PluginInfo.PLUGIN_NAME} v{PluginInfo.PLUGIN_VERSION} Starting ===");
            Logger.LogInfo($"Plugin GUID: {PluginInfo.PLUGIN_GUID}");

            // Initialize Harmony
            harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            Logger.LogInfo("Harmony patches applied successfully!");

            // Initialize managers
            configUI = new ConfigUI(Logger, this);
            
            // Initialize the config UI (this creates the config entries)
            configUI.InitializeConfigUI(Config);

            // Set the static instance for patches to access
            Instance = this;

            // Log config file location for debugging
            if (configUI.debugMode.Value) Logger.LogInfo($"Config file location: {Config.ConfigFilePath}");
            
            Logger.LogInfo($"=== {PluginInfo.PLUGIN_NAME} initialization COMPLETED ===");
        }
        catch (Exception ex)
        {
            Logger.LogError($"❌ ERROR during {PluginInfo.PLUGIN_NAME} initialization: {ex.Message}");
            Logger.LogError($"Stack trace: {ex.StackTrace}");
        }
    }
    
    
    // Method to set the bell color
    public void SetBellColor(GlobalEnums.BellhomePaintColours newColor)
    {
        PlayerData.instance.BelltownHouseColour = newColor;
    }
}