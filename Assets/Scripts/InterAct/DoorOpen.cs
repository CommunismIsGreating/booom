using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DoorOpen : MonoBehaviour
{
    [SerializeField]protected bool isOpen = true;
    [SerializeField]private float Distance = 10f;
    [SerializeField] private float speed = 10f;
    private Vector3 origin;
    private Vector3 end;
    private void Awake()
    {
        origin = transform.position;
        end=new Vector3(origin.x,origin.y-Distance,origin.z);
    }

    private void FixedUpdate()
    {
        ChangeDoor();
    }

    public void DoorStateChange()
    {
        isOpen=!isOpen;
    }
    private void ChangeDoor()
    {
        if (isOpen&&transform.position.y>=end.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
        }else if(!isOpen&&transform.position.y <= origin.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
        }
    }
}
