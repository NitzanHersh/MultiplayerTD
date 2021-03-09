using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemySpawner : NetworkBehaviour
{
    public GameObject enemyPrefab;
    public int numOfEnemies;

    private Vector3 enemySpawnLocation;

    // Start is called before the first frame update
    public override void OnStartServer()
    {
        for(int i =0; i<numOfEnemies; i++)
        {
            enemySpawnLocation = new Vector3(Random.Range(-5, 7), 0, Random.Range(0, 10));

            GameObject enemy = (GameObject)Instantiate(enemyPrefab, enemySpawnLocation, Quaternion.identity);
            NetworkServer.Spawn(enemy);
        }
        
        


        

    }

    // Update is called once per frame
    void Update()
    {
     


    }


}
