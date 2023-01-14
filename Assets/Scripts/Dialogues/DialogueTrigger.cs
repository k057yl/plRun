using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    GameObject dial;   
    public Dialogue dialogue;

    public void NPCDialog()
    {
        dial = GameObjectManager.instance.allObject[0];
        dial.GetComponent<DialogueManager>().StartDialogue(dialogue);
    }
}
