using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GotoScene gotoScene;

    public void NewGame()
    {
        gotoScene.GoTo();
    }

    public void Continue()
    {
        gotoScene.GoTo();
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}