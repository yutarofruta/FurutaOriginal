using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour {

    public GameObject goal;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnDrag() {

        if(Input.touchCount == 1) {
            Touch touch = Input.GetTouch(0);
            Vector3 vec = touch.position;
            vec.z = 10f;
            vec = Camera.main.ScreenToWorldPoint(vec);
            transform.position = vec;
        }

        if(Input.GetMouseButton(0)) {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = mousePosition;
        }

        gameObject.GetComponent<SpringJoint2D>().enabled = false;

    }

    public void EndDrag() {
        gameObject.GetComponent<SpringJoint2D>().enabled = true;
    }
}
