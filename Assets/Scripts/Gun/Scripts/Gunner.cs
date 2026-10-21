using UnityEngine;

namespace Gun.Scripts
{
    public class Gunner : MonoBehaviour
    {
        [SerializeField] public bool isPlayerGunner;
        Weapon weapon;

        

        private void Awake()
        {
            weapon = new Weapon();
        }
        
        private void OnAttack()
        {
            if (isPlayerGunner) Fire();
        }

        private void Fire()
        {
          weapon.Fire();
        }
    }
}