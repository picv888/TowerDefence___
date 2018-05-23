using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void VoidNotDelegate();
public class UIUpLevel : MonoBehaviour {
    public VoidNotDelegate click;
    // Use this for initialization
    void Start () {
        //绑定点击事件
        UIEventListener.Get(gameObject).onClick = Click;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="g"></param>
    void Click(GameObject g) {
        if (click != null) {
            click();
        }
    }
}
