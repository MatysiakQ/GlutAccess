using System;
using Exiled.API.Features;
using Exiled.API.Enums;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace GlutAccess
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "Ty";
        public override string Name => "GlutAccess";
        public override Version Version => new Version(1, 1, 0);
        public override Version RequiredExiledVersion => new Version(8, 0, 0); // Niższa wymagana wersja, żeby nie gryzło

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.InteractingDoor -= OnInteractingDoor;
            base.OnDisabled();
        }

        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Player != null && ev.Player.Role.Type == RoleTypeId.Tutorial)
            {
                // Sprawdzamy drzwi po nazwie/typie
                if (ev.Door.Type.ToString().Contains("Checkpoint"))
                {
                    ev.IsAllowed = true;
                }
            }
        }
    }

    public class Config : Exiled.API.Interfaces.IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}