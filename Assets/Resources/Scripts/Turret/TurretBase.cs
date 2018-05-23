using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretBase : MonoBehaviour, IPoolReSetable {
    public float attackRadius;
    protected float damagePoint;
    protected float fireCD;
    protected float currentFireCD;
    public Transform gunPos;
    public EnemyBase attackTarget;
    public GameObject bulletPrefab;
    public ParticleSystem muzzleEffect;
    public LayerMask attackLayer;//可以攻击哪些层
    public AudioSource shootAudioSource;

    protected virtual void Start() {
        attackRadius = 8f;
        damagePoint = 1f;
        fireCD = 0.1f;
        if (attackRadius <= 0f) {
            Debug.Log("没有设置 attackRadius");
        }
        if (fireCD <= 0f) {
            Debug.Log("没有设置 fireCD");
        }
        if (gunPos == null) {
            throw new UnityException("没有设置 gunPos");
        }
        if (muzzleEffect == null) {
            Debug.Log("没有设置 muzzleEffect");
        }
        if (attackLayer == 0) {
            Debug.Log("没有设置 LayerMask");
        }
        if (shootAudioSource == null) {
            Debug.Log("没有设置 AudioSource");
        }
        muzzleEffect = GetComponentInChildren<ParticleSystem>();
    }

    public void ResetFromPool(){
        Start();
    }

    // Update is called once per frame
    protected virtual void Update() {
        Attack();
        currentFireCD -= Time.deltaTime;
    }

    private bool IsContain(Collider[] colliders, Collider co) {
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i] == co) {
                return true;
            }
        }
        return false;
    }

    private void Attack() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius, attackLayer);
        if (attackTarget != null) {
            Collider co = attackTarget.GetComponent<Collider>();
            if (attackTarget.state == EnemyState.Death ||
                co == null ||
                colliders.Length <= 0 ||
                !IsContain(colliders, co)) {
                attackTarget = null;
            }
        }

        if (attackTarget == null) {
            EnemyBase monster = GetOneMonsterInRadius(colliders);
            attackTarget = monster;
        }

        if (attackTarget != null) {
            if (CustomAttackJudge()) {
                if (currentFireCD <= 0f) {
                    //枪口特效
                    if (muzzleEffect != null) {
                        muzzleEffect.Play();
                    }

                    //开枪声音
                    if (shootAudioSource != null) {
                        shootAudioSource.Play();
                    }

                    //生成子弹
                    GameObject bullet = Createbullet();
                    bullet.transform.position = gunPos.position;
                    BulletBase bulletBase = bullet.GetComponent<BulletBase>();
                    if (bulletBase == null) {
                        bulletBase = bullet.AddComponent<BulletBase>();
                    }
                    bulletBase.attackTarget = attackTarget;
                    bulletBase.damagePoint = damagePoint;
                    currentFireCD = fireCD;
                }
            }
            else {
                CustomAttackJudgeFalse();
            }
        }
    }

    /// <summary>
    /// 自定义攻击判断，返回是否可以发动攻击。子类可以重写该方法，实现如：在攻击扇形范围内才可以发动攻击等功能
    /// </summary>
    protected virtual bool CustomAttackJudge() {
        return true;
    }

    /// <summary>
    /// 自定义攻击判断为false时执行的方法。
    /// </summary>
    protected virtual void CustomAttackJudgeFalse() {

    }

    /// <summary>
    /// 获取攻击半径内5个敌人层的Collider, 获取MonsterBase脚本，如果其中一个MonsterBase的state不是Death，则返回这个MonsterBase
    /// 可以在子类中重写改方法，实现如：获取血量最少的、血最多的、怪最密集的敌人等功能
    /// </summary>
    protected virtual EnemyBase GetOneMonsterInRadius(Collider[] colliders) {
        for (int i = 0; i < colliders.Length; i++) {
            Collider l_collider = colliders[i];
            EnemyBase enemy = l_collider.GetComponentInParent<EnemyBase>();
            if (enemy != null && enemy.state != EnemyState.Death) {
                return enemy;
            }
        }
        return null;
    }

    protected virtual GameObject Createbullet() {
        return Pool.Instance.Instantiate(bulletPrefab, gunPos.position);
    }

    protected virtual void DidFire() {
        if (muzzleEffect != null) {
            muzzleEffect.Play();
        }
    }

    protected virtual bool JudgeAttack() {
        Vector3 monsterDir = attackTarget.transform.position - gunPos.position;
        monsterDir.y = 0;
        if (Vector3.Angle(gunPos.forward, monsterDir) < 5) {
            return true;
        }
        return false;
    }
}
