using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player inputs;
    /// <summary>
    /// 存放变量
    /// </summary>
    public bool Jump => inputs.GamePlay.Jump.WasPressedThisFrame();//表示是否在这一帧按下jump绑定按键
    public bool stopJump => inputs.GamePlay.Jump.WasReleasedThisFrame();//表示是否在这一帧松开jump绑定按键


    public bool onQ => inputs.GamePlay.Q_exchangeSelf.WasPressedThisFrame();//表示是否在这一帧按下Q绑定按键
    public bool deQ => inputs.GamePlay.Q_exchangeSelf.WasReleasedThisFrame();
    public bool onE => inputs.GamePlay.E_exchange.WasPressedThisFrame();//表示是否在这一帧按下E绑定按键
    public bool deE => inputs.GamePlay.E_exchange.WasReleasedThisFrame();
    public bool onR => inputs.GamePlay.R_Throw.WasPressedThisFrame();//表示是否在这一帧按下R绑定按键
    public bool deR => inputs.GamePlay.R_Throw.WasReleasedThisFrame();

    public bool Move => axes.x != 0f;
    public Vector2 axes => inputs.GamePlay.Move.ReadValue<Vector2>();//读取轴的输入

    private void Awake()
    {
        inputs = new Player();
    }
    public void EnableGamePlay()
    {
        inputs.GamePlay.Enable();
        Cursor.lockState = CursorLockMode.None;
    }
}
