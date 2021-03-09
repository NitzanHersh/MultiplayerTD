using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        PlayerHealth health = hit.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(10);
        }


        Destroy(gameObject);

    }
}
