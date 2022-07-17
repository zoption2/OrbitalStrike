﻿using UnityEngine;

namespace TheGame
{
    [CreateAssetMenu(fileName = "PrefabReferenceHolder", menuName = "ScriptableObjects/Prefab Referance Holder")]
    public class PrefabReferanceHolder : ScriptableObject
    {
        [field: SerializeField] public Player Player { get; private set; }
    }
}
