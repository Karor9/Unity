using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadSprite : MonoBehaviour
{
    public static LoadSprite instance;
    private float progress;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //StartCoroutine(loadImage("/Leagues/Logos/", 0.ToString() + ".png", im));
    }

    public IEnumerator loadImage(string path, string logo, Image im)
    {
        Debug.Log(GlobalVariables.instance.PATH + path);
        string[] filePath = Directory.GetFiles(GlobalVariables.instance.PATH  + path, logo);
        //"/Mods/Teams/Logos/", "2.png"
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(filePath[0]);
        var async = www.SendWebRequest();

        while (!www.isDone)
        {
            progress = async.progress;
            yield return null;
        }

        progress = 1f;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError(www.error);
        }
        Texture2D texture = DownloadHandlerTexture.GetContent(www);
        im.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
    }

    public void loadImageResources(string path, Image im)
    {
        Sprite sp = Resources.Load<Sprite>(path);
        im.sprite = sp;
    }
 
}
