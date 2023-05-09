using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDect : MonoBehaviour
{
    [SerializeField] float groundDectRadius = 0.05f;
    [SerializeField] Collider[] Colliders = new Collider[1];
    public LayerMask groundLayerMask;
    public bool canJump => Physics.OverlapSphereNonAlloc(this.transform.position, groundDectRadius, Colliders,groundLayerMask) == 1;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, groundDectRadius);
    }
    private void Update()
    {
        
    }
    //public IEnumerator AgentOn()
    //{
    //    Debug.Log("agent");
    //    while (!canJump)
    //    {
    //        yield return null;
    //    }
    //    PlayerControllor.Instance.agent.enabled = true;
    //}
    public bool CanJump()
    {
        return canJump;
    }
}
