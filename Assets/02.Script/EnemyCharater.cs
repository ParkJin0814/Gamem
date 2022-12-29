using System.Collections;
using UnityEngine;

public class EnemyCharater : MonoBehaviour
{
    public float MoveSpeed=1f;
    public GameObject myTarget;
    //0 side 1 back 2 front
    public GameObject[] myView; 
    float x;
    float y;   
    Coroutine Attackco = null;
    public float myAttackRange;
    public bool IsWalk = false;
    public bool IsAttack = false;
    public bool IsAttacking = false;
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

        if(!IsAttacking)Movement();

    }
    void Movement()
    {
        if (myTarget != null && Attackco == null)
        {
            Attackco = StartCoroutine(AttckingTarget(myTarget.transform, myAttackRange));
        }
        else if (myTarget == null)
        {
            if (Attackco != null) StopCoroutine(Attackco);
            Attackco = null;
        }
        if (Mathf.Abs(y) > Mathf.Abs(x))
        {
            if (y > 0.0f)
            {
                ChangeState(ViewCheck.Back);
            }
            else
            {
                ChangeState(ViewCheck.Front);
            }
        }
        else
        {
            ChangeState(ViewCheck.Side);
            if (myView[0].activeSelf)
            {
                if (x < 0.0f) myView[0].transform.localScale = new Vector3(-1, 1, 1);
                else myView[0].transform.localScale = new Vector3(1, 1, 1);
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
            // �̵��ҰŸ��� ���ݹ������� Ŭ��
            if (dist > AttackRange) 
            {
                IsAttack = false;
                IsWalk = true;
                delta = MoveSpeed * Time.deltaTime;
                if (delta > dist)
                {
                    delta = dist;
                }
                if(!IsAttacking)transform.Translate(dir * delta, Space.World);
            }
            else
            {
                IsWalk = false;                
                IsAttack = true;
            }
            yield return null;
        }        
        IsAttack= false;
    }
}
