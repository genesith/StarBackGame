using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    Image Fillbox;

	// Use this for initialization
	void Start () {
        Fillbox = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	public void UpdateHealth(float health)
    {
        Fillbox.fillAmount = health;
    }
}
