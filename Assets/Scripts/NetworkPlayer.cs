using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {
    [SerializeField]
    GameObject[] Unit;
    GameObject MyUnit;
    static int playernumber = 1;
    public bool playeralive;

    Rigidbody2D Face;

    float force;

	// Use this for initialization
	void Start () {
        playeralive = false;
        if (!isLocalPlayer)
            return;
        CmdSpawnUnit();
        
	}

    // Update is called once per frame
    void Update() {
        if (false)
        if (playeralive && isLocalPlayer)
        {
            if (force == 0)
            { force = 13; }

            Vector2 forcetoserver = new Vector2(0, 0);
            if (Input.GetKey
                        (KeyCode.RightArrow))
            {
                forcetoserver += new Vector2(force, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                forcetoserver += new Vector2(-force, 0);
            }
            if (Input.GetKey
                (KeyCode.UpArrow))
            {
                forcetoserver += new Vector2(0, force);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                forcetoserver += new Vector2(0, -force);
            }
            CmdAddForce(forcetoserver);
            
            
        }
        else
        {
            if (isLocalPlayer)
            {
                Face.velocity = new Vector2(0, 0);
                Debug.Log("Stop");
            }
        }

    }

    [Command]
    void CmdSpawnUnit()
    {
        while (playernumber >=3)
        {
            playernumber -= 2;
        }
        GameObject spawn = Instantiate(Unit[playernumber -1 ]);
        playernumber++;
        NetworkServer.Spawn(spawn);
        
        spawn.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        MyUnit = spawn;
        Face = spawn.GetComponent<Rigidbody2D>();
        MyUnit.GetComponent<UnitControl>().parent = this;
        MyUnit.GetComponent<UnitControl>().Face = Face;
    }

    public Rigidbody2D getFace()
    {
        return Face;
    }

    [Command]
    void CmdAddForce(Vector2 serverforce)
    {
        //if (serverforce != new Vector2 (0,0))
          //  Debug.Log("cmd add force");
        //Face.AddForce(serverforce);
    }
}
