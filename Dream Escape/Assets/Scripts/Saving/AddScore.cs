using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AddScore : MonoBehaviour
{
    public float totalTime;
    public int totalMoves;
    public string name;
    public GameObject inputField;
    Player player;
    // Start is called before the first frame update

    void Start()
    {
        name = inputField.GetComponent<Text>().text;
        totalTime = GameObject.Find("SlidingTile_4by4").GetComponent<ST_PuzzleDisplay>().getTime();
        totalMoves = GameObject.Find("SlidingTile_4by4").GetComponent<ST_PuzzleDisplay>().getMoves();
        player = new Player(name, totalTime, totalMoves);
        int itemCount = 0;

        for (int k = 0; k < Leaderboard.leaderboard.Length; k++)
        {
            if (Leaderboard.leaderboard[k] != null)
            {
                itemCount++;
            }
        }
        if (itemCount > 0)
        {
            /*if(itemCount == Leaderboard.leaderboard.Length)
            {
                if (player.getTime() > Leaderboard.leaderboard[Leaderboard.leaderboard.Length - 1].getTime())
                {

                }
            }*/
            
            for (int i = 0; i < Leaderboard.leaderboard.Length; i++)
            {
                if(player.getTime() < Leaderboard.leaderboard[i].getTime())
                {
                    Leaderboard.leaderboard = shiftArray(Leaderboard.leaderboard, i);
                }
            }
        }
        else
        {
            Leaderboard.leaderboard[0] = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Player[] shiftArray(Player[] leaderboard, int i)
    {
        Player[] shiftedArray;
        Player oldPlayer;
        int itemCount = 0;

        for (int k = 0; k < Leaderboard.leaderboard.Length; k++)
        {
            if (Leaderboard.leaderboard[k] != null)
            {
                itemCount++;
            }
        }

        if ((i + 1) >= itemCount)
        {
             if(itemCount == Leaderboard.leaderboard.Length)
             {
                leaderboard[Leaderboard.leaderboard.Length - 1] = player;
            }
            else
            {
                oldPlayer = leaderboard[i];
                leaderboard[i] = player;
                leaderboard[i + 1] = oldPlayer;
            }
        }
        else
        {
            for (int k = i; k < leaderboard.Length - 1; k++)
            {
                oldPlayer = leaderboard[i];
                leaderboard[i] = player;
                leaderboard[i + 1] = oldPlayer;
            }
        }

        shiftedArray = leaderboard;
        return shiftedArray;
    }
}
