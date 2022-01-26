using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class liftButton : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float distance = 1f;
    [SerializeField]
    private int sceneIndex = 0;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPos.x > 0.4f && screenPos.y > 0.4f && screenPos.x < 0.6f && screenPos.y < 0.6f) 
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= distance && Input.GetKeyDown(KeyCode.E)) 
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}
