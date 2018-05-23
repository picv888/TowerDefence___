using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public Transform destinationTransform;
    Vector3 oldPosition;
    Vector3 dirHorizontal;

    float sHorizontal;
    float sVertical;
    float tSum;
    float aVertical;
    float t;

    float speed = 3.0f;
    public float Speed {
        set {
            if(value != 0f) {
                speed = value;
            }
            else {
                speed = 0.0001f;
            }
        }
        get {
            return speed;
        }
    }


	// Use this for initialization
	void Start () {
        oldPosition = transform.position;
        dirHorizontal = destinationTransform.position - oldPosition;
        dirHorizontal.y = 0;
        dirHorizontal = dirHorizontal.normalized;
        sHorizontal = Vector3.Distance(new Vector3(oldPosition.x, destinationTransform.position.y, oldPosition.z), destinationTransform.position);
        sVertical = oldPosition.y - destinationTransform.position.y;
        tSum = sHorizontal / Speed;
        aVertical = 2 * sVertical / tSum / tSum;
    }
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        transform.position = oldPosition + dirHorizontal * Speed * t + Vector3.down * 0.5f * aVertical * t * t;
	}
}
