using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerBack : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;

    public enum AnimState
    { 
        Idle, Walk
    }
    private AnimState _AnimState;
    private string CurrentAnimation;
    private Rigidbody rig;
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rig.velocity = new Vector3(rig.velocity.x, rig.velocity.y, xx * 5 * Time.deltaTime);
    }


    private float xx;
    
    void Update()
    {
        transform.parent.position = transform.position;
        xx = Input.GetAxisRaw("Vertical");
        if (xx==0f)
        {
            _AnimState = AnimState.Idle;
        }
        else
        {
            _AnimState = AnimState.Walk;            
        }

        //애니메이션
        SetCurrentAnimation(_AnimState);
    }    
    void _AsncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale = 1.0f)
    {
        // 동일한 애니메이션은 리턴
        if (animClip.name.Equals( CurrentAnimation))
            return;
        //해당애니메이션으로 실행

        skeletonAnimation.AnimationState.SetAnimation(0, animClip, loop).TimeScale=timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;
        //현재 재생되고 있는 애니메이션 이름으로 변경
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
