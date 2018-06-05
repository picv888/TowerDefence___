using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGauss : EnemyBase {

    protected override void Awake() {
        base.Awake();
    }

    protected override void OnEnable() {
        base.OnEnable();
        health = 20;
        speed = 5;
    }
}
