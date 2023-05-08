using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour
{
    public bool isCatch=false;
    private void Update()
    {
        if (isCatch)
        {
            transform.position=PlayerControllor.Instance.catchDect.transform.position;
        }
    }
}
