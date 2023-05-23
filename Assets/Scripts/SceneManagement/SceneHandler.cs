using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public void ChangeScene(string SceneName)
    {
        StartCoroutine(DelayScene(0, SceneName));
    }

    public void ChangeScene(string SceneName, float Delay)
    {
        StartCoroutine(DelayScene(Delay,SceneName));
    }

    IEnumerator DelayScene(float _delay,string _sceneName)
    {
        yield return new WaitForSeconds(_delay);
        gameObject.GetComponent<GameManager>().FadeToBlack();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_sceneName);
    }
}
