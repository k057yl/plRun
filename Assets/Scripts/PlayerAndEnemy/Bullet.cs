
using UnityEngine;
using UnityEngine.XR;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private void Update()
    {
        Destroy(gameObject, 1.5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Enemy enemy)) 
        {
            Vector2 position = collision.contacts[0].point;
            enemy.mark = Instantiate(enemy.markPrefab, position, Quaternion.identity);
            Destroy(enemy.mark, 0.2f);
            enemy.hp -= 5;
            Destroy(gameObject);
        }
        if(collision.gameObject.TryGetComponent(out CarBoom car))
        {
            car.hp -= 5;
            Destroy(gameObject);
        }
    }
}
