﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class OnUnityLoad {

    static OnUnityLoad() {
        EditorApplication.playModeStateChanged += HandlePlayModeState;
    }

    private static void HandlePlayModeState(PlayModeStateChange state) {

        if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying) {
            string strScenes = "";
            int i;
            for (i = 0; i < EditorSceneManager.loadedSceneCount; i++) {
                strScenes += EditorSceneManager.GetSceneAt(i).name + ", ";
            }

            Debug.Log("Auto-Saving scene before entering Play mode: " + strScenes);

            EditorSceneManager.SaveOpenScenes();

            AssetDatabase.SaveAssets();
        }
    }
}