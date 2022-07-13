using UnityEngine;
using Zenject;

namespace TheGame
{
    public interface IAim
    {
        Transform Aim { get; }
    }

    public class Barrel : MonoBehaviour, IAim
    {
        [field: SerializeField] public Transform Aim { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        private IPoolController pool;
        private int reloadMS;

        public bool IsEmpty { get; private set; }
        public bool OnCooldown { get; private set; }
        public bool IsReady { get; private set; }


        public System.Action<Barrel> OnProjectileAwait;

        [Inject]
        private void Constructor(IPoolController poolController)
        {
            pool = poolController;
        }

        public void Init(Sprite sprite, int reloadMS)
        {
            SpriteRenderer.sprite = sprite;
            this.reloadMS = reloadMS;
            IsEmpty = false;
            OnCooldown = false;
            IsReady = true;
        }

        public void LoadProjectile()
        {
            if (gameObject.activeSelf)
            {
                SpriteRenderer.enabled = true;
                IsEmpty = false;
                OnCooldown = false;
                IsReady = true;
            }
        }

        public void SetEmpty()
        {
            SpriteRenderer.enabled = false;
            IsEmpty = true;
            OnCooldown = false;
            IsReady = false;
        }

        public void Launch(ISettings settings)
        {
            pool.Get(settings);
            SpriteRenderer.enabled = false;
            IsEmpty = true;
            IsReady = false;
            DelayShot(reloadMS);
        }

        private async void DelayShot(int milliseconds)
        {
            OnCooldown = true;
            await System.Threading.Tasks.Task.Delay(milliseconds);
            OnCooldown = false;
            OnProjectileAwait?.Invoke(this);
        }
    }
}

