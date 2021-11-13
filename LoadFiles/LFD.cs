using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LFD : MonoBehaviour
{
    [System.Serializable]
    public class TeamToLoad
    {
        public int key; //klucz zespo³u
        public string name; //nazwa zespo³u
        public int[] players;
    }

    public class Teams
    {
        public string name;
        public int[] players;

        public Teams(string name, int[] players)
        {
            this.name = name;
            this.players = players;
        }
    }

    [System.Serializable]
    public class LoadedTeams
    {
        public TeamToLoad[] items; //tablica zespo³ów
    }

    [System.Serializable]
    public class LeagueToLoad
    {
        public int key;
        public string name;
        public string region;
        public int level;
        public int[] teams;
    }

    public class Leagues
    {
        public string name;
        public string region;
        public int level;
        public int[] teams;

        public Leagues(string name, string region, int level, int[] teams)
        {
            this.name = name;
            this.region = region;
            this.level = level;
            this.teams = teams;
        }
    }

    [System.Serializable]
    public class LoadedLeagues
    {
        public LeagueToLoad[] items;
    }

    [System.Serializable]
    public class PlayerToLoad
    {
        public int key;
        public string nickname;
        public int role;
        public string country;
    }

    public class Players
    {
        public string nickname;
        public int role;
        public string country;

        public Players(string nickname, int role, string country)
        {
            this.nickname = nickname;
            this.role = role;
            this.country = country;
        }
    }

    [System.Serializable]
    public class LoadedPlayers
    {
        public PlayerToLoad[] items;
    }
}
