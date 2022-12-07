using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SkeletonAnimation Pajama;    
    public SkeletonDataAsset[] Data;
    //������ �ִϸ��̼�    
    public AnimationReferenceAsset[] SideAnimClip;
    public AnimationReferenceAsset[] FrontAnimClip;
    public AnimationReferenceAsset[] BackAnimClip;

    public enum PlayerView
    {
        BACK, FRONT, SIDE
    }
    private PlayerView _playerView=PlayerView.SIDE;
    public enum AnimState
    {
        IDLE, WALK, RUN
    }    
    //���� �ִϸ��̼� ������Ʈ
    private AnimState _AnimState= AnimState.IDLE;
        
    private string CurrentAnimation;
    private Rigidbody rig;
    float x;
    float y;
    private void Awake()
    {
        rig=GetComponent<Rigidbody>();
    }
    void Start()
    {
        //SetCurrentAnimation(_AnimState, 2);
    }
    void Update()
    {
        Movement();
    }
    private void FixedUpdate()
    {
        rig.velocity = new Vector3(x*100*Time.deltaTime,0,y*100*Time.deltaTime);
    }
    void _AsncAnimation(AnimationReferenceAsset animClip,bool loop,float timeScale=1.0f)
    {
        // ������ �ִϸ��̼��� ����
        if (animClip.name.Equals(CurrentAnimation)) 
            return;
        //�ش�ִϸ��̼����� ����
        Pajama.state.SetAnimation(0,animClip,loop).TimeScale= timeScale; 
        Pajama.loop=loop;
        Pajama.timeScale = timeScale;
        //���� ����ǰ� �ִ� �ִϸ��̼� �̸����� ����
        CurrentAnimation= animClip.name;
    }
    AnimationReferenceAsset[] AnimClip;
    void SetCurrentAnimation(AnimState _state)
    {        
        switch ((int)_playerView)
        {
            case 0:
                AnimClip = BackAnimClip;
                break;
            case 1:
                AnimClip = FrontAnimClip;
                break;
            case 2:
                AnimClip = SideAnimClip;
                break;
        }
        switch(_state)
        {
            case AnimState.IDLE:
                _AsncAnimation(AnimClip[(int)AnimState.IDLE], true);
                break;
            case AnimState.WALK:
                _AsncAnimation(AnimClip[(int)AnimState.WALK], true);
                break;
            case AnimState.RUN:
                break;
        }        
    }
    void Movement()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (!Mathf.Approximately(x, 0.0f)|| !Mathf.Approximately(y, 0.0f))
        {
            //ChangeState(AnimState.WALK);
            _AnimState = AnimState.WALK;
            
            if (y > 0.0f)
            {
                Pajama.skeletonDataAsset = Data[0];
                Pajama.Initialize(true);                
            }
            else
            {
                if (!Mathf.Approximately(x, 0.0f))
                {
                    Pajama.skeletonDataAsset = Data[2];
                    Pajama.Initialize(true);
                    transform.localScale = new Vector3(x, 1, 1);
                    if (x > 0.0f)
                    {
                     
                        //�ִϸ��̼�
                        
                    }
                    else
                    {                       
                        
                    }
                }
                else
                {
                    Pajama.skeletonDataAsset = Data[1];
                    Pajama.Initialize(true);                    
                }
            }
        }
        else
        {
            //ChangeState(AnimState.IDLE);
            _AnimState = AnimState.IDLE;            
        }
        SetCurrentAnimation(_AnimState);
    }
    
}
