using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {

    public Text qText;  //問題表示用テキスト
    private int qNum = 1;    　//問題番号
    public string answerTag;    //それぞれの問題の果物を確かめるタグ

    public GameObject goal;     //オブジェクトを持っていく場所

    public QuestionObject[] questions;      //Questionオブジェクトのプレハブ
    public SpriteRenderer[] selectionImages;   //動かす果物オブジェクトを入れる

    public GameObject activeCharacter;     //現在扱っている果物のキャラクター

    private void Start() {

        //各果物のQuestionオブジェクトのanswerSpriteを取って、動かす果物オブジェクトを表示する
        for (int i = 0; i < questions.Length; i++){
            selectionImages[i].sprite = questions[i].answerSprite;
            selectionImages[i].tag = questions[i].answerTag;
        }
    }

    private void Update() {

        //Debug.Log(qNum);

        //今の対応する問題のデータを持ったquestionObjectを決める
        QuestionObject questionObject = questions[qNum - 1];

        //問題文を呼び出す
        qText.GetComponent<Text>().text = questionObject.questionMessage;

        //goalの果物を呼び出す
        goal.GetComponent<SpriteRenderer>().sprite = questionObject.goalSprite;

        //タグを呼び出す
        answerTag = questionObject.answerTag;

        //現在activeなキャラクターがいない(次の問題に移った時)場合に、Characterを生成する
        if(activeCharacter == null) {
            activeCharacter = Instantiate(questionObject.Character) as GameObject;
        }

    }

    public void GoNextQuestion(){

        qNum++;

        //次の問題に移るタイミングで、今のactiveCharacterを消去する
        Destroy(activeCharacter);

    }


}
