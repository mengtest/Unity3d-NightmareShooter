using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthy : MonoBehaviour {

    //总的血量
    private int hp = 100;
    //死亡动画播放
    private Animator anim;
    //当主角死亡时禁用移动
    private PlayerMove playerMove;
    //受到伤害时，主角变色
    private SkinnedMeshRenderer skin;
    //变色速度
    private float colorSpeed = 1f;
    //受到伤害的音效
    public AudioClip hurtAudio,dieAudio;
	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
        playerMove = this.GetComponent<PlayerMove>();
        // skin  = transform.Find("Player").renderer as SkinnedMeshRenderer;
        skin = this.transform.Find("Player").GetComponent<SkinnedMeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        skin.material.color = Color.Lerp(skin.material.color,Color.white, colorSpeed*Time.deltaTime);
	}
    /// <summary>
    /// 受到受害
    /// </summary>
    public void Damage(int damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        AudioSource.PlayClipAtPoint(hurtAudio,this.transform.position);
        if (hp <= 0) {
            Die();
        }
        skin.material.color = Color.red;
    }
    
    /// <summary>
    /// 死亡方法
    /// </summary>
    void Die() {
        AudioSource.PlayClipAtPoint(dieAudio,this.transform.position);
        anim.SetTrigger("Die");
        //禁用脚本
        playerMove.enabled = false;
        this.GetComponentInChildren<PlayerShoot>().enabled = false;
    }
}
