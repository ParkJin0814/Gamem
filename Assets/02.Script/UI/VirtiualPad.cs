using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public class VirtiualPad : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler,IPointerUpHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;
    [SerializeField, Range(10f, 150f)]
    private float leverRange;

    private Vector2 inputVector;
    public bool isInput;
    public Player myPlayer;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        lever.localPosition = Vector3.zero;
        isInput = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isInput= true;
        ControlJoystickLever(eventData);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        ControlJoystickLever(eventData); 
         
    }   
    public void OnDrag(PointerEventData eventData)
    {
        
        ControlJoystickLever(eventData);
        
    }

    public void ControlJoystickLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir
            : inputDir.normalized * leverRange;
        lever.anchoredPosition = clampedDir;
        inputVector = clampedDir / leverRange;
    }
    
    private void InputControlVector()
    {        
        myPlayer.x= Mathf.Round(inputVector.x);
        myPlayer.y= Mathf.Round(inputVector.y);
    }
    void Update()
    {
        if (isInput)
        {
            InputControlVector();
        }
        else
        {
            myPlayer.x = 0;
            myPlayer.y = 0;
        }
    }

}
