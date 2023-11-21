using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour{
    public int ItemCount;
    public int totalItemCount;
    public int stage; // 매 Scene마다 숫자를 바꿔야함!!
    public Text HealthText;
    public Text ItemText;
    public Fracture fracture;
    public float health;
    public GameObject CrashedScreen00;
    public GameObject CrashedScreen01;
    public GameObject CrashedScreen02; 
    public GameObject[] Screens;
    public GameObject Screen;
    public GameObject item;
    void Awake(){
        //1. Initial setup Health
        health = 5;
        HealthText.text = "X " + health; 
        //2. Initial setup Item Count
        ItemCount = 0;
        totalItemCount = item.transform.childCount;
        ItemText.text = "0 / " + totalItemCount;

    }

    public void GetItemCount(float health, int count){
        HealthText.text = "X " + (health).ToString();
        ItemText.text = (count).ToString() + " / " + totalItemCount;
    }

    public void ItemUpDown(){
        ItemCount += 1;
    }

     public void HealthUpDown(float damage){
        //1. Change Health
        health = health - damage;

        //2. Turn the Emoji Screen on and off for a while.
        if(damage > 0){ 
            if(health > 2)StartCoroutine(ChangeScreen(4, true));
            else if(health == 2) StartCoroutine(ChangeScreen(5, true));
            else if(health == 1) StartCoroutine(ChangeScreen(8, true));
        }
        else 
            StartCoroutine(ChangeScreen(3, true));

        // 3. Turn on Crashed Screen.
        if(health < 4)
            StartCoroutine(ChangeScreen(-1 * (int)health + 3, true));
        
        // Break the Phone.
        if(health == 0){
            //All health UI off
            //UIhealth[0].color = new Color(1, 0, 0, 0.4f);
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

    IEnumerator ChangeScreen(int ScreenIndex, bool ScreenOn)
    {
        //1. Turn off EveryScreen
        foreach(GameObject screen in Screens){
            if(screen.activeSelf == true) 
                screen.SetActive(false);
        }
        yield return null;
        //2. Turn on the one Screen
       Screens[ScreenIndex].SetActive(ScreenOn);
        yield return null;
        //3. Turn the screen on and off for a while.
        if(ScreenIndex > 2){ 
            yield return new WaitForSeconds(3.0f);
            Screens[ScreenIndex].SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other) 
    { //떨어지면 Restart
        if(other.gameObject.tag == "Player"){
            SceneManager.LoadScene(stage); //각 장면의 index 사용 가능!
        }
    }

}



