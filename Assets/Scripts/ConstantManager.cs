using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantManager : MonoBehaviour {

    public static ConstantManager Manager;
    public int[] CharacterList;
    public string endmsg;

    // CHANGE WHEN MORE PLAYERS ARE ALLOWED;
    public int numplayers = 2;

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
