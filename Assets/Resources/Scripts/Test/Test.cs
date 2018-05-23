using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public GameObject go;
    bool active;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject g = Pool.Instance.Instantiate(go, Vector3.zero);
            g.transform.position = new Vector3(5, 7, 5);    
        }
	}
}
