using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private Animator anim;
    //Move speed
    public float speed = 5f;
    //射线碰撞的物体为地板
    private int layoutIndex = -1;
	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
        //获取地板 ground
        layoutIndex = LayerMask.GetMask("Ground");
	}
	
	// Update is called once per frame
    //使用FixedUpdate确保移动中的帧数相同
	void FixedUpdate () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }
        // 直接移动到某个坐标 忽略碰撞 this.transform.Translate();
        //当前的坐标加上移动的坐标
        this.GetComponent<Rigidbody>().MovePosition(this.transform.position+ new Vector3(h,0,v)*speed*Time.deltaTime);
        //通过鼠标控制主角的面向
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,100,layoutIndex)) {
            //目标位置
            Vector3 target = hit.point;
            target.y = transform.position.y;
            transform.LookAt(target);

        }
    }
}
