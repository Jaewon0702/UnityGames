using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
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
    int KidThrowSpeed = 7;
    public Transform target;
    NavMeshAgent nav;

    //public Vector3 dirNormalized;
    //public Vector3 finalDir;
    void Awake()
    {
       rigid = GetComponent<Rigidbody>();
       anim = GetComponent<Animator>();
       kidThrow = GetComponent<KidThrow>();
       nav = GetComponent<NavMeshAgent>();
       Invoke("Think", waitTime);
       //StartCoroutine(ChageMovement());
       //dirNormalized = transform.localPosition.normalized;
    }
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
       Vector3 frontVec = transform.position + new Vector3(nextMoveX, 1, nextMoveZ) * 10; //Ray in front of the player positon
       //Debug.DrawRay(frontVec, -transform.up * 15, Color.red);
       bool CollisioDetectionF = Physics.Raycast(frontVec, -transform.up * 3, out rayHit, LayerMask.GetMask("Floor")); // Detect Kid on Floor

       //4. Change direction if kid meets cliff
        if(!CollisioDetectionF)
            ChangeDirection();
        
        //5. Change direction if kid meets obstacle
        /*
        finalDir = transform.TransformPoint(dirNormalized * 4);
        if(Physics.Linecast(transform.position, finalDir, out rayHit, LayerMask.GetMask("Obstacle")))
            ChangeDirection();
       

     //5. Check the kid in front of Obstacle
     /*  Debug.DrawRay(frontVec, transform.forward * 5, Color.red);
       bool CollisioDetectionW = Physics.Raycast(frontVec, transform.forward, out rayHit, LayerMask.GetMask("Enemy")); // Detect Kid in front of wall

       //6. Change direction if kid meets wall
        if(CollisioDetectionW){
            Think();
       }*/

    }    

  // void Update(){
        //nav.SetDestination(target.position);

        /*if(Input.GetMouseButtonDown(0)){
            kidThrow.Throw(); 
            KidsMoveAnim(false, false, true);
        }*/
  //  }


    void Think()
    { 
        //1. Set Next Active
        nextMoveX = Random.Range(-4, 5);
        nextMoveZ = Random.Range(-4, 5);
        SetNextMove(Random.Range(startSpeed, 30), 5, 0);
        //2. Sprite Animation
    if(isWalk == true){
        //anim.SetTrigger("isTurnOn");
        // Idle
        if(nextMoveX == 0 && nextMoveZ == 0) 
            KidsMoveAnim(false, false, false);
        //Throw
        else if(nextSpeed < KidThrowSpeed){ // The Kids throws the ball with a 7/30 chance
            KidsMoveAnim(false, false, true);
            kidThrow.Throw();
            SetNextMove(0, 1, KidThrowSpeed);
            // why 0?: Kid stops when throwing
            // why 1?: Kid moves in short time after throwing
            // why kidThrowSpeed?: Kid can't throw more than twice
        }
        // Walk
        else if(nextSpeed < 20) 
            KidsMoveAnim(true, false, false);
            
        // Run
        else
            KidsMoveAnim(false, true, false);
            
    }

    else{ 
        if(nextSpeed > 10) {
            // Kid on the Vehicle throws the ball with a 2/3 chance 
            // And only in move quickly
            kidThrow.Throw(); 
            waitTime = 3; // Kid move in short time after throwing
        
        }
    }
        //Recursive
        Invoke("Think", waitTime);
        //StartCoroutine(ChageMovement());
    }
    IEnumerator ChageMovement()
    {   //1. Kids Move randomly
        yield return new WaitForSeconds(waitTime);
        Think();
    }

    void KidsMoveAnim(bool isWalking, bool isRunning, bool isThrowing){
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isRunning", isRunning);
        if(isThrowing == true) anim.SetTrigger("isThrow"); 
    }

    void SetNextMove(int Speed, int time, int startS){
        nextSpeed = Speed;
        waitTime = time;
        startSpeed = startS;
    }

    void ChangeDirection(){
        nextMoveX = Random.Range(-4, 5); // Change Direction
        nextMoveZ = Random.Range(-4, 5);

        if(isWalk == true)
            anim.SetTrigger("isTurnOn");
        CancelInvoke(); // Stop Invoke
        Invoke(nameof(Think), waitTime);
       // StopCoroutine(ChageMovement());
        //StartCoroutine(ChageMovement());
        
    }
}
























