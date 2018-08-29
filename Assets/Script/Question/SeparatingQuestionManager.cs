using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeparatingQuestionManager : MonoBehaviour {

    public Text finText;    //ゲーム終了時に出すテキスト
    public Text leftCharacterText;      //左のキャラクターのセリフ
    public Text rightCharacterText;      //右のキャラクターのセリフ
    private int qNum = 1;    　//問題番号
    private int maxNum;     //問題数
    public int leftAnswerNum;        //左の果物数の正解
    public int rightAnswerNum;       //右の果物数の正解
    public int leftCurrentNum;        //左の現在の果物数
    public int rightCurrentNum;       //右の現在の果物数
    private bool isFinished = false;        //ゲーム終了

    public GameObject fruit;        //果物のプレハブ
    public GameObject[] activeFruits;       //画面上にある果物
    public GameObject[] characters;     //現在扱っている果物のキャラクター

    private SeparatingQuestionObject[] separatingQuestions;      //Questionオブジェクトのプレハブ


    // Use this for initialization
    void Start() {

        //問題を読み出す
        ReadQuestion();

        //出題順をシャッフル
        //QuestionShuffle();

        //問題の数を取得
        maxNum = separatingQuestions.Length;

        //一問目を表示
        GoNextQuestion();
    }

    // Update is called once per frame
    void Update() {

        //ゲームメニューに戻る
        if (isFinished && Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene("GameMenu");
        }
    }

    public void GoNextQuestion() {

        //一問目でなければ、前の果物を消去する
        if (qNum != 1) {
            for (int i = 0; i < activeFruits.Length; i++) {
                Destroy(activeFruits[i]);
            }
        }

        if (qNum <= maxNum) {      //問題出題中

            //今の対応する問題のデータを持ったquestionObjectを決める
            SeparatingQuestionObject questionObject = separatingQuestions[qNum - 1];

            //左右の果物の個数を取得する
            leftAnswerNum = questionObject.leftFruitNum;
            rightAnswerNum = questionObject.rightFruitNum;

            //キャラクターのセリフを答えの数に合わせる
            leftCharacterText.text = "I want to eat\n" + leftAnswerNum.ToString() + "\napples";
            rightCharacterText.text = "I want to eat\n" + rightAnswerNum.ToString() + "\napples";

            //activeFruits[]の要素数と果物の数を対応
            activeFruits = new GameObject[questionObject.fruitNum];

            //果物を生成する
            for (int i = 0; i < questionObject.fruitNum; i++) {
                activeFruits[i] = Instantiate(fruit, questionObject.fruitPos[i], Quaternion.identity);
                activeFruits[i].GetComponent<SpriteRenderer>().sortingOrder = 4;
            }
        }

        //全部の問題が終わったら
        if (qNum > maxNum) {

            //問題文を消す
            finText.GetComponent<Text>().text = "AWESOME!";

            //ゲーム終了
            isFinished = true;

            //次のレベルを遊べるようにする
            if (GameManager.openLevelDic["Game7"] == GameManager.levelNum) {
                GameManager.openLevelDic["Game7"] = GameManager.levelNum + 1;
                Debug.Log("レベルを開放する");
            }
        }

        //問題番号を1増やす
        qNum++;
    }

    //答えが正しいかどうかをチェックする

    //questionObjectをシャッフルする
    public void QuestionShuffle() {
        for (int index1 = 0; index1 < separatingQuestions.Length; index1++) {
            int index2 = Random.Range(0, separatingQuestions.Length);
            QuestionSwap(ref separatingQuestions[index1], ref separatingQuestions[index2]);
        }
    }

    // question1 と questioon2 を入れ替える
    private void QuestionSwap(ref SeparatingQuestionObject question1, ref SeparatingQuestionObject question2) {
        SeparatingQuestionObject question = question1;
        question1 = question2;
        question2 = question;
    }

    public void ReadQuestion() {

        //シーン名を取得する
        string sceneName = SceneManager.GetActiveScene().name;

        //Resourcesからlevelに対応する問題を読みだして、objectArrayに入れる
        object[] objectArray = Resources.LoadAll(sceneName + "_" + GameManager.levelNum.ToString(), typeof(SeparatingQuestionObject));

        //selectingQuestionの配列の大きさを、呼び出した問題の配列数と揃える
        System.Array.Resize(ref separatingQuestions, objectArray.Length);

        //objectArrayの中身をselectingQuestionに入れる
        for (int i = 0; i < objectArray.Length; i++) {
            separatingQuestions[i] = (SeparatingQuestionObject)objectArray[i];
        }
    }
}
