using UnityEngine.SceneManagement;
using UnityEngine;

public class EventsMenu : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
