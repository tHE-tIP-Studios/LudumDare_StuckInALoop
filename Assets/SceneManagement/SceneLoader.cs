using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/*
use this to load all scenes, it will automatically open the 
loading screen and load the scene using a nice fade in/out
YOU JUST NEED TO USE =>>>> SceneLoader.Load(scene name);
*/

[Serializable]
public class UnityEventFloat : UnityEvent<float> { }

public class SceneLoader : MonoBehaviour
{
    // Scene name for the loading Scene
    private const string LOAD_SCENE = "LoadScene";

    private static string sceneToLoad;

    [SerializeField]private float timeBeforeUnloadPrevious = 0.5f;
    [SerializeField]private float timeAfterFinishLoad = 0.5f;
    [SerializeField]private float minimumLoadTime = 1;
    [SerializeField]private UnityEvent onFinishLoad = default;
    [SerializeField]private UnityEventFloat onUpdateLoading = default;

    public float loadingProgress { get; private set; }

    public static void Load(string scene)
    {
        sceneToLoad = scene;
        SceneManager.LoadSceneAsync(LOAD_SCENE, LoadSceneMode.Additive);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeBeforeUnloadPrevious);

        //unload previous scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name != LOAD_SCENE)
            {
                var op = SceneManager.UnloadSceneAsync(scene.name);
                yield return new WaitWhile(() => !op.isDone);
            }
        }

        var startLoadTime = Time.time;
        var operation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;
        
        while (!operation.isDone)
        {
            loadingProgress = Mathf.InverseLerp(0, 0.9f, operation.progress);
            onUpdateLoading.Invoke(loadingProgress);

            if(loadingProgress >= 1)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        //just to remove the weird pop effect when the loading is too fast
        var leftTime = minimumLoadTime - (Time.time - startLoadTime);
        leftTime = Math.Max(0,leftTime);
        yield return new WaitForSeconds(leftTime);

        //unload loading scene
        onFinishLoad.Invoke();
        yield return new WaitForSeconds(timeBeforeUnloadPrevious);
        SceneManager.UnloadSceneAsync(LOAD_SCENE);
    }
}