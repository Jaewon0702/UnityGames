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
    RaycastHit rayHit;
    Animator anim;
    KidThrow kidThrow;
    NavMeshAgent nav;

    public int nextMoveX; // x direction
    public int nextMoveZ; // z direction
    public int nextSpeed;
    public int waitTime;
    public int startSpeed;
    public bool isWalk;
    public int probThrow1;
    int probThrow2 = 4;
    public bool isChase;
    public float detectDistance;

    Vector3 moveVec;
    public Transform target;

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
    {   
        //ChaseStart();
        /*if(isChase)
          MoveTowardsPlayer();
        
        else*/
            MoveRandomly();
        
        
        
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

    void ChaseStart(){
        if(Vector3.Distance(target.position, transform.position) < detectDistance)
            isChase = true;
        else 
            isChase = false;
    }
    void MoveTowardsPlayer()
    {
        // Ensure that a target (player) is assigned
        if (target == null)
        {
            Debug.LogError("Target not assigned to KidsMove.");
            return;
        }

        // Use NavMeshAgent to move towards the player
        //nav.SetDestination(target.position);

        // Optional: Update animations based on NavMeshAgent's velocity
        //nav.speed = nextSpeed;
        nextSpeed = (int)nav.velocity.magnitude; // To change Animation
        //Debug.Log("moveSpeed: " + nextSpeed);
        //KidsMoveAnim(moveSpeed > 0.1f, moveSpeed > 2f, false);
    }

    void MoveRandomly()
    {
        // 1. Move Position
        moveVec = new Vector3(nextMoveX, 0, nextMoveZ) * nextSpeed * Time.fixedDeltaTime;
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
       //bool CollisioDetectionO = Physics.Raycast(frontVec, -transform.up * 3, out rayHit, LayerMask.GetMask("Obstacle"));
       //4. Change direction if kid meets cliff
        if(!CollisioDetectionF)
            ChangeDirection();
    }

    void Think()
    { 
        //1. Set Next Active
        nextMoveX = Random.Range(-4, 5);
        nextMoveZ = Random.Range(-4, 5);
        SetNextMove(Random.Range(2, 30), 5, 1,Random.Range(1, 11));
        
        //2. Sprite Animation
    if(isWalk == true){
        //anim.SetTrigger("isTurnOn");
        // Idle
        if(nextMoveX == 0 && nextMoveZ == 0 && nextSpeed == 0) 
            KidsMoveAnim(false, false, false);
            
        // Walk
        else if(nextSpeed < 20) 
            KidsMoveAnim(true, false, false);
            
        // Run
        else
            KidsMoveAnim(false, true, false);
    //Throw
    if(probThrow1 < probThrow2){ // The Kids throws the ball with a 40% chance in 5s.
        KidsMoveAnim(false, false, true);
        kidThrow.Throw();
        SetNextMove(0, 1, 1, probThrow2 + 1);
        // why 0?: Kid stops when throwing
        // why 1?: Kid moves in short time after throwing
        // why probThrow2 + 1?: Kid can't throw more than twice in 6s.
        }
            
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
   /* IEnumerator ChageMovement()
    {   //1. Kids Move randomly
        yield return new WaitForSeconds(waitTime);
        Think();
    }*/

    void KidsMoveAnim(bool isWalking, bool isRunning, bool isThrowing){
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isRunning", isRunning);
        if(isThrowing == true) anim.SetTrigger("isThrow"); 
    }

    void SetNextMove(int Speed, int time, int startS, int probT){
        nextSpeed = Speed;
        waitTime = time;
        startSpeed = startS;
        probThrow1 = probT;
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
