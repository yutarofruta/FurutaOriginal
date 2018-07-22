using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour {

    private GameObject leftBasket;
    private GameObject rightBasket;
    private GameObject answerBasket;

    private GameObject questionManager;

    public string answerTag;
    public bool isTouchable = true;
    private bool isCorrect = false;

    private Vector3 normalScale;
    private Vector3 expandedScale;

    // Use this for initialization
    void Start () {
        leftBasket = GameObject.Find("LeftBasket");
        rightBasket = GameObject.Find("RightBasket");
        questionManager = GameObject.Find("QuestionManager");

        //拡大・縮小用スケール
        normalScale = transform.localScale;
        expandedScale = normalScale * 1.3f;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnDrag() {

        //Touchableでないときはオブジェクトを移動させない
        if (!isTouchable) {
            return;
        }

        //ドラッグされている間は拡大
        transform.localScale = expandedScale;

        //最前面に出す
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;

        //答えのbasketを決める
        answerBasket = (leftBasket.tag == gameObject.tag) ? leftBasket : rightBasket;

        //distanceを指定
        float distance = Vector3.Distance(transform.position, answerBasket.transform.position);

        //正解のオブジェクトがゴールの十分近くに来たら、静止してCLEARに移る
        if (distance < 0.7f && gameObject.tag == answerBasket.tag) {

            isCorrect = true;

            transform.localScale = normalScale;

            Debug.Log("いいぞ！");

        }
        else {  //オブジェクトがタッチについてくる

            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                Vector3 vec = touch.position;
                vec.z = 10f;
                vec = Camera.main.ScreenToWorldPoint(vec);
                transform.position = vec;
            }
            else if (Input.GetMouseButton(0)) {
                Vector3 vec = Input.mousePosition;
                vec.z = 10f;
                vec = Camera.main.ScreenToWorldPoint(vec);
                transform.position = vec;
            }
        }
        //オブジェクト移動中はSpringJointを無効にする    
        gameObject.GetComponent<SpringJoint2D>().enabled = false;
    }



    //EventSystem

    public void EndDrag() {

        //ドラッグが終わったら縮小
        transform.localScale = normalScale;

        //元のOrder In Layerに戻す
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

        //ドラッグが終わったと時に正解の場所でなかったらSpringJointを有効にする
        if (!isCorrect) {
            gameObject.GetComponent<SpringJoint2D>().enabled = true;
        }
    }

    public void PointerDown() {

        if (!isTouchable) {
            return;
        }

        //最前面に出す
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;

        //オブジェクトが触られている間は拡大
        transform.localScale = expandedScale;
    }

    public void PointerUp() {

        //元のOrder In Layerに戻す
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

        //オブジェクトが離されたら縮小
        transform.localScale = normalScale;
    }

}

