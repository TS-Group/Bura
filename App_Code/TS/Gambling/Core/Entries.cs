using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TS.Gambling.Core;

/// <summary>
/// Summary description for Entries
/// </summary>
public class Entries
{
    private GamblingModel.Entities entities;

    public Entries()
    {
        entities = new GamblingModel.Entities();
    }

    public void StartGame(int dbGameId, Dictionary<int, Player> players, double gameAmount)
    {
        int[] pList = new int[2];
        int i = 0;
        foreach (int pk in players.Keys)
        {            
            Player p = players[pk];
            pList[i++] = p.PlayerId;
        }
        DataBaseManager.StartGame(dbGameId, pList[0], pList[1], gameAmount);       
    }

    public void EndGame(int dbGameId, Dictionary<int, Player> players, int winnerPlayerId, double gameAmount)
    {
        DataBaseManager.EndGame(dbGameId, winnerPlayerId, gameAmount);
    }
}