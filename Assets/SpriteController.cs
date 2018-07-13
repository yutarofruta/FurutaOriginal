using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour {

    public GameObject goal;
    public Text text;

    private bool isTouchable = true;

    Animator anim;
    
	// Use this for initialization
	void Start () {
        text.GetComponent<Text>().text = "2 - 1 = ?";
        this.anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void OnDrag() {

        if (!isTouchable) {
            return;
        }

        float distance = Vector3.Distance(transform.position, goal.transform.position);
        Debug.Log(distance);

        if (distance < 0.7f && gameObject.tag == "answer") {

            transform.position = goal.transform.position;
            text.GetComponent<Text>().text = "Correct!";
            this.anim.SetTrigger("JumpTrigger");
        }
        else {
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

            gameObject.GetComponent<SpringJoint2D>().enabled = false;
        

    }

    public void EndDrag() {
        if(transform.position != goal.transform.position) {
            gameObject.GetComponent<SpringJoint2D>().enabled = true;
        }
    }

   
}
