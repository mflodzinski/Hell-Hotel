using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private HealthScript healthScript;
    private Image image;
    void Start()
    {
        healthScript = GameObject.FindWithTag("Player").GetComponent<HealthScript>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.fillAmount = healthScript.health/healthScript.maxHealth;
    }
}
