using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigglypuffScript : Photon.MonoBehaviour {

    string ability1 = "zucc";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("q"))
        {
            Debug.Log("Q pressed");
            GameObject QParticle = PhotonNetwork.Instantiate(ability1, transform.position, transform.rotation, 0);
            QParticle.GetComponent<PhotonView>().RPC("ParticleInit", PhotonTargets.All, Camera.main.ScreenToWorldPoint(Input.mousePosition), PhotonNetwork.player.ID);
            //myPlayer.GetComponent<HitParticle>().newposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //myPlayer.GetComponent<HitParticle>().owningplayer = PhotonNetwork.player.ID;
        }
    }
    [PunRPC]
    void Tagger(string text)
    {
        this.tag = text;
    }
}
