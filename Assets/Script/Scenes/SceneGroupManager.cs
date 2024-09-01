using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Scenes
{
    public readonly struct AsyncOperationGroup
    {
        public readonly List<AsyncOperation> Operations;
        
        public float Progress => Operations.Count == 0 ? 0 : Operations.Average(o => o.progress);
        public bool IsDone => Operations.All(o => o.isDone);

        public AsyncOperationGroup(int intialCapacity)
        {
            Operations = new List<AsyncOperation>(intialCapacity);
        }
    }
    
    public class SceneGroupManager
    {
        public event Action<string> OnSceneLoaded = delegate {};
        public event Action<string> OnSceneUnloaded = delegate {};
        public event Action OnSceneGroupLoaded = delegate {};

        private SceneGroup ActiveSceneGroup;

        public async Task LoadScenes(SceneGroup group, IProgress<float> progress, bool reloadDupScenes = false)
        {
            ActiveSceneGroup = group;
            var loadedScenes = new List<string>();

            await UnloadScenes();
            
            // Scene load setup

            int sceneCount = SceneManager.sceneCount;

            for (int i = 0; i < sceneCount; i++)
            {
                loadedScenes.Add(SceneManager.GetSceneAt(i).name);
            }

            int totalScenesToLoad = ActiveSceneGroup.Scenes.Count;
            var operationGroup = new AsyncOperationGroup(totalScenesToLoad);
            
            // Actual scene loading

            for (int i = 0; i < totalScenesToLoad; i++)
            {
                var sceneData = group.Scenes[i];
                if (!reloadDupScenes && loadedScenes.Contains(sceneData.Name)) continue;

                var operation = SceneManager.LoadSceneAsync(sceneData.Reference.Path, LoadSceneMode.Additive);
                operationGroup.Operations.Add(operation);
                
                OnSceneLoaded.Invoke(sceneData.Name);
            }

            // Fills the progress float while operations are remaining
            while (!operationGroup.IsDone)
            {
                progress?.Report(operationGroup.Progress);
                await Task.Delay(100); // Delay to avoid tight loop
            }

            Scene activeScene =
                SceneManager.GetSceneByName(ActiveSceneGroup.FindSceneNameByType(SceneType.ActiveScene));

            if (activeScene.IsValid())
            {
                SceneManager.SetActiveScene(activeScene);
            }

            OnSceneGroupLoaded.Invoke();
        }

        public async Task UnloadScenes()
        {
            var unloadScenes = new List<string>();
            var activeScene = SceneManager.GetActiveScene().name;

            int sceneCount = SceneManager.sceneCount;

            for (int i = sceneCount - 1; i > 0; i--)
            {
                var sceneAt = SceneManager.GetSceneAt(i);
                if(!sceneAt.isLoaded) continue;

                var sceneName = sceneAt.name;
                if(sceneName.Equals(activeScene) || sceneName == "Bootstrapper") continue;
                unloadScenes.Add(sceneName);
            }

            var operationGroup = new AsyncOperationGroup(unloadScenes.Count);

            for (int i = 0; i < unloadScenes.Count; i++)
            {
                var operation = SceneManager.UnloadSceneAsync(unloadScenes[i]);
                if(operation == null) continue;
                operationGroup.Operations.Add(operation);
                
                OnSceneUnloaded.Invoke(unloadScenes[i]); // Unalive these scenes
            }
            
            while (!operationGroup.IsDone)
            {
                await Task.Delay(100); // Delay to avoid tight loop
            }
        }
    }
}