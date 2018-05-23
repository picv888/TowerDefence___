using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour,IPoolReSetable {
    public Transform pathParent;
    public List<Transform> pathNodeList;
    public int currentPathNodeID;
    public float speed;
    public float health;
    public EnemyState state;

    protected virtual void Start() {
        pathNodeList = new List<Transform>();
        currentPathNodeID = 1;
        GameObject path = GameObject.FindWithTag("Path");
        if(path == null) {
            Debug.Log("找不到寻路路径父节点");
        }
        else {
            pathParent = path.transform;
            //初始化路点列表
            //把路点导入到路点列表
            for(int i = 0; i < pathParent.childCount; i++) {
                pathNodeList.Add(pathParent.GetChild(i));
            }
            //把自己放在路点的第一个位置
            transform.position = pathNodeList[0].position;
            if(pathNodeList.Count >= 2) {
                transform.rotation = Quaternion.LookRotation(pathNodeList[1].position - pathNodeList[0].position);
            }
        }

        speed = 5f;
        health = 10f;
        if(health <= 0f) {
            Debug.Log("没有设置生命值");
        }
        state = EnemyState.Move;
    }

    public virtual void ResetFromPool() {
        Start();
    }

    protected virtual void Update() {
        Action();
    }

    public virtual void Action() {
        switch(state) {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Death:
                Death();
                break;
            default:
                break;
        }
    }

    public virtual void Move() {
        //如果怪物还没有到达路点中最后一个点
        if(currentPathNodeID < pathNodeList.Count) {

            transform.position = Vector3.MoveTowards(transform.position, pathNodeList[currentPathNodeID].position, speed * Time.deltaTime);
            Quaternion to = Quaternion.LookRotation((pathNodeList[currentPathNodeID].position - transform.position));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, to, 720f * Time.deltaTime);

            //如果我离下一个点的距离到达某一个值
            float distance = Vector3.Distance(transform.position, pathNodeList[currentPathNodeID].position);
            if(distance <= speed * Time.deltaTime) {
                //改变我的当前点，进而改变目标点，成下一个点
                currentPathNodeID++;
            }
        }
        else {
            Player.player.Damage(1f);
            state = EnemyState.Death;
        }
    }

    public virtual void Idle() {

    }

    public virtual void Damage(float damage) {
        health -= damage;
        if(health <= 0f) {
            state = EnemyState.Death;
        }
    }

    public virtual void Death() {
        Pool.Instance.Destroy(gameObject);
    }
}

public enum EnemyState {
    Idle,
    Move,
    Death
}
