using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerScript : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;


    public override void OnStartLocalPlayer()
    {
        //base.OnStartLocalPlayer();
        //will change the color of the localplayer only
        GetComponent<MeshRenderer>().material.color = Color.cyan;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isLocalPlayer && this.hasAuthority)
        {

            if (Input.GetKeyDown(KeyCode.W))
            {
                CmdPressW();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                CmdPressS();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                CmdPressD();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                CmdPressA();
            }

        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
       
    }
    [Command]
    void CmdFire()
    {
        //Create bullet from prefab
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        //Add velocity to bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6.0f;

        //spawn bullet on clients
        NetworkServer.Spawn(bullet);

        // Destroy bullet after 2 sec
        Destroy(bullet, 2f);
    }

    [Command]
    private void CmdPressW()
    {
        RpcPressW();
    }

    [Command]
    private void CmdPressS()
    {
        RpcPressS();
    }

    [Command]
    private void CmdPressD()
    {
        RpcPressD();
    }

    [Command]
    private void CmdPressA()
    {
        RpcPressA();
    }

    [ClientRpc]
    private void RpcPressW()
    {
        transform.Translate(Vector3.forward);
    }

    [ClientRpc]
    private void RpcPressS()
    {
        transform.Translate(Vector3.back);
    }

    [ClientRpc]
    private void RpcPressD()
    {
        transform.Translate(Vector3.right);
    }

    [ClientRpc]
    private void RpcPressA()
    {
        transform.Translate(Vector3.left);
    }

}
