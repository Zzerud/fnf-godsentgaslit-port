﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;

public class SongDataGenerator : EditorWindow
{
    public string songName = "Bopeebo";
    public string authorName = "Kawai Sprite";
    public string charterName = "ninjamuffin99";
    public string songDescription = "cool swag.";
    
    [MenuItem("Window/Meta Data Generator")]
    public static void Init()
    {
        SongDataGenerator generator = (SongDataGenerator)EditorWindow.GetWindow(typeof(SongDataGenerator));
        generator.Show();
    }

    private void OnGUI()
    {
        
        
        GUILayout.Label("Song Meta Data Generator", EditorStyles.boldLabel);

        GUILayout.Space(10);
        
        songName = EditorGUILayout.TextField("Song Name", songName);
        authorName = EditorGUILayout.TextField("Composer Name", authorName);
        charterName = EditorGUILayout.TextField("Charter Name", charterName);

        GUILayout.Space(10);

        
        GUILayout.Label("Description");

        
        songDescription = GUILayout.TextArea(songDescription, 500,GUILayout.Height(100));
        

        if (GUILayout.Button("Generate Song Meta Data"))
            GenerateMetaData();

    }

    private void GenerateMetaData()
    {
        string metaJson = JsonConvert.SerializeObject(new SongMetaV2()
        {
            songName = songName,
            credits = new Dictionary<string,string> {{"Composer",authorName},{"Charter",charterName}},
            difficulties = new Dictionary<string, Color> {{"Easy",Color.blue},{"Normal",Color.green},{"Hard",Color.red}},
            songDescription = songDescription
        },Formatting.Indented);

        string filePath = EditorUtility.SaveFilePanel("Save Song Meta Data", Application.persistentDataPath, "meta",
            "json");

        if (filePath.Length == 0) return;
        File.WriteAllText(filePath, metaJson);
        AssetDatabase.Refresh();
    }
}
