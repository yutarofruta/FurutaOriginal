using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {

    public Text qText;
    public int qNum = 1;
    public string answerTag;

    public GameObject goal;

    public Sprite grapeFruit;
    public Sprite cherryFruit;
    public Sprite melonFruit;

    public QuestionObject[] questions;
    public SpriteRenderer[] answerImages;
    GameObject activeCharacter;

    private void Start() {
        for (int i = 0; i < questions.Length; i++){
            answerImages[i].sprite = questions[i].answerSprite;
        }
    }

    private void Update() {

        Debug.Log(qNum);

        QuestionObject questionObject = questions[qNum - 1];

        qText.GetComponent<Text>().text = questionObject.questionMessage;
        goal.GetComponent<SpriteRenderer>().sprite = questionObject.answerSprite;
        answerTag = questionObject.asnwerTag;
        if(activeCharacter == null)
            activeCharacter = Instantiate(questionObject.Character) as GameObject;

        /*
        if(qNum == 1) {//一問目

            qText.GetComponent<Text>().text = "Which is Grape?";
            goal.GetComponent<SpriteRenderer>().sprite = grapeFruit;
            answerTag = "grape";

        }
        else if (qNum == 2) {//二問目

            qText.GetComponent<Text>().text = "Which is Cherry?";
            goal.GetComponent<SpriteRenderer>().sprite = cherryFruit;
            answerTag = "cherry";

        }
        else if(qNum == 3) {//三問目

            qText.GetComponent<Text>().text = "Which is Melon?";
            goal.GetComponent<SpriteRenderer>().sprite = melonFruit;
            answerTag = "melon";

        }
        */
    }

    public void GoNextQuestion(){
        qNum++;
        Destroy(activeCharacter);
    }


}
