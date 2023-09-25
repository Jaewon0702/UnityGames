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
    public GameObject CrashedScreen00;
    public float befpos = -34;

    public float strength = 100;
    

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

        else if(Input.GetButtonDown("Submit")){
            CrashedScreen00.SetActive(true);
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
            OnDamaged(collision.transform.position);
            }

        else if(collision.gameObject.tag == "HiddenFloor"){
            isHiddenJump = false;

        }
        else if(collision.gameObject.tag == "Enemy"){
            OnDamaged(collision.transform.position);
            CrashedScreen00.SetActive(true);

        }

       

    }

      void OnDamaged(Vector3 targetPos){
        float heig = befpos - targetPos.y;
        befpos = transform.position.y; // 높은 곳에서 폰이 떨어지면 데미지를 입는다.
        Debug.Log(heig);

        if(heig > strength){
            CrashedScreen00.SetActive(true);
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
