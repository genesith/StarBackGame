using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.MonoBehaviour {

    public static GameManager Manager;
    [SerializeField]
    public GameObject[] Spawns;

    [SerializeField]
    GameObject[] Canvases;
    GameObject Canvas;

    public int playernum;

    static PlayerClass[] PlayerList;

    class PlayerClass
    {
        public GameObject PlayerObject;
        public int kills, deaths, points;
        public string name; //maybe

    }

	// Use this for initialization
	void Start () {
        StartCoroutine(DelayedSpawn());
        Manager = this;
        playernum = PhotonNetwork.player.ID;
        PlayerList = new PlayerClass[ConstantManager.Manager.numplayers];
    }
	
    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("GameManager Start " + PhotonNetwork.player.ID);
        SelectCharacter(ConstantManager.Manager.CharacterList[PhotonNetwork.player.ID - 1]);
    }
	// Update is called once per frame
	void Update () {
        
    }

    public void Die(int playernum)
    {
        PlayerList[playernum - 1].deaths += 1;
        StartCoroutine(DieRoutine(playernum));
    }

    IEnumerator DieRoutine(int playernum)
    {
        GameObject DeadDude = PlayerList[playernum - 1].PlayerObject;
        HeroScript killme = DeadDude.GetComponent<HeroScript>();
        DeadDude.SetActive(false);
        yield return new WaitForSeconds(killme.respawntime);
        DeadDude.SetActive(true);
        killme.CHealth = killme.maxhealth;
        DeadDude.transform.position = Spawns[killme.playernum-1].transform.position;
        DeadDude.GetComponent<ReceiveInfo>().ReceivedStop();
    }


    void SelectCharacter(int charcode)
    {
        GameObject mySpawn = Spawns[PhotonNetwork.player.ID - 1];
        GameObject myPlayer;
        switch (charcode)
        {
            case 1:
                //char 1 is Jigglypuff
                Debug.Log("I am player " + PhotonNetwork.player.ID + " and I choose jigglypuff");
                myPlayer = PhotonNetwork.Instantiate("jigglypuff", mySpawn.transform.position, mySpawn.transform.rotation, 0);
                myPlayer.GetComponent<SendInfo>().enabled = true;
                myPlayer.GetComponent<JigglypuffScript>().enabled = true;
                myPlayer.GetComponent<PhotonView>().RPC("InitTag", PhotonTargets.All, PhotonNetwork.player.ID);
                
                Canvas= Instantiate(Canvases[0]);
                
                break;
            case 2:
                //char 2 is putin
                Debug.Log("I am player " + PhotonNetwork.player.ID + " and I choose putin");
                myPlayer = PhotonNetwork.Instantiate("putin", mySpawn.transform.position, mySpawn.transform.rotation, 0);
                myPlayer.GetComponent<SendInfo>().enabled = true;
                myPlayer.GetComponent<putin>().enabled = true;
                myPlayer.GetComponent<PhotonView>().RPC("InitTag", PhotonTargets.All, PhotonNetwork.player.ID);

                Canvas = Instantiate(Canvases[1]);
                break;
        }
    }

    public void InitAddToPlayerList(GameObject GO, int playerID)
    {
        PlayerClass addme = new PlayerClass();
        addme.PlayerObject = GO;
        addme.name = "Player" + playerID;
        PlayerList[playerID - 1] = addme;
    }

    


}
