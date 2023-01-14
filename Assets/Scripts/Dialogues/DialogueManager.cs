using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text textName;
    public Text textDialog;
    public Animator anim;
    private Queue<string> sentences;
    
    private void Awake()
    {
        GameObjectManager.instance.allObject.Add(gameObject);
    }
    private void OnDestroy()
    {
        GameObjectManager.instance.allObject.Remove(gameObject);
    }
    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        anim.SetBool("DialOpen", true);
        textName.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        textDialog.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            textDialog.text += letter;
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void EndDialog()
    {
        anim.SetBool("DialOpen", false);
    }
}
