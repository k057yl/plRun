using System.Collections;
using UnityEngine;

public class TriggerSpawnEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
