using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using static CharaterMovement;

public class PlayerFront : CharaterMovement
{
    
    
    
    protected override void Movement()
    {        
        yy = Input.GetAxisRaw("Vertical");
        if (yy == 0f)
        {
            _AnimState = AnimState.Idle;
        }
        else
        {
            if (!isRunning) _AnimState = AnimState.Walk;
            else _AnimState = AnimState.Run;
        }
        //애니메이션
        SetCurrentAnimation(_AnimState);
    }
}
