using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    NoGravFPSController owner;

    //array for the weapons
    public int currentWeapon;

    public SemiAuto pistolPrefab; // Add weapon prefabs in Editor
    public Laser laserPrefab;
    public FullAuto assaultRifle;
    public Shotgun shotgun;

    private bool[] availableWeapons;
    private float weaponLastFired;

    public void addWeapon(int index, NoGravFPSController owner)
    {
        availableWeapons[index] = true;
        pistolPrefab.owner = owner;
    }

    public void fireActiveWeapon(bool mouseHeld)
    {
        switch (currentWeapon)
        {
            case 0:
                pistolPrefab.Fire(ref weaponLastFired, mouseHeld);
                break;
            case 1:
                laserPrefab.Fire(ref weaponLastFired, mouseHeld);
                break;
            case 2:
                assaultRifle.Fire(ref weaponLastFired, mouseHeld);
                break;
            case 3:
                shotgun.Fire(ref weaponLastFired, mouseHeld);
                break;
        }
    }

    public void setOwner()
    {
        owner = GetComponent<NoGravFPSController>();
        pistolPrefab.owner = owner;
        laserPrefab.owner = owner;
        assaultRifle.owner = owner;
        shotgun.owner = owner;
    }

    void switchWeapon(int newActive)
    {
        switch (currentWeapon)
        {
            case 0:
                pistolPrefab.gameObject.SetActive(false);
                break;
            case 1:
                laserPrefab.gameObject.SetActive(false);
                break;
            case 2:
                assaultRifle.gameObject.SetActive(false);
                break;
            case 3:
                shotgun.gameObject.SetActive(false);
                break;
        }

        switch (newActive)
        {
            case 0:
                pistolPrefab.gameObject.SetActive(true);
                currentWeapon = 0;
                break;
            case 1:
                laserPrefab.gameObject.SetActive(true);
                currentWeapon = 1;
                break;
            case 2:
                assaultRifle.gameObject.SetActive(true);
                currentWeapon = 2;
                break;
            case 3:
                shotgun.gameObject.SetActive(true);
                currentWeapon = 3;
                break;
        }
    }

    // Use this for initialization
    void Start ()
    {
        currentWeapon = 0;
        weaponLastFired = 0;
        availableWeapons = new bool[10];

        availableWeapons[0] = true;
        availableWeapons[1] = true;
        availableWeapons[2] = true;
        availableWeapons[3] = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        weaponLastFired += Time.deltaTime;

        //Semi Auto
	    if(Input.GetKeyDown(KeyCode.Alpha1) && availableWeapons[0])
        {
            Debug.Log("Switch weapon to Pistol");
            switchWeapon(0);
        }
        //Laser
        if (Input.GetKeyDown(KeyCode.Alpha2) && availableWeapons[1])
        {
            Debug.Log("Switch weapon to Laser");
            switchWeapon(1);
        }
        //Full Auto
        if (Input.GetKeyDown(KeyCode.Alpha3) && availableWeapons[2])
        {
            Debug.Log("Switch weapon to Assault Rifle");
            switchWeapon(2);
        }
        //Shotgun
        if (Input.GetKeyDown(KeyCode.Alpha4) && availableWeapons[3])
        {
            Debug.Log("Switch weapon to Shotgun");
            switchWeapon(3);
        }
    }
}
