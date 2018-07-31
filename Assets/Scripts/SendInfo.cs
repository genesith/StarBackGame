﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendInfo : Photon.MonoBehaviour {

    bool stun = false;
    float stunfreetime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        bool RMB = Input.GetMouseButtonDown(1);
        if (stun)
        {
            if (Time.time > stunfreetime)
                stun = false;
            else
                return;
        }
        if (RMB)
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.GetComponent<PhotonView>().RPC("ReceivedMove", PhotonTargets.All, target);
        }
        
        
	}
    [PunRPC]
    public void GotStunned(float time)
    {
        stun = true;
        stunfreetime = Time.time + time;
        this.GetComponent<PhotonView>().RPC("ReceivedStop", PhotonTargets.All);

    }
}
