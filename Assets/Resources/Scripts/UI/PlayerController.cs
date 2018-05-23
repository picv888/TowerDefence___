using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    GameObject createTower;
    UpTower ut;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (UICamera.hoveredObject != null) return;
                if (ut != null)
                {
                    ut.hideTool();
                }
                ut = hit.transform.GetComponent<UpTower>();//获取点击的物体的升级的脚本
                if (ut != null)//如果有升级的脚本说明，可以升级，那就显示他
                {
                    ut.showTool();
                }

            }
        }

    }
}
