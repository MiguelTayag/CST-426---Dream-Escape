using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{
    public string input;
    public float totalTime;
    public int totalMoves;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalTime = GameObject.Find("SlidingTile_4by4").GetComponent<ST_PuzzleDisplay>().getTime();
        totalMoves = GameObject.Find("SlidingTile_4by4").GetComponent<ST_PuzzleDisplay>().getMoves();
        //Debug.Log("Totals\n" + totalTime + "\n" + totalMoves);
    }

    public void ReadStringInput(string s)
    {
        input = s;
        addScore();
        Debug.Log(input);
    }

    public void addScore()
    {
        string name = input;
        totalTime = GameObject.Find("SlidingTile_4by4").GetComponent<ST_PuzzleDisplay>().getTime();
        totalMoves = GameObject.Find("SlidingTile_4by4").GetComponent<ST_PuzzleDisplay>().getMoves();
        Player player = new Player(name, totalTime, totalMoves);
        int itemCount = 0;
        Debug.Log("name: " + name);
        Debug.Log("time: " + totalTime);
        Debug.Log("moves: " + totalMoves);


        /*for (int k = 0; k < Leaderboard.leaderboard.Length; k++)
        {
            if (Leaderboard.leaderboard[k] != null)
            {
                itemCount++;
            }
        }*/
            /*
            if (itemCount > 0)
            {
                Leaderboard.leaderboard[] = player;
            }
            
        else
        {
            Leaderboard.leaderboard[0] = player;
        }
        
        Debug.Log(Leaderboard.toString());
        */
        Leaderboard.leaderboard[5] = player;

    }

   
}
