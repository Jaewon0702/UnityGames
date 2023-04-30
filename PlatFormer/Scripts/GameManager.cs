using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject RestartBtn;

    void Update(){
       UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    public void NextStage()
    {
        //Change Stage
        if(stageIndex < Stages.Length - 1){
        Stages[stageIndex].SetActive(false); 
        stageIndex++;
        Stages[stageIndex].SetActive(true);
        PlayerReposition();

        UIStage.text = "STAGE " + (stageIndex + 1).ToString();
        }

        else{ //Game Clear
        // Player Control Lock
        Time.timeScale = 0;
        //Result UI
        Debug.Log("게임 클리어!");
        //Restart Button UI
        Text btnText = RestartBtn.GetComponentInChildren<Text>();
        btnText.text = "Clear!";
        RestartBtn.SetActive(true);

        }
        //Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;

    }

    public void HealthDown(){
        if(health > 1){
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else{
            //All health UI off
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            //Player Death Effect
            player.OnDie();
            health = 0;
            //Result UI
            Debug.Log("플레이어가 죽었습니다!");
            //Retry Button UI
            RestartBtn.SetActive(true);

        }
    }

    // When Falling down
     void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            

            //Player Reposition
            if(health > 1){
                PlayerReposition();
            }
            //Health down
            HealthDown();
        
        }
    }
    void PlayerReposition(){

    player.transform.position = new Vector3(-5.65f, -0.87f, -1);
    player.VelocityZero();
    }

    public void Restart(){
        SceneManager.LoadScene(0);
    }
}

