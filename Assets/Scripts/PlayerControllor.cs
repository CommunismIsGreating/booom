using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllor : MonoBehaviour
{
    public static PlayerControllor Instance;    
   public NavMeshAgent agent;
    [SerializeField] private float InterActRadius=5f;
    [SerializeField] private float CatchRadius = 3f;
    [SerializeField] private float JumpRadius = 5f;
    [SerializeField] private float JumpTime = 1f;
    private float timer=0f;
    public float JumpForce = 10f;
    [SerializeField] float JumpV = 10f;
    public float ThrowDistance_Max = 20f;
    public CatchDect catchDect;
    public GroundDect groundDect;

    public Rigidbody body;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        timer = 0;
        agent = GetComponent<NavMeshAgent>();
        body = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        MouseManager.instance.MouseClick += MoveToTarget;
        //MouseManager.instance.MouseClickWithInteract += ChangePosition;
    }
    private void Update()
    {
        
       
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
    public bool IsCatch(Collider two)
    {
        Vector3 vector3 = transform.position - two.transform.position;
        return vector3.magnitude < CatchRadius&&!catchDect.isCatch;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, CatchRadius);
    }
    public void Jump()
    {
        Debug.Log("jump");
        timer = 0;
        agent.enabled= false;   
        body.velocity =new Vector3(MouseManager.instance.Dir.x*JumpForce,JumpV, MouseManager.instance.Dir.z*JumpForce);
        StartCoroutine(AgentOn());
    }
    IEnumerator AgentOn()
    {
        Debug.Log("1");
        while (timer < JumpTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        StartCoroutine( groundDect.AgentOn());
        timer = 0f;
    }
}
