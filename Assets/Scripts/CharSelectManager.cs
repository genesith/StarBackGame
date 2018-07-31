using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharSelectManager : Photon.MonoBehaviour {
    float timer = 20f;
    [SerializeField]
    Text timertext;
    [SerializeField]
    GameObject[] SpawnSpots;
    [SerializeField]
    GameObject[] CharactersList;

    bool[] ReadyList;
    int[] CharSelected;
    int mychar;
    bool ImReady = false;

    int numplayers;

    float Timer
    {
        get
        {
            return timer;
        }

        set
        {
            timer = value;
            this.timertext.text = timer.ToString("0.0");
        }
    }
    void OnGUI()
    {
        GUIStyle centeredTextStyle = new GUIStyle("label");
        centeredTextStyle.alignment = TextAnchor.MiddleCenter;
        

        if (!ImReady)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 30, 100, 30), "준비하기") )
            {
                if (mychar != 0)
                {
                    ImReady = true;
                    this.GetComponent<PhotonView>().RPC("LockinRPC", PhotonTargets.All, mychar, PhotonNetwork.player.ID);
                }
                else
                {
                    GUI.color = Color.black;
                    GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 50, 300, 30), "캐릭터를 먼저 선택 하십시오", centeredTextStyle);
                    GUI.color = Color.white;
                }
            }
        }
        else
        {
            GUI.color = Color.black;
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 50, 300, 30), "상대방을 기다립니다", centeredTextStyle);
            GUI.color = Color.white;
        }
    }


    public void ButtonChooseChar ( int charid)
    {
        mychar = charid;
    }

    bool AllReady()
    {
        for (int i = 0; i < numplayers; i++)
        {
            if (ReadyList[i] == false)
                return false;
        }
        return true;
    }

    void Start() {
        
        numplayers = ConstantManager.Manager.numplayers;
        ReadyList = new bool[numplayers];
        CharSelected = new int[numplayers];
        

    }

    // Update is called once per frame
    void Update() {

        if (Timer <= 0 || AllReady())
        {

            if (PhotonNetwork.isMasterClient == true)
            {
                if (!AllReady())
                    MakeListWithRandoms();
                this.GetComponent<PhotonView>().RPC("StartGame", PhotonTargets.All, CharSelected);
            }
        }
        else
        {
            Timer -= Time.deltaTime;
        }

    }

    void MakeListWithRandoms()
    {
        for (int i = 0; i < numplayers; i++)
        {
            if (ReadyList[i] == false)
                CharSelected[i] = Random.Range(0, numplayers) + 1;
        }
    }


    [PunRPC]
    public void StartGame(int[] MasterCharlist)
    {
        ConstantManager.Manager.CharacterList = MasterCharlist;
        SceneManager.LoadSceneAsync(2);
    }

    [PunRPC]
    public void LockinRPC(int charcode, int playercode)
    {
        Debug.Log("Player " + playercode + " has selected char " + charcode);
        CharSelected[playercode - 1] = charcode;
        ReadyList[playercode - 1] = true;
        GameObject rdysprite = Instantiate(CharactersList[charcode - 1]);
        rdysprite.transform.position = SpawnSpots[playercode - 1].transform.position;
    }


}
