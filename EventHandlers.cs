// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace AdvancedHints
{
    using AdvancedHints.Components;
    using PluginAPI.Core.Attributes;
#if EXILED
    using Exiled.Events.EventArgs.Player;
#else
    using PluginAPI.Events;
#endif

    public class EventHandlers
    {
#if EXILED
        public void OnDestroying(DestroyingEventArgs ev)
#else
        [PluginEvent]
        public void OnDestroying(PlayerLeftEvent ev)
#endif
        {
            if (ev.Player.GameObject.TryGetComponent(out HudManager hudManager))
                hudManager.Destroy();
        }

#if EXILED
        public void OnVerified(VerifiedEventArgs ev)
#else
        [PluginEvent]
        public void OnVerified(PlayerJoinedEvent ev)
#endif
        {
            ev.Player.GameObject.AddComponent<HudManager>();
        }
    }
}