using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TheGame.UI
{
    public class ModuleInfoPanel : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Button button;
        private IModule module;

        public void Setup(IModule module)
        {
            image.sprite = module.Info.Icon;
            title.text = module.Info.Title;
            description.text = module.Info.Description;
            this.module = module;
            button.onClick.AddListener(OnClick);
            gameObject.SetActive(true);
        }

        private void OnClick()
        {

        }
    }
}

