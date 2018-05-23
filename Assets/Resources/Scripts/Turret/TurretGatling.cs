using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGatling : TurretBase {
    public Transform trunkTrans;//可以旋转的身体
    [Range(1f, 360f)] public float attackAngle;

    protected override void Start() {
        base.Start();
        if (attackAngle <= 0f) {
            Debug.Log("没有设置攻击扇形角度");
        }
        damagePoint = 1.5f;
        if (trunkTrans == null) {
            throw new UnityException("找不到trunkTrans");
        }
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    protected override bool CustomAttackJudge() {
        return CheckAttackAngle();
    }

    protected override void CustomAttackJudgeFalse() {
        RotatGun();
    }

    private bool CheckAttackAngle(){
        Vector3 dir = attackTarget.transform.position - trunkTrans.position;
        dir.y = 0;
        float degree = Vector3.Angle(trunkTrans.forward, dir);
        if (degree < attackAngle / 2f) {
            return true;
        }
        return false;
    }

    private void RotatGun() {
        Vector3 dir = attackTarget.transform.position - trunkTrans.position;
        dir.y = 0;
        Quaternion targetRot = Quaternion.LookRotation(dir);
        trunkTrans.rotation = Quaternion.RotateTowards(trunkTrans.rotation, targetRot, 10f);
    }
}
