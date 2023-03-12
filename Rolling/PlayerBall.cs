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

    new AudioSource audio;


    private void Awake() {
        isJump = false;
        isHiddenJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update() {
        if(Input.GetButtonDown("Jump") && !isJump){
            isJump = true;
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }

        else if(Input.GetButtonDown("Jump") && !isHiddenJump){
            isHiddenJump = true;
            rigid.AddForce(Vector3.up * JumpPower * 2.1f, ForceMode.Impulse);
        }
        
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h,0,v) * MovePower, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision) { //Floor에 한 번 닿아야 점프 가능!
        
        if(collision.gameObject.tag == "Floor"){
            isJump = false; //바닥에 닿으면 점프 중이 아니다
            }

        else if(collision.gameObject.tag == "HiddenFloor"){
            isHiddenJump = false;
        }

    }

    void OnTriggerEnter(Collider other){

        if(other.tag == "Item"){
            Score++;
            audio.Play();
            other.gameObject.SetActive(false); //SetActive(bool): 오브젝트 활성화 함수
            manager.GetItemCount(Score);
        }

        else if(other.tag == "Finish"){

            if(Score == manager.totalItemCount){
                if(manager.stage == 2){
                    SceneManager.LoadScene(0);
                }
                else{
                    SceneManager.LoadScene(manager.stage + 1); //Game Clear! && Nest Stage
                }

            }
            
            else {
                SceneManager.LoadScene(manager.stage);
                //Restart
            }
        }   
    }
}
