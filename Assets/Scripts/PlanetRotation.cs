using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TheGame
{
    public class PlanetRotation : MonoBehaviour, IGlobalFixedUpdateObserver
    {
        [SerializeField] private Transform toRorate;
        [SerializeField] private float speed = 0.1f;
        private IGlobalFixedUpdateProvider fixedUpdateProvider;

        public void FixedUpdateGlobal()
        {
            Rotate();
        }

        [Inject]
        private void Constructor(IGlobalFixedUpdateProvider timer)
        {
            fixedUpdateProvider = timer;
        }

        private void OnEnable()
        {
            fixedUpdateProvider.Add(this);
        }

        private void OnDisable()
        {
            fixedUpdateProvider.Remove(this); 
        }

        private void Rotate()
        {
            toRorate.Rotate(Vector3.up, speed);
        }
    }
}

