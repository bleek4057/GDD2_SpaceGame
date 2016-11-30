using UnityEngine;
using System.Collections;

public class NoGravFPSController : MonoBehaviour {

    public float impulsePower;
    public float jumpPower;
    public float mouseSensitvity;
    public float maxForce;
    public GameManager inventory;

    private Rigidbody body;
    private float yaw;
    private float pitch;

    public Rigidbody Body
    {
        get { return body; }
    }

	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        yaw += mouseSensitvity * Input.GetAxis("Mouse X");
        pitch -= mouseSensitvity * Input.GetAxis("Mouse Y");

        transform.localEulerAngles = new Vector3(pitch, yaw, 0);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float vx = Input.GetAxis("Horizontal");
        float vz = Input.GetAxis("Vertical");
        float vy = Input.GetAxis("VerticalStrafe");

        Vector3 force = new Vector3(vx, vy, vz) * impulsePower;

        body.AddForce(transform.right * force.x, ForceMode.Force);
        body.AddForce(transform.forward * force.z, ForceMode.Force);
        body.AddForce(transform.up * force.y, ForceMode.Force);

        body.velocity = Vector3.ClampMagnitude(body.velocity, maxForce);
    }
}
