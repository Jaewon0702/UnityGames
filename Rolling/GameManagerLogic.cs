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

     public PlayerBall player;
    public float health;
    public GameObject CrashedScreen00;
    public GameObject CrashedScreen01;
    public GameObject CrashedScreen02;

    void Awake(){
        health = 200;

        stageCountText.text = "/ " + totalItemCount.ToString();

    }

    public void GetItemCount(int count){

        playerCountText.text = count.ToString();


    }

     public void HealthDown(float damage){ //Heath가 떨어짐에 따라 폰에 점점 금이 간다.

        if(health > 150 && damage == 50){
            health = health - damage;
        }

        else if(health <= 150 && health > 100){
            health = health - damage;
            CrashedScreen00.SetActive(true);
        }

        else if(health <= 100 && health > 50){
            health = health - damage;
            CrashedScreen00.SetActive(false);
            CrashedScreen01.SetActive(true);
        }

        else if(health <= 50 && health > 1){
            health = health - damage;
            CrashedScreen01.SetActive(false);
            CrashedScreen02.SetActive(true);
        }

        else{
            //All health UI off
            //UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            //Player Death Effect
           // player.OnDie();
            //health = 0;
            Debug.Log(health);
            //Result UI
            CrashedScreen01.SetActive(false);
            CrashedScreen02.SetActive(true);
            Debug.Log("플레이어가 죽었습니다!");
            //Retry Button UI
            //RestartBtn.SetActive(true);

        }
    }


    private void OnTriggerEnter(Collider other) 
    { //떨어지면 Restart
        if(other.gameObject.tag == "Player"){
            SceneManager.LoadScene(stage); //각 장면의 index 사용 가능!
        }
    }

}

