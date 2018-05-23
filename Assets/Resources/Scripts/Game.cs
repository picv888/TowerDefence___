using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {
    private static Game _instance;
    public static Game Instance {
        get {
            if (_instance == null) {
                _instance = new Game();
            }
            return _instance;
        }
    }
    private Game() { }

    private Transform _towerBaseParent;
    public Transform TowerBaseParent {
        get {
            if (_towerBaseParent == null) {
                string rootStr = "Root";
                GameObject root = GameObject.Find(rootStr);
                if (root == null) {
                    Debug.Log("找不到物体" + rootStr);
                }
                else {
                    string name = "TowerBaseParent";
                    _towerBaseParent = root.transform.Find(name);
                    if (_towerBaseParent == null) {
                        Debug.Log("找不到物体" + name);
                    }
                }
            }
            return _towerBaseParent;
        }
    }
}
