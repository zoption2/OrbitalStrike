using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace TheGame
{
    public class MothershipLobby : MonoBehaviour, IModule, IMothershipComponent
    {
        [field: SerializeField] public ModuleInfo Info { get; private set; }
        private Dictionary<IPlayer, IControl> players = new Dictionary<IPlayer, IControl>();
        private const int maxPlayersInside = 4;
        private int currentPlayersInside;
        private List<IModule> modules;
        [Inject] private ControlUI.Factory uiFactory;

        private List<ControlUI> controlledUI = new List<ControlUI>();

        public void InitModule(IPlayer player)
        {
            if (!players.ContainsKey(player))
            {
                players.Add(player, player.Control);
                player.Module = this;
                var ui = uiFactory.Create(player.Control.OnSecondaryWeapon, player, modules);
                controlledUI.Add(ui);
            }
        }

        public bool IsAvailable()
        {
            return currentPlayersInside < maxPlayersInside;
        }

        public void LeaveModule(IPlayer player)
        {
            if (players.ContainsKey(player))
            {
                players.Remove(player);
                currentPlayersInside--;
            }
        }

        public void SetAllModules(List<IModule> modules)
        {
            this.modules = modules;
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


    }


    public class ControlUI
    {
        [Inject] private IPoolController poolController;
        [Inject] private IMothershipUIFactory uiFactory;

        private IPlayer player;
        private List<IModule> all;

        public ControlUI(ControlEvent<RoutinePhase> controlEvent, IPlayer player, List<IModule> all)
        {
            this.player = player;
            this.all = all;
            controlEvent.action += ShowUI;
        }

        private async void ShowUI(RoutinePhase phase)
        {
            if(phase == RoutinePhase.complete)
            {
                var waiter = uiFactory.GetPrefab(UIType.mothershipUI);
                await waiter;
                var uiPrefab = waiter.Result;

                var ui = (IMothershipUI)poolController.Get(UIType.mothershipUI, Vector2.zero, Quaternion.identity, uiPrefab);
                ui.Initialize(player, all);
            }

        }

        public class Factory : PlaceholderFactory<ControlEvent<RoutinePhase>, IPlayer, List<IModule>, ControlUI>
        {

        }
    }
}

