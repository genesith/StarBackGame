using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnitControl : NetworkBehaviour {
    
    public Rigidbody2D Face;
    public float force;
    public bool p1;
    public bool playeralive;
    public NetworkPlayer parent;

    bool nonservercontrol = true;



	// Use this for initialization
	void Start () {
       
        playeralive = true;
        Face = gameObject.GetComponent<Rigidbody2D>();
        force = 0.1f;
	}

    // Update is called once per frame
    void Update()
    {
        if (nonservercontrol)
        {
            if (playeralive)
            {
                if (hasAuthority)
                {
                    if (Input.GetKey
                        (KeyCode.RightArrow))
                    {
                        Face.transform.Translate(new Vector2(force, 0));
                    }
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        Face.transform.Translate(new Vector2(-force, 0));
                    }
                    if (Input.GetKey
                       (KeyCode.UpArrow))
                    {
                        Face.transform.Translate(new Vector2(0, force));
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        Face.transform.Translate(new Vector2(0, -force));
                    }
                }

            }
            else
                Face.velocity = new Vector2(0, 0);
        }
        else
        {
            if (playeralive && hasAuthority)
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
                Face.AddForce(forcetoserver);
                if (forcetoserver == new Vector2(0,0))
                {
                    //Debug.Log("wtf");
                }

            }
        }
        
        
    
       
    }
    [Command]
    public void CmdParticleHit(int particle)
    {
        if (particle == 1)
        {
            transform.localScale = 0.5f * transform.localScale;
        }
    }
    

}
