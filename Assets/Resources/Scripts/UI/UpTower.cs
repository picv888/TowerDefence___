using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpTower : MonoBehaviour
{
    public GameObject upTower;
    public GameObject createUpLevel;
    public GameObject destroyTower;
    public Transform upLevel;
    public Transform dTower;
    public Transform uiPos1;
    public Transform uiPos2;
    Camera worldCamera;
    Camera uiCamera;
    // Use this for initialization
    void Start()
    {
        upLevel = Instantiate<GameObject>(createUpLevel).transform;
        //隐藏物体（物体失活）
        upLevel.gameObject.SetActive(false);

        dTower = Instantiate<GameObject>(destroyTower).transform;

        dTower.gameObject.SetActive(false);
        //给点击事件注册委托
        upLevel.GetComponent<UIUpLevel>().click = CreateUpLevelTower;

        dTower.GetComponent<UIUpLevel>().click = DestroyTower;

        worldCamera = Camera.main;
        //根据物体的层获取他所渲染的摄像机
        uiCamera = NGUITools.FindCameraForLayer(upLevel.gameObject.layer);
    }

    // Update is called once per frame
    void Update()
    {
        //首先，把当前物体的坐标转到屏幕坐标系
        Vector3 pos = worldCamera.WorldToScreenPoint(uiPos1.position);
        //去掉z轴
        pos.z = 0;
        //在使用ui的摄像机，将该坐标从屏幕坐标转换到世界坐标
        upLevel.position = uiCamera.ScreenToWorldPoint(pos);



        //首先，把当前物体的坐标转到屏幕坐标系
        pos = worldCamera.WorldToScreenPoint(uiPos2.position);
        //去掉z轴
        pos.z = 0;
        //在使用ui的摄像机，将该坐标从屏幕坐标转换到世界坐标
        dTower.position = uiCamera.ScreenToWorldPoint(pos);
    }

    void CreateUpLevelTower()
    {
        GameObject g = Instantiate(upTower, transform.position, Quaternion.identity);
        g.transform.parent = transform.parent;
        Destroy(gameObject);
        Destroy(upLevel.gameObject);
        Destroy(dTower.gameObject);
    }

    void DestroyTower() {
        Destroy(gameObject);
        Destroy(upLevel.gameObject);
        Destroy(dTower.gameObject);
    }

    public void showTool()
    {
        if (upTower != null) { upLevel.gameObject.SetActive(true); }
        dTower.gameObject.SetActive(true);

    }
    public void hideTool()
    {
        if (upTower != null) { upLevel.gameObject.SetActive(false); }
        dTower.gameObject.SetActive(false);
    }
}
