using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupingQuestionManager : MonoBehaviour {

    private int qNum = 1;    　//問題番号
    private int maxNum;     //問題数(QuestionObjectの数)
    public int correctNum = 0;      //正解の数

    public GroupingQuestionObject[] groupingQuestions;      //GroupingQuestionオブジェクトのプレハブ

    public GameObject leftBasket;     //左のかご
    public GameObject rightBasket;     //右のかご

    private GameObject leftCharacter;     //左のキャラクター
    private GameObject rightCharacter;     //右のキャラクター

    public GameObject fruit;     //sprite入れる前の果物

    public GameObject fruitsParent;     //Instanceする果物の親

    private GameObject[] leftFruits;     //左の果物を入れる
    private GameObject[] rightFruits;    //右の果物を入れる

    private string leftFruitTag;    //左の果物のタグ
    private string rightFruitTag;    //左の果物のタグ


    private void Start() {

        //問題の数を取得
        maxNum = groupingQuestions.Length;

        //一問目を表示
        GoNextQuestion();
    }
    
    public void GoNextQuestion() {

        //一問目でなければ、前の果物を消去し、correctNumも初期化する
        if(qNum != 1) {
            for(int i = 0; i < leftFruits.Length; i++) {
                Destroy(leftFruits[i]);
            }
            for (int i = 0; i < rightFruits.Length; i++) {
                Destroy(rightFruits[i]);
            }
            correctNum = 0;
        }
        
        if (qNum <= maxNum) {

            //今の対応する問題のデータを持ったquestionObjectを決める
            GroupingQuestionObject questionObject = groupingQuestions[qNum - 1];

            //バスケットにタグをつける
            leftBasket.tag = questionObject.leftFruitTag;
            rightBasket.tag = questionObject.rightFruitTag;

            //fruitのオブジェクトを入れる配列の数を、各問題の果物数に合わせる
            leftFruits = new GameObject[questionObject.leftFruitNum];
            rightFruits = new GameObject[questionObject.rightFruitNum];

            //左の果物を生成する
            for (int i = 0; i < questionObject.leftFruitNum; i++) {
                leftFruits[i] = Instantiate(fruit, questionObject.leftFruitPos[i], Quaternion.identity);
                leftFruits[i].tag = questionObject.leftFruitTag;
                leftFruits[i].GetComponent<SpriteRenderer>().sprite = questionObject.leftFruit;
                leftFruits[i].GetComponent<SpringJoint2D>().connectedAnchor = questionObject.leftFruitPos[i];
            }

            //右の果物を生成する
            for (int i = 0; i < questionObject.rightFruitNum; i++) {
                rightFruits[i] = Instantiate(fruit, questionObject.rightFruitPos[i], Quaternion.identity);
                rightFruits[i].tag = questionObject.rightFruitTag;
                rightFruits[i].GetComponent<SpriteRenderer>().sprite = questionObject.rightFruit;
                rightFruits[i].GetComponent<SpringJoint2D>().connectedAnchor = questionObject.rightFruitPos[i];
            }

        }
        else {
            //終了
        }

        //問題番号を1増やす
        qNum++;
    }

    public void AddCorrectNum() {

        //正解数を加算
        correctNum++;

        //正解数が果物の数と同じになったら次の問題へ
        if(correctNum == leftFruits.Length + rightFruits.Length) {
            GoNextQuestion();
        }
    }
}
