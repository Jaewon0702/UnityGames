using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fracture : MonoBehaviour
{   public GameObject pieces; 
    public GameObject smartphone;
    public GameObject mainCamera;   
    public bool broken;

    void Awake(){
        broken = false; // Is Camera Broken?
    }
    // Start is called before the first frame update

    public void OnFracture(){
        smartphone.GetComponent<Renderer>().enabled = false; //enabled and SetActive has Same Effct
        pieces.SetActive(true);
        transform.position =  GameObject.FindGameObjectWithTag("Player").transform.position; //Same Position with Player
        transform.localRotation = GameObject.FindGameObjectWithTag("Player").transform.localRotation;//Same Rotation with Player 
        broken = true;
        //5초 정도 있다가 다시 시작하는 로직을 추가하자.
    }
}

