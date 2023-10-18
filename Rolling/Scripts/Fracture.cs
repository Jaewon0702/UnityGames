using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{   public GameObject pieces; 
    public GameObject smartphone;   
    // Start is called before the first frame update

    public void OnFracture(){
        smartphone.GetComponent<Renderer>().enabled = false; //enabled and SetActive has Same Effct
        pieces.SetActive(true);
        transform.position =  GameObject.FindGameObjectWithTag("Player").transform.position; //Same Position with Player
        transform.localRotation = GameObject.FindGameObjectWithTag("Player").transform.localRotation;//Same Rotation with Player 
    }
}

