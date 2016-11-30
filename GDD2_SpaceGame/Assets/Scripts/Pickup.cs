using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    //the kind of weapon the pickup has
    public int weaponIndex;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<NoGravFPSController>().getInventory.addWeapon(weaponIndex, collision.gameObject.GetComponent<NoGravFPSController>());

        }
    }
}
