using UnityEngine;

namespace Gun.Scripts
{
    public class Gunner : MonoBehaviour
    {
        [SerializeField] public bool isPlayerGunner;

        private GunRack _gunRack;

        private void Awake()
        {
            _gunRack = GetComponent<GunRack>();
        }
        
        private void OnAttack()
        {
            if (isPlayerGunner) Fire();
        }

        private void Fire()
        {
            _gunRack.Fire();
        }
    }
}