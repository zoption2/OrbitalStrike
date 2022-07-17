using UnityEngine;
using System;
using TheGame.UI;
using System.Collections.Generic;

namespace TheGame
{
    public class MothershipUI : MonoBehaviour
    {
        [SerializeField] private ModuleInfoPanel[] modulePanels;
        [SerializeField] private Canvas canvas;
        private IControl control;

        private List<IModule> modules = new List<IModule>();

        public void AddModule(IModule module)
        {
            if (!modules.Contains(module))
            {
                modules.Add(module);
            }
        }

        public void Initialize(IIdentifiers identifiers, Camera camera, IControl control)
        {
            this.control = control;
            canvas.worldCamera = camera;
            int layer = 6;
            switch (identifiers.ID)
            {
                case 1: layer = 7; break;
                case 2: layer = 8; break;
                case 3: layer = 9; break;
                default:
                    break;
            }
            gameObject.layer = layer;

            for (int i = 0, j = modules.Count; i < j; i++)
            {
                modulePanels[i].Setup(modules[i]);
                modulePanels[i].gameObject.SetActive(true);
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}

