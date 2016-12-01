using UnityEngine;
using System.Collections;

public class NoGravFPSController : MonoBehaviour {

    public float impulsePower;
    public float jumpPower;
    public float jumpRadius;
    public float mouseSensitvity;
    public float maxForce;

    private Rigidbody body;
    private Inventory inventory;
    private float yaw;
    private float pitch;
    private bool mouseHeld;

    public Rigidbody Body
    {
        get { return body; }
    }

    public Inventory getInventory
    {
        get { return inventory; }
    }

	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
        inventory.setOwner();
        mouseHeld = false;
    }

    void Update()
    {
        yaw += mouseSensitvity * Input.GetAxis("Mouse X");
        pitch -= mouseSensitvity * Input.GetAxis("Mouse Y");
        
        if (Input.GetMouseButton(0))
        {
            inventory.fireActiveWeapon(mouseHeld);
            mouseHeld = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            mouseHeld = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, jumpRadius);
            Collider closest = null;
            foreach (Collider collider in colliders)
            {
                if (collider != GetComponent<Collider>())
                {
                    if (closest == null)
                    {
                        closest = collider;
                    }
                    else
                    {
                        float closestDistance = (transform.position - closest.ClosestPointOnBounds(transform.position)).magnitude;
                        float newDistance = (transform.position - collider.ClosestPointOnBounds(transform.position)).magnitude;

                        if (newDistance < closestDistance)
                        {
                            closest = collider;
                        }
                    }
                }
            }

            if (closest != null)
            {
                Debug.Log(transform.position + " " + closest.ClosestPointOnBounds(transform.position));
                Vector3 direction = transform.position - closest.ClosestPointOnBounds(transform.position);
                direction.Normalize();
                direction *= jumpPower;

                Debug.Log(direction);

                body.AddForce(direction);
            }
        }

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
