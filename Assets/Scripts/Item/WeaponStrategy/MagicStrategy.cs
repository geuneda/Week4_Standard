using System.Collections;
using UnityEngine;

namespace Item
{
    public class MagicStrategy : MonoBehaviour, IWeaponStrategy
    {
        public float maxRange = 5f;
        public float expandSpeed = 1f;
        public LayerMask enemyLayerMask;
        private float currentRange;
        
        private readonly Collider[] hitColliders = new Collider[10]; // 공격가능한 적의 수
        
        public void UseWeapon()
        {
            StartCoroutine(ExpandWave());
        }

        private IEnumerator ExpandWave()
        {
            currentRange = 0f;
            
            while (currentRange < maxRange)
            {
                currentRange += expandSpeed * Time.deltaTime;

                var numColliders =
                    Physics.OverlapSphereNonAlloc(transform.position, currentRange, hitColliders, enemyLayerMask);

                for (var i = 0; i < numColliders; i++)
                {
                    // 데미지 처리 로직
                }
                
                yield return null;
            }
            
            Destroy(gameObject);
        }
    }
}