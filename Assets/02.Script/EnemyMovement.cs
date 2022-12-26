using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharaterMovement;
using static EnemyCharater;

public class EnemyMovement : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;
    protected string CurrentAnimation;
    EnemyCharater enemyCharater;
    private void Start()
    {
        enemyCharater = GetComponentInParent<EnemyCharater>();
    }
    private void Update()
    {
        SetCurrentAnimation(enemyCharater._battleState);
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
        //현재 재생되고 있는 애니메이션 이름으로 변경
        CurrentAnimation = animClip.name;
    }
    protected void SetCurrentAnimation(BattleState _state)
    {
        _AsncAnimation(AnimClip[(int)_state], true);
    }
}
