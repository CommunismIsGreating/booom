using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchDect : MonoBehaviour
{
    [SerializeField] float CatchDectRadius = 0.5f;
    [SerializeField] Collider[] Colliders=new Collider[1];
    public bool isCatch=>Physics.OverlapSphereNonAlloc(this.transform.position,CatchDectRadius,Colliders)==1;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, CatchDectRadius);
    }
    public Collider DectCollider()
    {
        return Colliders[0];
    }
   
    private void Update()
    {
    }
}
