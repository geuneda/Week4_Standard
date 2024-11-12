using UnityEngine;

namespace Item
{
    public class BowStrategy : MonoBehaviour, IWeaponStrategy
    {
        // rigidbody에 useGravity 활성화
        
        public GameObject arrowPrefab;
        public Transform launchPoint;
        public float launchForce = 10f;
        public float launchAngle = 30f;
        
        public void UseWeapon()
        {
            LaunchArrow();
        }

        public void LaunchArrow()
        {
            GameObject arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity);
            Rigidbody rb = arrow.GetComponent<Rigidbody>();

            Vector3 direction = ArrowDirection();
            rb.velocity = direction * launchForce;
        }
        
        // 발사각 계산
        public Vector3 ArrowDirection()
        {
            float radianAngle = Mathf.Deg2Rad * launchAngle;
            Vector3 direction = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0);
            
            return transform.TransformDirection(direction);
        }
    }
}