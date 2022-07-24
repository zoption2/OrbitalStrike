using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace TheGame
{
    public class MotherShip : Ship, IMothership
    {
        [SerializeField] private MothershipLobby lobby;
        [Inject] private ControlUI.Factory uiFactory;
        private List<PlayerInside> players;
        private List<IModule> modules;
    
        public override ShipType ShipType => ShipType.mothership;

        public IModule CaptainsBridge { get => this; }
        public IModule Lobby { get => lobby; }
        private bool moduleAvailable => players.Count < 4;

        public override void OnCreate()
        {
            players = new List<PlayerInside>();

            modules = new List<IModule>();
            modules.Add(this);
            modules.Add(lobby);

            lobby.InitByMothership(this);
        }

        public override bool IsAvailable()
        {
            return moduleAvailable;
        }

        public override void JoinModule(IPlayer player)
        {
            for (int i = 0, j = players.Count; i < j; i++)
            {
                if (players[i].player.Equals(player))
                {
                    Debug.Log($"Player {0} - already present at Lobby!");
                    return;
                }
            }
            var visitor = new PlayerInside();
            visitor.player = player;
            visitor.player.Module.ChangeModule(lobby, player);
            visitor.controlUI = uiFactory.Create(player.Control.OnXButton, player, modules);
            players.Add(visitor);
            
        }

        public override void LeaveModule(IPlayer player)
        {
            for (int i = 0, j = players.Count; i < j; i++)
            {
                if (players[i].player.Equals(player))
                {
                    players[i].controlUI.DisableControl();
                    players.Remove(players[i]);
                    return;
                }
            }
        }



        public class PlayerInside
        {
            public IPlayer player;
            public ControlUI controlUI;
        }
    }



    public class ControlUI
    {
        [Inject] private MothershipPopupProvider popupProvider;

        private IPlayer player;
        private ControlEvent<RoutinePhase> controlEvent;
        private List<IModule> all;

        public ControlUI(ControlEvent<RoutinePhase> controlEvent, IPlayer player, List<IModule> all)
        {
            this.player = player;
            this.all = all;
            this.controlEvent = controlEvent;
            EnableControl();
        }

        public void DisableControl()
        {
            controlEvent.action -= ShowUI;
        }

        public void EnableControl()
        {
            controlEvent.action += ShowUI;
        }

        private void ShowUI(RoutinePhase phase)
        {
            if (phase == RoutinePhase.started)
            {
                popupProvider.Show(player, all, DisableControl, EnableControl);
            }
        }

        public class Factory : PlaceholderFactory<ControlEvent<RoutinePhase>, IPlayer, List<IModule>, ControlUI>
        {

        }
    }
}

