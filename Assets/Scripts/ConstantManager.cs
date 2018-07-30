using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantManager : MonoBehaviour {

    static ConstantManager Manager;

    void Awake()
    {
        if (Manager == null)
        {
            DontDestroyOnLoad(gameObject);
            Manager = this;
        }
        else if (Manager != this)
        {
            Destroy(gameObject);
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
