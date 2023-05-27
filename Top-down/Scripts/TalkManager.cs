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

//Talk Data
   void GenerateData(){
    talkData.Add(1000, new string[]{"Hi.:0", "It's your first time here!:1" }); //1000: Npc Luna, :number = portraitData
    talkData.Add(2000, new string[]{"Hey!:1", "What a beautiful lake.:1", "There is something secret hidden in this lake.:1"}); //2000:Npc Ludo
    talkData.Add(100, new string[]{"It's an ordinary wooden box."}); //100:Box
    talkData.Add(200, new string[]{"It's a desk with traces of someone's use."}); //200:Desk

    //Quest Talk
    talkData.Add(10 + 1000, new string[]{"Welcome.:0", 
                                         "There's an amazing legend in this town.:1",
                                         "If you go to the lake on the right, Ludo will tell you.:1"});

    talkData.Add(10 + 1 + 2000, new string[]{"Hey!:0 ",
                                         "Are you here to hear the legend of this lake?:1",
                                         "Then I'd like you to do work.:1",
                                         "I want you to pick up a coin that fell near my house.:1"}); //questId + NpcId + questActionIndex;

   talkData.Add(20 + 1000, new string[]{"Ludo's Coin?:1",
                                         "Don't drop the money!:3",
                                         "I'll have to say something to Ludo later.:3"}); 
   talkData.Add(20 + 2000, new string[]{"If you find it, make sure to bring it.:1"}); 
   talkData.Add(20 + 5000, new string[]{"I found a coin."});
   talkData.Add(20 + 1 + 2000, new string[]{"Thank you for finding.:2"});
   
   
//Portrait Data
//+0: Idle, +1:Talk, +2: Smile, +3: Angry
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

      if(!talkData.ContainsKey(id)){

         if(!talkData.ContainsKey(id - id % 10))
            return GetTalk(id - id % 100, talkIndex); //Get First Talk

         else
             return GetTalk(id - id % 10, talkIndex); //Get First Quest Talk 
         
      }
      
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



