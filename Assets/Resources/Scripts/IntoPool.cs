using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoPool : MonoBehaviour {
    public float interval;
    float time;
	// Use this for initialization
	void Start () {

    }

    private void OnDisable() {
        time = 0f;
    }

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        if (time > interval) {
            Pool.Instance.Destroy(gameObject);
        }
    }
}
