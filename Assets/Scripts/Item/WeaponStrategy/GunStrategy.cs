using UnityEngine;

namespace Item
{
    public class GunStrategy : MonoBehaviour, IWeaponStrategy
    {
        // rigidbody에 useGravity 비활성화
        
        public GameObject bulletPrefab;
        public Transform bulletSpawn;
        public float bulletSpeed = 30f;
        
        public void UseWeapon()
        {
            LaunchBullet();
        }

        private void LaunchBullet()
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = bulletSpawn.forward * bulletSpeed;
        }
    }
}