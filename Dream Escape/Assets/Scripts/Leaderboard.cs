using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Leaderboard : MonoBehaviour
{
    // Start is called before the first frame update
    public static Player[] leaderboard = new Player[3];
    public string text;
    void Start()
    {
        Array.Sort(leaderboard, delegate (Player x, Player y) { return x.time.CompareTo(y.time); });
        for (int i = 0; i < leaderboard.Length; i++)
        {
            text = GameObject.Find("Players").GetComponent<TMPro.TextMeshProUGUI>().text;
            text = text + "\n" + leaderboard[i].toString();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
