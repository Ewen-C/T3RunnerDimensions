using UnityEngine.SceneManagement;
using UnityEngine;

public class EventsMenu : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
