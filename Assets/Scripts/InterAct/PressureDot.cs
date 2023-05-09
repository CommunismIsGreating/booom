using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventNullDot : UnityEvent { }
public class PressureDot : MonoBehaviour
{
    public EventNullDot eventNullDot;
    private void Update()
    {
       if(MouseManager.instance.hit.collider.gameObject == gameObject&&Input.GetMouseButtonDown(0))
        {
            Open();
        }
    }
    public void Open()
    {
        Debug.Log("Openµ÷ÓÃ");
        eventNullDot?.Invoke();
    }
}
