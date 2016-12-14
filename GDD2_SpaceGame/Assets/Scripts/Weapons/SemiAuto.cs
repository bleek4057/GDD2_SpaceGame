using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SemiAuto : MonoBehaviour
{
    public NoGravFPSController owner;
    public float kickback;
    public float fireRate;
    public float damage;

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

    void TestHit(Vector3 localForward)
    {
        RaycastHit hit;
        if (Physics.Raycast(owner.transform.position + (localForward), localForward, out hit, 15f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //hit.rigidbody.AddForce(kickback * (-hit.normal), ForceMode.Impulse);
                hit.collider.GetComponentInParent<NoGravFPSController>().RpcApplyImpulse(kickback, -hit.normal);

                Health health = hit.collider.GetComponentInParent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }

    public void Fire(bool mouseHeld, Vector3 localForward)
    {
        if (count >= fireRate && !mouseHeld)
        {
            count = 0;
            //GetComponentInParent<Inventory>().ResetWeaponCool();

            owner.RpcApplyImpulse(kickback, -localForward);
            Debug.DrawRay(owner.transform.position + (localForward), localForward * 25, Color.red, 1);
            owner.RpcMuzzleFlash();

            TestHit(localForward);
        }
    }
}
