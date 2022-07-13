using UnityEngine;


namespace TheGame
{
    public class Health : MonoBehaviour, IDestructable
    {
        [SerializeField] private float health;
        [SerializeField] private Collider2D collider;
        private IIdentifiers ship;
        float IDestructable.Health => health;
        public Collider2D Collider => collider;

        public int ID => ship.ID;

        public void Init(IIdentifiers ship)
        {
            this.ship = ship;
        }

        public void DoDamage(float value)
        {
            health -= value;
            if (health <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

