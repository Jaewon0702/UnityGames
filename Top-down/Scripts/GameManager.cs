using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator talkPanel;
    public Animator portraitAnim;
    public Text talkText;
    public Text questText;
    public GameObject scanObject;
    public GameObject menuSet;
    public GameObject player;
    public bool isAction;
    public TalkManager talkManager;
    public QuestManager questManager;
    public int talkIndex;
    public Image portraitImg;
    public Sprite prevPortrait;

    void Start(){
        GameLoad();
        questText.text = questManager.CheckQuest();    
    }

    void Update(){

    //Sub Menu
        if(Input.GetButtonDown("Cancel")){
            if(menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);

        }
    }
    

    public void Action(GameObject scanObj)
    { 
        scanObject = scanObj;
        ObjData ObjData = scanObject.GetComponent<ObjData>();
        Talk(ObjData.id, ObjData.isNpc);       
        
        
       talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNpc){

        //Set Talk Data
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex , talkIndex);//Quest Talk Data Id = NpcId + QuestId

        // When talk is done
        if(talkData == null){ 
            isAction = false;
            talkIndex = 0;
            questText.text = questManager.CheckQuest(id);
            return; // void 함수에서 강제 종료 역할.

    }
        //Continue Talk
        if(isNpc){
            talkText.text = talkData.Split(':')[0];
        //Show Portrait
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        //Animation Portrait
           if(prevPortrait != portraitImg.sprite){
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
        } 
        else{
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }   

    public void GameSave()
    {
       PlayerPrefs.SetFloat("PlayerX", player.transform.position.x); //player.x
       PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);//player.y
       PlayerPrefs.SetInt("QuestId", questManager.questId);//Quest Id
       PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex); //Quest Action Index
       PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        if(!PlayerPrefs.HasKey("PlayerX"))
            return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questid = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questid;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }

    
    
    public void GameExit()
    {
        Application.Quit();
    }
}


