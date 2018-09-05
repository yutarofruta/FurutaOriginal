using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceController : MonoBehaviour {

    public bool isTouchable = true;
    public bool isCorrect = false;

    private Vector3 normalScale;
    private Vector3 expandedScale;

    public GameObject currentCollision;

    // Use this for initialization
    void Start () {

        //拡大・縮小用スケール
        normalScale = transform.localScale;
        expandedScale = normalScale * 1.3f;
    }

    // Update is called once per frame
    void Update () {
	}


    //EventSystem

    public void OnDrag() {

        //Touchableでないときはオブジェクトを移動させない
        if (!isTouchable) {
            return;
        }

        //ドラッグされている間は拡大
        transform.localScale = expandedScale;

        //最前面に出す
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;

        //オブジェクトがタッチについてくる
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
        
        //オブジェクト移動中はSpringJointを無効にする    
        gameObject.GetComponent<SpringJoint2D>().enabled = false;
    }

    public void EndDrag() {

        //正解後は触らせない
        if (!isTouchable) {
            return;
        }

        //ドラッグが終わったら縮小
        transform.localScale = normalScale;

        //元のOrder In Layerに戻す
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

        /*
         * 
        問題ごとの特有な正誤判断を行うクラスを追加して、EventTriggerに登録してください！
         *
         */
    }

    public void PointerDown() {

        //回答中以外は触らせない
        if (!isTouchable) {
            return;
        }

        //最前面に出す
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;

        //オブジェクトが触られている間は拡大
        transform.localScale = expandedScale;
    }

    public void PointerUp() {

        //元のOrder In Layerに戻す
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

        //オブジェクトが離されたら縮小
        transform.localScale = normalScale;
    }

    public void OnTriggerEnter2D(Collider2D collision) {

        //衝突した場所のタグとgameObjectのタグが等しければ、isCorrectをtrue
        if(collision.tag == gameObject.tag) {
            currentCollision = collision.gameObject;
            isCorrect = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {

        //衝突が終わったら、isCorrectをfalse
        isCorrect = false;

        currentCollision = null;
    }

}

