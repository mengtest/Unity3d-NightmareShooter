using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    private float enemyHp;
    public float damageToPlayer = 10;
    public float attackRate = 1;
    private float timer = 1;
    void Awake() {
        enemyHp = this.GetComponent<Enemy>().hp;
    }
    public void OnTriggerStay(Collider collider) {
        timer += Time.deltaTime;
        if (timer>=1/attackRate) {
            timer -= 1/attackRate;
            if (enemyHp > 0) {
                if (collider.tag == "Player") {
                    collider.GetComponent<PlayerHealthy>().Damage(10);
                }
            }
        }
    }
}
