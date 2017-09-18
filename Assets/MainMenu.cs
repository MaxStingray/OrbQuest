using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    GameObject settingsPanel;
    GameObject exitPanel;

	// Use this for initialization
	void Start ()
    {
        settingsPanel = GameObject.FindGameObjectWithTag("dungeonSettings");
        settingsPanel.SetActive(false);
        exitPanel = GameObject.FindGameObjectWithTag("exitPanel");
        exitPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartMenuShow()
    {
        Time.timeScale = 0;
        settingsPanel.SetActive(true);
    }

    public void StartMenuHide()
    {
        Time.timeScale = 1;
        settingsPanel.SetActive(false);
    }

    public void ExitPanelShow()
    {
        Time.timeScale = 0;
        exitPanel.SetActive(true);
    }

    public void ExitPanelHide()
    {
        Time.timeScale = 1;
        exitPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
