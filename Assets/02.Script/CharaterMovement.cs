using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class CharaterMovement : MonoBehaviour
{
    float x;
    float y;
    Animator myAnimator;
    public bool myViewSide = false;
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
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (x != 0 || y != 0)
        {
            if(x!=0&& myViewSide)
            {
                transform.parent.localScale=new Vector3(x,1,1);
            }
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
