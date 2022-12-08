using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string name;
    public float time;
    public int moves;
    public PlayerData(Player player)
    {
        this.name = player.getName();
        this.time = player.getTime();
        this.moves = player.getMoves();
    }

    public string toString()
    {
        return this.name + "----- time: " + this.time + "----- moves: " + this.moves;
    }
}