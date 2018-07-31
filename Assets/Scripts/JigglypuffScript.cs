using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigglypuffScript : Photon.MonoBehaviour {

    string ability1 = "zucc";
    string ability2 = "stunpurin";
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("q"))
        {
            Debug.Log("Q pressed");
            GameObject QParticle = PhotonNetwork.Instantiate(ability1, transform.position, transform.rotation, 0);
            QParticle.GetComponent<PhotonView>().RPC("ParticleInit", PhotonTargets.All, Camera.main.ScreenToWorldPoint(Input.mousePosition), PhotonNetwork.player.ID, gameObject);
            //myPlayer.GetComponent<HitParticle>().newposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //myPlayer.GetComponent<HitParticle>().owningplayer = PhotonNetwork.player.ID;
        }
        if (Input.GetKeyDown("w"))
        {
            Debug.Log("W pressed");
            GameObject WParticle = PhotonNetwork.Instantiate(ability2, transform.position, transform.rotation, 0);
            WParticle.GetComponent<PhotonView>().RPC("ParticleInit", PhotonTargets.All, Camera.main.ScreenToWorldPoint(Input.mousePosition), PhotonNetwork.player.ID, gameObject);
            //myPlayer.GetComponent<HitParticle>().newposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //myPlayer.GetComponent<HitParticle>().owningplayer = PhotonNetwork.player.ID;
        }
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("E pressed");
            //while () { if () }
            //모든 캐릭터들의 위치를 변수로 받아서 키다운이 된 시점에 본인과 일정 거리 안에 있는 모든 개체에 대해 광역 스턴을 잠시 걸고 마우스방향을 반대로 설정한다.
            GameObject WParticle = PhotonNetwork.Instantiate(ability2, transform.position, transform.rotation, 0);
            WParticle.GetComponent<PhotonView>().RPC("ParticleInit", PhotonTargets.All, Camera.main.ScreenToWorldPoint(Input.mousePosition), PhotonNetwork.player.ID, gameObject);
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
