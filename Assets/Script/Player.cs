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
    
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (y > 0.0f)
        {
            PlayerViewChange(1);
        }
        else
        {
            if(!x.Equals(0.0f))
            {
                PlayerViewChange(0);
            }
            else if(x.Equals(0)&& !y.Equals(0))
            {
                PlayerViewChange(2);
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
