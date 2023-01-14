using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMiddle : Enemy
{

    [SerializeField] GameObject healtPrefab;

    GameObject healt;

    public void EnemyMiddleDead()
    {
        if (hp <= 0)
        {
            GameController.onEnemyKilled?.Invoke();
            Destroy(gameObject);
            healt = Instantiate(healtPrefab, transform.position, Quaternion.identity);
            bone = Instantiate(bonePrefab, transform.position, Quaternion.identity);
        }
    }
}
