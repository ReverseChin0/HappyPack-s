using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SCR_SceneTransition : MonoBehaviour
{
    public new AudioSource audio = default;
    public string nivel;
    CanvasGroup cGroup = default;

    private void Awake() {
        cGroup = gameObject.AddComponent<CanvasGroup>();
        cGroup.DOFade(0, 1).SetEase(Ease.OutExpo);
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);
        if(audio!=null)
        DontDestroyOnLoad(audio);
    }

    public void CargarEscena() 
    {
        cGroup.DOFade(1, 1).SetEase(Ease.OutExpo).OnComplete(LoadScene);
    }

    public void CargarA(string _nombre) {
        nivel = _nombre;
        cGroup.DOFade(1, 1).SetEase(Ease.OutExpo).OnComplete(LoadScene);
    }

    void LoadScene() 
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene() 
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nivel);

        while (!asyncLoad.isDone) {
            yield return null;
        }
        cGroup.DOFade(0, 1).SetEase(Ease.OutExpo).OnComplete(DestroyMyself);
    }

    void DestroyMyself() 
    {
        /*GameObject newGO = new GameObject("Audio");
        audio.transform.parent = newGO.transform;*/
        Destroy(gameObject);
    }
}

