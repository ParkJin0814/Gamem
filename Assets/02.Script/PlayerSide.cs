using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerSide : CharaterMovement
{    
        
    protected override void Movement()
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
        //애니메이션
        SetCurrentAnimation(_AnimState);
    }

}
