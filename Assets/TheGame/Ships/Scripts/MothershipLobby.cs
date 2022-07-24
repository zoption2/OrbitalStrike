using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace TheGame
{
    public class MothershipLobby : MothershipModule
    {
        private List<IPlayer> players = new List<IPlayer>();

        public override bool IsAvailable()
        {
            return true;
        }

        public override void JoinModule(IPlayer player)
        {
            if(!players.Contains(player))
            {
                players.Add(player);
                player.CameraControl.ChangeCameraTarget(this.transform);
            }
        }

        public override void LeaveModule(IPlayer player)
        {
            if (players.Contains(player))
            {
                players.Remove(player);
            }
        }
    }
}

