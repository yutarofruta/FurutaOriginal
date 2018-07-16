using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    Animator anim;
    AnimatorStateInfo animInfo;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        
	}

    public void SetJumpTrigger() {
        anim.SetTrigger("JumpTrigger");
    }

}
