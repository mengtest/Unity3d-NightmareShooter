using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPlayer : MonoBehaviour {
    //Move speed of camera
    public float smoothing = 3f;
    private Transform player;
	// Use this for initialization
	void Start () {
         player = GameObject.FindGameObjectWithTag(Tag.player).transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = player.position + new Vector3(0.6f,4f,-7.5f);
        this.transform.position = Vector3.Lerp(this.transform.position,targetPos,smoothing*Time.deltaTime);
	}
}
