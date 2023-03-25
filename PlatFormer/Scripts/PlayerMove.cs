using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Jump
         if(Input.GetButtonDown("Jump")){
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

         }
        //Stop Speed
        if(Input.GetButtonUp("Horizontal")){
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.3f, rigid.velocity.y);
        }

        //Direction Sprite
        if(Input.GetButtonDown("Horizontal")){
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        
        //Animation
        if(Mathf.Abs(rigid.velocity.x) < 0.5){
            anim.SetBool("isWalking", false);
        }
        else{
            anim.SetBool("isWalking", true);
        }

        
    }

    void FixedUpdate(){

        //Move by key control
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //Max Speed
        if(rigid.velocity.x > maxSpeed){ //Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if(rigid.velocity.x < maxSpeed * (-1)){ //Left Max Speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
        
    }
    // Start is called before the first frame update

}
