using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class ScreenManager : MonoBehaviour
    {
        private List<AsyncOperation> scenesUnLoading = new List<AsyncOperation>();
        private static ScreenManager instance;
        public static ScreenManager Instance => instance ?? (instance = FindObjectOfType<ScreenManager>());
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            if(SceneManager.sceneCount != SceneManager.sceneCountInBuildSettings) LoadScenes((int)SceneIndexes.Game);

        }

        public void RestartActiveScene()
        {
            UnloadActiveScene();
            LoadScenes((int)SceneIndexes.Game);
        }
        private void UnloadActiveScene()
        {
            scenesUnLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.UI));
            scenesUnLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Game));
            StartCoroutine(WaitForSceneUnload());
        }
        private void LoadScenes(int index)
        {
            SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
            AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync((int)SceneIndexes.UI, LoadSceneMode.Additive);
            Debug.Log(SceneManager.GetSceneByBuildIndex(index).name);
            StartCoroutine(WaitForSceneLoad(asyncLoadScene));
        }
        public IEnumerator WaitForSceneLoad(AsyncOperation scene)
        {
            while (!scene.isDone)
            {
                yield return null;
            }
            Debug.Log("Setting active scene..");
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)SceneIndexes.Game));
        }
        public IEnumerator WaitForSceneUnload()
        {
            for (int i = 0; i < scenesUnLoading.Count; i++)
            {
                while (!scenesUnLoading[i].isDone)
                {
                    yield return null;
                }
            }
        }

    }
}

