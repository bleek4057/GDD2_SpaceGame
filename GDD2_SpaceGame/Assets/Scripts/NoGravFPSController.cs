using UnityEngine;
using System.Collections;

public class NoGravFPSController : MonoBehaviour {

    public float impulsePower;
    public float jumpPower;
    public float mouseSensitvity;
    public float rollPower;

    private Rigidbody body;
    private float yaw;
    private float pitch;
    private float roll;

	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        yaw += mouseSensitvity * Input.GetAxis("Mouse X");
        pitch -= mouseSensitvity * Input.GetAxis("Mouse Y");
        //roll -= rollPower * Input.GetAxis("Roll");
        roll = 0;

        transform.localEulerAngles = new Vector3(pitch, yaw, roll);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float vx = Input.GetAxis("Horizontal");
        float vz = Input.GetAxis("Vertical");
        float vy = Input.GetAxis("VerticalStrafe");

        body.AddForce(transform.right * vx * impulsePower);
        body.AddForce(transform.forward * vz * impulsePower);
        body.AddForce(transform.up * vy * impulsePower);
    }
}
