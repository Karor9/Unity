using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLogo : MonoBehaviour
{
    public Image logo;


    void Start()
    {
        LoadSprite.instance.loadImageResources("Data/Teams/Logos/" + GlobalVariables.instance.playerID.ToString(), logo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
