using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonNetworkManager : Photon.MonoBehaviour {
    [SerializeField]
    GameObject[] Spawns;


    private int state = 0;
	void OnGUI()
    {
        switch (state)
        {
            case 0:// Unconnected
                if (GUI.Button(new Rect (10, 10, 100, 30), "서버에 연결하기"))
                {
                    Connect();
                }
                break;
            case 1://Connected to server, in lobby
                GUI.Label(new Rect(10, 40, 100, 30), "서버에 연결 되었습니다");
                if (GUI.Button(new Rect(10, 10, 100, 30), "방 찾기"))
                {
                    PhotonNetwork.JoinRandomRoom();
                }
                break;
            case 2://Connected to a room, need to wait for it to fill
                int reqppl = 2;
                string currentstate = "방에 연결 되었습니다, 현재 " + PhotonNetwork.playerList.Length + "명 접속중. " + reqppl + "명이 되면 게임이 시작됩니다";
                GUI.Label(new Rect(10, 40, 600, 30), currentstate);
                if (PhotonNetwork.playerList.Length == reqppl && PhotonNetwork.isMasterClient == true)
                {//when filled, the master client calls RPC to start game.
                    this.GetComponent<PhotonView>().RPC("StartGame", PhotonTargets.All);
                }
                break;
            case 3://Hero select screen
                GUI.Label(new Rect(10, 40, 100, 30), "영웅을 선택하세요");
                if (GUI.Button(new Rect(10, 10, 100, 30), "푸린"))
                {
                    SelectCharacter(1);
                }
                if (GUI.Button(new Rect(10, 50, 100, 30), "푸틴"))
                {
                    SelectCharacter(2);
                }
                break;
            case 4:
                break;

        }
    }

    private void Connect()
    {
        Debug.Log("Attempting to Connect... ");
        PhotonNetwork.ConnectUsingSettings("V0.0");
    }


    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("No room was found, so we are making our own room");
        
        PhotonNetwork.CreateRoom(null);
    }
    void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        state = 2;
    }
    void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        state = 1;
    }
    void Start()
    {
        
    }
    void SelectCharacter(int charcode)
    {
        state = 4;
        int choosespawn = PhotonNetwork.player.ID -1;
        GameObject mySpawn = Spawns[choosespawn];

        switch (charcode) {
            case 1:
                //char 1 is Jigglypuff
                GameObject myPlayer = PhotonNetwork.Instantiate("jigglypuff", mySpawn.transform.position, mySpawn.transform.rotation, 0);
                myPlayer.GetComponent<SendInfo>().enabled = true;
                myPlayer.GetComponent<JigglypuffScript>().enabled = true;
                myPlayer.GetComponent<PhotonView>().RPC("Tagger", PhotonTargets.All, "P" + PhotonNetwork.player.ID.ToString()) ;
                break;
            case 2:
                GameObject myPlayer2 = PhotonNetwork.Instantiate("putin", mySpawn.transform.position, mySpawn.transform.rotation, 0);
                myPlayer2.GetComponent<SendInfo>().enabled = true;
                myPlayer2.GetComponent<putin>().enabled = true;
                myPlayer2.GetComponent<PhotonView>().RPC("Tagger", PhotonTargets.All, "P" + PhotonNetwork.player.ID.ToString());
                break;
            default:
                break;
        }
    }

    [PunRPC]
    public void StartGame()
    {
        state = 3;
    }
}
