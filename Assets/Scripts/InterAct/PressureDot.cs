using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventNullDot : UnityEvent { }
public class PressureDot : MonoBehaviour
{
    public EventNullDot eventNullDot;
    private void Start()
    {
        MouseManager.instance.DotOpen += this.Open;
    }
    public void Open()
    {
        Debug.Log("Openµ÷ÓÃ");
        eventNullDot?.Invoke();
    }
}
