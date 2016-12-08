using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Shotgun : NetworkBehaviour
{
    public NoGravFPSController owner;
    public float kickback;
    public float fireRate;
    public int damage;

    private float count;
    private bool hit;

    // Use this for initialization
    void Start()
    {
        count = 0;
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
    }

    void TestHit(Transform player)
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, 15f))
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

    public void Fire(bool mouseHeld, Transform player)
    {
        if (count >= fireRate && !mouseHeld)
        {
            count = 0;
            //GetComponentInParent<Inventory>().ResetWeaponCool();

            owner.Body.AddForce(kickback * (-player.transform.forward), ForceMode.Impulse);
            Debug.DrawRay(player.transform.position, player.transform.forward * 15, Color.red, 5);
            owner.RpcMuzzleFlash();

            TestHit(player);
        }
        hit = false;
    }
}
