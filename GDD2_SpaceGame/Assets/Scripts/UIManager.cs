using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
    private GameObject player;
    private Inventory inven;

    public Animator matchEndAnim;
    public Text matchEndText;

    public Image[] weaponTabs;

    private int displayed; //What weapon are we displaying on the weapon wheel
    private int invenCurrentWeapon; //What weapon should we be displaying on the weapon wheel

    public Text healthText;

	void Start () {
        ChangeDisplayedWeapon(0);
    }
	
	void Update () {

	}

    public void SetPlayer(GameObject _player) {
        player = _player;
        inven = player.GetComponent<Inventory>();
    }
    public void ChangeDisplayedWeapon(int i) {
        //print(i);
        weaponTabs[displayed].enabled = false;
        weaponTabs[i].enabled = true;

        displayed = i;
    }
    public void ShowEndGame(bool _winner) {
        matchEndAnim.enabled = true;

        if(_winner) {
            matchEndText.text = "YOU WIN!";
        }
        else {
            matchEndText.text = "LOSER!";
        }
    }
    public void ChangeHealth(int _newHealth) {
        healthText.text = _newHealth + "";
    }
}
