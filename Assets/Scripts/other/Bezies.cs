using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezies : MonoBehaviour
{
   public static Bezies instance;
    //跳跃,投掷高度系数
    [SerializeField] private float Highcoefficient=0.75f;
    //跳跃、投掷时间
    [SerializeField] private float JumpTime=1f;
    private void Awake()
    {
        if(instance != null) Destroy(gameObject);
        instance = this;
    }
    public IEnumerator BeziesCurvor(Transform one,Vector3 two)
    {
        Vector3 temp=new Vector3((one.position.x-two.x)/2,Highcoefficient* (one.position.x - two.x) / 2, (one.position.z - two.z) / 2);
        float timer = 0f;
        while (one.position.y != two.y)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, 1);
            Vector3 v1 = new Vector3(Mathf.Lerp(one.position.x, temp.x, timer), Mathf.Lerp(one.position.y, temp.y, timer), Mathf.Lerp(one.position.z, temp.z, timer));
            Vector3 v2 = new Vector3(Mathf.Lerp( temp.x,two.x, timer), Mathf.Lerp( temp.y,two.y, timer), Mathf.Lerp(temp.z,two.z, timer));
            one.position = v1 + v2;
            yield return null;
        }
    }
}
