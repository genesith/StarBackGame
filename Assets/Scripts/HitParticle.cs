using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : Photon.MonoBehaviour
{

    [SerializeField]
    int particletype;
    [SerializeField]
    float speed;

    public int owningplayer;

    [SerializeField]
    bool shortrange;

    [SerializeField]
    float lifetime;
    

    public Vector2 newposition;
    Vector3 normalizeDirection;

    // Use this for initialization
    void Start()
    {
        normalizeDirection = ((Vector3)newposition - this.transform.position).normalized;
    }


    void Update()
    {
        if (shortrange)
        {
            lifetime -= Time.deltaTime;
            if (lifetime < 0)
            {
                Destroy(this);
            }
        }
        this.transform.position += normalizeDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "P" + owningplayer.ToString())
            return;
        if (other.tag == "P1" || other.tag == "P2" || other.tag == "P3" || other.tag == "P4")
        {
            bool DestroyOnHit = true;
            if (other.GetComponent<HeroScript>() != null && (GameManager.Manager.playernum == owningplayer))
        
            {
                switch (particletype)
                {
                    case 0: //푸린 기본공격
                        other.GetComponent<PhotonView>().RPC("GotDamaged", PhotonTargets.All, 10, owningplayer);
                        break;
                    case 1: //푸린 스턴
                        other.GetComponent<PhotonView>().RPC("GotStunned", PhotonTargets.All, 2.0f);
                        DestroyOnHit = false;
                        break;
                    case 2: //푸틴 티타임
                        other.GetComponent<PhotonView>().RPC("putinvictim", PhotonTargets.All);
                        other.GetComponent<PhotonView>().RPC("GotStunned", PhotonTargets.All, 2.0f);
                        GameManager.PlayerList[owningplayer-1].PlayerObject.GetComponent<PhotonView>().RPC("putinhimself", PhotonTargets.All);
                        break;
                }
                if (DestroyOnHit)
                {
                    gameObject.GetComponent<PhotonView>().RPC("DestroyRPC", PhotonTargets.All);
                    Destroy(gameObject);
                }
            }
            
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
    public void ParticleInit(Vector3 movePos, int player)
    {
        newposition = movePos;
        owningplayer = player;
        // OwningphotonView = p;
        //  OwningObject = owner;
    }
    [PunRPC]
    public void DestroyRPC()
    {
        Destroy(gameObject);
    }

}