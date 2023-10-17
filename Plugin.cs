// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace AdvancedHints
{
    using System;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using HarmonyLib;
    using PluginAPI.Core.Attributes;
    using PluginAPI.Enums;
    using PluginAPI.Events;

    /// <summary>
    /// The main plugin class.
    /// </summary>
#if EXILED
    public class Plugin : Plugin<Config>
#else
    public class Plugin
#endif
    {
#if EXILED
        private EventHandlers eventHandlers;
        private Harmony harmony;

        /// <inheritdoc />
        public override string Author { get; } = "Build";

        /// <inheritdoc />
        public override string Name { get; } = "AdvancedHints";

        /// <inheritdoc />
        public override string Prefix { get; } = "AdvancedHints";

        /// <inheritdoc />
        public override Exiled.API.Enums.PluginPriority Priority { get; } = Exiled.API.Enums.PluginPriority.Higher;

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(8, 2, 1);

        /// <inheritdoc />
        public override Version Version { get; } = new Version(1, 0, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            harmony = new Harmony($"advancedHints.{DateTime.UtcNow.Ticks}");
            harmony.PatchAll();

            eventHandlers = new EventHandlers();
            Exiled.Events.Handlers.Player.Destroying += eventHandlers.OnDestroying;
            Exiled.Events.Handlers.Player.Verified += eventHandlers.OnVerified;
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Destroying -= eventHandlers.OnDestroying;
            Exiled.Events.Handlers.Player.Verified -= eventHandlers.OnVerified;
            eventHandlers = null;

            harmony.UnpatchAll();
            harmony = null;
            base.OnDisabled();
        }
#else
        [PluginConfig("RolePlay-Tools/Config.yml")]
        public Config Config;

        private Harmony harmony;

        [PluginAPI.Core.Attributes.PluginPriority(LoadPriority.Medium)]
        [PluginEntryPoint("Advanced-Hints", "1.0.0", "Adds hint system", "Build, ported by pan_andrzej")]
        void LoadPlugin()
        {
            harmony = new Harmony($"advancedHints.{DateTime.UtcNow.Ticks}");
            harmony.PatchAll();

            EventManager.RegisterEvents(this);
            EventManager.RegisterEvents<EventHandlers>(this);
        }
#endif
    }
}