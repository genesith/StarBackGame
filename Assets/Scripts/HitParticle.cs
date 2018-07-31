﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : Photon.MonoBehaviour {

    [SerializeField]
    int particletype;
    [SerializeField]
    float speed;
    public int owningplayer;
    GameObject OwningObject;

    public Vector2 newposition;
    Vector3 normalizeDirection;
    
    // Use this for initialization
    void Start()
    {
        normalizeDirection = ((Vector3) newposition - this.transform.position).normalized;
    }


    void Update () {
        this.transform.position += normalizeDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "P" + owningplayer.ToString())
            return;
        if (other.tag == "P1" || other.tag == "P2" || other.tag == "P3" || other.tag == "P4")
        { 
            if (other.GetComponent<HeroScript>() != null)
            {
                switch (particletype)
                {
                    case 0: //푸린 기본공격
                        Debug.Log("this case?");
                        other.GetComponent<HeroScript>().DoDamage(10);
                        break;
                    case 1:
                        other.GetComponent<PhotonView>().RPC("GotStunned", PhotonTargets.All,2.0f);
                        break;
                    case 2:
                        other.GetComponent<PhotonView>().RPC("putinvictim", PhotonTargets.All);
                        other.GetComponent<PhotonView>().RPC("GotStunned", PhotonTargets.All, 2.0f);
                        OwningObject.GetComponent<PhotonView>().RPC("putinhimself", PhotonTargets.All);
                        break;
                }

            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Stadium")
        {
            Debug.Log("hit stadium");
            Destroy(gameObject);
        }
    }
    [PunRPC]
    public void ParticleInit(Vector3 movePos, int player, GameObject owner)
    {
        newposition = movePos;
        owningplayer = player;
        OwningObject = owner;
    }

}
