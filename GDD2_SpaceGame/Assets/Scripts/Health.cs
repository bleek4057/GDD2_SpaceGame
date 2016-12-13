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

        if (isLocalPlayer) { GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().ChangeHealth(currentHealth); }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if(isLocalPlayer)
        {
            if(currentHealth <= 0) 
            {
                //This player lost
                GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().ShowEndGame(false);
            }
            else {
                //This player won
                GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().ShowEndGame(true);
            }

            currentHealth = maxHealth;
            GetComponent<Inventory>().currentWeapon = 0;
            GetComponent<NoGravFPSController>().RespawnPosition();
        }
    }
}
