using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGauss : EnemyBase {

    protected override void Start () {
        base.Start();
        speed = 10f;
        health = 50f;
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }
}
