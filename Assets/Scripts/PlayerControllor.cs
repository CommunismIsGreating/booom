using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllor : MonoBehaviour
{
    public static PlayerControllor Instance;    
   private NavMeshAgent agent;
    [SerializeField] private float InterActRadius=5f;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        agent = GetComponent<NavMeshAgent>();   

    }
    private void Start()
    {
        MouseManager.instance.MouseClick += MoveToTarget;
        //MouseManager.instance.MouseClickWithInteract += ChangePosition;
    }
    public void MoveToTarget(Vector3 target)
    {
        Debug.Log("修改目标点");
        agent.destination = target;
    }

    //public void ChangePosition(Vector3 target)
    //{
    //    transform.position = target;    
    //}
    public void StopSelf()
    {
        agent.destination=transform.position;
    }

    public bool IsInterAct(Transform one,Transform two)
    {
        Vector3 temp= one.position - two.position;
        return temp.magnitude < InterActRadius;
    }
}
