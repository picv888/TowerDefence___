using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour {
    private GameObject pathPrefab;
    public Transform pathParent;
    public List<Transform> pathNodeList;
    public int currentPathNodeID;
    public float speed;
    public float health;
    public EnemyState state;

    protected virtual void Awake() {
        pathNodeList = new List<Transform>();
        GameObject path = GameObject.Find("Path12312313123123");
        if (path == null) {
            //-34.59 1.5 -3.87
            pathPrefab = Resources.Load<GameObject>("Prefabs/Path-1");
            path = Instantiate<GameObject>(pathPrefab, new Vector3(-34.59f, 1.5f, -3.87f), Quaternion.identity);
            path.name = "Path12312313123123";
        }
        pathParent = path.transform;
        //初始化路点列表
        //把路点导入到路点列表
        for (int i = 0; i < pathParent.childCount; i++) {
            pathNodeList.Add(pathParent.GetChild(i));
        }
        //把自己放在路点的第一个位置
        transform.position = pathNodeList[0].position;
        transform.rotation = pathNodeList[0].rotation;
    }

    protected virtual void OnEnable() {
        //把自己放在路点的第一个位置
        transform.position = pathNodeList[0].position;
        transform.rotation = pathNodeList[0].rotation;
        currentPathNodeID = 1;
        health = 10f;
        speed = 5f;
        state = EnemyState.Move;
    }

    protected virtual void Update() {
        Action();
    }

    public virtual void Action() {
        switch (state) {
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
        if (currentPathNodeID < pathNodeList.Count) {

            transform.position = Vector3.MoveTowards(transform.position, pathNodeList[currentPathNodeID].position, speed * Time.deltaTime);
            Quaternion to = Quaternion.LookRotation((pathNodeList[currentPathNodeID].position - transform.position));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, to, 720f * Time.deltaTime);

            //如果我离下一个点的距离到达某一个值
            float distance = Vector3.Distance(transform.position, pathNodeList[currentPathNodeID].position);
            if (distance <= speed * Time.deltaTime) {
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
        if (health <= 0f) {
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
