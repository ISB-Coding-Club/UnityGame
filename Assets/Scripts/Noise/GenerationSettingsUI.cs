using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerationSettings))]
public class GenerationSettingsUI : Editor
{
    public GenerationSettings settings;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        settings = (GenerationSettings)target;

        if (GUILayout.Button("Generate"))
        {
            settings.generator.GenerateTile();
        }

        if (GUILayout.Button("Randomize"))
        {
            Randomize();
            // settings.generator.GenerateTile();
        }
    }

    void Randomize()
    {
        settings.generator.waves = new Wave[] {
            RandomizeWave(),
            RandomizeWave(),
            RandomizeWave(),
            RandomizeWave(),
            RandomizeWave(),
        };

        settings.generator.levelScale = UnityEngine.Random.Range(10f, 100f);
        settings.generator.heightMultiplier = UnityEngine.Random.Range(1f, 10f);
        settings.generator.heightCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    }

    Wave RandomizeWave()
    {
        Wave w = new Wave();
        System.Random r = new System.Random();

        w.seed = r.Next(1000, 10000);
        w.frequency = (float)r.Next(100, 1000);
        w.amplitude = (float)r.Next(10, 100);

        return w;
    }
}
