using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{
    public const int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public RectTransform healthBar;

    public void TakeDamage(float amount)
    {
        if(!isServer) { return; }

        currentHealth -= (int)amount;
        if(currentHealth <= 0)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                player.GetComponent<Health>().RpcRespawn();
            }
        }
    }

    void OnChangeHealth(int currentHealth)
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if(isLocalPlayer)
        {
            currentHealth = maxHealth;
            GetComponent<Inventory>().currentWeapon = 0;
            GetComponent<NoGravFPSController>().RespawnPosition();
        }
    }
}
