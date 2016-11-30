using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    NoGravFPSController owner;

    //array for the weapons
    public int currentWeapon;

    public SemiAuto pistolPrefab; // Add weapon prefabs in Editor
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
                pistolPrefab.Fire(weaponLastFired, mouseHeld);
                break;
            case 1:
                break;
            case 2:
                assaultRifle.Fire(weaponLastFired, mouseHeld);
                break;
            case 3:
                shotgun.Fire(weaponLastFired, mouseHeld);
                break;
        }
    }

    public void setOwner()
    {
        owner = GetComponent<NoGravFPSController>();
        pistolPrefab.owner = owner;
        assaultRifle.owner = owner;
        shotgun.owner = owner;
    }

    // Use this for initialization
    void Start ()
    {
        currentWeapon = 0;
        weaponLastFired = 0;
        availableWeapons = new bool[10];

        availableWeapons[0] = true;
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
            currentWeapon = 0;
        }
        //Laser
        if (Input.GetKeyDown(KeyCode.Alpha2) && availableWeapons[1])
        {
            currentWeapon = 1;
        }
        //Full Auto
        if (Input.GetKeyDown(KeyCode.Alpha3) && availableWeapons[2])
        {
            Debug.Log("Switch weapon to Assault Rifle");
            currentWeapon = 2;
        }
        //Shotgun
        if (Input.GetKeyDown(KeyCode.Alpha4) && availableWeapons[3])
        {
            Debug.Log("Switch weapon to Shotgun");
            currentWeapon = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && availableWeapons[4])
        {
            currentWeapon = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && availableWeapons[5])
        {
            currentWeapon = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && availableWeapons[6])
        {
            currentWeapon = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && availableWeapons[7])
        {
            currentWeapon = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && availableWeapons[8])
        {
            currentWeapon = 8;
        }
    }
}
