using UnityEngine;
using System.Collections.Generic;

namespace TheGame
{
    public class MothershipLobby : MonoBehaviour, IModule, IMothershipComponent
    {
        [field: SerializeField] public ModuleInfo Info { get; private set; }
        private Dictionary<Player, IControl> players = new Dictionary<Player, IControl>();
        private const int maxPlayersInside = 4;
        private int currentPlayersInside;
        private MothershipUI mothershipUI;

        private List<ControlUI> controlledUI = new List<ControlUI>();

        public void InitModule(Player player)
        {
            if (!players.ContainsKey(player))
            {
                players.Add(player, player.control);
                controlledUI.Add(new ControlUI(player.control.OnSecondaryWeapon, mothershipUI, player));
            }
        }

        public bool IsAvailable()
        {
            return currentPlayersInside < maxPlayersInside;
        }

        public void LeaveModule(Player player)
        {
            if (players.ContainsKey(player))
            {
                players.Remove(player);
                currentPlayersInside--;
            }
        }

        public void SetUI(MothershipUI ui)
        {
            mothershipUI = ui;
        }

        public void OnCreate()
        {
            
        }

        public void OnRestore()
        {
            
        }

        public void OnStore()
        {
            
        }

        public class ControlUI
        {
            private MothershipUI ui;
            public ControlUI(ControlEvent<RoutinePhase> controlEvent, MothershipUI ui, Player player)
            {
                this.ui = Instantiate(ui);
                ui.Initialize(player.identifiers, player.cameraController.Camera, player.control);
                controlEvent.action += ShowUI;
            }

            private void ShowUI(RoutinePhase phase)
            {
                ui.Show();
            }
        }
    } 
}

