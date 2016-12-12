using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
    public GameObject player;

    public Inventory inven;

    public Image[] weaponTabs;

    private int displayed; //What weapon are we displaying on the weapon wheel
    private int invenCurrentWeapon; //What weapon should we be displaying on the weapon wheel

	void Start () {
        //player = GameObject.FindGameObjectWithTag("Player");
        //inven = player.GetComponent<Inventory>();
        ChangeDisplayedWeapon(0);
    }
	
	void Update () {
        if(inven == null) { return;  }
        invenCurrentWeapon = inven.currentWeapon;
        
        if(displayed != invenCurrentWeapon) {
            ChangeDisplayedWeapon(invenCurrentWeapon);
        }   
	}

    public void SetPlayer(GameObject _player) {
        player = _player;
        inven = player.GetComponent<Inventory>();
    }
    void ChangeDisplayedWeapon(int i) {
        //print(i);
        weaponTabs[displayed].enabled = false;
        weaponTabs[i].enabled = true;

        displayed = i;
    }
}
