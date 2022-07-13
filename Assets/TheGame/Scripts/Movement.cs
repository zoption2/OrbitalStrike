using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TheGame
{
    public class Movement : MonoBehaviour, IGlobalFixedUpdateObserver
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;
        private IGlobalFixedUpdateProvider fixedUpdateProvider;

        [Inject] 
        private void Constructor(IGlobalFixedUpdateProvider fixedUpdateProvider)
        {
            this.fixedUpdateProvider = fixedUpdateProvider;
        }

        private void OnEnable()
        {
            fixedUpdateProvider.Add(this);
        }

        private void OnDisable()
        {
            fixedUpdateProvider.Remove(this);
        }

        public void Move()
        {
            rb.AddForce(transform.up * speed);
        }

        public void FixedUpdateGlobal()
        {
            Move();
        }
    }
}

