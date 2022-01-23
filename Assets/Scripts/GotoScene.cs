using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour
{
    [SerializeField] private int sceneName;

    public void GoTo(string scene_Name)
    {
        SceneManager.LoadScene(scene_Name);
    }

    public void GoTo()
    {
        SceneManager.LoadScene(sceneName);
    }
}