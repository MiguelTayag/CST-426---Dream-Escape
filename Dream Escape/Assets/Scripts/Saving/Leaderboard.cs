using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class Leaderboard : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerData playerData;
    private string path = "";
    private string persistentPath = "";

    public static Player[] leaderboard = new Player[6];
    public GameObject textTMP;
    public GameObject topTMP;
    public string text;
    public string top;
    public Player newPlayer;

    int index = 0;
    void Start()
    {
        for (int k = 0; k < leaderboard.Length; k++)
        {
            if (leaderboard[k] == null)
            {
                leaderboard[k] = new Player("<empty>", 999999, 999999);
            }
        }
        newPlayer = new Player(leaderboard[5].name, leaderboard[5].time, leaderboard[5].moves);

        Array.Sort(leaderboard, delegate (Player x, Player y) { return x.time.CompareTo(y.time); });
        leaderboard[5] = null;
        for (int i = 0; i < leaderboard.Length - 1; i++)
        {
            if (leaderboard[i] != null)
            {
                text = textTMP.GetComponent<TMPro.TextMeshProUGUI>().text;
                text = text + leaderboard[i].toString() + "\n";
                textTMP.GetComponent<TMPro.TextMeshProUGUI>().SetText(text);
            }
        }
        
        try
        {
            SetPaths();
            Debug.Log("Loaded data = " + LoadData().toString());
            PlayerData loadedPlayer = LoadData();
            if (loadedPlayer.time > newPlayer.time)
            {
                playerData = new PlayerData(newPlayer);
                top = playerData.toString();
                topTMP.GetComponent<TMPro.TextMeshProUGUI>().SetText(top);
            }
            else
            {
                top = loadedPlayer.toString();
                playerData = loadedPlayer;
                topTMP.GetComponent<TMPro.TextMeshProUGUI>().SetText(top);
            }
            SaveData();
        }
        
        catch (FileNotFoundException e)
        {
            CreatePlayerData(newPlayer);
            SetPaths();
            SaveData();
            LoadData();
            top = playerData.toString();
            topTMP.GetComponent<TMPro.TextMeshProUGUI>().SetText(top);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string toString()
    {
        string dLog = "";
        for (int i = 0; i < leaderboard.Length; i++)
        {
            if(leaderboard[i] == null)
            {
                dLog = dLog + " null \n";
            }else
            {
                dLog = dLog + i + " name: " + leaderboard[i].name + "\n time: " + leaderboard[i].time + "\n moves: " + leaderboard[i].moves + "\n";
            }
        }
        return dLog;
    }

    
    private void CreatePlayerData(Player player)
    {
        playerData = new PlayerData(player);
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }
    public void SaveData()
    {
        string savePath = path;
        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public PlayerData LoadData()
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log(data.ToString());
        return data;
    }

}
