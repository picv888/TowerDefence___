using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConnon : EnemyBase {

    protected override void Awake() {
        base.Awake();
    }

    protected override void OnEnable() {
        base.OnEnable();
        speed = 8f;
        health = 100f;
    }
}
