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
        Idle, Walk, Attack
    }
    protected AnimState _AnimState;
    protected string CurrentAnimation;

    protected float xx;
    protected float yy;

    public bool isAttacking = false;
    void Update()
    {
        
        if (!isAttacking)
        {
            Movement();
        }
        else
        {
            if (!AnimClip[(int)AnimState.Attack].Equals(CurrentAnimation)) isAttacking = false;
        }
        if (Input.GetKey(KeyCode.Z)) Attack();
    }
    protected void Attack()
    {
        isAttacking= true;
        _AnimState = AnimState.Attack;
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
        if (animClip.name.Equals(CurrentAnimation))
            return;
        //해당애니메이션으로 실행

        skeletonAnimation.AnimationState.SetAnimation(0, animClip, loop).TimeScale = timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;
        if (animClip.name.Equals(AnimClip[(int)AnimState.Attack]))
        {
            skeletonAnimation.AnimationState.AddAnimation(0, CurrentAnimation, loop,0f);
        }
        //현재 재생되고 있는 애니메이션 이름으로 변경
        CurrentAnimation = animClip.name;
    }
    protected void SetCurrentAnimation(AnimState _state)
    {
        switch (_state)
        {
            case AnimState.Idle:
                _AsncAnimation(AnimClip[(int)AnimState.Idle], true);
                break;
            case AnimState.Walk:
                _AsncAnimation(AnimClip[(int)AnimState.Walk], true);
                break;
            case AnimState.Attack:
                _AsncAnimation(AnimClip[(int)AnimState.Attack], true);
                break;
        }
    }
}
