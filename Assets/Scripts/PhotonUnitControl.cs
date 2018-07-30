using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonUnitControl : MonoBehaviour {
    Vector3 target;
    bool startmoving = false;

    private float speed = 4.5f;
	// Use this for initialization
	void Start () {
        target = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        

        if (Input.GetMouseButtonDown(1))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }
}
