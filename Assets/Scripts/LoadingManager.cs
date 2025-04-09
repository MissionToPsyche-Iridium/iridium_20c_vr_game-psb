using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    void Start()
    {
   // Load the scene asynchronously
        StartCoroutine(loadScene("MainMenu"));
    }


IEnumerator loadScene (string sceneName)
{
    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
    operation.allowSceneActivation = false;

    while (!operation.isDone)
    {
        if (operation.progress >= 0.9f)
        {
            // Optionally, you can add a delay here before allowing the scene to activate
            yield return new WaitForSeconds(1f);
            operation.allowSceneActivation = true;
        }
        yield return null;
    }
}
}