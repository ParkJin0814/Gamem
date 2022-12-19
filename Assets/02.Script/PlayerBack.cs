using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerBack : CharaterMovement
{
    
    protected override void Movement()
    {
        xx = Input.GetAxisRaw("Vertical");
        if (xx == 0f)
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
}
