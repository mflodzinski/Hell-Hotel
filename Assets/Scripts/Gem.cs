using UnityEngine;

public class Gem : MonoBehaviour
{
    public Transform[] gems;
    private float distance = 2f;

    void Update()
    {
        foreach (Transform gem in gems)
        {
            if (Vector3.Distance(gem.position, transform.position) <= distance && Input.GetKeyDown(KeyCode.E))
            {
                gem.gameObject.SetActive(false);
            }
        }
    }
}
