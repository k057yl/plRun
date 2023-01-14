using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] Text textKilled;
    [SerializeField] Text textHp;
    [SerializeField] Text textBone;


    private void Update()
    {
        textBone.text = "Bone : " + gameController.boneUi;
        textKilled.text = "Kill : " + gameController.killedUi;
        textHp.text = "Hp : " + gameController.hpUi;
    }
}
