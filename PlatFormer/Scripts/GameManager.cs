using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    // Start is called before the first frame update
    public void NextStage()
    {
        stageIndex++;
        totalPoint += stagePoint;
        stagePoint = 0;

    }

    // Update is called once per frame
     void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            //Health down
            health--;

            //Player Reposition
            collision.attachedRigidbody.velocity = Vector2.zero; //Velocity Reset
            collision.transform.position = new Vector3(-6, 0.21f, 0);

        }
        }
}
