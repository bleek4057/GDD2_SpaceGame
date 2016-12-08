using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SemiAuto : NetworkBehaviour
{
    public NoGravFPSController owner;
    public float kickback;
    public float fireRate;
    public int damage;

    private float count;

    // Use this for initialization
    void Start ()
    {
        count = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        count += Time.deltaTime;
    }

    [Command]
    void CmdTestHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(owner.transform.position, owner.transform.forward, out hit, 15f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                hit.collider.GetComponentInParent<NoGravFPSController>().RpcApplyImpulse(kickback, -hit.normal);

                Health health = hit.collider.GetComponentInParent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }

    public void Fire(bool mouseHeld)
    {
        if (count >= fireRate && !mouseHeld)
        {
            count = 0;
            //GetComponentInParent<Inventory>().ResetWeaponCool();

            owner.Body.AddForce(kickback * (-owner.transform.forward), ForceMode.Impulse);
            Debug.DrawRay(owner.transform.position, owner.transform.forward * 25, Color.red, 1);
            owner.muzzleFlash().Emit(3);
            owner.muzzleFlash().Stop();

            CmdTestHit();
        }
    }
}
