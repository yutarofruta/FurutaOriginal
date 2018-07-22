using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {

    public Text qText;  //問題表示用テキスト
    public Text finText;    //ゲーム終了時に出すテキスト
    private int qNum = 1;    　//問題番号
    public int maxNum;
    public string answerTag;    //それぞれの問題の果物を確かめるタグ

    public GameObject goal;     //オブジェクトを持っていく場所

    public QuestionObject[] groupingQuestions;      //Questionオブジェクトのプレハブ
    public SpriteRenderer[] selectionImages;   //動かす果物オブジェクトを入れる

    public GameObject activeCharacter;     //現在扱っている果物のキャラクター

    private void Start() {

        //各果物のQuestionオブジェクトのanswerSpriteを取って、動かす果物オブジェクトを表示する
        for (int i = 0; i < groupingQuestions.Length; i++){
            selectionImages[i].sprite = groupingQuestions[i].answerSprite;
            selectionImages[i].tag = groupingQuestions[i].answerTag;
        }

        //問題の数を取得
        maxNum = groupingQuestions.Length;
    }

    private void Update() {

        //Debug.Log(qNum);

        if(qNum <= maxNum) {

            //今の対応する問題のデータを持ったquestionObjectを決める
            QuestionObject questionObject = groupingQuestions[qNum - 1];

            //問題文を呼び出す
            qText.GetComponent<Text>().text = questionObject.questionMessage;

            //goalの果物を呼び出す
            goal.GetComponent<SpriteRenderer>().sprite = questionObject.goalSprite;

            //タグを呼び出す
            answerTag = questionObject.answerTag;

            //現在activeなキャラクターがいない(次の問題に移った時)場合に、Characterを生成する
            if (activeCharacter == null) {
                activeCharacter = Instantiate(questionObject.Character) as GameObject;
            }
        }
        

    }

    public void GoNextQuestion(){

        //問題番号を1増やす
        qNum++;

        //次の問題に移るタイミングで、今のactiveCharacterを消去する
        Destroy(activeCharacter);

        if (qNum > maxNum) {

            //問題文を消す
            qText.GetComponent<Text>().text = "";

            //ゴールのスプライトを消す
            Destroy(goal);

            //問題文を消す
            finText.GetComponent<Text>().text = "AWESOME!";


        }


    }


}
