using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Catch : MonoBehaviour
{
    public bool isCatch=false;
    Vector3 pos;
    private void Awake()
    {
        pos = transform.position;
    }
    private void Update()
    {
        if (isCatch)
        {
            transform.position=PlayerControllor.Instance.catchDect.transform.position;
        }

    }

    public void init()
    {
        transform.position = pos;
    }
}
