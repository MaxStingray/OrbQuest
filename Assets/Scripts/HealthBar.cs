using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public PlayerHealthManager playerHealth;

    public float maxValue;
    public float currentValue;

    Text theText;

    public float percentage;

    Image bar;
    
	// Use this for initialization
	void Start ()
    {
        bar = GetComponent<Image>();
        theText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (playerHealth == null)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthManager>();
        }
        else
        {
            maxValue = playerHealth.HP;
            currentValue = playerHealth.currentHP;
            percentage = ((float)currentValue / maxValue)  * 100;
            theText.text = Mathf.RoundToInt(currentValue) + "/" + Mathf.RoundToInt(maxValue);        
            bar.fillAmount = percentage * 0.01f;
            

        }	
	}
}
