using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class EventNull : UnityEvent { }
public class PressureInteract : MonoBehaviour
{
    public EventNull eventnull;
    //public event Action PressureBoard;
    private void OnTriggerEnter(Collider other)
    {
        if (other!=null)
        {
            Debug.Log("触发压力板");
            eventnull?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            Debug.Log("释放压力板");
            eventnull?.Invoke();
        }
    }

}
