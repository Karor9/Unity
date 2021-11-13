using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Animations;
using static LoadSprite;




public class LoadLeaguesButton : MonoBehaviour
{
    public Button confirmButton;
    public GameObject leaguePrefab;
    public GameObject teamPrefab;
    public Transform panel;

    


    public TMP_Text t;
    void Start()
     {
         GameObject go = GameObject.Find("Logo");
         LoadSprite other = (LoadSprite)go.GetComponent(typeof(LoadSprite));

         for(int i = 0; i<LoadFiles.instance.leagueSize.Count; i++)
         {
            GameObject buton = Instantiate(leaguePrefab) as GameObject;
            buton.GetComponentInChildren<TextMeshProUGUI>().SetText(LoadFiles.instance.leagues[i].name);
            buton.transform.SetParent(panel, true);
            buton.transform.localScale = new Vector3(1, 1, 1);
            var panelButtons = buton.transform.GetChild(2).transform;
            if(GlobalVariables.instance.modsEnabled)
            {
                StartCoroutine(other.loadImage("/Data/Leagues/Logos/", i.ToString() + ".png", buton.transform.GetChild(1).GetComponent<Image>()));
            } else
            {
                other.loadImageResources("Data/Leagues/Logos/" + i.ToString(), buton.transform.GetChild(1).GetComponent<Image>());
            }
            buton.GetComponent<Button>().onClick.AddListener(() => setL(panelButtons));
            
            for (int j = 0; j < LoadFiles.instance.leagueSize[i]; j++)
            {
                GameObject buton2 = Instantiate(teamPrefab) as GameObject;
                buton2.transform.SetParent(panel, false);
                if (GlobalVariables.instance.modsEnabled)
                {
                    StartCoroutine(other.loadImage("/Data/Teams/Logos/", j.ToString() + ".png", buton2.transform.GetChild(1).GetComponent<Image>()));
                }
                else
                {
                    other.loadImageResources("Data/Teams/Logos/" + j.ToString(), buton2.transform.GetChild(1).GetComponent<Image>());
                }
                string name = LoadFiles.instance.teams[j].name;
                buton2.transform.localScale = new Vector3(1, 1, 1);
                buton2.GetComponentInChildren<TextMeshProUGUI>().SetText(name);
                buton2.transform.SetParent(panelButtons, false);
                //buton2.transform.localScale = new Vector3(res / 1920, resH / 1080, 1);
                buton2.GetComponent<Button>().onClick.AddListener(() => setT(name));
            }
            panelButtons.transform.localScale = new Vector3(1, 1, 1);
            panelButtons.gameObject.SetActive(false);
            panelButtons.transform.SetParent(panel, true);
        }
        confirmButton.onClick.AddListener(startGame);
     }
    /*
    private void Start()
    {
        GameObject b = (GameObject)Instantiate(leaguePrefab);
        b.GetComponent<Button>().onClick.AddListener(setT);
        b.transform.SetParent(panel, false);
        
    }*/

    public void setL(Transform g)
    {
        if(g.gameObject.activeSelf)
        {
            g.gameObject.SetActive(false);
        }
        else
        {
            g.gameObject.SetActive(true);
        }
    }

    public void setT(String name)
    {
        GlobalVariables.instance.playerTeam = name;
        for(int i = 0; i<LoadFiles.instance.teams.Count; i++)
        {
            if(LoadFiles.instance.teams[i].name == name)
            {
                GlobalVariables.instance.playerID = i;
                return;
            }
        }
    }

    public void startGame()
    {
        if(GlobalVariables.instance.playerID > -1)
        {
            SceneManager.LoadScene(2);
        }
    }
}
