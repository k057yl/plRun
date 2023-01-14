using UnityEngine;

public class CarBoom : MonoBehaviour
{
    internal int hp = 15;
    private void Update()
    {
        if(hp <= 0) 
        {
            Destroy(gameObject.GetComponent<Collider2D>());
        }
    }
}
