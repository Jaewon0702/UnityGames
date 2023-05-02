using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;

    public float maxSpeed;
    public float jumpPower;
    
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    BoxCollider2D Boxcollider;
    AudioSource audioSource;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Boxcollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Jump
        if(Input.GetButtonDown("Jump") && !anim.GetBool("isJumping")){
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            PlaySound("JUMP");

        }
        //Stop Speed
        if(Input.GetButtonUp("Horizontal")){
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 1.0f, rigid.velocity.y);
        }

        //Direction Sprite
        if(Input.GetButton("Horizontal")){
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
    // Update is called once per frame
    void FixedUpdate()
    {
        //Move by Key Control
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //Max Speed
        if(rigid.velocity.x > maxSpeed){//Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if(rigid.velocity.x < maxSpeed * (-1)){ //Left Max Speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        //Landing Platform
        if(rigid.velocity.y < 0){
        Debug.DrawRay(rigid.position + new Vector2(-0.55f, 0), Vector3.down, new Color(0, 1, 0));
        Debug.DrawRay(rigid.position + new Vector2(0.55f, 0), Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHitL = Physics2D.Raycast(rigid.position + new Vector2(-0.55f, 0), Vector3.down, 1, LayerMask.GetMask("Platform"));
        RaycastHit2D rayHitR = Physics2D.Raycast(rigid.position + new Vector2(0.55f, 0), Vector3.down, 1, LayerMask.GetMask("Platform"));

        if(rayHitL.collider != null | rayHitR.collider != null ){
            if(rayHitL.distance < 1.0f | rayHitR.distance < 1.0f){
                anim.SetBool("isJumping", false);
            }
        }
        }

       
    }


    void OnCollisionEnter2D(Collision2D collision){
            if(collision.gameObject.tag == "Enemy"){
                // Step on(Attack) = falling & upper than monster
                if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y){
                    OnAttack(collision.transform);

                }
                //Demaged
                else{
                OnDamaged(collision.transform.position);
                }
            }
            else if(collision.gameObject.tag == "Spike"){
                OnDamaged(collision.transform.position);
            }
        }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Item"){
            //Point
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");
            if(isBronze){
                gameManager.stagePoint += 50;
            }
            else if(isSilver){
                gameManager.stagePoint += 100;

            }
            else if(isGold){
                gameManager.stagePoint += 300;

            }

            //Deactive Item
            collision.gameObject.SetActive(false);

            //Get Item Sound
            PlaySound("ITEM");
        }
        else if(collision.gameObject.tag == "Finish"){
            //Next stage
            gameManager.NextStage();

            //Finish Sound
            PlaySound("ITEM");

        }
        
    }

    void OnDamaged(Vector2 targetPos){
        //Health Down
        gameManager.HealthDown();

        //Change Layer(Immortal Active)
        gameObject.layer = 8;

        //View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Reaction Force
        int dicr = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dicr, 1) * 10, ForceMode2D.Impulse);

        //Animation
        anim.SetTrigger("doDamaged");

        //Damage Sound
        PlaySound("DAMAGE");

        Invoke("OffDamaged", 1);
    }

    void OffDamaged(){
        gameObject.layer = 7;
        spriteRenderer.color = new Color(1, 1, 1, 1);

    }

    void OnAttack(Transform enemy){
        //Point
        gameManager.stagePoint += 200;

        //Reaction Force
        rigid.AddForce(Vector2.up * 10,  ForceMode2D.Impulse);

        //Enemy Die
        EnemyMove enemymove = enemy.GetComponent<EnemyMove>();
        enemymove.EOnDamaged();

        //Attack Sound
        PlaySound("ATTACK");

    }

    public void OnDie(){
        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Sprite Flip Y
        spriteRenderer.flipY = true;

        //Collider Disable
        Boxcollider.enabled = false;

        //Death effect jump
        rigid.AddForce(Vector2.up * 4, ForceMode2D.Impulse);

        //Die Sound
        PlaySound("DIE");

    }

    public void VelocityZero(){
        rigid.velocity = Vector2.zero; 
    }

     void PlaySound(string action){
        switch(action){
            case "JUMP" :
                audioSource.clip = audioJump;
                break;
            case "ATTACK" :
                audioSource.clip =  audioAttack;
                break;
            case "DAMAGED" :
                audioSource.clip = audioDamaged;
                break;
            case "ITEM" :
                audioSource.clip = audioItem;
                break;
            case "DIE" :
                audioSource.clip = audioDie;
                break;
            case "FINISH" :
                audioSource.clip = audioFinish;
                break;
        }
        audioSource.Play();
    }

}
