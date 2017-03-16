using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour {

    public GameObject enemyCreate;
    void Start() {
        InvokeRepeating("CreateEnemy",5,2);
    }

    void CreateEnemy() {
        GameObject.Instantiate(enemyCreate,this.transform.position,Quaternion.identity);
    }
}
