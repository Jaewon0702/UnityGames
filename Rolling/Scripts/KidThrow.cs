using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class KidThrow : MonoBehaviour
{
    // Set Ball throw positon
    public GameObject throwPosition;
    public GameObject ballFactory;
    public float throwPower;
    new AudioSource audio;
    public AudioClip[] audioClips;
    public bool isWalk;

    void Awake(){
        audio = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            Throw();
        }
        
    }

    public void Throw()
    {
        GameObject ball = Instantiate(ballFactory); 
        ball.transform.position = throwPosition.transform.position;
        Rigidbody rigid = ball.GetComponent<Rigidbody>();
        //Kid의 정면 방향으로 Ball을 던지자.
        rigid.AddForce((throwPosition.transform.forward + Vector3.up) * throwPower,ForceMode.Impulse);
        //Kid Laughs when throw
        if(isWalk == true){
            ThrowSound("LAUGH");
            ThrowSound("THROW");
        }
        else ThrowSound("SHOT");

        

    }

    void ThrowSound(string action){ // Sound Set Function
    switch(action){
        case "LAUGH":
            audio.clip = audioClips[0];
            break;
        case "THROW":
            audio.clip = audioClips[1];
            break;
        case "DRIVE":
            audio.clip = audioClips[2]; 
            break;
        case "SHOT" :
            audio.clip = audioClips[3];
            break;
    }
    audio.PlayOneShot(audio.clip); // Several Sounds can be played at the same time.
    }
}


