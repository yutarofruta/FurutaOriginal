using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BiggestQuestionManager : MonoBehaviour {

    private int qNum = 1;    　//問題番号
    private int maxNum;     //問題数(QuestionObjectの数)
    private float fruitScale;       //果物の拡大率
    private string fruitTag;    //果物のタグ

    public BiggestQuestionObject[] biggestQuestions;      //BiggestQuestionオブジェクトのプレハブ

    public GameObject activeCharacter;     //現在のキャラクター
    public GameObject fruit;     //sprite入れる前の果物プレハブ
    public GameObject fruitsParent;     //Instanceする果物の親
    private GameObject[] fruits;     //果物を入れる
    public GameObject target;       //キャラクターが真ん中でストップするときの目印

    private Vector3[] fruitPos;      //果物の配置場所

    public Text text;       //Awesomeのテキスト

    // Use this for initialization
    void Start () {
        //問題の数を取得
        maxNum = biggestQuestions.Length;

        //一問目を表示
        GoNextQuestion();
    }

    public void GoNextQuestion() {

        //一問目でなければ、残った果物を消去する
        if (qNum != 1) {
            for (int i = 0; i < fruits.Length; i++) {
                Destroy(fruits[i]);
            }
        }

        if (qNum <= maxNum) {

            //今の対応する問題のデータを持ったquestionObjectを決める
            BiggestQuestionObject questionObject = biggestQuestions[qNum - 1];

            //Characterを生成し、タグをつけて定位置に置く
            activeCharacter = Instantiate(questionObject.character) as GameObject;
            activeCharacter.transform.position = new Vector3(-11f, 1, 0);

            //fruitのオブジェクトを入れる配列の数を、各問題の果物数に合わせる
            fruits = new GameObject[questionObject.fruitNum];

            //果物の位置を入れる配列を初期化する
            fruitPos = new Vector3[questionObject.fruitNum];

            //果物の位置を取得する
            for (int i = 0; i < questionObject.fruitNum; i++) {
                fruitPos[i] = questionObject.fruitPos[i];
            }

            //果物の位置をシャッフルする
            PositionShuffle();

            //果物をシャッフル後の位置に生成する。
            for (int i = 0; i < questionObject.fruitNum; i++) {
                fruits[i] = Instantiate(fruit, fruitPos[i], Quaternion.identity);
                fruits[i].GetComponent<SpriteRenderer>().sprite = questionObject.fruit;
                fruits[i].GetComponent<SpringJoint2D>().connectedAnchor = fruitPos[i];
                fruits[i].transform.parent = fruitsParent.transform;

                //i=0にsmallestタグを付けて縮小、最後のiにbiggestタグをつけて拡大
                if(i == 0) {
                    fruits[i].tag = "smallest";
                    fruits[i].transform.localScale = new Vector3(4, 4, 1);
                }
                else if(i == questionObject.fruitNum - 1) {
                    fruits[i].tag = "biggest";
                    fruits[i].transform.localScale = new Vector3(9, 9, 1);
                }
            }
            activeCharacter.GetComponent<Game3Character>().SetMoveTarget(target, 0.1f);
        }
        else {
            //終了
            text.GetComponent<Text>().text = "AWESOME!";
        }

        //問題番号を1増やす
        qNum++;
    }

    // fruitPos[]の中身をシャッフルする
    public void PositionShuffle() {
        for (int index1 = 0; index1 < fruitPos.Length; index1++) {
            int index2 = Random.Range(0, fruitPos.Length);
            PositionSwap(ref fruitPos[index1], ref fruitPos[index2]);
        }
    }

    // position1 と position2 を入れ替える
    private void PositionSwap(ref Vector3 position1, ref Vector3 position2) {
        Vector3 postion = position1;
        position1 = position2;
        position2 = postion;
    }
}
