using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class (could be a struct) to hold LevelInfo

public class LevelInfo : ScriptableObject
{
    private int level;
    private int timeLimit;
    public Dictionary<string, List<string>> animalItemInfo;

    public LevelInfo(int _level, int _timeLimit, Dictionary<string, List<string>> _animalItemInfo) {
        level = _level;
        timeLimit = _timeLimit;
        animalItemInfo = _animalItemInfo;
    }
}
