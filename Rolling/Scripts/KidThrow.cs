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
    public TrailRenderer trailEffect;

    void Awake(){
        audio = GetComponent<AudioSource>();
    }
     public void Throw()
    {
        //Kid Laughs when throw
        if(isWalk == true){
            ThrowSound("LAUGH");
            ThrowSound("THROW");
            StopCoroutine(nameof(ThrowEffect));
            StartCoroutine(nameof(ThrowEffect));
        }
        else ThrowSound("SHOT");
        GameObject ball = Instantiate(ballFactory); 
        ball.transform.position = throwPosition.transform.position;
        Rigidbody rigid = ball.GetComponent<Rigidbody>();
        //Let's Throw Ball Foward.
        rigid.AddForce((throwPosition.transform.forward + Vector3.up) * throwPower,ForceMode.Impulse);
    }

    IEnumerator ThrowEffect()
    {
        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.5f);
        trailEffect.enabled = false;

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




