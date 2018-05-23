using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreateButton : MonoBehaviour
{
    //准备生成的物体
    [Tooltip("准备生成的物体")]
    public GameObject readyTower;
    //当前物体
    [Tooltip("当前物体")]
    public GameObject currentReadyTower;
    public GameObject towerBase;
    // Use this for initialization
    void Start()
    {
        towerBase = Game.Instance.TowerBaseParent.gameObject;
        UIEventListener.Get(gameObject).onClick = Click;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Click(GameObject g)
    {
        if (currentReadyTower == null)//如果你有一个需要创建的物体，那么久不让你生成了
        {
            towerBase.SetActive(true);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //将需要创建的物体设置为当前的物体
                currentReadyTower = Instantiate<GameObject>(readyTower, hit.point, Quaternion.identity);
            }
        }
    }
}
