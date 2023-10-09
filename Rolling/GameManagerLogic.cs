using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour{
    public int totalItemCount;
    public int stage; // 매 Scene마다 숫자를 바꿔야함!!
    public Text playerCountText;
    //public PlayerBall player;
    public Fracture fracture;
    public float health;
    public GameObject CrashedScreen00;
    public GameObject CrashedScreen01;
    public GameObject CrashedScreen02; 
    public GameObject Screen;
    void Awake(){
        health = 5;
        playerCountText.text = "X 5"; //Initial setup Health
    }

    public void GetItemCount(float count){

        playerCountText.text = "X " + (count).ToString();


    }

     public void HealthUpDown(float damage){ //Heath가 떨어짐에 따라 폰에 점점 금이 간다.
     if(damage > 0){
        if(health > 3 + 1 && damage == 1) health = health - damage;
        
        else if(health == 3 + 1){
            health = health - damage;
            CrashedScreen00.SetActive(true);       
        }

        else if(health == 2 + 1){
            health = health - damage;
            CrashedScreen01.SetActive(true);
        }

        else if(health == 1 + 1){
            health = health - damage;
           CrashedScreen02.SetActive(true);
        }
        else if(health <= 0) health = 0;
        
        else{
            health = health - damage;
            //All health UI off
            //UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            //Player Death Effect
           // player.OnDie();
            //health = 0;
            Screen.SetActive(false);
            Debug.Log("Helath: " + health);
            //Result UI
            Debug.Log("플레이어가 죽었습니다!");
            fracture.OnFracture();
            //Time.timeScale = 0;
            //Retry Button UI
            //RestartBtn.SetActive(true);

        }
    }
    else{
        if(health > 3 + 1 && damage == -1) health = health - damage;
    
        else if(health == 3 + 1) health = health - damage;

        else if(health == 2 + 1){
            health = health - damage;
            CrashedScreen00.SetActive(false);
            }

        else if(health == 1 + 1){
            health = health - damage;
            CrashedScreen01.SetActive(false);
            }

        else if(health <= 0) health = 0;
        
        else{
            CrashedScreen02.SetActive(false);
            health = health - damage;
            }
        }
    }


    private void OnTriggerEnter(Collider other) 
    { //떨어지면 Restart
        if(other.gameObject.tag == "Player"){
            SceneManager.LoadScene(stage); //각 장면의 index 사용 가능!
        }
    }

}




