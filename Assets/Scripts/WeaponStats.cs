using UnityEngine;
using UnityEditor;

/// <summary>
/// attach this to any weapon prefab to define attributes
/// </summary>
public class WeaponStats : MonoBehaviour {

    public string weaponName;
    public bool loadedStats;//have these stats been assigned? (on pickup)
    public int damage;//set in inspector. Amount of damage this weapon does
    public GameObject projectile;//assign in heirarchy, this is the type of projectile fired by this weapon
    public GameObject model;//the model for this staff head

    public float _projSpeed;//projectile speed
    public float _rOf;//rate of fire

    public bool isPickup;
    //public WeaponController controller;


	// Use this for initialization
	void Start ()
    {
        loadedStats = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void CreateWeaponPrefab()
    {
        GameObject temp = gameObject;

        string localPath = "Assets/Resources/CurrentWeapon.prefab";

        CreateNew(temp, localPath);
    }

    void CreateNew(GameObject obj, string localPath)
    {
        //NOTE: In non-prototype build we cannot use UnityEditor functions like these.
        Object prefab = PrefabUtility.CreateEmptyPrefab(localPath);
        PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
        Debug.Log("prefab created successfully");
    }
}
