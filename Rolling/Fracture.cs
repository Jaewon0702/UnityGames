using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    Collider[] colliders;
    Rigidbody rigid;
    // Start is called before the first frame update
    void Awake()
    {
        colliders = GetComponentsInChildren<Collider>();
        foreach(Collider c in colliders){
            if(c.name == "1114_Fracture") continue; //Block Parent turn off
            c.gameObject.GetComponent<Renderer>().enabled = false;
            rigid = c.gameObject.GetComponent<Rigidbody>();
            rigid.constraints = (RigidbodyConstraints)126; // 126 == All constraints


        }
        
    }

    public void OnFracture(){
        GetComponent<Renderer>().enabled = false;
        foreach(Collider c in colliders){
            if(c.name == "1114_Fracture") continue;
            c.gameObject.GetComponent<Renderer>().enabled = true;
            rigid = c.gameObject.GetComponent<Rigidbody>();
            rigid.constraints = (RigidbodyConstraints)0;
        }

    }


    
}
