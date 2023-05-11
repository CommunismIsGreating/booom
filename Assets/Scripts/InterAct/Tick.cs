using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tick : MonoBehaviour
{
    Rigidbody body;
    Vector3 offset;
    [SerializeField] float speed=2f;
    [SerializeField] float Fire_speed = 10f;
    [SerializeField] float StopRadius=0.5f;

    public bool On=false;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(On)Near_Player();
    }
    void Near_Player()
    {
        offset =transform.position-PlayerControllor.Instance.transform.position;
        if (offset.magnitude <= StopRadius) 
        { 
            body.velocity = Vector3.zero;
            return;
        }
        body.velocity = new Vector3(-speed * offset.x, body.velocity.y, -speed * offset.z);
    }
    public void Fire(Vector3 Dir)
    {
        body.velocity = Dir * Fire_speed;
    }
}
