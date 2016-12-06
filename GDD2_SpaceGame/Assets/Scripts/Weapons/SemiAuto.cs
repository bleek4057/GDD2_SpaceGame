using UnityEngine;
using System.Collections;

public class SemiAuto : MonoBehaviour
{
    public NoGravFPSController owner;
    public float kickback;
    public float fireRate;
    public int damage;

    //private float count;
    //private bool ableToFire;

    // Use this for initialization
    void Start ()
    {
        //ableToFire = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //count += Time.deltaTime;

        //if(Input.GetMouseButtonUp(0)) { ableToFire = true; }
    }

    public bool Fire(ref float count, bool mouseHeld)
    {
        if (count >= fireRate && !mouseHeld)
        {
            count = 0;
            //ableToFire = false;

            owner.Body.AddForce(kickback * (-owner.transform.forward), ForceMode.Impulse);
            Debug.DrawRay(owner.transform.position, owner.transform.forward * 25, Color.red, 1);
            owner.muzzleFlash().Emit(3);
            owner.muzzleFlash().Stop();

            RaycastHit hit;
            if(Physics.Raycast(owner.transform.position, owner.transform.forward, out hit, 25f))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    hit.rigidbody.AddForce(kickback * (-hit.normal), ForceMode.Impulse);
                    hit.collider.GetComponentInParent<NoGravFPSController>().TakeDamage(damage);
                    return true;
                }
            }
            return false;
        }

        return false;
    }
}
