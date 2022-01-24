using UnityEngine;

public class GemSlotsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject exitBlockade;
    [HideInInspector]
    public bool exitOpen;
    GemSlot[] gemSlots;

    private void Start()
    {
        gemSlots = transform.GetComponentsInChildren<GemSlot>();
    }
    void Update()
    {
        int gemCount = 0;
        foreach (GemSlot slot in gemSlots) 
        {
            if (slot.GemInSlot.activeSelf) gemCount++;
        }

        if (gemCount == gemSlots.Length) 
        {
            exitOpen = true;
            exitBlockade.SetActive(false);
        }
    }
}
