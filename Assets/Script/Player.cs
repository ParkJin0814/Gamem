using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SkeletonAnimation Pajama;    
    public SkeletonDataAsset[] Data;
    //스파인 애니메이션    
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
    //현재 애니메이션 스테이트
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
        // 동일한 애니메이션은 리턴
        if (animClip.name.Equals(CurrentAnimation)) 
            return;
        //해당애니메이션으로 실행
        Pajama.state.SetAnimation(0,animClip,loop).TimeScale= timeScale; 
        Pajama.loop=loop;
        Pajama.timeScale = timeScale;
        //현재 재생되고 있는 애니메이션 이름으로 변경
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
                     
                        //애니메이션
                        
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
