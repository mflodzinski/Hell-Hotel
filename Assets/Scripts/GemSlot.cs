using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSlot : MonoBehaviour
{
    private GameObject Gem;
    [SerializeField]
    private int gemIndex;
    public GameObject GemInSlot;
    [SerializeField]
    private GameObject GemSymbol;
    private float distance = 2f;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Gem = player.GetComponent<Gem>().gems[gemIndex].gameObject;
        GemInSlot.GetComponent<MeshRenderer>().material = Gem.GetComponent<MeshRenderer>().material;
        GemSymbol.GetComponent<MeshRenderer>().material = Gem.GetComponent<MeshRenderer>().material;
    }
    private void Update()
    {
        if (!transform.parent.GetComponent<GemSlotsManager>().exitOpen) 
        {
            if (Camera.main.WorldToViewportPoint(GemInSlot.transform.position).x > 0.4f && Camera.main.WorldToViewportPoint(GemInSlot.transform.position).y > 0.4f && Camera.main.WorldToViewportPoint(GemInSlot.transform.position).x < 0.6f && Camera.main.WorldToViewportPoint(GemInSlot.transform.position).y < 0.6f) 
            {
                if (Vector3.Distance(transform.position, player.transform.position) <= distance && Input.GetKeyDown(KeyCode.E) && Gem.activeSelf == false)
                {
                    if (GemInSlot.activeSelf) GemInSlot.gameObject.SetActive(false);
                    else GemInSlot.gameObject.SetActive(true);
                }
            }
        }
    }
}