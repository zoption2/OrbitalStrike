using UnityEngine;

namespace TheGame
{
    public interface IDestructable
    {
        int ID { get; }
        Collider2D Collider { get; }
        float Health { get; }
        void DoDamage(float value);
    }
}

