using UnityEngine;
using System.Collections;

public class Shotgun : MonoBehaviour
{
    public NoGravFPSController owner = null;
    public float kickback;
    public float fireRate;

    //private float count;
    private bool ableToFire;

    // Use this for initialization
    void Start()
    {
        ableToFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        //count += Time.deltaTime;

        if (Input.GetMouseButtonUp(0)) { ableToFire = true; }
    }

    public bool Fire(float count, bool mouseHeld)
    {
        if (count >= fireRate && !mouseHeld)
        {
            count = 0;
            ableToFire = false;

            owner.Body.AddForce(kickback * (-owner.transform.forward), ForceMode.Impulse);
            Debug.DrawRay(owner.transform.position, owner.transform.forward, Color.red, 5);
            return Physics.Raycast(owner.transform.position, owner.transform.forward, 25, 8);
        }

        return false;
    }
}
