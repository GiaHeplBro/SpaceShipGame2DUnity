using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void ToMission1CutScene()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void GotoMissicon1()
    {
        SceneManager.LoadSceneAsync(3);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}
