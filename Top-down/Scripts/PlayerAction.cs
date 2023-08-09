using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;

    public GameManager manager;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;

    Rigidbody2D rigid;
    Animator anim;

    GameObject scanObject;

    //Mobile key bar
    int up_Value;
    int down_Value;
    int right_Value;
    int left_Value;
    bool up_Down;
    bool down_Down;
    bool right_Down;
    bool left_Down;
    bool up_Up;
    bool down_Up;
    bool right_Up;
    bool left_Up;

   void Awake(){
    rigid = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    
   }

    // Update is called once per frame
    void Update()
    {
        //Move Value
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal") + right_Value + left_Value; // Can use bluetooth keyboard in Mobile 
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical") + up_Value + down_Value; // Cuz, plused Mobile key var

        //Check Button Down & Up
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal") || right_Down || left_Down;
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical") || up_Down || down_Down;
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical") || up_Up || down_Up;

        //Check Horizontal Move
        if(hDown){
            isHorizonMove = true;
        }
        else if(vDown){
            isHorizonMove = false;
        }
        else if(hUp || vUp){
            isHorizonMove = h != 0;

        }

        //Mobile var init
        up_Down = false;
        down_Down = false;
        right_Down = false;
        left_Down = false;
        up_Up = false;
        down_Up = false;
        right_Up = false;
        left_Up = false;

        //Animation
        if(anim.GetInteger("hAxisRaw") != h){
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if(anim.GetInteger("vAxisRaw") != v){
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else{
            anim.SetBool("isChange", false);
        }

        //Direction
        if(vDown && v == 1){
            dirVec = Vector3.up;
        }
        else if(vDown && v == -1){
            dirVec = Vector3.down;
        }
        else if(hDown && h == -1){
            dirVec = Vector3.left;
        }
        else if(hDown && h == 1){
            dirVec = Vector3.right;
        }

        //Scan Object
        if(Input.GetButtonDown("Jump") && scanObject != null){
            manager.Action(scanObject); 
        }
        
        

    }

    void FixedUpdate(){
        //Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

        //Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if(rayHit.collider != null){
            scanObject = rayHit.collider.gameObject;
        }
        else{
            scanObject = null;
        }
        

    }

    public void ButtonDown(string type)
    {
        switch(type){
            case "U" :
                up_Value = 1;
                up_Down = true;
                break;
            case "D" :
                down_Value = -1;
                down_Down = true;
                break;
            case "L" :
                left_Value = -1;
                left_Down = true;
                break;
            case "R" :
                right_Value = 1;
                right_Down = true;
                break;

            case "A" :
                if(scanObject != null)
                manager.Action(scanObject);
                break;

            case "E" :
                manager.SubMenuActive();
                break;
            }
    }

    public void ButtonUp(string type)
    {
        switch(type){
            case "U" :
                up_Value = 0;
                up_Up = true;
                break;
            case "D" :
                down_Value = 0;
                down_Up = true;
                break;
            case "L" :
                left_Value = 0;
                left_Up = true;
                break;
            case "R" :
                right_Value = 0;
                right_Up = true;
                break;

        }
    }
}

