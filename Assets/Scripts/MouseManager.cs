using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;
using System;
using System.Numerics;
using Unity.VisualScripting;
//[System.Serializable]//未继承monobehavior的类不显示,要序列化
//public class EventV3:UnityEvent<Vector3> { }
public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;

    public event Action<UnityEngine.Vector3> MouseClick;
    public event Action<Transform,Transform> MouseClickWithInteract;
    public event Action DotOpen;
    public Transform playerPosition;
    //public event Action<Transform, Transform> MouseClickWithInteractOnly;


    //存储射线撞到的物体信息
    RaycastHit hit;
    Collider temp;


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
        if (Input.GetMouseButtonDown(0) && hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Ground")|| hit.collider.gameObject.CompareTag("NoMovableInt"))
            {
                Debug.Log("移动");
                MouseClick?.Invoke(hit.point);

            }
            //按钮交互
            if (hit.collider.gameObject.CompareTag("NoMovableIntForever"))
            {
                Debug.Log("尝试启动按钮");
                DotOpen?.Invoke();
            }
        }
        //自身与物体变换位置
        if (Input.GetKey(KeyCode.Q) && Input.GetMouseButtonDown(0) && hit.collider.gameObject.CompareTag("Interact")&&PlayerControllor.Instance.IsInterAct(hit.collider.transform,playerPosition))
        {
            Debug.Log("玩家位置变换");
            MouseClickWithInteract?.Invoke(hit.collider.gameObject.transform,PlayerControllor.Instance.transform);
            PlayerControllor.Instance.StopSelf();
        }
        //物体变换位置
        if (Input.GetKey(KeyCode.E) && Input.GetMouseButtonDown(0) && hit.collider.gameObject.CompareTag("Interact")&& PlayerControllor.Instance.IsInterAct(hit.collider.transform, playerPosition))
        {
            if (temp == null) {
                temp = hit.collider;
                
                return;
            }
            else if (temp != hit.collider)
            {
                PosionChange(temp, hit.collider);
                PlayerControllor.Instance.StopSelf();
                Debug.Log("物体位置变换");
                
                temp = null;
            }
            else
            {
                Debug.Log("取消物体位置变换");
                temp = null;
            }
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
