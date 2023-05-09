using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere : MonoBehaviour
{
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void Off_gravity()
    {
        rb.useGravity = false;
    }
    public void On_gravity()
    {
        rb.useGravity = true;
    }
    public void Change_Pos(Vector3 pos)
    {
        transform.position = pos;
        Stop();
    }
    public float Pos_y()
    {
        return transform.position.y;
    }
    void Stop()
    {
        rb.velocity = Vector3.zero;
    }
}
