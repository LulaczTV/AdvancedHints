// -----------------------------------------------------------------------
// <copyright file="ShowHint.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace AdvancedHints.Patches
{
#pragma warning disable SA1313
    using AdvancedHints.Components;
    using AdvancedHints.Enums;
#if EXILED
    using Exiled.API.Features;
#else
    using PluginAPI.Core;
#endif
    using HarmonyLib;

#if EXILED
    [HarmonyPatch(typeof(Player), nameof(Player.ShowHint), typeof(string), typeof(float))]
#else
    [HarmonyPatch(typeof(Player), nameof(Player.ReceiveHint), typeof(string), typeof(float))]
#endif
    internal static class ShowHint
    {
        private static bool Prefix(Player __instance, string message, float duration = 3f)
        {
            if (message.Contains("You will respawn in"))
            {
                HudManager.ShowHint(__instance, "\n" + message, duration, displayLocation: DisplayLocation.Middle);
                return false;
            }

            HudManager.ShowHint(__instance, message, duration);
            return false;
        }
    }
}