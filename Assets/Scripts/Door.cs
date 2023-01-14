using UnityEngine;
using System;
public class Door : MonoBehaviour
{
    private Action action;
    [SerializeField] private GameController gameController;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Player player))
        {
            action = () => {
                GetComponent<SpriteRenderer>().color = Color.gray;
                GetComponent<Collider2D>().enabled = false;
            };
            
            Popup popup = UIController.Instance.CreatePopup();
            popup.InitDoor(UIController.Instance.MainCanvas,
                "Open the door for 56 bones?",
                "Not",
                "Sure!",
                action
            );
        }
    }
}
