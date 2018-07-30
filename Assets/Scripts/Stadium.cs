using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stadium : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if(other.tag == "P1")
        {
            Debug.Log("P1 died, P2 wins!");
        }
        if (other.tag == "P2")
        {
            Debug.Log("P2 died, P1 wins!");
        }

    }
}
