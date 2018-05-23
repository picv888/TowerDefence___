using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDistance : MonoBehaviour {
    public float distanceAtoB;
    public Transform A;
    public Transform B;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        distanceAtoB = Vector3.Distance(A.position, B.position);
	}
}
