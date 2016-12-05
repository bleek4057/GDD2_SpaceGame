using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
    public GameObject player;

    private Inventory inven;

    public Animator[] weaponTabs;

    private int displayed; //What weapon are we displaying on the weapon wheel
    private int invenCurrentWeapon; //What weapon should we be displaying on the weapon wheel

	void Start () {
        inven = player.GetComponent<Inventory>();
    }
	
	void Update () {
        invenCurrentWeapon = inven.currentWeapon;
        
        if(displayed != invenCurrentWeapon) {
            ChangeDisplayedWeapon(invenCurrentWeapon);
        }
	}

    void ChangeDisplayedWeapon(int i) {
        print(i);
        weaponTabs[displayed].enabled = false;
        weaponTabs[i].enabled = true;

        displayed = i;
    }
}
