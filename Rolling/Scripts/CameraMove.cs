using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset;
    public Fracture fracture;
    
    //1. Camera Movement variables
    public Transform objectToFollow;
    public float followSpeed;
    public float sensivity;
    public float clampAngle;

    //2. Mouse Input value
    private float rotX;
    private float rotY;

    //3. Direction & Distance
    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness;

    RaycastHit rayHit;
    void Awake(){
        //Shoot Player
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position;
    }

    void Start(){
        rotX = transform.localRotation.eulerAngles.x;
        rotX = transform.localRotation.eulerAngles.y;
        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude; // size

        Cursor.visible = false;
    }

    /*void Update() {
        
        
    }*/
    void LateUpdate() {
        //1. Get Mouse Button
        //Input.GetMouseButtonDown(0)
        float x = Input.GetAxis("Mouse Y");
        float y = Input.GetAxis("Mouse X");
        // For Use in Moblie
        if(Input.touchCount > 0){
            x = Input.touches[0].deltaPosition.y;
            y = Input.touches[0].deltaPosition.x;
        }
        rotX += x * sensivity * Time.deltaTime * -1f;
        rotY += y * sensivity * Time.deltaTime;
        
        //2. Limit the angle to prevent the camera from turning.
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        //3. Rotate Camera
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;

        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed * Time.deltaTime);
        // Zoom in if there is an object between the camera and the player
        /*finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        if(Physics.Linecast(transform.position, finalDir, out rayHit))
            finalDistance = Mathf.Clamp(rayHit.distance, minDistance, maxDistance);
        else
            finalDistance = maxDistance;*/

        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * maxDistance, Time.deltaTime * smoothness);
        Reflect();


    }
    void Reflect(){
        if(fracture.broken) // When the phone breaks, the camera is reflected in the piece
        transform.position = GameObject.FindGameObjectWithTag("Piece").transform.position + Offset; 
        //else transform.position = playerTransform.position + Offset;

    }
}



