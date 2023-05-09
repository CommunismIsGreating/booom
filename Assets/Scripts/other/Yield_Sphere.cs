using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Yield_Sphere : MonoBehaviour
{
    public sphere OwnSphere;
   


    private void Update()
    {
        if (OwnSphere.Pos_y() < -10.0f)
        {
            init();
        }
    }

    void init()
    {
        OwnSphere.Off_gravity();
        OwnSphere.Change_Pos(transform.position);
    }
    public void Enable()
    {
        init();
        OwnSphere.On_gravity();
    }
}
