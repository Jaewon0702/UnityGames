using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{

    Rigidbody rigid;
    public float JumpPower;
    public float MovePower;
    bool isJump;

    private void Awake() {

        isJump = false;
        rigid = GetComponent<Rigidbody>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h,0,v) * MovePower, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && !isJump){
            isJump = true;
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            }
    }
    
    void OnCollisionEnter(Collision collision) {
        
        if(collision.gameObject.name == "Floor"){
            isJump = false;
        }
        
    }

}

