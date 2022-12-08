using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string name;
    public float time;
    public int moves;
    public Player(string name, float time, int moves)
    {
        this.name = name;
        this.time = time;
        this.moves = moves;
    }

    public string toString()
    {
        return this.name + "----- time: " + this.time + "----- moves: " + this.moves;

    }

    public string getName()
    {
        return name;
    }

    public float getTime()
    {
        return time;
    }

    public int getMoves()
    {
        return moves;
    }
}
