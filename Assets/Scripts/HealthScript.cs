using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    public float health = 5f;
    private float healthRegen = 0.0003f;

    [HideInInspector]
    public float maxHealth;

    private void Start()
    {
        maxHealth = health;
    }
    private void Update()
    {
        if (health < maxHealth)
        {
            health += healthRegen;
        }
    }
    public void Damage(int amount) 
    {
        health -= amount;
        if (health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
