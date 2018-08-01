using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigglypuffScript : Photon.MonoBehaviour {

    string ability1 = "zucc";
    string ability2 = "stunpurin";

    //쿨다운 구현 대충 해봄

    bool stun = false;
    float stunfreetime;


    // Update is called once per frame
    void Update () {
        if (stun)
        {
            if (Time.time > stunfreetime)
                stun = false;
            else
                return;
        }
        if (Input.GetKeyDown("q") && CDManager.Instance.SkillAvailable(0))
        {
            CDManager.Instance.PutOnCD(0);
            //Debug.Log("Q pressed");
            GameObject QParticle = PhotonNetwork.Instantiate(ability1, transform.position, transform.rotation, 0);
            QParticle.GetComponent<PhotonView>().RPC("ParticleInit", PhotonTargets.All, Camera.main.ScreenToWorldPoint(Input.mousePosition), PhotonNetwork.player.ID);
            //myPlayer.GetComponent<HitParticle>().newposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //myPlayer.GetComponent<HitParticle>().owningplayer = PhotonNetwork.player.ID;
        }
        if (Input.GetKeyDown("w") && CDManager.Instance.SkillAvailable(1))
        {
            CDManager.Instance.PutOnCD(1);
            //Debug.Log("W pressed");
            GameObject WParticle = PhotonNetwork.Instantiate(ability2, transform.position, transform.rotation, 0);
            WParticle.GetComponent<PhotonView>().RPC("ParticleInit", PhotonTargets.All, Camera.main.ScreenToWorldPoint(Input.mousePosition), PhotonNetwork.player.ID);
            //myPlayer.GetComponent<HitParticle>().newposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //myPlayer.GetComponent<HitParticle>().owningplayer = PhotonNetwork.player.ID;
        }
        if (Input.GetKeyDown("e") && CDManager.Instance.SkillAvailable(2))
        {
            CDManager.Instance.PutOnCD(2);
            //Debug.Log("E pressed");
            //while () { if () }
            //모든 캐릭터들의 위치를 변수로 받아서 키다운이 된 시점에 본인과 일정 거리 안에 있는 모든 개체에 대해 광역 스턴을 잠시 걸고 마우스방향을 반대로 설정한다.
            // 우진 " 그럴 필요 없고 이것도 푸린 위치에서 동그라미에 만들어놓고 그거랑 collide 하는거로 하면 될듯"
            GameObject WParticle = PhotonNetwork.Instantiate(ability2, transform.position, transform.rotation, 0);
            WParticle.GetComponent<PhotonView>().RPC("ParticleInit", PhotonTargets.All, Camera.main.ScreenToWorldPoint(Input.mousePosition), PhotonNetwork.player.ID);
            //myPlayer.GetComponent<HitParticle>().newposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //myPlayer.GetComponent<HitParticle>().owningplayer = PhotonNetwork.player.ID;
        }

    }

    [PunRPC]
    public void GotStunned(float time)
    {
        stun = true;
        stunfreetime = Time.time + time;
        gameObject.GetComponent<SendInfo>().GotStunnedForInfo(stunfreetime);
    }

}
