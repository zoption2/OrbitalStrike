using UnityEngine;
using System.Collections.Generic;

namespace TheGame
{
    public class MotherShip : Ship, IMothership
    {
        [SerializeField] private MothershipLobby lobby;
        [SerializeField] private MothershipUI mothershipUI;

        public override ShipType ShipType => ShipType.mothership;

        public IModule CaptainsBridge { get => this; }
        public IModule Lobby { get => lobby; }

        public override void OnCreate()
        {
            mothershipUI.AddModule(this);
            mothershipUI.AddModule(Lobby);
            lobby.SetUI(mothershipUI);
        }
    }
}

