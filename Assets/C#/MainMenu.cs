using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Import để thay đổi UI

public class MainMenu : MonoBehaviour
{
    public Button mission1Button;
    public Button mission2Button;
    public Button mission3Button;

    private void Start()
    {
        // ✅ Lấy màn chơi cao nhất đã mở khóa
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 3); // Mặc định là Màn 1 (index 3)

        // ✅ Vô hiệu hóa các nút nếu chưa mở khóa
        mission1Button.interactable = true;
        mission2Button.interactable = unlockedLevel >= 4; // Chỉ mở khóa nếu đã hoàn thành màn 1
        mission3Button.interactable = unlockedLevel >= 5; // Chỉ mở khóa nếu đã hoàn thành màn 2
    }

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

    public void GotoMission1()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void GotoMission2()
    {
        if (PlayerPrefs.GetInt("UnlockedLevel", 3) >= 4)
            SceneManager.LoadSceneAsync(4);
    }
    public void GotoMission3()
    {
        if (PlayerPrefs.GetInt("UnlockedLevel", 3) >= 5)
            SceneManager.LoadSceneAsync(5);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
