
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
    public int waitTime;
    public int startSpeed;
    public bool isWalk;
    void Awake()
    {
       rigid = GetComponent<Rigidbody>();
       anim = GetComponent<Animator>();
       kidThrow = GetComponent<KidThrow>();
       Invoke("Think", waitTime);
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
    { 
        //1. Set Next Active
        nextMoveX = Random.Range(-4, 5);
        nextMoveZ = Random.Range(-4, 5);
        nextSpeed = Random.Range(startSpeed, 30);
        waitTime = 5;
        startSpeed = 0;
        //2. Sprite Animation
    if(isWalk == true){
        // Idle
        if(nextMoveX == 0 && nextMoveZ == 0) anim.SetInteger("walkSpeed", nextSpeed);
        //Throw
        else if(nextSpeed < 10){ // The Kids throws the ball with a 1/3 chance
            anim.SetInteger("walkSpeed", nextSpeed);
            anim.SetBool("isThrowing", true);
            anim.SetBool("isRunning", false);
            kidThrow.Throw();
            nextSpeed = 0; // Kid stops when throwing
            waitTime = 2; // Kid move in short time after throwing
            startSpeed = 11; // Kid can't throw more than twice
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
    }

    else{ 
        if(nextSpeed > 10) {
            // Kid on the Vehicle throws the ball with a 2/3 chance 
            // And only in move quickly
            kidThrow.Throw(); 
            waitTime = 2; // Kid move in short time after throwing
        
        }
    }

        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", waitTime);
    }

    void ChangeDirection(){
        nextMoveX = Random.Range(-4, 5); // Change Direction
        nextMoveZ = Random.Range(-4, 5);
        CancelInvoke(); // Stop Invoke
        Invoke("Think", waitTime);
    }
}




