using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
   public GameObject explosionEffect;
  void OnCollisionEnter(Collision collision){
    //1. Generate Effect Prefab
    GameObject effect = Instantiate(explosionEffect);
    //2. Set Position Effect Prefab
    effect.transform.position = transform.position + Vector3.up * 50;
    //3. Destroy this Object
    if(collision.gameObject.tag == "Floor") Destroy(gameObject);
    }
  }

