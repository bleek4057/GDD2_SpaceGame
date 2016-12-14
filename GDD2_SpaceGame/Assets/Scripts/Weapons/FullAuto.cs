using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FullAuto : MonoBehaviour
{
    public NoGravFPSController owner;
    public float kickback;
    public float fireRate;
    public float damage;
    public float range;

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

    void TestHit(Vector3 localForward, Vector3 localUp)
    {
        RaycastHit hit;
        if (Physics.Raycast(owner.transform.position + (0.5f * localUp), localForward, out hit, range))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                hit.collider.GetComponentInParent<NoGravFPSController>().RpcApplyImpulse(kickback, -hit.normal);

                Health health = hit.collider.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }

    public void Fire(bool mouseHeld, Vector3 localForward, Vector3 localUp)
    {
        if (count >= fireRate && mouseHeld)
        {
            count = 0;

            GetComponentInParent<Inventory>().ResetWeaponCool();

            owner.RpcApplyImpulse(kickback, -localForward);
            Debug.DrawRay(owner.transform.position + (0.5f * localUp), localForward * range, Color.red, 5);
            owner.RpcMuzzleFlash();

            TestHit(localForward, localUp);
        }
    }
}
