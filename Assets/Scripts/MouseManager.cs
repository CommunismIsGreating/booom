using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;
using System;
using System.Numerics;
using Unity.VisualScripting;
using Vector3 = UnityEngine.Vector3;
//[System.Serializable]//未继承monobehavior的类不显示,要序列化
//public class EventV3:UnityEvent<Vector3> { }
public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;

    //public event Action<UnityEngine.Vector3> MouseClick;
    public event Action<Transform,Transform> MouseClickWithInteract;
    public Transform playerPosition;
    [SerializeField]private CatchDect catchD;
    [SerializeField]private Catch catchT;
    private float timer;
    //public event Action<Transform, Transform> MouseClickWithInteractOnly;


    //存储射线撞到的物体信息
    public RaycastHit hit;
    //public Vector3 Dir=>(hit.point-PlayerControllor.Instance.transform.position).normalized;

    Collider temp;


    //跳跃,投掷高度系数
    [SerializeField] private float Highcoefficient = 1.75f;

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
            //切换鼠标贴图
        }
    }
    void MouseControl()
    {
        //Debug.Log("on");
        if (Input.GetMouseButtonDown(0) && hit.collider != null)
        {
            //if (hit.collider.gameObject.CompareTag("Ground")|| hit.collider.gameObject.CompareTag("NoMovableInt") || hit.collider.gameObject.CompareTag("Catch"))
            //{
                //Debug.Log("移动");
                //MouseClick?.Invoke(hit.point);

            //}
            //按钮交互
            if (hit.collider.gameObject.CompareTag("NoMovableIntForever"))
            {
                Debug.Log("尝试启动按钮");
            }
        }
        //自身与物体变换位置
        if (PlayerControllor.Instance.isQ && Input.GetMouseButtonDown(0) && (hit.collider.gameObject.CompareTag("Interact")|| hit.collider.gameObject.CompareTag("Catch")) &&PlayerControllor.Instance.IsInterAct(hit.collider.transform,playerPosition))
        {
            Debug.Log("玩家位置变换");
            MouseClickWithInteract?.Invoke(hit.collider.gameObject.transform,PlayerControllor.Instance.transform);
            //PlayerControllor.Instance.StopSelf();
        }
        //物体变换位置
        if (PlayerControllor.Instance.isE && Input.GetMouseButtonDown(0) && (hit.collider.gameObject.CompareTag("Interact") || hit.collider.gameObject.CompareTag("Catch")) && PlayerControllor.Instance.IsInterAct(hit.collider.transform, playerPosition))
        {
            if (temp == null) {
                temp = hit.collider;
                
                return;
            }
            else if (temp != hit.collider)
            {
                PosionChange(temp, hit.collider);
                //PlayerControllor.Instance.StopSelf();
                Debug.Log("物体位置变换");
                
                temp = null;
            }
            else
            {
                Debug.Log("取消物体位置变换");
                temp = null;
            }
        }
        //Debug.Log("" + Input.GetKey(KeyCode.R)  +" "+ hit.collider.gameObject.CompareTag("Catch") +" "+ PlayerControllor.Instance.IsCatch( hit.collider));
        //抓取;
        if (PlayerControllor.Instance.isR  && hit.collider.gameObject.CompareTag("Catch") && PlayerControllor.Instance.IsCatch( hit.collider))
        {
            Debug.Log("抓取");
            catchT = hit.collider.gameObject.GetComponent<Catch>();
            catchT.isCatch = true;
            timer = 0;
            //StartCoroutine(BeziesCurvor(hit.collider.transform, catchD.transform.position));
        }
        //投掷
        if (PlayerControllor.Instance.isR && Input.GetMouseButtonDown(0) && hit.collider != null && !PlayerControllor.Instance.IsCatch(hit.collider)&&timer>2f)
        {
            Debug.Log("on");
            Vector3 temp = PlayerControllor.Instance.transform.position - hit.point;
            if (temp.magnitude > PlayerControllor.Instance.ThrowDistance_Max) return;
            Debug.Log("投掷");
            catchT.isCatch = false;
            //StartCoroutine(BeziesCurvor(catchT.transform,hit.point));
            move(catchT.transform,hit.point);
            catchT=null;
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
