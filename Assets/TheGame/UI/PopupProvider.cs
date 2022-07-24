using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace TheGame
{
    public abstract class PopupProvider<T> where T : BasePopup
    {
        [Inject] protected IPopupReferences references;
        [Inject] private IShipFactory shipFactory;
        [Inject] private IPoolController pool;
        protected IPlayer player;
        protected AsyncOperationHandle<GameObject> opHandle;

        protected AsyncOperationHandle<GameObject> Create(AssetReferenceGameObject referance)
        {
            //opHandle = Addressables.LoadAssetAsync<GameObject>(referance);
            opHandle = Addressables.InstantiateAsync(referance);
            return opHandle;
        }

        protected IEnumerator PopupLifeTracker(BasePopup popup, IPlayer player, System.Action onComplete)
        {
            yield return new WaitWhile(() => popup.Result == BasePopup.PopupResult.processing);
            popup.DoOnPopupClose(player);
            onComplete?.Invoke();
            Addressables.ReleaseInstance(opHandle);
        }

        protected async void InternalShow(IPlayer player, System.Action onOpen = null, System.Action onClose = null)
        {
            this.player = player;
            onOpen?.Invoke();
            player.Module.DisableControl();
            var operation = Create(references.MothershipUI);
            await operation.Task;
            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                var ui = operation.Result.GetComponent<T>();
                InternalInit(ui);
                //ui.Initialize(player, allModules, shipFactory, pool);
                ui.gameObject.SetActive(true);
                player.MultiplayerUI.SetPlayerPopup(operation.Result);
                ui.StartCoroutine(PopupLifeTracker(ui, player, onClose));
            }
            else
            {
                onClose?.Invoke();
                player.Module.EnableControl();
            }
        }

        protected abstract void InternalInit(T popup);
    }

    public class MothershipPopupProvider : PopupProvider<MothershipPopup>
    {
        [Inject] private IShipFactory shipFactory;
        [Inject] private IPoolController pool;
        private List<IModule> allModules;

        public void Show(IPlayer player, List<IModule> allModules, System.Action onOpen = null, System.Action onClose = null)
        {
            this.allModules = allModules;
            InternalShow(player, onOpen, onClose);
        }

        protected override void InternalInit(MothershipPopup popup)
        {
            popup.Initialize(player, allModules, shipFactory, pool);
        }
    }
}

