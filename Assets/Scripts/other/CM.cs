using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCameraBase[] cm=new CinemachineVirtualCameraBase[4];
    [SerializeField]
    CinemachineVirtualCameraBase m_Camera;
    [SerializeField] int pos_index = 0;
    private void Start()
    {
        if (cm[0])
        {
            cm[0].MoveToTopOfPrioritySubqueue();
        }
    }
    public void ChangePos()
    {
        cm[(++pos_index)%4].MoveToTopOfPrioritySubqueue();
    }
    public void MView()
    {
        if(m_Camera != null)
        {
            m_Camera.MoveToTopOfPrioritySubqueue();
        }
    }
    public void ReturnPlayer()
    {
        cm[pos_index].MoveToTopOfPrioritySubqueue();
    }
}
