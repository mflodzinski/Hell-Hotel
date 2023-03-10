using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    int CurrentSceneIndex;
    int NumberOfScenes;

    private void Start()
    {
        NumberOfScenes = SceneManager.sceneCountInBuildSettings;
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("current scene:"+ CurrentSceneIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger");
        if (CurrentSceneIndex < NumberOfScenes - 1)
            SceneManager.LoadScene(CurrentSceneIndex + 1);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0) && CurrentSceneIndex < NumberOfScenes -1)
            SceneManager.LoadScene(CurrentSceneIndex + 1);

        if (Input.GetKey(KeyCode.Alpha9) && CurrentSceneIndex > 0)
            SceneManager.LoadScene(CurrentSceneIndex - 1);
    }
}
