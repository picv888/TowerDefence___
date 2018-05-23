using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player player;
    public float health;
    void Awake() {
        player = this;
    }

    void Start() {
        health = 5f;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Damage(float damage) {
        health -= damage;
    }
}
