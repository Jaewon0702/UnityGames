using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour{
    public int totalItemCount;
    public int stage; // 매 Scene마다 숫자를 바꿔야함!!
    public Text stageCountText;
    public Text playerCountText;

    void Awake(){

        stageCountText.text = "/ " + totalItemCount.ToString();

    }

    public void GetItemCount(int count){

        playerCountText.text = count.ToString();


    }


    private void OnTriggerEnter(Collider other) 
    { //떨어지면 Restart
        if(other.gameObject.tag == "Player"){
            SceneManager.LoadScene(stage); //각 장면의 index 사용 가능!
        }
    }
}
