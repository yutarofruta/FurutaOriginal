using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectingQuestionManager : MonoBehaviour {

    public Text qText;  //問題表示用テキスト
    public Text finText;    //ゲーム終了時に出すテキスト
    private int qNum = 1;    　//問題番号
    private int maxNum;     //問題数

    public GameObject target;       //キャラクターの停止位置
    public GameObject goal;     //オブジェクトを持っていく場所

    public SelectingQuestionObject[] selectingQuestions;      //Questionオブジェクトのプレハブ
    public SpriteRenderer[] choiceImages;   //Chiceにあたるオブジェクトを入れる

    public GameObject activeCharacter;     //現在扱っている果物のキャラクター
    public GameObject clearedChoice;        //消去する

    private void Start() {

        //selectingQuestionsをシャッフル
        QuestionShuffle();

        //Choiceの場所をシャッフル
        ChoiceImageShuffle();

        //各果物のQuestionオブジェクトのanswerSpriteを取って、動かす果物Choiceオブジェクトを表示する
        for (int i = 0; i < selectingQuestions.Length; i++){
            choiceImages[i].sprite = selectingQuestions[i].answerSprite;
            choiceImages[i].tag = selectingQuestions[i].answerTag;
        }

        //問題の数を取得
        maxNum = selectingQuestions.Length;

        //一問目を表示
        GoNextQuestion();
    }

    private void Update() {     

    }

    public void GoNextQuestion(){

        //次の問題に移るタイミングで、今のactiveCharacterを消去する
        if (activeCharacter != null) {
            Destroy(activeCharacter);
            Destroy(clearedChoice);
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

            //Characterを生成する
            activeCharacter = Instantiate(questionObject.Character) as GameObject;

            //targetの位置を指定
            activeCharacter.GetComponent<GoBackCharacter>().SetMoveTarget(target, 0.1f);
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

    // selectingQuestionsをシャッフルする
    public void QuestionShuffle() {
        for (int index1 = 0; index1 < selectingQuestions.Length; index1++) {
            int index2 = Random.Range(0, selectingQuestions.Length - 1);
            QuestionSwap(ref selectingQuestions[index1], ref selectingQuestions[index2]);
        }
    }

    // question1 と questioon2 を入れ替える
    private void QuestionSwap(ref SelectingQuestionObject question1, ref SelectingQuestionObject question2) {
        SelectingQuestionObject question = question1;
        question1 = question2;
        question2 = question;
    }

    // choiceImagesをシャッフルする
    public void ChoiceImageShuffle() {
        for (int index1 = 0; index1 < choiceImages.Length; index1++) {
            int index2 = Random.Range(0, choiceImages.Length - 1);
            ChoiceImageSwap(ref choiceImages[index1], ref choiceImages[index2]);
        }
    }

    // choice1 と choice2 を入れ替える
    private void ChoiceImageSwap(ref SpriteRenderer choice1, ref SpriteRenderer choice2) {
        SpriteRenderer choice = choice1;
        choice1 = choice2;
        choice2 = choice;
    }





}
