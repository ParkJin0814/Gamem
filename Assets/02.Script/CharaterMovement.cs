using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class CharaterMovement : MonoBehaviour
{
    float x;
    float y;
    Animator myAnimator;    
    Player myPlayer;
    void Start()
    {
        myAnimator=GetComponent<Animator>();
        myPlayer=gameObject.GetComponentInParent<Player>();
    }
    void Update()
    {

        Movement();
    }
    void Movement()
    {
        x = myPlayer.x;
        y = myPlayer.y;
        if (x != 0 || y != 0)
        {
            myAnimator.SetBool("IsWalk", true);
        }
        else
        {
            myAnimator.SetBool("IsWalk", false);
        }
        if(Input.GetKey(KeyCode.X))
        {
            myAnimator.SetBool("IsRun", true);
        }
        else
        {
            myAnimator.SetBool("IsRun", false);
        }
        if(Input.GetKeyDown(KeyCode.Z)&&!myAnimator.GetBool("IsAttacking"))
        {
            myAnimator.SetTrigger("Attack");
        }
        myPlayer.IsAttacking=myAnimator.GetBool("IsAttacking");
    }
}
