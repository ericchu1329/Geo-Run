/*
NAME: GPEJ
MEMBERS: Eric Chu, Jake Wong
COURSE: CPSC 254-01

FILE DESCRIPTION:
This file contains the GameManager class, which keeps track of the user and the current game state
*/
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private UserSettings user_settings;
    [SerializeField] private GameObject[] character_list;

    private GameObject active_player_object;

    private enum GameState { Initial, Running, Pause, Resuming, Lost }
    private GameState current_state;

    public event Action<Transform> OnPlayerSpawn;
    public void GetPlayerTransform (Transform player)
    {
        OnPlayerSpawn?.Invoke(player);
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TransitionState(GameState.Initial);
    }

    private void Update()
    {
        switch (current_state)
        {
            case GameState.Initial:
                break;
            case GameState.Running:

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    TransitionState(GameState.Pause);
                }

                break;
            case GameState.Pause:

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    TransitionState(GameState.Running);
                }

                break;
            case GameState.Resuming:
                break;
            case GameState.Lost:
                break;
            default:
                break;
        }
    }

    private void TransitionState (GameState state)
    {
        TerminateState(current_state);
        ActivateState(state);
    }

    private void TerminateState(GameState state)
    {
        switch (state)
        {
            case GameState.Initial:
                break;
            case GameState.Running:
                break;
            case GameState.Pause:
                Time.timeScale = 1f;
                break;
            case GameState.Resuming:
                break;
            case GameState.Lost:
                break;
            default:
                break;
        }
    }

    private void ActivateState(GameState state)
    {
        current_state = state;
        Debug.Log(current_state);
        switch (current_state)
        {
            case GameState.Initial:
                SpawnPlayer();
                TransitionState(GameState.Running);
                break;
            case GameState.Running:
                break;
            case GameState.Pause:
                Time.timeScale = 0f;
                break;
            case GameState.Resuming:
                break;
            case GameState.Lost:
                break;
            default:
                break;
        }
    }

    private void SpawnPlayer()
    {
        active_player_object = Instantiate(character_list[user_settings.character_type].gameObject);
        GameManager.instance.GetPlayerTransform(active_player_object.transform);
    }

    public bool IsPaused()
    {
        if (current_state == GameState.Pause)
        {
            return true;
        }
        return false;
    }
}
