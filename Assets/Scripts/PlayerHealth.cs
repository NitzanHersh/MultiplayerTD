using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    public const int maxHealth = 100;
    [SyncVar (hook = nameof(OnChangeHealth))] public int currentHealth = maxHealth;
    public RectTransform healthBar;
    public bool destroyOnDeath;

    private NetworkStartPosition[] _spawnPoints;

    void Start()
    {
        if (isLocalPlayer)
        {
            _spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            return;
        }

        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                //currentHealth = 0;
                Debug.Log("Im dead already!");
                RpcRespawn();
            }
            
        }

        
    }


    void OnChangeHealth(int currentHealth, int health)
    {
        healthBar.sizeDelta = new Vector2(health * 2, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            currentHealth = maxHealth;
            Vector3 spawnPoint = Vector3.zero;

            if(_spawnPoints != null && _spawnPoints.Length > 0)
            {
                spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;
            }

            transform.position = spawnPoint;
            
        }
    }
}
