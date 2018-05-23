using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolReSetable {
    void ResetFromPool();
}

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
    public Dictionary<string, LinkedList<GameObject>> poolDict = new Dictionary<string, LinkedList<GameObject>>();

    private Pool() { }

    public GameObject Instantiate(GameObject go, Vector3 position) {
        GameObject g = Instantiate(go, position, Quaternion.identity);
        return g;
    }
    //生成怪物         
    public GameObject Instantiate(GameObject go, Vector3 position, Quaternion quaternion) {
        string key = "Pool-" + go.name;
        GameObject gameObj;

        if (poolDict.ContainsKey(key) && poolDict[key].Count > 0) {
            gameObj = poolDict[key].First.Value;
            poolDict[key].RemoveFirst();
        }
        else if (poolDict.ContainsKey(key) && poolDict[key].Count == 0) {
            gameObj = Object.Instantiate<GameObject>(go, position, Quaternion.identity);
        }
        else {
            gameObj = Object.Instantiate<GameObject>(go, position, Quaternion.identity);
            LinkedList<GameObject> list = new LinkedList<GameObject>();
            poolDict.Add(key, list);
        }
        gameObj.name = key;
        gameObj.transform.position = position;
        IPoolReSetable r = gameObj.GetComponent<IPoolReSetable>();
        if (r != null) {
            r.ResetFromPool();
        }
        gameObj.SetActive(true);
        return gameObj;
    }

    //放入池子
    public void Destroy(GameObject go) {
        string key = go.name;
        poolDict[key].AddLast(go);
        go.transform.position = new Vector3(9999, 9999, 9999);
        go.SetActive(false);
    }

}
