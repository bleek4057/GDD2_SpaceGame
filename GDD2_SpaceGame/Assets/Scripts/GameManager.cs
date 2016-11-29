using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //array for the weapons
    public int currentWeapon;
    public GameObject[] weapons; // Add weapon prefabs in Editor

    bool[] availableWeapons;

    public void addWeapon(int index)
    {
        availableWeapons[index] = true;
    }

	// Use this for initialization
	void Start ()
    {
        currentWeapon = 0;
        availableWeapons = new bool[10];
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Semi Auto
	    if(Input.GetKeyDown(KeyCode.Alpha1) && availableWeapons[1])
        {
            currentWeapon = 0;
        }
        //Laser
        if (Input.GetKeyDown(KeyCode.Alpha2) && availableWeapons[2])
        {
            currentWeapon = 1;
        }
        //Full Auto
        if (Input.GetKeyDown(KeyCode.Alpha3) && availableWeapons[3])
        {
            currentWeapon = 2;
        }
        //Shotgun
        if (Input.GetKeyDown(KeyCode.Alpha4) && availableWeapons[4])
        {
            currentWeapon = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && availableWeapons[5])
        {
            currentWeapon = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && availableWeapons[6])
        {
            currentWeapon = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && availableWeapons[7])
        {
            currentWeapon = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && availableWeapons[8])
        {
            currentWeapon = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && availableWeapons[9])
        {
            currentWeapon = 8;
        }
    }
}
