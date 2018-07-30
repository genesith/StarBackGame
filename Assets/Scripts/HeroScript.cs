using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroScript : MonoBehaviour {
    HealthScript Health;
    public int maxhealth, currenthealth;
    public float healthregentime; // 이게 필요할까

    private int CHealth
    {
        get
        {
            return currenthealth;
        }

        set
        {
            currenthealth = value;
            float ratio = (float) currenthealth / maxhealth;
            Health.UpdateHealth(ratio);
            Debug.Log("Health now " + currenthealth + "/" + maxhealth);
        }
    }



	// Use this for initialization
	void Start () {
        
        Health = GetComponentInChildren<HealthScript>();
        currenthealth = maxhealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoDamage(int damage)
    {
        if (CHealth > damage)
        {
            CHealth -= damage;
        }
        else
        {
            CHealth = 0;
            gameObject.GetComponent<SendInfo>().enabled = false;
            Destroy(gameObject);
        }
    }
}
