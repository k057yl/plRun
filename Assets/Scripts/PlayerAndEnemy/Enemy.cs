using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Data
{

    GameObject player;

    public GameObject markPrefab;
    public GameObject bonePrefab;

    internal GameObject mark;
    internal GameObject bone;
    

    float rotateZ;
    Vector3 diference;
    private void Awake()
    {
        player = GameObjectManager.instance.allObject[0];
    }
    private void Update()
    {
        EnemyFollow();
        EnemyDead();
    }
    public void EnemyFollow()
    {
        rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg - 90f;
        diference = player.transform.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
    }
    public void EnemyDead()
    {
        if (hp <= 0)
        {
            GameController.onEnemyKilled?.Invoke();
            Destroy(gameObject);
            bone = Instantiate(bonePrefab, transform.position, Quaternion.identity);
        }
    }
}
