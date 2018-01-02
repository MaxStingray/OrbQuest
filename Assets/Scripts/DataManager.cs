using UnityEngine;
public class DataManager : MonoBehaviour {

    /// <summary>
    /// saving and loading the player character. When multiplayer is implemented saving can no longer happen in PlayerPrefs
    /// </summary>

    PlayerHealthManager _health;
    PlayerExpManager _exp;
    WeaponController _weapon;
    public GameObject weap;


	// Use this for initialization
	void Awake ()
    {
        _health = GetComponent<PlayerHealthManager>();
        _exp = GetComponent<PlayerExpManager>();
        _weapon = GetComponentInChildren<WeaponController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Save()
    {
        PlayerPrefs.SetFloat("maxHealth", _health.HP);
        PlayerPrefs.SetFloat("currHealth", _health.currentHP);
        PlayerPrefs.SetFloat("reqExp", _exp.requiredXP);
        PlayerPrefs.SetFloat("currExp", _exp.currentXP);//possibly save here instead?
        PlayerPrefs.SetFloat("rateOfFire", _weapon.timeBetweenShots);
        PlayerPrefs.SetFloat("projSpeed", _weapon.projectileSpeed);
    }

    public void Load()
    {
        _health.HP = PlayerPrefs.GetFloat("maxHealth");
        _health.currentHP = PlayerPrefs.GetFloat("currHealth");
        _exp.requiredXP = PlayerPrefs.GetFloat("reqExp");
        _exp.currentXP = PlayerPrefs.GetFloat("currExp");
        _weapon.timeBetweenShots = PlayerPrefs.GetFloat("rateOfFire");
        _weapon.projectileSpeed = PlayerPrefs.GetFloat("projSpeed");
        LoadWeapon();
        
    }

   
    public void LoadWeapon()
    {
        GameObject previousWeapon = GetComponentInChildren<WeaponStats>().gameObject;
        GameObject newWeapon = Resources.Load("CurrentWeapon") as GameObject;
        if (newWeapon != null)
        {
            WeaponController controller = GetComponentInChildren<WeaponController>();
            Vector3 position = new Vector3(controller.transform.position.x, controller.transform.position.y + 0.426f, controller.transform.position.z);
            GameObject weapon = Instantiate(newWeapon, position, controller.transform.rotation);
            controller.stats = weapon.GetComponent<WeaponStats>();
            weapon.GetComponent<WeaponStats>().loadedStats = false;
            weapon.transform.parent = controller.transform;
            Destroy(previousWeapon);
        }
        else
        {
            Debug.Log("ERROR: Weapon could not be loaded");
        }
        
    }

}
