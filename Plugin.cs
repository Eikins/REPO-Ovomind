using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using OvomindEmotions.Patches;
using System;
using System.Collections;
using UnityEngine;

namespace OvomindEmotions;

[BepInPlugin(Guid, Name, Version)]
public class Plugin : BaseUnityPlugin
{
    public const string Guid = "Eikins.REPO.Emotions";
    public const string Name = "Emotions";
    public const string Version = "1.0.0";

    internal static new ManualLogSource Logger;

    private Harmony m_Harmony = new Harmony(Guid);
        
    private void Awake()
    {
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {Guid} is loaded! Version {Version}");

        m_Harmony.PatchAll(typeof(EnemyDirectorPatch));
    }
}
