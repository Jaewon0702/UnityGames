using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    Rigidbody rigid;
    public float JumpPower;
    public float MovePower;
    bool isJump;
    bool isHiddenJump;
    public int Score;
    public GameManagerLogic manager;
    public float befpos;
    public float strength;
    Vector3 moveVec;

    public VariableJoystick joy;

    new AudioSource audio;
    public AudioClip audioItem;
    public AudioClip audioDamaged;
    public AudioClip audioFinish;


    private void Awake() {
        isJump = false;
        isHiddenJump = false;
        befpos = -34;
        strength = 50;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update() {
        //1. Jump when I press space bar
        if(Input.GetButtonDown("Jump") && !isJump){
            isJump = true;
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }

        /*else if(Input.GetButtonDown("Jump") && !isHiddenJump){
            isHiddenJump = true;
            rigid.AddForce(Vector3.up * JumpPower * 2.1f, ForceMode.Impulse);
        }
        //2. Jump when swipe up
        /*else if(joy.Horizontal > 0 && !isJump){
            isJump = true;
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }*/

        
    }
    void FixedUpdate()
    {
        //1. Input Value
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float x = joy.Horizontal;
        float z = joy.Vertical;
        //2. Move using keyboard
        moveVec = new Vector3(h, 0, v) * MovePower;
        rigid.AddForce(moveVec, ForceMode.Impulse);

       //3. move using joystick on touch screen or mouse
       Vector3 direction = new Vector3(x, 0, z);
       rigid.AddForce(direction * MovePower, ForceMode.Impulse); //ForceMode.VelocityChange

       //4. Keep Move forward
      // rigid.AddForce(new Vector3(0,0,1) * 0.4f, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision collision) { //Floor에 한 번 닿아야 점프 가능!
        
        if(collision.gameObject.tag == "Floor"){
            isJump = false; //바닥에 닿으면 점프 중이 아니다
            OnDamaged(collision.transform.position, false);
            }

        else if(collision.gameObject.tag == "HiddenFloor"){
            isHiddenJump = false;

        }

        else if(collision.gameObject.tag == "Obstacle"){ // Can Jump on Obstaccle
            isJump = false;
        }

        else if(collision.gameObject.tag == "Enemy"){
            OnDamaged(collision.transform.position, true);

        }
    }

    // Falled from high or Bumped into enemy, Get damage
      void OnDamaged(Vector3 targetPos, bool bumped){
        float heig = befpos - targetPos.y;
        befpos = transform.position.y; // 높은 곳에서 폰이 떨어지면 데미지를 입는다.
        //Debug.Log(heig);

        if(heig > strength || bumped == true){ // Damaged
            manager.HealthUpDown(1);
            manager.GetItemCount(manager.health);
            PlaySound("DAMAGED");

            //여기에서 GameManager의 Helthdown 함수가 실행
        }
    }

    void OnTriggerEnter(Collider other){

        if(other.tag == "Item"){ //1. Get Item Logic
            //manager.health++;
            manager.HealthUpDown(-1);
            manager.GetItemCount(manager.health);
            PlaySound("ITEM");
            other.gameObject.SetActive(false); //SetActive(bool): 오브젝트 활성화 함수
        }

        else if(other.tag == "Finish"){ //2. Finish Logic

            if(Score == manager.totalItemCount){
                if(manager.stage == 2){
                    SceneManager.LoadScene(0);
                }
                else{
                    SceneManager.LoadScene(manager.stage + 1); //3. Game Clear! && Nest Stage
                }

            }
            
            else {
                SceneManager.LoadScene(manager.stage); //4. Restart
            }
        }   
    }

    void PlaySound(string action){ // Sound Set Function
        switch(action){
            case "ITEM":
                audio.clip = audioItem;
                break;
            case "DAMAGED":
                audio.clip = audioDamaged;
                break;
            case "FINISH":
                audio.clip = audioFinish;
                break;
        }
        audio.Play();
    }
}
