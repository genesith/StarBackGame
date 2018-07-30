using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour {

    [SerializeField]
    int particletype;
    [SerializeField]
    float speed;
    public int owningplayer;

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
                other.GetComponent<HeroScript>().DoDamage(10);
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
    public void ParticleInit(Vector3 movePos, int player)
    {
        newposition = movePos;
        owningplayer = player;
    }
}
