using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayScript : MonoBehaviour
{
    int players;

    [SerializeField]
    Text DeadText, TimeText;


    float gametime;

    bool ready, over;
    bool dead = false;

    float deadtime;

    float showdeadtime
    {
        get
        {
            return deadtime;
        }

        set
        {
            deadtime = value;
            
            if (deadtime < 0.1)
            {
                DeadText.text = "";
            }
            else
                DeadText.text = deadtime.ToString("0.0");
        }
    }

    float showgametime
    {
        get
        {
            return gametime;
        }

        set
        {
            gametime = value;

            if (gametime < 0.1)
            {
                over = true;
                DeadText.text = "Game Over";
                
                TimeText.text = "0";
            }
            else
                TimeText.text = gametime.ToString("0.0");
        }
    }

    void Start()
    {
        players = ConstantManager.Manager.numplayers;
        gametime = GameManager.Manager.gametime;
    }

    void Update()
    {
        if (dead && deadtime >0 && !over)
        {
            showdeadtime -= Time.deltaTime;
            if (deadtime <= 0)
                dead = false;
        }
        if (ready)
        {
            showgametime -= Time.deltaTime;
            if (gametime <= 0)
            {
                GameManager.Manager.GameOver();
                ready = false;
            }
        }
    }

    public void IDied(float deathtime)
    {
        dead = true;
        deadtime = deathtime;
        
    }

    void OnGUI()
    {
        if (ready)
        {
            string[] labels = MakeIntoStringList(GameManager.PlayerList);
            GUI.color = Color.black;
            for (int i = 0; i < players; i++)
            {
                GUI.Label(new Rect(Screen.width - 200, Screen.height - 120 + 30 * i, 200, 30), labels[i]);
            }

        }
        else
        {
            if (GameManager.PlayerList != null)
            {
                for (int i = 0; i < players; i++)
                {
                    if (GameManager.PlayerList[i] == null)
                        return;
                }
                ready = true;
            }
        }
    }

    string[] MakeIntoStringList(GameManager.PlayerClass[] list) {
        string[] strlist = new string[players];
        for (int i =0; i < players; i++)
        {
            strlist[i] = list[i].name + "  " + list[i].kills + "/" + list[i].deaths + "   " + list[i].points;
        }
        return strlist;
    }
}
