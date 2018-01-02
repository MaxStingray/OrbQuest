using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ooparts.dungen;

using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text statsName;
    public Text statsDamage;
    public Text statsRoF;
    public Text statsDesc;

    [HideInInspector]
    public GameObject _player;
    public WeaponPickup _pickup;


    GameObject[] pauseObjects; //an array of all objects to display on pause
    GameObject[] victoryObjects;
    GameObject[] weaponStats;
   

    public int killCount;

    float timer;
    public string minutes;
    public string seconds;

    bool timerStarted = false;
	// Use this for initialization
	void Start ()
    {
        Time.timeScale = 1;//regular timescale
        pauseObjects = GameObject.FindGameObjectsWithTag("showOnPause");
        victoryObjects = GameObject.FindGameObjectsWithTag("ShowOnVictory");
        weaponStats = GameObject.FindGameObjectsWithTag("statsScreen");
        hideVictory();
        hidePaused();
        hideStats();
        timerStarted = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timerStarted)
            timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Debug.Log("high");
                Time.timeScale = 1;
                hidePaused();
            }
        }

        if (GetComponent<RoomMapManager>().bossDead)
            showVictory();
	}

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
    }

    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    public void showVictory()
    {
        minutes = Mathf.Floor(timer / 60).ToString("00");
        seconds = (timer % 60).ToString("00");
        foreach (GameObject g in victoryObjects)
        {
            g.SetActive(true);
            Time.timeScale = 0;
            if (g.GetComponentInChildren<StatsText>() != null)
            {
                g.GetComponentInChildren<StatsText>().Refresh();
            }
        }

        timerStarted = false;
    }

    public void hideVictory()
    {
        foreach (GameObject g in victoryObjects)
        {
            g.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void LoadLevel(string level)
    {
        Time.timeScale = 1;
        GameObject DP = GameObject.FindGameObjectWithTag("parametersObject");
        Destroy(DP);
        SceneManager.LoadScene(level);
    }

    public void showStats(GameObject pickup, GameObject player)//pass in picked up object as argument
    {
        foreach (GameObject g in weaponStats)
        {
            g.SetActive(true);
        }
        _pickup = pickup.GetComponent<WeaponPickup>();
        _player = player;
        statsName.text = _pickup.weaponName;
        statsDesc.text = _pickup.weaponDescription;
        if (_pickup.thisStats != null)
        {
            statsDamage.text = "Damage: " + _pickup.thisStats.damage.ToString();
            statsRoF.text = "Rate of Fire: " + _pickup.thisStats._rOf.ToString();
        }
        else
        {
            Debug.Log("Set to null. Why?");
        }
        Time.timeScale = 0;
    }

    public void AssignStats()
    {
        if (_pickup != null && _player != null)
        {
            _pickup.Assign(_player);
        }
        else
        {
            Debug.LogError("Pickup or Player is set to null!");
        }

        hideStats();
    }

    public void hideStats()
    {
        foreach (GameObject g in weaponStats)
        {
            g.SetActive(false);
        }

        Time.timeScale = 1;
    }
}
