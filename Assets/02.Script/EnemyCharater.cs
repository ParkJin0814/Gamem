using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using static EnemyCharater;
using static Player;

public class EnemyCharater : MonoBehaviour
{
    public float MoveSpeed=50.0f;
    public GameObject myTarget;
    bool myTargetRange;
    public GameObject[] myView; //0 side 1 back 2 front
    float x;
    float y;   
    Coroutine Attackco = null;
    public float myAttackRange;
    public enum BattleState
    {
        Idle, Walk, Attack, Die
    }
    public BattleState _battleState= BattleState.Idle;
    public enum ViewCheck
    {
        Side, Back, Front
    }
    ViewCheck _viewCheck = ViewCheck.Side;
    
    void ChangeState(ViewCheck s)
    {
        if (_viewCheck == s) return;
        _viewCheck = s;
        switch (s)
        {
            case ViewCheck.Side:
                MyViewChange(0);
                break;
            case ViewCheck.Back:
                MyViewChange(1);
                break;
            case ViewCheck.Front:
                MyViewChange(2);
                break;

        }
    }    
    void Update()
    {
        if (_battleState != BattleState.Die)
        {
            if (myTarget != null && Attackco == null)
            {
                Attackco = StartCoroutine(AttckingTarget(myTarget.transform, myAttackRange));
            }
            else if (myTarget == null)
            {
                if (Attackco != null) StopCoroutine(Attackco);
                Attackco= null;
            }
            if (Mathf.Abs(y) > Mathf.Abs(x))
            {
                if (y > 0.0f) ChangeState(ViewCheck.Back);
                else ChangeState(ViewCheck.Front);
            }
            else
            {
                ChangeState(ViewCheck.Side);
                if (x < 0.0f) transform.localScale = new Vector3(-1, 1, 1);
                else transform.localScale = new Vector3(1, 1, 1);
            }
        }        
    }
    void MyViewChange(int a)
    {
        foreach (GameObject k in myView)
        {
            k.SetActive(false);
        }
        myView[a].SetActive(true);
    }
    IEnumerator AttckingTarget(Transform target, float AttackRange)
    {        
        float delta = 0.0f;
        while (target != null)
        {
            Vector3 dir = target.position - transform.position;
            x= dir.x;
            y= dir.z;
            float dist = dir.magnitude;
            dir.Normalize();
            if (dist > AttackRange) // 이동할거리가 공격범위보다 클때
            {
                _battleState = BattleState.Walk;
                delta = MoveSpeed * Time.deltaTime;
                if (delta > dist)
                {
                    delta = dist;
                }
                transform.Translate(dir * delta, Space.World);
            }
            else
            {
                //공격
                _battleState = BattleState.Attack;
            }
            yield return null;
        }
        _battleState= BattleState.Idle;
    }
}
