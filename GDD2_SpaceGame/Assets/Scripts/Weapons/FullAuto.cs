using UnityEngine;
using System.Collections;

public class FullAuto : MonoBehaviour
{
    public NoGravFPSController owner;
    public float kickback;
    public float fireRate;
    public int damage;

    //private float count;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //count += Time.deltaTime;
    }

    public bool Fire(ref float count, bool mouseHeld)
    {
        if (count >= fireRate && mouseHeld)
        {
            count = 0;

            owner.Body.AddForce(kickback * (-owner.transform.forward), ForceMode.Impulse);
            Debug.DrawRay(owner.transform.position, owner.transform.forward * 25, Color.red, 5);
            owner.muzzleFlash().Emit(3);
            owner.muzzleFlash().Stop();

            RaycastHit hit;
            if (Physics.Raycast(owner.transform.position, owner.transform.forward, out hit, 25f))
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
