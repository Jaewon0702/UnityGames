using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset;
    public Fracture fracture;

    void Awake(){
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position;
    }
    void LateUpdate()
    {
        if(fracture.broken)
        // When the phone breaks, the camera is reflected in the piece
        transform.position = GameObject.FindGameObjectWithTag("Piece").transform.position + Offset; 
        else
        transform.position = playerTransform.position + Offset;
    }
}
