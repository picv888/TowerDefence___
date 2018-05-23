using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConnon : EnemyBase {

    protected override void Start() {
        base.Start();
        speed = 8f;
        health = 100f;
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }
}
