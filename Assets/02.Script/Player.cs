using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float PlayerSpeed;
    public GameObject[] PlayerView; //0 side 1 back 2 front
    float x;
    float y;
    private Rigidbody rig;
    public bool Attacking = false;
    public bool Running = false;
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
        /*float a = Running? 1.5f:1.0f;

        if (!Attacking)
        {
            rig.velocity = new Vector3(x * PlayerSpeed * a * Time.deltaTime, rig.velocity.y, y * PlayerSpeed * a * Time.deltaTime);
        }*/
    }
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        Vector3 pos = transform.position;
        pos.x += x;
        pos.z += y;
        float delta = PlayerSpeed * Time.deltaTime;
        Vector3 dir = pos - transform.position;
        transform.Translate(dir * delta, Space.World);
        if (y>0.0f)
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
