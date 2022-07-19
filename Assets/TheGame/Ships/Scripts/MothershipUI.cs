using UnityEngine;
using System;
using TheGame.UI;
using System.Collections.Generic;

namespace TheGame
{
    public enum UIType
    {
        mothershipUI
    }

    public class MothershipUI : MonoBehaviour, IMothershipUI
    {
        [SerializeField] private ModuleInfoPanel[] modulePanels;
        [SerializeField] private Canvas canvas;
        private IControl control;

        public void Initialize(IPlayer player, List<IModule> mothershipModules)
        {
            control = player.Control;
            canvas.worldCamera = player.CameraControl.Camera;
            int layer = 6;
            switch (player.Identifiers.ID)
            {
                case 1: layer = 7; break;
                case 2: layer = 8; break;
                case 3: layer = 9; break;
                default:
                    break;
            }
            gameObject.layer = layer;

            modulePanels[0].Setup(player.Module);

            var newModules = new List<IModule>();
            for (int i = 0, j = mothershipModules.Count; i < j; i++)
            {
                if (!mothershipModules[i].Equals(player.Module))
                {
                    newModules.Add(mothershipModules[i]);
                }
            }

            for (int i = 0, j = newModules.Count; i < j; i++)
            {
                modulePanels[i + 1].Setup(newModules[i]);
            }
        }

        public void OnCreate()
        {
            Debug.Log("Mothership UI created!");
        }

        public void OnRestore()
        {
            Debug.Log("Mothership UI restored!");
        }

        public void OnStore()
        {
            Debug.Log("Mothership UI stored!");
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}

