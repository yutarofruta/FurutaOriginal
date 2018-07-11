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
        if (Input.touchCount > 0) {

            Touch touch = Input.GetTouch(0);
            Vector3 vec = touch.position;
            vec.z = 10f;
            vec = Camera.main.ScreenToWorldPoint(vec);
            transform.position = vec;

            float distance = Vector3.Distance(vec, goal.transform.position);
            if (distance <= 1f) {
                transform.position = goal.transform.position;

            }


        }

        if (Input.GetMouseButton(0)) {

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = mousePosition;

            float distance = Vector3.Distance(mousePosition, goal.transform.position);
            if (distance <= 1f) {
                transform.position = goal.transform.position;

            }


        }
    }

}
