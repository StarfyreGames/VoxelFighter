using System;
using UnityEngine;

namespace Gun.Scripts
{
    public class Gunner : MonoBehaviour
    {
        [SerializeField] public bool autoFire;
        [SerializeField] public bool isPlayerGunner;

        private GunRack _gunRack;

        private void Awake()
        {
            _gunRack = GetComponent<GunRack>();
        }

        private void Update()
        {
            if (autoFire) Fire();
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