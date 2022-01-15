using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    private int health = 5;

    public void Damage(int amount) 
    {
        health -= amount;
        Debug.Log(health);
        if (health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
