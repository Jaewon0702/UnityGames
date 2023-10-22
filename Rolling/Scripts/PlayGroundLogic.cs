using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGroundLogic : MonoBehaviour
{
    Transform swingsRotate;

    void Start(){
        swingsRotate = transform.GetChild(1);
    }
    // Update is called once per frame
    void Update()
    {
        swingsRotate.Rotate(0, 5, 0); // Spin Swings 
    }
}
