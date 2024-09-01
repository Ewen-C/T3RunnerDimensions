using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Scenes
{
    public class LoadingProgress : IProgress<float>
    {
        public event Action<float> Progressed;
        private const float ratio = 1f;
        
        public void Report(float value)
        {
            Progressed?.Invoke(value/ratio);
        }
    }

    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Image loaddingBar;
        [SerializeField] private float fillSpeed = 0.5f;
        [SerializeField] private Canvas loadingCanvas;
        [SerializeField] private Camera loadingCamera;
        [SerializeField] private SceneGroup[] sceneGroups;

        private float targetProgress;
        private bool isLoading;

        public readonly SceneGroupManager manager = new SceneGroupManager();

        private void Awake()
        {
            manager.OnSceneLoaded += sceneName => Debug.Log("Loaded : " + sceneName);
            manager.OnSceneUnloaded += sceneName => Debug.Log("Loaded : " + sceneName);
            manager.OnSceneGroupLoaded += () => Debug.Log("Scene group loaded");
        }

        async void Start()
        {
            await LoadSceneGroup(0);
        }

        private void Update()
        {
            if (!isLoading) return;

            float currentFillAmount = loaddingBar.fillAmount;
            float progressDifference = Mathf.Abs(currentFillAmount - targetProgress);
            float dynamicFillSpeed = progressDifference * fillSpeed;

            loaddingBar.fillAmount = Mathf.Lerp(currentFillAmount, targetProgress, Time.deltaTime * dynamicFillSpeed);
        }

        public async Task LoadSceneGroup(int index)
        {
            loaddingBar.fillAmount = 0f;
            targetProgress = 1f;

            if (index < 0 || index >= sceneGroups.Length)
            {
                Debug.Log("Invalid scene group index : " + index);
                return;
            }

            LoadingProgress progress = new LoadingProgress();
            progress.Progressed += target => targetProgress = Mathf.Max(target, targetProgress);

            EnableLoadingCancas();
            await manager.LoadScenes(sceneGroups[index], progress);
            EnableLoadingCancas(false);
        }

        void EnableLoadingCancas(bool enable = true)
        {
            isLoading = true;
            loadingCanvas.gameObject.SetActive(enable);
            loadingCamera.gameObject.SetActive(enable);
        }
    }
}