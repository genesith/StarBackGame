using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour {
    String displaythis;
    [SerializeField]
    Text displayhere;
    // Use this for initialization
    void Start() {
        displaythis = ConstantManager.Manager.endmsg;
        
        displayhere.text = displaythis;

        PhotonNetwork.Disconnect();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 30, 100, 30), "돌아가기"))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }



    
}
