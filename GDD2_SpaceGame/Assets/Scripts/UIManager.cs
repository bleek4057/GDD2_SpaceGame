using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
    public GameObject player;

    public Image[] weaponWheelImages;

    private Vector3 origWeaponTabScale;
    private Vector3 selectedWeaponTabScale;
    
    private float origWeaponTabHeight;
    public float selectedWeaponTabHeight;

    public float selectedWeaponImageScalar;

    private Inventory inventory;
    private NoGravFPSController fpsController;

	void Start () {
        inventory = player.GetComponent<Inventory>();
        selectedWeaponTabScale = new Vector3(selectedWeaponImageScalar, selectedWeaponImageScalar, 0);
        origWeaponTabScale = weaponWheelImages[0].transform.localScale;
        origWeaponTabHeight = weaponWheelImages[0].transform.localPosition.y;

    }
	
	void Update () {
        for(int i = 0; i < weaponWheelImages.Length; i++){
            if (i == inventory.currentWeapon) {
                //weaponWheelImages[i].transform.localScale = selectedWeaponTabScale;
                weaponWheelImages[i].rectTransform.localPosition = new Vector3(weaponWheelImages[i].transform.localPosition.x, origWeaponTabHeight, weaponWheelImages[i].transform.localPosition.z);
            }else {
                //weaponWheelImages[i].transform.localScale = origWeaponTabScale;
                weaponWheelImages[i].rectTransform.localPosition = new Vector3(weaponWheelImages[i].transform.localPosition.x, origWeaponTabHeight, weaponWheelImages[i].transform.localPosition.z);
            }
        }
	}
}
