using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour {
    public EnemyBase attackTarget;
    float speed;
    public float damagePoint;//伤害值
    public GameObject bombEffectPrefab;

    void Start () {
        speed = 13f;
        damagePoint = 1f;
	}

    protected virtual void OnEnable() {
        
    }

    // Update is called once per frame
    void Update () {
        if(attackTarget.state == EnemyState.Death) {
            Pool.Instance.Destroy(gameObject);
        }
        else {
            Move();
        }
	}

    private void Move() {
        transform.LookAt(attackTarget.transform);
        float distanceDelta = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, attackTarget.transform.position, distanceDelta);
        if(Vector3.Distance(transform.position, attackTarget.transform.position) < 1f) {
            GameObject bomb = Pool.Instance.Instantiate(bombEffectPrefab, transform.position);
            ParticleSystem ps = bomb.GetComponent<ParticleSystem>();
            if(ps != null) {
                ps.Play();
            }
            Bomb();
            Pool.Instance.Destroy(gameObject);
        }
    }

    //对敌人造成伤害
    protected virtual void Bomb(){
        attackTarget.Damage(damagePoint);
    }
}
