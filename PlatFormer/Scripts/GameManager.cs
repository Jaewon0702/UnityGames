using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    // Start is called before the first frame update
    public void NextStage()
    {
        stageIndex++;
        totalPoint += stagePoint;
        stagePoint = 0;

    }

    public void HealthDown(){
        if(health > 1){
            health--;
        }
        else{
            //Player Death Effect
            player.OnDie();
            health = 0;
            //Result UI
            Debug.Log("플레이어가 죽었습니다!");
            //Retry Button UI

        }
    }

    // When Falling down
     void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            

            //Player Reposition
            if(health > 1){
                collision.attachedRigidbody.velocity = Vector2.zero; //Velocity Reset
                collision.transform.position = new Vector3(-6, 0.21f, 0);
                HealthDown();
            }
            else{
            //Health down
                health = 0;
                HealthDown();
            }

        }
        }
}
