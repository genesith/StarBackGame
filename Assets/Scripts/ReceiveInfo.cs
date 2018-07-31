using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveInfo : MonoBehaviour {

    Vector2 newposition;
    public float speed;
    public float walkRange = 0.01f;


	// Use this for initialization
	void Start () {
        newposition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Vector3.Distance(newposition,this.transform.position) > walkRange)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, newposition, speed * Time.deltaTime);
        }

	}

    [PunRPC]
    public void ReceivedMove(Vector2 movePos)
    {
        newposition = movePos;
    }
    [PunRPC]
    public void ReceivedStop()
    {
        newposition = transform.position;
    }

}
