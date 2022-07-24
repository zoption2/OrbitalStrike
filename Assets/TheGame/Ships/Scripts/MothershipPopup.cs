using UnityEngine;
using System;
using TheGame.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheGame
{
    public class BasePopup : MonoBehaviour
    {
        public enum PopupResult { processing, complete }
        public PopupResult Result;

        public virtual void DoOnPopupOpen(IPlayer player)
        {
            
        }

        public virtual void DoOnPopupClose(IPlayer player)
        {
            
        }

    }

    public class MothershipPopup : BasePopup
    {
        [SerializeField] private ModuleInfoPanel[] modulePanels;
        [SerializeField] private Canvas canvas;
        private IControl control;
        private int selectedIndex;
        private IPlayer Player;

        private IShipFactory shipFactory;
        private IPoolController pool;

        public void Initialize(IPlayer player, List<IModule> mothershipModules, IShipFactory shipFactory, IPoolController pool)
        {
            this.shipFactory = shipFactory;
            this.pool = pool;
            Player = player;
            control = player.Control;
            control.OnRightTriggerUse.action += MoveUp;
            control.OnRightShoulder.action += MoveDown;
            //control.OnAButton.action += Select;
            control.OnBButton.action += Cancel;
            canvas.worldCamera = player.CameraControl.Camera;
            gameObject.layer = player.CameraControl.Layer;

            for (int i = 0, j = mothershipModules.Count; i < j; i++)
            {
                modulePanels[i].Setup(mothershipModules[i]);
                if (mothershipModules[i].Equals(player.Module))
                {
                    
                    player.MultiplayerUI.SetFirstSelected(modulePanels[i].gameObject);
                    modulePanels[i].Button.Select();
                    selectedIndex = i;
                }
                else
                {
                    modulePanels[i].OnPressed += OnPressed;
                }
            }

            DoOnPopupOpen(player);
            Result = PopupResult.processing;
        }

        public override void DoOnPopupClose(IPlayer player)
        {
            base.DoOnPopupClose(player);
            control.OnBButton.action -= Cancel;
        }

        private async void OnPressed(IModule module)
        {
            var waiter = shipFactory.GetPrefab(ShipType.jetFighter);
            await waiter;
            var newShip = waiter.Result;

            IModule mothership = (IModule)pool.Get(ShipType.jetFighter, Vector2.zero, Quaternion.identity, newShip);
            Player.Module.ChangeModule(mothership, Player);
            Debug.Log("Pressed: " + module);
        }

        private void Cancel(RoutinePhase phase)
        {
            if (phase == RoutinePhase.started)
            {
                Result = PopupResult.complete;
            }
        }

        private void MoveUp(RoutinePhase phase)
        {
            if (phase == RoutinePhase.started)
            {
                modulePanels[selectedIndex - 1].Button.Select();
            }
        }

        private void MoveDown(RoutinePhase phase)
        {
            if (phase == RoutinePhase.started)
            {
                modulePanels[selectedIndex + 1].Button.Select();
            }
        }

        public void Select(RoutinePhase phase)
        {
            if (phase == RoutinePhase.started)
            {
                //Button.onClick.Invoke();
            }
        }
    }
}

