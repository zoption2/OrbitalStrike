using UnityEngine;
using System.Collections.Generic;

namespace TheGame
{
    public class MotherShip : Ship, IMothership
    {
        [SerializeField] private MothershipLobby lobby;
        private List<IModule> modules;
        public override ShipType ShipType => ShipType.mothership;

        public IModule CaptainsBridge { get => this; }
        public IModule Lobby { get => lobby; }

        public override void OnCreate()
        {
            modules = new List<IModule>();
            modules.Add(this);
            modules.Add(lobby);

            lobby.SetAllModules(modules);
        }
    }
}

