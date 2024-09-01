using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Scenes
{
    public class SceneBootstrapper : PersistentSingleton<SceneBootstrapper>
    {
        // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        // static async void Init()
        // {
        //     Debug.Log("Bootstrapper Init");
        //     await SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Single);
        // }
    }
}