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
    protected void SetCurrentAnimation(BattleState _state)
    {
        _AsncAnimation(AnimClip[(int)_state], true);
    }
}
