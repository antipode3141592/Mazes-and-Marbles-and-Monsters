using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public enum LevelStateType
    {
        Inactive,
        ShowBackground,
        PopulateBoard,
        PopulateMarbles,
        PopulateMonsters,
        PopulatePlayer,
        GameStart,
        GamePause,
        GameEnd
    };

    private LevelStateType levelState;
    // Start is called before the first frame update
    void Awake()
    {
        levelState = LevelStateType.Inactive;
    }

    // Update is called once per frame
    void Update()
    {
        switch (levelState)
        {
            case LevelStateType.Inactive:
                break;
            case LevelStateType.ShowBackground:
                break;
            case LevelStateType.PopulateBoard:
                break;
            case LevelStateType.PopulateMarbles:
                break;
            case LevelStateType.PopulateMonsters:
                break;
            case LevelStateType.PopulatePlayer:
                break;
            case LevelStateType.GameStart:
                break;
            case LevelStateType.GamePause:
                break;
            case LevelStateType.GameEnd:
                break;
            default:
                break;
        }

    }
}
