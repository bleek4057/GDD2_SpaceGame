using UnityEngine;
using System.Collections;

public class FullAuto : MonoBehaviour
{
    public NoGravFPSController owner = null;
    public float kickback;
    public float fireRate;

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

    public bool Fire(float count, bool mouseHeld)
    {
        if (count >= fireRate && mouseHeld)
        {
            owner.Body.AddForce(kickback * (-owner.transform.forward), ForceMode.Impulse);
            Debug.DrawRay(owner.transform.position, owner.transform.forward, Color.red, 5);
            return Physics.Raycast(owner.transform.position, owner.transform.forward, 25, 8);
        }

        return false;
    }
}
