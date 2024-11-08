using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemData itemToGive;
    public int quantityPerHit = 1;
    public int capacy;

    public void Gather(Vector3 hitPoint, Vector3 hitNomal)
    {
        int spawnCount = Mathf.Min(quantityPerHit, capacy);
        
        for(int i = 0; i < spawnCount; i++)
        {
            capacy -= 1;
            Instantiate(itemToGive.dropPrefab, hitPoint + Vector3.up, Quaternion.LookRotation(hitNomal, Vector3.up));
        }
    }
}
