using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class CharaterMovement : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;

    public enum AnimState
    {
        Idle, Walk, Attack, Run
    }
    protected AnimState _AnimState;
    //재생되고있는 애니메이션이름
    protected string CurrentAnimation;
    //애니메이션 체크를위한 x y값
    protected float xx;
    protected float yy;    
    protected bool _IsSide = false;
    //달리는지 체크
    public bool isRunning = false;
    //공격중인지 체크
    public bool isAttacking = false;
    void Update()
    {
        GetComponentInParent<Player>().Attacking=isAttacking;
        GetComponentInParent<Player>().Running = isRunning;
        if (!isAttacking) Movement();        
        else
        {
            if (!AnimClip[(int)AnimState.Attack].Equals(CurrentAnimation)) isAttacking = false;
        }
        if (Input.GetKey(KeyCode.Z)) Attack();
        if(Input.GetKey(KeyCode.X)) isRunning= true;
        else isRunning= false;
    }
    protected void Attack()
    {        
        isAttacking = true;
        _AnimState = AnimState.Attack;
        xx = Input.GetAxisRaw("Horizontal");
        if (!xx.Equals(0f) && _IsSide)
        {            
            transform.localScale = new Vector3(xx, 1, 1);
        }
        SetCurrentAnimation(_AnimState);
    }
    protected virtual void Movement()
    {
        xx = Input.GetAxisRaw("Horizontal");
        yy = Input.GetAxisRaw("Vertical");
        if (!yy.Equals(0f) || !xx.Equals(0f))
        {
            _AnimState = AnimState.Walk;
            if (!xx.Equals(0f)) transform.localScale = new Vector3(xx, 1, 1);
        }
        else
        {
            _AnimState = AnimState.Idle;
        }
        //애니메이션
        SetCurrentAnimation(_AnimState);
    }

    protected void _AsncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale = 1.0f)
    {
        // 동일한 애니메이션은 리턴
        if (animClip.name.Equals(CurrentAnimation)) return;
        //해당애니메이션으로 실행
        skeletonAnimation.AnimationState.SetAnimation(0, animClip, loop).TimeScale = timeScale;        
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;        
        //현재 재생되고 있는 애니메이션 이름으로 변경
        CurrentAnimation = animClip.name;
    }
    protected void SetCurrentAnimation(AnimState _state)
    {
        _AsncAnimation(AnimClip[(int)_state], true);        
    }
}
