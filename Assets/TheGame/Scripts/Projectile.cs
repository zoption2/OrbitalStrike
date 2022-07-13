using System.Collections;
using UnityEngine;
using Zenject;
using System;


namespace TheGame
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour, IProjectiles, IGlobalFixedUpdateObserver, IGlobalUpdateObserver
    {
        [field: SerializeField] public float speed { get; private set; } = 10;
        [field: SerializeField] public float damage { get; private set; } = 1;
        [field: SerializeField] public float lifetime { get; private set; } = 1.5f;
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }

        [SerializeField] protected Rigidbody2D rigidBody;
        [SerializeField] protected Collider2D myCollider;

        protected IPoolController pool;
        protected IGlobalFixedUpdateProvider fixedUpdateProvider;
        protected IGlobalUpdateProvider updateProvider;
        protected IIdentifiers identifiers;
        protected bool isActive;
        protected float currentLifetime;

        public abstract Enum WeaponType { get; }

        [Inject]
        private void Constructor(IPoolController poolController
                                ,IGlobalFixedUpdateProvider fixedUpdateProvider 
                                ,IGlobalUpdateProvider updateProvider)
        {
            pool = poolController;
            this.fixedUpdateProvider = fixedUpdateProvider;
            this.updateProvider = updateProvider;
        }

        protected virtual void Move()
        {
            rigidBody.velocity = transform.up * speed;
        }

        protected void CheckLifetime()
        {
            currentLifetime -= Time.deltaTime;
            if (currentLifetime <= 0)
            {
                Release();
            }
        }

        protected virtual void Release()
        {
            pool.Release(WeaponType, this);
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDestructable destructable))
            {
                if (destructable.ID == identifiers.ID)
                {
                    Physics2D.IgnoreCollision(destructable.Collider, myCollider);
                    return;
                }
                destructable.DoDamage(damage);
            }
            DoOnCollision();
        }

        protected virtual void DoOnCollision()
        {
            Release();
        }

        public virtual void Initialize(IIdentifiers identifiers)
        {
            this.identifiers = identifiers;
            fixedUpdateProvider.Add(this);
            updateProvider.Add(this);
            myCollider.enabled = true;
        }

        public virtual void OnCreate()
        {
            currentLifetime = lifetime;
        }

        public virtual void OnRestore()
        {
            currentLifetime = lifetime;
        }

        public virtual void OnStore()
        {
            myCollider.enabled = false;
            fixedUpdateProvider.Remove(this);
            updateProvider.Remove(this);
        }

        public void FixedUpdateGlobal()
        {
            Move();
        }

        public void UpdateGlobal()
        {
            CheckLifetime();
        }
    }
}

