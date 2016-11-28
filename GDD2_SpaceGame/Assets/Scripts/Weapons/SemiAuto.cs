using UnityEngine;
using System.Collections;

public class SemiAuto : MonoBehaviour {

    public NoGravFPSController owner = null;
    public float kickback;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetMouseButtonDown(1) && owner != null)
        {
            Fire();
        }
	}

    bool Fire()
    {
        owner.Body.AddForce(kickback * (-transform.forward), ForceMode.Impulse);
        Debug.DrawRay(owner.transform.position, owner.transform.forward, Color.red, 5);
        return Physics.Raycast(owner.transform.position, owner.transform.forward, 25, 8);
    }
}
