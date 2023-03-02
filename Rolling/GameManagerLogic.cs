using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerLogic : MonoBehaviour{
    public int totalItemCount;
    public int stage; // 매 Scene마다 숫자를 바꿔야함!!

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            SceneManager.LoadScene(stage); //각 장면의 index 사용 가능!
        }
    }
}
