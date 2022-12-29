using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float PlayerSpeed;
    //0 side 1 back 2 front      
    public GameObject[] PlayerView;
    public bool IsAttacking;
    public float x;
    public float y;
    public enum PlayerViewCheck
    { 
        Side, Back, Front
    }
    PlayerViewCheck _playerViewCheck=PlayerViewCheck.Side;
    void ChangeState(PlayerViewCheck s)
    {
        if(_playerViewCheck==s) return;
        _playerViewCheck=s;
        switch(s)
        {
            case PlayerViewCheck.Side:
                PlayerViewChange(0);
                break;
            case PlayerViewCheck.Back:
                transform.localScale = new Vector3(1, 1, 1);
                PlayerViewChange(1);
                break;
            case PlayerViewCheck.Front:
                transform.localScale = new Vector3(1, 1, 1);
                PlayerViewChange(2);
                break;
        }
    }     
    void Update()
    {
        if(!IsAttacking)myMovement();
    }
    void myMovement()
    {
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");
        
        Vector3 pos = transform.position;
        pos.x += x;
        pos.z += y;
        float delta = PlayerSpeed * Time.deltaTime;
        Vector3 dir = pos - transform.position;
        dir.Normalize();
        transform.Translate(dir * delta, Space.World);
        if ((int)y > 0.0f)
        {
            ChangeState(PlayerViewCheck.Back);
        }
        else
        {
            if (!x.Equals(0.0f))
            {
                PlayerView[0].transform.localScale = new Vector3(x, 1, 1);
                ChangeState(PlayerViewCheck.Side);
            }
            else if (y < 0.0f)
            {                
                ChangeState(PlayerViewCheck.Front);
            }
        }
    }
    void PlayerViewChange(int a)
    {
        foreach(GameObject k in PlayerView)
        {
            k.SetActive(false);
        }
        PlayerView[a].SetActive(true);
    }
}
