using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour {

    private NavMeshAgent agent;
    private Transform player;
    private Animator anim;
    public float hp = 100;
    private bool isDie = false;
    private ParticleSystem hitPs;
    public AudioClip hurtAudio, dieAudio;
    void Awake() {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        hitPs = this.GetComponentInChildren<ParticleSystem>();
    }
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tag.player).transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDie) {
            MoveToPlayer();
        }
        if (this.hp <= 0)
        {
            this.GetComponent<CapsuleCollider>().enabled = false;
            agent.enabled = false;
            transform.Translate(Vector3.down * Time.deltaTime * 0.5f);
        }
    }
    /// <summary>
    /// 追击主角，当两者的距离小于某个值时，停止追击
    /// </summary>
    void MoveToPlayer() {
        if (Vector3.Distance(player.position, this.transform.position) < 1.2f)
        {
            anim.SetBool("IsMove",false);
            agent.Stop();
        }
        else {
            agent.Resume();
            anim.SetBool("IsMove",true);
            agent.SetDestination(player.position);
        }
    }
    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="hitPos">受到伤害的特效</param><
    /// <param name="damage">受到伤害的点</param>
    public void TakeDamage(float damage,Vector3 hitPos) {
        if (hp <= 0) return;
        AudioSource.PlayClipAtPoint(hurtAudio,this.transform.position);
        hp -= damage;
        hitPs.transform.position = hitPos;
        hitPs.Play();
        if (hp<=0) {
            Die();
        }         
        }
        /// <summary>
        /// 死亡
        /// </summary>
    void Die() {
        AudioSource.PlayClipAtPoint(dieAudio,this.transform.position);
        agent.Stop();
        isDie = true;
        anim.SetTrigger("IsDie");
        Invoke("DestroyMyself",4f);
        }
    /// <summary>
    /// 死亡后销毁自身
    /// </summary>
    void DestroyMyself() {
        GameObject.Destroy(this.gameObject);
    }
    
     
    
}
