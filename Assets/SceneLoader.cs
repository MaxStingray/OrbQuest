using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour {

    [SerializeField]
    private int scene;
    [SerializeField]
    private Text loadingText;

    public SettingsMenu settingsMenu;
	// Use this for initialization
	void Start ()
    {
        loadingText.text = "";	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (settingsMenu.loadScene)
        {
            loadingText.text = "Loading...";
            StartCoroutine(LoadNewScene());
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
	}

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(3);

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
