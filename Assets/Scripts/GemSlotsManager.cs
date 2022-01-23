using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSlotsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject exitBlockade;
    [HideInInspector]
    public bool exitOpen;
    void Update()
    {
        int gemCount = 0;
        foreach (GemSlot slot in transform.GetComponentsInChildren<GemSlot>()) 
        {
            if (slot.GemInSlot.activeSelf) gemCount++;
        }

        if (gemCount == transform.GetComponentsInChildren<GemSlot>().Length) 
        {
            exitOpen = true;
            exitBlockade.SetActive(false);
        }
    }
}
