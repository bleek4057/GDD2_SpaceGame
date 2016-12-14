using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class NoGravFPSController : NetworkBehaviour
{
    public float impulsePower;
    public float jumpPower;
    public float jumpRadius;
    public float mouseSensitvity;
    public float maxForce;
    public Text healthText;
    public float respawnTimer;

    private Rigidbody body;
    private Inventory inventory;
    private ParticleSystem[] particleSystems;
    private float yaw;
    private float pitch;
    private bool mouseHeld;
    private Vector3 respawnPoint;
    private Quaternion respawnRotation;
    private float currentRespawnTime;
    private bool respawned = false;

    public Rigidbody Body
    {
        get { return body; }
    }

    public Inventory getInventory
    {
        get { return inventory; }
    }

    [ClientRpc]
    public void RpcLaserBeam()
    {
        particleSystems[0].Emit(1);
    }

    [ClientRpc]
    public void RpcMuzzleFlash()
    {
        particleSystems[1].Emit(3);
        particleSystems[1].Stop();
        //return particleSystems[1];
    }

    [ClientRpc]
    public void RpcApplyImpulse(float impulse, Vector3 direction)
    {
        body.AddForce(impulse * (direction), ForceMode.Impulse);
    }

    // Use this for initialization
    void Start ()
    {
        body = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        inventory.setOwner();
        mouseHeld = false;
        //healthText.text = health.ToString();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public override void OnStartLocalPlayer()
    {
        GetComponentInChildren<Camera>().enabled = true;
        GetComponentInChildren<AudioListener>().enabled = true;
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().SetPlayer(gameObject);

        yaw = Vector3.Angle(transform.forward, transform.position.normalized);

        respawnPoint = transform.position;
        respawnRotation = transform.rotation;
    }

    void Update()
    {
        if (!isLocalPlayer) { return; }

        if (respawned)
        {
            currentRespawnTime -= Time.deltaTime;
            if (currentRespawnTime <= 0)
            {
                respawned = false;
                GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        else
        {
            yaw += mouseSensitvity * Input.GetAxis("Mouse X");
            pitch -= mouseSensitvity * Input.GetAxis("Mouse Y");

            if (Input.GetMouseButton(0))
            {
                inventory.CmdFireActiveWeapon(mouseHeld, transform.forward);
                mouseHeld = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                mouseHeld = false;
                particleSystems[0].Clear();
            }

            transform.localEulerAngles = new Vector3(pitch, yaw, 0);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!isLocalPlayer) { return; }

        float vx = Input.GetAxis("Horizontal");
        float vz = Input.GetAxis("Vertical");
        float vy = Input.GetAxis("VerticalStrafe");

        Vector3 force = new Vector3(vx, vy, vz) * impulsePower;

        body.AddForce(transform.right * force.x, ForceMode.Force);
        body.AddForce(transform.forward * force.z, ForceMode.Force);
        body.AddForce(transform.up * force.y, ForceMode.Force);

        body.velocity = Vector3.ClampMagnitude(body.velocity, maxForce);
    }

    public void RespawnPosition()
    {
        transform.position = respawnPoint;
        transform.rotation = respawnRotation;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
        yaw = Vector3.Angle(transform.forward, transform.position.normalized);
        currentRespawnTime = respawnTimer;
        respawned = true;
    }
}
