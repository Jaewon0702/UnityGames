using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public TalkManager talkManager;
    public int talkIndex;
    public Image portraitImg;
    

    public void Action(GameObject scanObj)
    { 
        scanObject = scanObj;
        ObjData ObjData = scanObject.GetComponent<ObjData>();
        Talk(ObjData.id, ObjData.isNpc);       
        
        
       talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc){
        string talkData = talkManager.GetTalk(id, talkIndex);
        if(talkData == null){ // When talk is done
            isAction = false;
            talkIndex = 0;
            return; // void 함수에서 강제 종료 역할.

    }

        if(isNpc){
            talkText.text = talkData.Split(':')[0];

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else{
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }   
}


