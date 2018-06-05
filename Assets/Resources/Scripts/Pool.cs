using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool {

    public static Pool _instance;
    public static Pool Instance {
        get {
            if (_instance == null) {
                _instance = new Pool();
            }
            return _instance;
        }
    }
    public Dictionary<string, List<GameObject>> poolDict = new Dictionary<string, List<GameObject>>();

    private Pool() { }

    public GameObject Instantiate(GameObject go, Vector3 position) {
        GameObject g = Instantiate(go, position, Quaternion.identity);
        return g;
    }
    //生成怪物         
    public GameObject Instantiate(GameObject go, Vector3 position, Quaternion quaternion) {
        string key = "Pool-" + go.name;
        GameObject gameObj;
        bool isContainsKey = poolDict.ContainsKey(key);
        if (isContainsKey) {
            List<GameObject> list = poolDict[key];
            if (list.Count > 0) {
                int lastIndex = list.Count - 1;
                gameObj = list[lastIndex];
                gameObj.transform.position = position;
                gameObj.transform.rotation = quaternion;
                list.RemoveAt(lastIndex);
            }
            else {
                gameObj = Object.Instantiate<GameObject>(go, position, quaternion);
            }
        }
        else {
            gameObj = Object.Instantiate<GameObject>(go, position, quaternion);
            poolDict.Add(key, new List<GameObject>());
        }
        gameObj.name = key;
        gameObj.SetActive(true);
        return gameObj;
    }

    //放入池子
    public void Destroy(GameObject go) {
        string key = go.name;
        poolDict[key].Add(go);
        go.transform.position = new Vector3(0, 6, 0f);
        go.SetActive(false);
    }
}
