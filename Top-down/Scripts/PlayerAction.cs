using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;

    float h;
    float v;
    bool isHorizonMove;

    Rigidbody2D rigid;
    Animator anim;

   void Awake(){
    rigid = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    
   }

    // Update is called once per frame
    void Update()
    {
        //Move Value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //Check Button Down & Up
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        //Check Horizontal Move
        if(hDown){
            isHorizonMove = true;
        }
        else if(vDown){
            isHorizonMove = false;
        }
        else if(hUp || vUp){
            isHorizonMove = h != 0;

        }

        //Animation
        if(anim.GetInteger("hAxisRaw") != h){
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if(anim.GetInteger("vAxisRaw") != v){
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else{
            anim.SetBool("isChange", false);
        }
        
        

    }

    void FixedUpdate(){
        //Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

    }
}
