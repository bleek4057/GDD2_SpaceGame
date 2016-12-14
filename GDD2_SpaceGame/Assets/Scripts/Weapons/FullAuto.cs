using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FullAuto : MonoBehaviour
{
    public NoGravFPSController owner;
    public float kickback;
    public float fireRate;
    public float damage;

    private float count;

    // Use this for initialization
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
    }

    void TestHit(Vector3 localForward)
    {
        RaycastHit hit;
        if (Physics.Raycast(owner.transform.position, localForward, out hit, 25f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //hit.rigidbody.AddForce(kickback * (-hit.normal), ForceMode.Impulse);
                hit.collider.GetComponentInParent<NoGravFPSController>().RpcApplyImpulse(kickback, -hit.normal);

                Health health = hit.collider.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }

    public void Fire(bool mouseHeld, Vector3 localForward)
    {
        if (count >= fireRate && mouseHeld)
        {
            count = 0;

            GetComponentInParent<Inventory>().ResetWeaponCool();

            owner.RpcApplyImpulse(kickback, -localForward);
            Debug.DrawRay(owner.transform.position, localForward * 25, Color.red, 5);
            owner.RpcMuzzleFlash();

            TestHit(localForward);
        }
    }
}
