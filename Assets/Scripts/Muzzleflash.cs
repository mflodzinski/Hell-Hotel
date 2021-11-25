using UnityEngine;

public class Muzzleflash : MonoBehaviour
{
    public GameObject flashHolder;
    public float flashTime;

    private void Start()
    {
        Deactivate();
    }

    public void Activate()
    {
        flashHolder.SetActive(true);

        // Invoke function after time
        Invoke("Deactivate", flashTime);
    }

    public void Deactivate()
    {
        flashHolder.SetActive(false);
    }
}
