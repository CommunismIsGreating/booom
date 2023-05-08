using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;
using System;
using System.Numerics;
using Unity.VisualScripting;
using Vector3 = UnityEngine.Vector3;
//[System.Serializable]//δ�̳�monobehavior���಻��ʾ,Ҫ���л�
//public class EventV3:UnityEvent<Vector3> { }
public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;

    public event Action<UnityEngine.Vector3> MouseClick;
    public event Action<Transform,Transform> MouseClickWithInteract;
    public event Action DotOpen;
    public Transform playerPosition;
    [SerializeField]private CatchDect catchD;
    [SerializeField]private Catch catchT;
    private float timer;
    //public event Action<Transform, Transform> MouseClickWithInteractOnly;


    //�洢����ײ����������Ϣ
    RaycastHit hit;
    public Vector3 Dir=>(hit.point-PlayerControllor.Instance.transform.position).normalized;

    Collider temp;


    //��Ծ,Ͷ���߶�ϵ��
    [SerializeField] private float Highcoefficient = 1.75f;
    ////��Ծ��Ͷ��ʱ��
    //[SerializeField] private float JumpTime = 1f;
    //public IEnumerator BeziesCurvor(Transform one, Vector3 two)
    //{
    //    Vector3 temp = new Vector3((one.position.x - two.x) / 2, Highcoefficient * (one.position.x + two.x) / 2+(one.position.y+two.y)/2, (one.position.z - two.z) / 2);
    //    float timer = 0f;

    //    while (one.position != two)
    //    {
    //        Debug.Log(timer);
    //        timer += Time.deltaTime;
    //        timer = Mathf.Clamp(timer, 0, 1);
    //        //Vector3 v1 = new Vector3(Mathf.Lerp(one.position.x, temp.x, timer), Mathf.Lerp(one.position.y, temp.y, timer), Mathf.Lerp(one.position.z, temp.z, timer));
    //        //Vector3 v2 = new Vector3(Mathf.Lerp(temp.x, two.x, timer), Mathf.Lerp(temp.y, two.y, timer), Mathf.Lerp(temp.z, two.z, timer));
    //        //one.position = v1 + v2;
    //        Vector3 p0p1 = (1 - timer) * one.position + timer * temp;
    //        Vector3 p1p2 = (1 - timer) * temp + timer * two;
    //        one.position = (1 - timer) * p0p1 + timer * p1p2;
    //        yield return null;
    //    }
    //}
    public void move(Transform Throw,Vector3 point)
    {
        Throw.position=new Vector3(point.x, point.y+Highcoefficient, point.z);
        Throw.gameObject.GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
    }


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        MouseManager.instance.MouseClickWithInteract += ChangePosition;
        temp = null;
    }
    private void Update()
    {
        SetCursorTexture();
        MouseControl();
        playerPosition = PlayerControllor.Instance.transform;
        timer += Time.deltaTime;
    }

    void SetCursorTexture()
    {
        //Debug.Log("SetCursorTexture");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //�л������ͼ
        }
    }
    void MouseControl()
    {
        //Debug.Log("on");
        if (Input.GetMouseButtonDown(0) && hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Ground")|| hit.collider.gameObject.CompareTag("NoMovableInt") || hit.collider.gameObject.CompareTag("InteractCatch"))
            {
                Debug.Log("�ƶ�");
                MouseClick?.Invoke(hit.point);

            }
            //��ť����
            if (hit.collider.gameObject.CompareTag("NoMovableIntForever"))
            {
                Debug.Log("����������ť");
                DotOpen?.Invoke();
            }
        }
        //����������任λ��
        if (Input.GetKey(KeyCode.Q) && Input.GetMouseButtonDown(0) && (hit.collider.gameObject.CompareTag("Interact")|| hit.collider.gameObject.CompareTag("InteractCatch")) &&PlayerControllor.Instance.IsInterAct(hit.collider.transform,playerPosition))
        {
            Debug.Log("���λ�ñ任");
            MouseClickWithInteract?.Invoke(hit.collider.gameObject.transform,PlayerControllor.Instance.transform);
            PlayerControllor.Instance.StopSelf();
        }
        //����任λ��
        if (Input.GetKey(KeyCode.E) && Input.GetMouseButtonDown(0) && (hit.collider.gameObject.CompareTag("Interact") || hit.collider.gameObject.CompareTag("InteractCatch")) && PlayerControllor.Instance.IsInterAct(hit.collider.transform, playerPosition))
        {
            if (temp == null) {
                temp = hit.collider;
                
                return;
            }
            else if (temp != hit.collider)
            {
                PosionChange(temp, hit.collider);
                PlayerControllor.Instance.StopSelf();
                Debug.Log("����λ�ñ任");
                
                temp = null;
            }
            else
            {
                Debug.Log("ȡ������λ�ñ任");
                temp = null;
            }
        }
        //Debug.Log("" + Input.GetKey(KeyCode.R)  +" "+ hit.collider.gameObject.CompareTag("Catch") +" "+ PlayerControllor.Instance.IsCatch( hit.collider));
        //ץȡ;
        if (Input.GetKeyDown(KeyCode.R)&& hit.collider.gameObject.CompareTag("Catch") && PlayerControllor.Instance.IsCatch( hit.collider))
        {
            Debug.Log("ץȡ");
            catchT = hit.collider.gameObject.GetComponent<Catch>();
            catchT.isCatch = true;
            timer = 0;
            //StartCoroutine(BeziesCurvor(hit.collider.transform, catchD.transform.position));
        }
        //Ͷ��
        if (Input.GetKeyDown(KeyCode.R) && hit.collider != null && !PlayerControllor.Instance.IsCatch(hit.collider)&&timer>2f)
        {
            Debug.Log("on");
            Vector3 temp = PlayerControllor.Instance.transform.position - hit.point;
            if (temp.magnitude > PlayerControllor.Instance.ThrowDistance_Max) return;
            Debug.Log("Ͷ��");
            catchT.isCatch = false;
            //StartCoroutine(BeziesCurvor(catchT.transform,hit.point));
            move(catchT.transform,hit.point);
            catchT=null;
        }
        //��Ծ
        if (Input.GetKeyDown(KeyCode.Space)&&PlayerControllor.Instance.groundDect.canJump)
        {
            PlayerControllor.Instance.Jump();
            Debug.Log("Jump" + PlayerControllor.Instance.body.velocity.y);
        }
    }

    void PosionChange(Collider one, Collider two)
    {
        UnityEngine.Vector3 temp = one.gameObject.transform.position;
        one.gameObject.transform.position = two.gameObject.transform.position;
        two.gameObject.transform.position = temp;
    }
    public void ChangePosition(Transform one, Transform two)
    {
        UnityEngine.Vector3 temp = one.gameObject.transform.position;
        one.gameObject.transform.position = two.gameObject.transform.position;
        two.gameObject.transform.position = temp;
    }
}
