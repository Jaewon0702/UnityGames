using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class KidsMove : MonoBehaviour
{

    Rigidbody rigid;
    public int nextMoveX; // x direction
    public int nextMoveZ; // z direction
    public int nextSpeed;
    Vector3 moveVec;
    RaycastHit rayHit;
    Animator anim;
    KidThrow kidThrow;
    void Awake()
    {
       rigid = GetComponent<Rigidbody>();
       anim = GetComponent<Animator>();
       kidThrow = GetComponent<KidThrow>();
       Invoke("Think", 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {   // 1. Move Position
        moveVec = new Vector3(nextMoveX, 0, nextMoveZ) * nextSpeed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVec);

        if(moveVec.sqrMagnitude == 0) 
            return; // No input, no ratation

        // 2. Move Rotation
        Quaternion dirQuat = Quaternion.LookRotation(moveVec);
        Quaternion moveQuat =  Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
        rigid.MoveRotation(moveQuat); 

        // 3. Check the Kid on the Floor
       Vector3 frontVec = transform.position + new Vector3(nextMoveX, 2, nextMoveZ) * 10; //Ray in front of the player positon
       Debug.DrawRay(frontVec, -transform.up * 15, Color.red);
       bool CollisioDetectionF = Physics.Raycast(frontVec, -transform.up * 3, out rayHit, LayerMask.GetMask("Floor")); // Detect Kid on Floor

       //4. Change direction if kid meets cliff
       if(!CollisioDetectionF){
           ChangeDirection();
       }

     //5. Check the kid in front of Obstacle
     /*  Debug.DrawRay(frontVec, transform.forward * 5, Color.red);
       bool CollisioDetectionW = Physics.Raycast(frontVec, transform.forward, out rayHit, LayerMask.GetMask("Enemy")); // Detect Kid in front of wall

       //6. Change direction if kid meets wall
        if(CollisioDetectionW){
            Think();
       }*/
    }    

    void Think()
    { // 이런 공통적으로 쓰이는 함수를 Parent에 올리자.

        //1. Set Next Active
        nextMoveX = Random.Range(-4, 5);
        nextMoveZ = Random.Range(-4, 5);
        nextSpeed = Random.Range(0, 30);
        
        //2. Sprite Animation
        // Idle
        if(nextMoveX == 0 && nextMoveZ == 0) anim.SetInteger("walkSpeed", nextSpeed);
        //Throw
        else if(nextSpeed < 10){
            anim.SetInteger("walkSpeed", nextSpeed);
            anim.SetBool("isThrowing", true);
            anim.SetBool("isRunning", false);
            kidThrow.Throw();
            nextSpeed = 1; // Kid stops when throwing
        }
        // Walk
        else if(nextSpeed < 20){ 
            anim.SetInteger("walkSpeed", nextSpeed);
            anim.SetBool("isRunning", false);
            anim.SetBool("isThrowing", false);
            }
        // Run
        else{
            anim.SetInteger("walkSpeed", nextSpeed);
            anim.SetBool("isRunning", true);
            anim.SetBool("isThrowing", false);
            }

        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", 5);

    }

    void ChangeDirection(){
        nextMoveX *= -1; // Change Direction
        nextMoveZ *= -1;
        CancelInvoke(); // Stop Invoke
        Invoke("Think", 2);
    }
}


