using UnityEngine.SceneManagement;
using UnityEngine;

public class EventsMenu : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene("test");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
