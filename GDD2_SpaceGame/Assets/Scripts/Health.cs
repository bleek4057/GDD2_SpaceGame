using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{
    public const int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public float currentHealth = maxHealth;

    public RectTransform healthBar;

    void Update()
    {
        if(isLocalPlayer)
        {
            return;
        }
    }

    public void TakeDamage(float amount)
    {
        if(!isServer) { return; }

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            //for(int i = 0; i < players.Length; i++)
            //{
            //    players[i].GetComponent<Health>().RpcRespawn();
            //}
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.GetComponent<Health>().currentHealth <= 0)
                {
                    player.GetComponent<Health>().RpcRespawn(false);
                }
                else
                {
                    player.GetComponent<Health>().RpcRespawn(true);
                }
            }
            currentHealth = maxHealth;
        }
    }

    void OnChangeHealth(float currentHealth)
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);

        if (isLocalPlayer) { GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().ChangeHealth((int)currentHealth); }
    }

    [ClientRpc]
    void RpcRespawn(bool winner)
    {
        if(isLocalPlayer)
        {
            //if(currentHealth <= 0) 
            //{
            //    //This player lost
            //    GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().ShowEndGame(false);
            //}
            //else {
            //    //This player won
            //    GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().ShowEndGame(true);
            //}

            GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().ShowEndGame(winner);

            GetComponent<Inventory>().CmdSetWeapon(0);
            GetComponent<NoGravFPSController>().RespawnPosition();
        }
    }
}
