using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    public static GameObjectManager instance { get; private set; }
    public List<GameObject> allObject = new List<GameObject>();
    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != null)
        {
            Destroy(this.gameObject);
        }
    }
}
