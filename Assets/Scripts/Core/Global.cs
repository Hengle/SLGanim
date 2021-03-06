﻿using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour {

    private static Global instance;

    public GameDataBase gameDB;
    public CharacterDataBase characterDB;
    public PlayerDataBase playerDB;
    public List<string> scenes = new List<string>();
    public int CurrentSceneIndex { get; set; }

    public static Global GetInstance()
    {
        return instance;
    }

    static Global()
    {
        GameObject go = new GameObject("Global");
        DontDestroyOnLoad(go);
        instance = go.AddComponent<Global>();
    }

    private void Awake()
    {
        StartCoroutine(XMLManager.GetInstance().LoadGameData(Application.streamingAssetsPath + "/XML/gameData.xml"));
        StartCoroutine(XMLManager.GetInstance().LoadCharacterData(Application.streamingAssetsPath + "/XML/characterData.xml"));
        StartCoroutine(XMLManager.GetInstance().LoadPlayerData(Application.streamingAssetsPath + "/XML/playerData.xml"));

        CurrentSceneIndex = 0;
        
        LoadScenes();
    }

    public void OnLoadGameDataComplete()
    {
        instance.gameDB = XMLManager.GetInstance().gameDB;
    }

    public void OnLoadCharacterDataComplete()
    {
        instance.characterDB = XMLManager.GetInstance().characterDB;
    }

    public void OnLoadPlayerDataComplete()
    {
        instance.playerDB = XMLManager.GetInstance().playerDB;
    }

    //private void SaveScenes()
    //{
    //    XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
    //    var encoding = System.Text.Encoding.GetEncoding("UTF-8");
    //    StreamWriter stream = new StreamWriter(Application.streamingAssetsPath + "/XML/scenes.xml", false, encoding);
    //    serializer.Serialize(stream, scenes);
    //    stream.Close();
    //}

    private void LoadScenes()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
        string path = Application.streamingAssetsPath + "/XML/scenes.xml";
        StreamReader stream = new StreamReader(path);
        scenes = serializer.Deserialize(stream) as List<string>;
        stream.Close();
    }

    public void NextScene()
    {
        CurrentSceneIndex++;
        SceneManager.LoadScene(scenes[CurrentSceneIndex]);
    }
}
