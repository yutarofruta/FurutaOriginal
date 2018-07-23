using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectingQuestionManager : MonoBehaviour {

    public Text qText;  //問題表示用テキスト
    public Text finText;    //ゲーム終了時に出すテキスト
    private int qNum = 1;    　//問題番号
    private int maxNum;     //問題数

    public GameObject goal;     //オブジェクトを持っていく場所

    public SelectingQuestionObject[] selectingQuestions;      //Questionオブジェクトのプレハブ
    public SpriteRenderer[] selectionImages;   //動かす果物オブジェクトを入れる

    public GameObject activeCharacter;     //現在扱っている果物のキャラクター

    private void Start() {

        //各果物のQuestionオブジェクトのanswerSpriteを取って、動かす果物オブジェクトを表示する
        for (int i = 0; i < selectingQuestions.Length; i++){
            selectionImages[i].sprite = selectingQuestions[i].answerSprite;
            selectionImages[i].tag = selectingQuestions[i].answerTag;
        }

        //問題の数を取得
        maxNum = selectingQuestions.Length;

        //一問目を表示
        GoNextQuestion();
    }

    private void Update() {     

    }

    public void GoNextQuestion(){

        Debug.Log("qNum" + qNum);
        Debug.Log("maxNum" + maxNum);

        //次の問題に移るタイミングで、今のactiveCharacterを消去する
        
        if (activeCharacter != null) {
            Destroy(activeCharacter);
            Debug.Log("destroy");
        }
        

        if (qNum <= maxNum) {

            //今の対応する問題のデータを持ったquestionObjectを決める
            SelectingQuestionObject questionObject = selectingQuestions[qNum - 1];

            //問題文を呼び出す
            qText.GetComponent<Text>().text = questionObject.questionMessage;

            //goalの果物を呼び出す
            goal.GetComponent<SpriteRenderer>().sprite = questionObject.goalSprite;

            //goalのタグを答えのタグと同じにする
            goal.tag = questionObject.answerTag;

            //現在activeなキャラクターがいない(次の問題に移った時)場合に、Characterを生成する
            if (activeCharacter == null) {
                activeCharacter = Instantiate(questionObject.Character) as GameObject;
            }
        }

        //全部の問題が終わったら
        if (qNum > maxNum) {

            //問題文を消す
            qText.GetComponent<Text>().text = "";

            //ゴールのスプライトを消す
            Destroy(goal);

            //問題文を消す
            finText.GetComponent<Text>().text = "AWESOME!";


        }

        //問題番号を1増やす
        qNum++;


    }


}
