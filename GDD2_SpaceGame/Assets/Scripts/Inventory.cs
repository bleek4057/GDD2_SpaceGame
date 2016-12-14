using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Inventory : NetworkBehaviour {

    NoGravFPSController owner;

    //array for the weapons
    [SyncVar(hook = "SwitchWeapon")]
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

    [Command]
    public void CmdFireActiveWeapon(bool mouseHeld, Vector3 localForward)
    {
        switch (currentWeapon)
        {
            case 0:
                pistolPrefab.Fire(mouseHeld, localForward);
                break;
            case 1:
                laserPrefab.Fire(mouseHeld, localForward);
                break;
            case 2:
                assaultRifle.Fire(mouseHeld, localForward);
                break;
            case 3:
                shotgun.Fire(mouseHeld, localForward);
                break;
        }
    }

    public void ResetWeaponCool()
    {
        weaponLastFired = 0;
    }

    public void setOwner()
    {
        owner = GetComponent<NoGravFPSController>();
        pistolPrefab.owner = owner;
        laserPrefab.owner = owner;
        assaultRifle.owner = owner;
        shotgun.owner = owner;
    }

    [Command]
    public void CmdSetWeapon(int num)
    {
        currentWeapon = num;
    }

    void SwitchWeapon(int currentWeapon)
    {
        pistolPrefab.gameObject.SetActive(false);
        laserPrefab.gameObject.SetActive(false);
        assaultRifle.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(false);

        switch (currentWeapon)
        {
            case 0:
                pistolPrefab.gameObject.SetActive(true);
                break;
            case 1:
                laserPrefab.gameObject.SetActive(true);
                break;
            case 2:
                assaultRifle.gameObject.SetActive(true);
                break;
            case 3:
                shotgun.gameObject.SetActive(true);
                break;
        }

        //Not super efficient but it works
        if (isLocalPlayer) { GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().ChangeDisplayedWeapon(currentWeapon); }
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
        if (isLocalPlayer)
        {
            weaponLastFired += Time.deltaTime;

            //Semi Auto
            if (Input.GetKeyDown(KeyCode.Alpha1) && availableWeapons[0])
            {
                Debug.Log("Switch weapon to Pistol");
                CmdSetWeapon(0);
                //GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().ChangeDisplayedWeapon(0);
            }
            //Laser
            if (Input.GetKeyDown(KeyCode.Alpha2) && availableWeapons[1])
            {
                Debug.Log("Switch weapon to Laser");
                CmdSetWeapon(1);
                //GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().ChangeDisplayedWeapon(1);
            }
            //Full Auto
            if (Input.GetKeyDown(KeyCode.Alpha3) && availableWeapons[2])
            {
                Debug.Log("Switch weapon to Assault Rifle");
                CmdSetWeapon(2);
                //GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().ChangeDisplayedWeapon(2);
            }
            //Shotgun
            if (Input.GetKeyDown(KeyCode.Alpha4) && availableWeapons[3])
            {
                Debug.Log("Switch weapon to Shotgun");
                CmdSetWeapon(3);
                //GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().ChangeDisplayedWeapon(3);
            }
        }
    }
}
