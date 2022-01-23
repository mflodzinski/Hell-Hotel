using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public Transform[] gems;
    private float distance = 2f;
    private bool allGemsCollected;
    private int collectedGems;
    void Start()
    {
        
    }

    void Update()
    {
        foreach (Transform gem in gems)
        {
            if (Vector3.Distance(gem.position, transform.position) <= distance && Input.GetKeyDown(KeyCode.E))
            {
                gem.gameObject.SetActive(false);
                collectedGems += 1;
            }
            if (collectedGems == gems.Length)
            {
                allGemsCollected = true;
                print("collected");
            }
        }
    }
}
