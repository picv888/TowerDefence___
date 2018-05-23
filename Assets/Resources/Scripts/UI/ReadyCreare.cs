using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCreare : MonoBehaviour {
    MeshRenderer[] mr;

    Material[] m;
    public GameObject towerBase;
    public GameObject userTower;
    // Use this for initialization
    void Start() {
        towerBase = Game.Instance.TowerBaseParent.gameObject;
        mr = transform.GetComponentsInChildren<MeshRenderer>();//获取所有子对象的对应组件，包括自己
        m = new Material[mr.Length];
        for (int i = 0; i < mr.Length; i++) {
            m[i] = mr[i].materials[0];
        }
    }

    // Update is called once per frame
    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue)) {
            transform.position = hit.point;
        }

        RaycastHit hit2;
        LayerMask mask = LayerMask.GetMask("TowerBases");
        if (Physics.Raycast(ray, out hit2, float.MaxValue, mask)) {
            if (!hit2.transform.GetComponent<TowerBase>().isBuild) {
                for (int i = 0; i < m.Length; i++) {
                    m[i].SetColor("_Color", Color.green);
                }
                if (Input.GetMouseButtonDown(0)) {
                    GameObject g = Instantiate<GameObject>(userTower, hit2.transform.position, Quaternion.identity);
                    hit2.transform.GetComponent<TowerBase>().isBuild = true;
                    towerBase.SetActive(false);
                    gameObject.SetActive(false);
                    Destroy(gameObject);
                }
            }
            else {
                for (int i = 0; i < m.Length; i++) {
                    m[i].SetColor("_Color", Color.red);
                }
            }
        }
    }
}
