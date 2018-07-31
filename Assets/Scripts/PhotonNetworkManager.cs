using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PhotonNetworkManager : Photon.MonoBehaviour {
    [SerializeField]
    GameObject[] Spawns;
    [SerializeField]
    GameObject[] Canvases;


    private int state = 0;
	void OnGUI()
    {
        GUIStyle centeredTextStyle = new GUIStyle("label");
        centeredTextStyle.alignment = TextAnchor.MiddleCenter;
        switch (state)
        {
            case 0:// Unconnected
                if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height - 80, 300, 30), "서버에 연결하기"))
                {
                    Connect();
                }
                break;
            case 1://Connected to server, in lobby
                GUI.color = Color.black;
                GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 120, 300, 30), "서버에 연결 되었습니다", centeredTextStyle);
                GUI.color = Color.white;
                if (GUI.Button(new Rect(Screen.width / 2 - 80, Screen.height - 60, 160, 30), "방 찾기"))
                {
                    PhotonNetwork.JoinRandomRoom();
                }
                break;
            case 2://Connected to a room, need to wait for it to fill
                int reqppl = 2;

                string currentstate = "방에 연결 되었습니다, 현재 " + PhotonNetwork.playerList.Length + "명 접속중. " + reqppl + "명이 되면 게임이 시작됩니다";
                GUI.color = Color.black;
                GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height - 120, 500, 30), currentstate, centeredTextStyle);
                GUI.color = Color.white;
                if (PhotonNetwork.playerList.Length == reqppl && PhotonNetwork.isMasterClient == true)
                {//when filled, the master client calls RPC to start game.
                    this.GetComponent<PhotonView>().RPC("StartGame", PhotonTargets.All);
                }
                break;
            case 3://Change scene to hero select
                break;

        }
    }

    private void Connect()
    {
        Debug.Log("Attempting to Connect... ");
        PhotonNetwork.ConnectUsingSettings("V0.1");
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
    /*
    void SelectCharacter(int charcode)
    {
        int choosespawn = PhotonNetwork.player.ID - 1;
        GameObject mySpawn = Spawns[choosespawn];

        switch (charcode)
        {
            case 1:
                //char 1 is Jigglypuff
                GameObject myPlayer = PhotonNetwork.Instantiate("jigglypuff", mySpawn.transform.position, mySpawn.transform.rotation, 0);
                myPlayer.GetComponent<SendInfo>().enabled = true;
                myPlayer.GetComponent<JigglypuffScript>().enabled = true;
                myPlayer.GetComponent<PhotonView>().RPC("Tagger", PhotonTargets.All, "P" + PhotonNetwork.player.ID.ToString());
                GameObject canvas = Instantiate(Canvases[0]);
                //GameManager.Instance.setCanvas(canvas);
                break;
            default:
                break;
        }
    }*/

    [PunRPC]
    public void StartGame()
    {
        state = 3;
        
        SceneManager.LoadSceneAsync(1);
    }
}
