using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Laser : NetworkBehaviour
{
    public NoGravFPSController owner;
    public float kickback;
    public float fireRate;
    public int damage;

    private int count;

    // Use this for initialization
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Command]
    void CmdTestHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(owner.transform.position, owner.transform.forward, out hit, 15f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                hit.rigidbody.AddForce(kickback * (-hit.normal), ForceMode.Impulse);

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
        if (count >= fireRate && mouseHeld)
        {
            count = 0;
            //GetComponentInParent<Inventory>().ResetWeaponCool();

            owner.Body.AddForce(kickback * (-owner.transform.forward), ForceMode.Impulse);
            Debug.DrawRay(owner.transform.position, owner.transform.forward * 10, Color.red, 1);
            owner.laserBeam().Emit(1);

            CmdTestHit();
        }
    }
}
