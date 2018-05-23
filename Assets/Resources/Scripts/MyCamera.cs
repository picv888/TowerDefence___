using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour {
    float h;
    float v;
    public float speed;
    // Use this for initialization
    void Start() {
        if (speed <= 0f) {
            throw new UnityException("没有设置摄像机速度speed");
        }
    }

    // Update is called once per frame
    void Update() {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector3 position = transform.position;
        position += (new Vector3(0.71f, 0, 0.71f) * v + new Vector3(0.71f, 0, -0.71f) * h).normalized * Time.deltaTime * speed;
        transform.position = position;
    }
}
