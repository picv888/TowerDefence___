using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public GameObject[] enemyPrefabArr;
    public float[] genericTimeInterval;
    public int[] genericIndexArr;
    // Use this for initialization
    void Start() {
        if (genericTimeInterval.Length != genericIndexArr.Length) {
            throw new UnityException("两个数组长度不一样");
        }
        StartCoroutine(GenericEnemy());
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator GenericEnemy() {
        int i = 0;
        do {
            i %= genericIndexArr.Length;
            yield return new WaitForSeconds(genericTimeInterval[i]);
            int index = genericIndexArr[i];
            GameObject prefab = enemyPrefabArr[index];
            if (prefab == null) {
                throw new UnityException("找不到文件");
            }
            //Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
            Pool.Instance.Instantiate(prefab, Vector3.zero);
            i++;
        } while (true);
    }
}
