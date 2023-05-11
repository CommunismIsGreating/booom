using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Unity.VisualScripting.Member;

public class PlayerControllor : MonoBehaviour
{
    public static PlayerControllor Instance;
    PlayerInput input;
   //public NavMeshAgent agent;
    [SerializeField] private float InterActRadius=5f;
    [SerializeField] private float CatchRadius = 3f;
    [SerializeField] private float WalkV = 10f;
    Vector3 Dir=new Vector3(0,0,0);
    [SerializeField] float JumpV = 10f;
    public float ThrowDistance_Max = 20f;
    public CatchDect catchDect;
    public GroundDect groundDect;

    public Rigidbody body;

    public bool isJump => body.velocity.y != 0;
    public bool isQ = false;
    public bool isE = false;
    public bool isR = false;
    public float angle=0;
    Quaternion q;
    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        //agent = GetComponent<NavMeshAgent>();
        body = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        input.EnableGamePlay();
        //MouseManager.instance.MouseClick += MoveToTarget;
        //MouseManager.instance.MouseClickWithInteract += ChangePosition;
    }
    private void Update()
    {
        Dir = new Vector3(input.axes.x, body.velocity.y, input.axes.y);
        q = Quaternion.AngleAxis(angle+90, Vector3.up);
        Dir =q*Dir;

        Move();
        Q();
        E();
        R();
        //body.AddForce(Dir*WalkV);
        
    }

    void Move()
    {
       //ÒÆ¶¯
       setVelocityX(Dir.x*WalkV);
       setVelocityZ(Dir.z*WalkV);
        if (input.Jump&&groundDect.canJump)
        {
            setVelocityY(JumpV);
        }
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
        //agent.enabled= false;   
        setVelocityY(JumpV);
        //StartCoroutine(AgentOn());
    }


    void setVelocityX(float X)
    {
        body.velocity = new Vector3(X,body.velocity.y,body.velocity.z);
    }
    void setVelocityY(float Y)
    {
        body.velocity = new Vector3(body.velocity.x, Y, body.velocity.z);
    }
    void setVelocityZ(float Z)
    {
        body.velocity = new Vector3(body.velocity.x, body.velocity.y, Z);
    }
    void Q()
    {
        if (input.onQ)
        {
            isQ = true;
        }
        if(input.deQ) 
        {
            isQ = false;
        }
    }
    void E()
    {
        if (input.onE)
        {
            isE = true;
        }
        if (input.deE)
        {
            isE = false;
        }
    }
    void R()
    {
        if (input.onR)
        {
            Debug.Log("R");
            isR = true;
        }
        if (input.deR)
        {
            isR = false;
        }
    }
}
