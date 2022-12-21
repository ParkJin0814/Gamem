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
    //����ǰ��ִ� �ִϸ��̼��̸�
    protected string CurrentAnimation;    
    //�ִϸ��̼� üũ������ x y��
    protected float xx;
    protected float yy;    
    protected bool _IsSide = false;
    //�޸����� üũ
    public bool isRunning = false;
    //���������� üũ
    public bool isAttacking = false;
    void Update()
    {
        GetComponentInParent<Player>().Attacking=isAttacking;
        GetComponentInParent<Player>().Running = isRunning;
        if (!isAttacking)
        {
            Movement();
        }
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
        isAttacking= true;
        _AnimState = AnimState.Attack;
        if (!xx.Equals(0f)&&_IsSide) transform.localScale = new Vector3(xx, 1, 1);
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
        //�ִϸ��̼�
        SetCurrentAnimation(_AnimState);
    }

    protected void _AsncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale = 1.0f)
    {
        // ������ �ִϸ��̼��� ����
        if (animClip.name.Equals(CurrentAnimation))
            return;
        //�ش�ִϸ��̼����� ����

        skeletonAnimation.AnimationState.SetAnimation(0, animClip, loop).TimeScale = timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;        
        //���� ����ǰ� �ִ� �ִϸ��̼� �̸����� ����
        CurrentAnimation = animClip.name;
    }
    protected void SetCurrentAnimation(AnimState _state)
    {
        _AsncAnimation(AnimClip[(int)_state], true);        
    }

}
