using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    //射击速率
    public float shootRate = 2;
    //计时器
    private float timer = 0;
    //射击效果
    private Light shootLight;
    private ParticleSystem ps;
    private LineRenderer lineRenderer;
	// Use this for initialization
	void Start () {
        shootLight = this.GetComponent<Light>();
        ps = this.GetComponentInChildren<ParticleSystem>();
        lineRenderer = this.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        
        timer += Time.deltaTime;
        //一个周期进行设计
        if (timer>= 1/shootRate) {
            timer -= 1 / shootRate;
            Shoot();
        }
	}
    /// <summary>
    /// 射击
    /// </summary>
    void Shoot() {
        //射线判断
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0,this.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            lineRenderer.SetPosition(1,hit.point);
            //判断被射击对象是否为敌人
            if (hit.collider.tag == Tag.enemy) {
                hit.collider.GetComponent<Enemy>().TakeDamage(30,hit.point);
            }
        }
        else {
            lineRenderer.SetPosition(1,this.transform.position + this.transform.forward * 100);
        }
        this.GetComponent<AudioSource>().Play();
        shootLight.enabled = true;
        ps.Play();
        Invoke("ShutDown",0.1f);
    }
    /// <summary>
    /// 关闭射击特效
    /// </summary>
    void ShutDown() {
        lineRenderer.enabled = false;
        ps.Clear();
        shootLight.enabled = false;

    }
}
