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
    public AudioClip audioLaugh;
    public AudioClip audioThrow;

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
        ThrowSound("LAUGH");
        ThrowSound("THROW");

    }

    void ThrowSound(string action){ // Sound Set Function
    switch(action){
        case "LAUGH":
            audio.clip = audioLaugh;
            break;
        case "THROW":
            audio.clip = audioThrow;
            break;
    }
    audio.PlayOneShot(audio.clip); // Several Sounds can be played at the same time.
    }
}
