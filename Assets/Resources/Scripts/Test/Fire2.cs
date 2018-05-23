using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire2 : MonoBehaviour {
    public Transform destinationTransform;
    Vector3 oldPosition;
    Vector3 dirHorizontal;

    float sHorizontal;
    float sVertical;
    float tSum;
    float aVertical;
    float t;

    float speed;

	// Use this for initialization
	void Start () {
        oldPosition = transform.position;
        dirHorizontal = destinationTransform.position - oldPosition;
        dirHorizontal.y = 0;
        dirHorizontal = dirHorizontal.normalized;
        sHorizontal = Vector3.Distance(new Vector3(oldPosition.x, destinationTransform.position.y, oldPosition.z), destinationTransform.position);
        sVertical = oldPosition.y - destinationTransform.position.y;
        aVertical = 9.8f;
        tSum = Mathf.Sqrt(2* sVertical / aVertical);
        speed = sHorizontal / tSum;
    }
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        Vector3 to = oldPosition + dirHorizontal * speed * t + Vector3.down * 0.5f * aVertical * t * t;
        Debug.DrawLine(transform.position, to, Color.blue, float.MaxValue);
        transform.position = to;
	}
}
