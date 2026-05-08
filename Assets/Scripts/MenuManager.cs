using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void GoToLevel2()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void ExitGame()
    {
        Debug.Log("User exited the game");
        Application.Quit();
    }
}