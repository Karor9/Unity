using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static LFD;

public class LoadFiles : MonoBehaviour
{
    public static LoadFiles instance;

    string PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\MOBA Manager\\Mods";
    public TMP_Dropdown modsList;
    public Button currentOption;
    List<string> b = new List<string> { "Ustawienia Domyœlne" };

    public Text t;

    public Dictionary<int, Teams> teams = new Dictionary<int, Teams>();
    public Dictionary<int, Leagues> leagues = new Dictionary<int, Leagues>();
    public Dictionary<int, Players> players = new Dictionary<int, Players>();

    public List<List<int>> allHome = new List<List<int>>();
    public List<List<int>> allAway = new List<List<int>>();
    public List<int> leagueSize = new List<int>();

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        modsList.options.Clear();
        if(!Directory.Exists(PATH))
        {
            Directory.CreateDirectory(PATH);
        }
        else
        {
            foreach(var d in Directory.GetDirectories(PATH))
            {
                var dir = new DirectoryInfo(d);
                var dirName = dir.Name;

                b.Add(dirName);
            }
        }

        modsList.AddOptions(b);
        currentOption.onClick.AddListener(setGlobalPath);
    }

    private void setGlobalPath()
    {
        int pathValue = modsList.value;
        if (pathValue != 0)
        {
            GlobalVariables.instance.PATH = PATH + "/" + b[pathValue];
            GlobalVariables.instance.modsEnabled = true;
        }
        LoadTeams(GlobalVariables.instance.PATH + "/data/Teams/Teams.json");
        LoadLeagues(GlobalVariables.instance.PATH + "/data/Leagues/Leagues.json");
        LoadPlayers(GlobalVariables.instance.PATH + "/data/Players/Players.json");
        generate();
        SceneManager.LoadScene(1);

    }

    private void generate()
    {
        for (int i = 0; i < leagueSize.Count; i++)
        {
            List<int> home = new List<int>();
            List<int> away = new List<int>();
            List<int> listOfHome = new List<int>();
            List<int> listOfAway = new List<int>();

            for (int j = 0; j < leagueSize[i] / 2; j++)
            {
                listOfHome.Add(j);
            }
            for (int j = leagueSize[i]; j > leagueSize[i] / 2; j--)
            {
                listOfAway.Add(j - 1);
            }

            for (int j = 0; j < listOfHome.Count * 2 - 1; j++)
            {
                for (int k = 0; k < listOfAway.Count; k++)
                {
                    if (j % 2 == 0)
                    {
                        home.Add(listOfHome[k]);
                        away.Add(listOfAway[k]);
                    }
                    else
                    {
                        away.Add(listOfHome[k]);
                        home.Add(listOfAway[k]);
                    }


                    if (listOfHome[k] == 1)
                    {
                        listOfHome[k] = leagueSize[i] - 1;
                    }
                    else if (listOfHome[k] != 0)
                    {
                        listOfHome[k] -= 1;
                    }

                    if (listOfAway[k] == 1)
                    {
                        listOfAway[k] = leagueSize[i] - 1;
                    }
                    else
                    {
                        listOfAway[k] -= 1;
                    }
                }
            }

            for (int j = 0; j < listOfAway.Count * 2 - 1; j++)
            {
                for (int k = 0; k < listOfHome.Count; k++)
                {
                    if (j % 2 == 0)
                    {
                        away.Add(listOfAway[k]);
                        home.Add(listOfHome[k]);
                    }
                    else
                    {
                        home.Add(listOfAway[k]);
                        away.Add(listOfHome[k]);
                    }

                    if (listOfAway[k] == 1)
                    {
                        listOfAway[k] = leagueSize[i] - 1;
                    }
                    else if (listOfHome[k] != 0)
                    {
                        listOfAway[k] -= 1;
                    }

                    if (listOfHome[k] == 1)
                    {
                        listOfHome[k] = leagueSize[i] - 1;
                    }
                    else
                    {
                        listOfHome[k] -= 1;
                    }
                }
            }

            allHome.Add(home);
            allAway.Add(away);
        }
    }

    private void LoadTeams(string path)
    {
        LoadedTeams loadedTeams;
        if (File.Exists(path))
        {
            string dataAsJson = File.ReadAllText(path);
            loadedTeams = JsonUtility.FromJson<LoadedTeams>(dataAsJson);

        }
        else
        {
            var testData = Resources.Load<TextAsset>("data/Teams/Teams");
            loadedTeams = JsonUtility.FromJson<LoadedTeams>(testData.ToString());
        }
        for (int i = 0; i < loadedTeams.items.Length; i++)
        {
            Teams t = new Teams(loadedTeams.items[i].name, loadedTeams.items[i].players);
            teams.Add(loadedTeams.items[i].key, t);
        }

    }
    public void LoadLeagues(string path)
    {
        LoadedLeagues loadedData;
        if (File.Exists(path))
        {
            string dataAsJson = File.ReadAllText(path);
            loadedData = JsonUtility.FromJson<LoadedLeagues>(dataAsJson);
        }
        else
        {
            var testData = Resources.Load<TextAsset>("data/Leagues/Leagues");
            loadedData = JsonUtility.FromJson<LoadedLeagues>(testData.ToString());
        }
        
        for (int i = 0; i < loadedData.items.Length; i++)
        {
            Leagues l = new Leagues(loadedData.items[i].name, loadedData.items[i].region, loadedData.items[i].level, loadedData.items[i].teams);
            leagueSize.Add(loadedData.items[i].teams.Length);
            leagues.Add(loadedData.items[i].key, l);
        }
    }

    public void LoadPlayers(string path)
    {
        LoadedPlayers loadedData;
        if (File.Exists(path))
        {
            string dataAsJson = File.ReadAllText(path);
            loadedData = JsonUtility.FromJson<LoadedPlayers>(dataAsJson);
        }
        else
        {
            var testData = Resources.Load<TextAsset>("data/Players/Players");
            loadedData = JsonUtility.FromJson<LoadedPlayers>(testData.ToString());
        }
        
        for (int i = 0; i < loadedData.items.Length; i++)
        {
            Players p = new Players(loadedData.items[i].nickname, loadedData.items[i].role, loadedData.items[i].country);
            players.Add(loadedData.items[i].key, p);
        }
    }
}
