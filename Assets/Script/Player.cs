using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject[] Direction;
    //������ �ִϸ��̼�
    public SkeletonAnimation[] skeletonAnimation;
    public AnimationReferenceAsset[] SideAnimClip;
    public AnimationReferenceAsset[] FrontAnimClip;
    public AnimationReferenceAsset[] BackAnimClip;
    public enum AnimState
    {
        IDLE, WALK, RUN
    }
    public int a = 0;
    //���� �ִϸ��̼� ������Ʈ
    private AnimState _AnimState= AnimState.IDLE;
        
    private int CurrentAnimation;

    public bool MovingCheck = false;
    bool RightCheck=true;
    void ChangeState(AnimState s)
    {
        if(s==_AnimState) return;
        _AnimState = s;
        switch(s)
        {
            case AnimState.IDLE:
                break;
        }
    }
    void Start()
    {
        SetCurrentAnimation(_AnimState, SideAnimClip, 2);
    }

    void Update()
    {
        Movement();
    }    
    void _AsncAnimation(AnimationReferenceAsset animClip,bool loop,int a)
    {
        if (CurrentAnimation.Equals((int)_AnimState)) return; // ������ �ִϸ��̼��� ����

        skeletonAnimation[a].state.SetAnimation(0,animClip,loop); //�ش�ִϸ��̼����� ����
        CurrentAnimation= (int)_AnimState;
    }
    void SetCurrentAnimation(AnimState _state, AnimationReferenceAsset[] AnimClip,int a)
    {
        _AsncAnimation(AnimClip[(int)_state], true, a);
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (!Mathf.Approximately(x, 0.0f))
        {
            _AnimState = AnimState.WALK;
            if (!MovingCheck)
            {
                SetCurrentAnimation(_AnimState, SideAnimClip, 2);
                MovingCheck = true;
            }
            if (x > 0.0f)
            {                
                if (!RightCheck) Direction[2].transform.Rotate(0, 180, 0);
                RightCheck = true;
                DirectionCheck(2);
                
            }
            else
            {
                if (RightCheck) Direction[2].transform.Rotate(0, 180, 0);
                RightCheck = false;
                DirectionCheck(2);           
            }            
        }
        else
        {
            _AnimState = AnimState.IDLE;
            MovingCheck = false;
            if (Direction[0].activeSelf) SetCurrentAnimation(_AnimState, BackAnimClip, 0);
            if (Direction[1].activeSelf) SetCurrentAnimation(_AnimState, FrontAnimClip, 1);
            if (Direction[2].activeSelf) SetCurrentAnimation(_AnimState, SideAnimClip, 2);
        }
    }
    void DirectionCheck(int a)
    {
        foreach(GameObject go in Direction)
        {
            go.SetActive(false);
        }
        Direction[a].SetActive(true);
        
    }
}
