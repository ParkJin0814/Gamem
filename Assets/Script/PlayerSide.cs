using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerSide : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;

    public enum AnimState
    { 
        Idle, Walk
    }
    private AnimState _AnimState;
    private string CurrentAnimation;

    private float xx;
    private float yy;

    void Update()
    {        
        xx = Input.GetAxisRaw("Horizontal");
        yy = Input.GetAxisRaw("Vertical");

        if (!yy.Equals(0f)||!xx.Equals(0f))
        {
            _AnimState = AnimState.Walk;
            if(!xx.Equals(0f)) transform.localScale = new Vector3(xx, 1, 1);
        }
        else
        {
            _AnimState = AnimState.Idle;
        }

        //�ִϸ��̼�
        SetCurrentAnimation(_AnimState);
    }    
    void _AsncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale = 1.0f)
    {
        // ������ �ִϸ��̼��� ����
        if (animClip.name.Equals( CurrentAnimation))
            return;
        //�ش�ִϸ��̼����� ����

        skeletonAnimation.AnimationState.SetAnimation(0, animClip, loop).TimeScale=timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;
        //���� ����ǰ� �ִ� �ִϸ��̼� �̸����� ����
        CurrentAnimation = animClip.name;
    }
    void SetCurrentAnimation(AnimState _state)
    {        
        switch (_state)
        {
            case AnimState.Idle:
                _AsncAnimation(AnimClip[(int)AnimState.Idle], true);
                break;
            case AnimState.Walk:
                _AsncAnimation(AnimClip[(int)AnimState.Walk], true);
                break;            
        }
    }
}
