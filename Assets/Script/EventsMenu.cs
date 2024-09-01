using UnityEngine.SceneManagement;
using UnityEngine;

public class EventsMenu : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
