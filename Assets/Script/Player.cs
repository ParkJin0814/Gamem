using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float PlayerSpeed=50.0f;
    public GameObject[] PlayerView; //0 side 1 back 2 front
    float x;
    float y;
    private Rigidbody rig;

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
                PlayerViewChange(1);
                break;
            case PlayerViewCheck.Front:
                PlayerViewChange(2);
                break;

        }
    }
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rig.velocity = new Vector3(x * PlayerSpeed * Time.deltaTime, rig.velocity.y, y * PlayerSpeed * Time.deltaTime);
    }
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if(y>0.0f)
        {
            ChangeState(PlayerViewCheck.Back);
        }
        else
        {
            if(!x.Equals(0.0f))
            {
                ChangeState(PlayerViewCheck.Side);
            }
            else if(y<0.0f)
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
