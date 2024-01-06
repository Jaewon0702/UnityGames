using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using UnityEngine.SceneManagement;
using System;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;
    public float JumpPower;
    public float MovePower;
    public bool isJump;
    bool isHiddenJump;
    public GameManagerLogic manager;
    public GameObject finishPoint;
    public float befpos;
    public float strength;
    Vector3 moveVec;

    public VariableJoystick joy;

    new AudioSource audio;
    public AudioClip[] audioClips;
    Renderer Renderer;
    Camera _camera;
    public GameObject[] Screens;
    public bool opaque;

    public float smoothness = 10f;
    private void Awake() {
        isJump = false;
        isHiddenJump = false;
        opaque = false;
        befpos = -34;
        strength = 50;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        Renderer = GetComponent<Renderer>();
    }

    void Start(){
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        //1. Jumps when press space bar
        if(Input.GetButtonDown("Jump") && !isJump){
            Jump();
        }
        // Make smartphone visible
        if(opaque == true){
            StopCoroutine(Translucent());
            Invisible(true);
            opaque = false;
        }
       /* //2. Jumps if touchs other side of the screen, not joystick
        if(Input.GetMouseButtonDown(0) && !isJump && (joy.Horizontal+joy.Vertical) == 0){
            //if joy.Horizontal+joy.Vertical equal 0, not touching joy range 
            Jump(JumpPower);
        }

        /*else if(Input.GetButtonDown("Jump") && !isHiddenJump){
            isHiddenJump = true;
            rigid.AddForce(Vector3.up * JumpPower * 2.1f, ForceMode.Impulse);
        }*/
     

        
    }
    void FixedUpdate()
    {
        // Get the camera's forward and right vectors
        Vector3 forward = _camera.transform.forward;
        Vector3 right = _camera.transform.right;

        // Project the vectors onto the horizontal plane (remove the vertical component)
        forward.y = 0f;
        right.y = 0f;

        // Normalize the vectors to ensure consistent movement speed in all directions
        forward.Normalize();
        right.Normalize();

        //1. Input Value
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float x = joy.Horizontal;
        float z = joy.Vertical;
       // Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(0, 1, 0));

        //2. Move using keyboard and Input value chages in the direction the camera rotates
        //moveVec = new Vector3(h, 0, v) * MovePower;
        moveVec = h * right + v * forward;
        rigid.AddForce(moveVec * MovePower, ForceMode.Impulse);
       // rigid.MovePosition(rigid.position + moveVec);

       //3. move using joystick on touch screen or mouse
       Vector3 direction = x * right + z * forward;
       rigid.AddForce(direction * MovePower, ForceMode.Impulse); //ForceMode.VelocityChange

       /*4. Move Rotation
       if(moveVec.sqrMagnitude == 0) 
            return; // No input, no ratation
        Quaternion dirQuat = Quaternion.LookRotation(direction);
        Quaternion moveQuat =  Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
        rigid.MoveRotation(moveQuat);*/

       //4. Keep Move forward
      // rigid.AddForce(new Vector3(0,0,1) * 0.4f, ForceMode.VelocityChange);
    }

    /*Rotate Person following camera
    void LateUpdate(){
       Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(0, -1, 0));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        //Debug.Log(_camera.transform.forward);
       // Debug.Log(playerRotate);
    }*/

    public void Jump(){
        if(!isJump){
        isJump = true;
        rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        PlaySound("JUMP");      
        }
    }

    void OnCollisionEnter(Collision collision) { //Floor에 한 번 닿아야 점프 가능!
        
        if(collision.gameObject.tag == "Floor"){
            isJump = false; //바닥에 닿으면 점프 중이 아니다
            OnDamaged(collision.transform.position, false,"General");
            }

        else if(collision.gameObject.tag == "HiddenFloor"){
            isHiddenJump = false;

        }

        else if(collision.gameObject.tag == "Obstacle"){ // Can Jump on Obstacle
            isJump = false;
           

        }

        else if(collision.gameObject.tag == "Enemy"){
            OnDamaged(collision.transform.position, true, "General");
            


        }
        else if(collision.gameObject.tag == "SmallBomb"){
            OnDamaged(collision.transform.position, true, "SmallBomb");
            
        }
        else if(collision.gameObject.tag == "BigBomb"){
            OnDamaged(collision.transform.position, true, "BigBomb");
        }
    }

    // Falled from high or Bumped into enemy, Get damage
      void OnDamaged(Vector3 targetPos, bool bumped, string type){
        float heig = befpos - targetPos.y;
        befpos = transform.position.y; // 높은 곳에서 폰이 떨어지면 데미지를 입는다.
        opaque = true;
        if(heig > strength || bumped == true){ 
            // Damaged 
            manager.HealthUpDown(1);
            manager.GetItemCount(manager.health, manager.ItemCount);
            

            //Change Layer(Immotal Active)
            gameObject.layer = 7;

            //View Alpha
            //Renderer.material.color = new Color(0, 0, 0, 1f); // not work

            //Reaction Force
            if(type == "General") ReactionForce(targetPos, 100);
            else if(type == "SmallBomb") ReactionForce(targetPos, 150);
            else if(type == "BigBomb") ReactionForce(targetPos, 250);

            PlaySound("DAMAGED");
            // no damage
            Invoke("OffDamaged",3);

        }
    }

    void ReactionForce(Vector3 targetPos, int ExplosionPower){
        int dirx = transform.position.x - targetPos.x > 0 ? 1 : -1;
        int dirz = transform.position.z - targetPos.z > 0 ? 1 : -1;
        rigid.AddForce(new Vector3(dirx, 2, dirz) * ExplosionPower, ForceMode.Impulse);
    }

    void OffDamaged(){
        gameObject.layer = 6;

    }

    void OnTriggerEnter(Collider other){

        //1. Get Item Logic
        if(other.tag == "Item"){ 
            manager.HealthUpDown(-1);
            manager.ItemUpDown();
            manager.GetItemCount(manager.health, manager.ItemCount);
            PlaySound("ITEM");
            other.gameObject.SetActive(false); //SetActive(bool): 오브젝트 활성화 함수

            if(manager.ItemCount == manager.totalItemCount){ 
                finishPoint.SetActive(true); //Active Finish Point
            }
        }

        //2. Finish Logic
        else if(other.tag == "Finish"){ 

            if(manager.ItemCount == 0){ //When get All Item
                if(manager.stage == 2){
                    SceneManager.LoadScene(manager.stage);
                }
            //3. Game Clear! && Nest Stage
                else{
                    SceneManager.LoadScene(manager.stage + 1); 
                }
            } 
            //4. Restart
            else {
                SceneManager.LoadScene(manager.stage); 
            }
        }   
    }

    void Cloak(){
        StartCoroutine(Translucent());
        /*if(opaque == true){
            StopCoroutine(Translucent());
            Invisible(true);
            opaque = false;
            }*/
    }
    
    
    IEnumerator Translucent(){
        //1. Smartphone Translucent
        Invisible(false);

       yield return null;
        //2. if player coliide(get item, damaged, etc..)
        if(opaque == true){
            Invisible(true); // 왜 이 부분이 작동 안 하는지 모르갰다. 내일 챗 지피티한테 물어보자.
            opaque = false;
            yield break;
           }
        else{
            yield return new WaitForSeconds(20.0f);
            Invisible(true);
        }
    }
        
        
    public void Invisible(bool invisible){
        //1. Smartphone Translucent
        Renderer.enabled = invisible; 
        Debug.Log("렌더러 작동 중");
        //2. Screen Translucent
        foreach(GameObject screen in Screens){
            //if(screen.activeSelf == true)
            screen.GetComponent<Renderer>().enabled = invisible;
                }
    }


    void PlaySound(string action){ // Sound Set Function
        switch(action){
            case "ITEM":
                audio.clip = audioClips[0];
                break;
            case "DAMAGED":
                audio.clip = audioClips[1];
                break;
            case "FINISH":
                audio.clip = audioClips[2];
                break;
            case "JUMP":
                audio.clip = audioClips[3];
                break; 
        }
        audio.Play();
    }
}
