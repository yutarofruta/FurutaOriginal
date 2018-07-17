using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    Animator anim;
    AnimatorStateInfo animInfo;

    private float stopPos = 5f;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(transform.position.x);
        if(gameObject.transform.position.x > stopPos) {
            Debug.Log("ここまで処理は届いている");
            transform.Translate(Vector3.left * Time.deltaTime);
        } else {
            anim.SetTrigger("IdleTrigger");
        }
    }

    public void SetJumpTrigger() {
        anim.SetTrigger("JumpTrigger");
    }

}
