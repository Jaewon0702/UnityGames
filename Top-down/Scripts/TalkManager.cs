using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
   Dictionary<int, string[]> talkData;
   Dictionary<int, Sprite> portraitData;

   public Sprite[] portraitArr;

   void Awake(){

    talkData = new Dictionary<int, string[]>(); 
    portraitData = new Dictionary<int, Sprite>();
    GenerateData();
   }

   void GenerateData(){
    talkData.Add(1000, new string[]{"Hi.:0", "It's your first time here!:1" }); //Npc Luna
    talkData.Add(2000, new string[]{"Hey!:1", "What a beautiful lake.:1", "There is something secret hidden in this lake.:1"}); //Npc Ludo
    talkData.Add(100, new string[]{"It's an ordinary wooden box."}); //Box
    talkData.Add(200, new string[]{"It's a desk with traces of someone's use."}); //Desk

//0: Idle, 1:Talk, 2: Smile, 3: Angry
    portraitData.Add(1000 + 0, portraitArr[0]); 
    portraitData.Add(1000 + 1, portraitArr[1]); 
    portraitData.Add(1000 + 2, portraitArr[2]); 
    portraitData.Add(1000 + 3, portraitArr[3]);

    portraitData.Add(2000 + 0, portraitArr[4]); //Idle
    portraitData.Add(2000 + 1, portraitArr[5]); //Talk
    portraitData.Add(2000 + 2, portraitArr[6]); //Smile
    portraitData.Add(2000 + 3, portraitArr[7]); //Angry

   }

   public string GetTalk(int id, int talkIndex){
      
      if(talkIndex == talkData[id].Length){
         return null;  
      }
      else{
         return talkData[id][talkIndex];
      }
   }

   public Sprite GetPortrait(int id, int portraitindex){
      
      return portraitData[id + portraitindex];

   }

}

