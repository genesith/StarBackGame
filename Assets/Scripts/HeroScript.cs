using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroScript : MonoBehaviour {
    HealthScript Health;
    public int maxhealth, currenthealth;
    public float healthregentime; // 이게 필요할까
    
    public float respawntime = 10;

    public int playernum;

    public int CHealth
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
            //Debug.Log("Health now " + currenthealth + "/" + maxhealth);
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
            GameManager.Manager.Die(playernum);
            
        }
    }
    
    [PunRPC]
    public void putinvictim()
    {
        this.transform.position = new Vector3(-3, 0, 0);
    }
    [PunRPC]
    public void putinhimself()
    {
        this.transform.position = new Vector3(3, 0, 0);
    }
    [PunRPC]
    void InitTag(int player)
    {
        this.tag = "P" + player.ToString();
        this.playernum = player;

        GameManager.Manager.InitAddToPlayerList(gameObject, player);
    }

    [PunRPC]
    void GotDamaged(int damage)
    {
        DoDamage(damage);
    }

    
}
