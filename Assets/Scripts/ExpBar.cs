using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour {

    public PlayerExpManager playerExp;

    public float required;
    public float current;

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
        if (playerExp == null)
        {
            playerExp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExpManager>();
        }
        else
        {
            required = playerExp.requiredXP;
            current = playerExp.currentXP;
            percentage = ((float)current / required) * 100;
            theText.text = Mathf.RoundToInt(current) + "/" + Mathf.RoundToInt(required);
            bar.fillAmount = percentage * 0.01f;
        }
		
	}
}
