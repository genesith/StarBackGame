using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool RMB = Input.GetMouseButtonDown(1);

        if (RMB)
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.GetComponent<PhotonView>().RPC("ReceivedMove", PhotonTargets.All, target);
        }
        
        
	}
}
