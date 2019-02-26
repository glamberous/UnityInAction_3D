using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public int curLevel { get; private set; }
    public int maxLevel { get; private set; }

    private NetworkService _Network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Mission manager starting...");

        _Network = service;

        UpdateData(0, 3);

        status = ManagerStatus.Started;
    }

    public void GoToNext()
    {
        if (curLevel < maxLevel)
        {
            curLevel++;
            string name = "Chapter12_level" + curLevel;
            Debug.Log("Loading " + name);
            SceneManager.LoadScene(name);
        }
        else
        {
            Debug.Log("Last level");
            Messenger.Broadcast(GameEvent.GAME_COMPLETE);
        }
    }

    public void ReachObjective()
    {
        // could have logic to handle multiple objectives
        Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
    }

    public void RestartCurrent()
    {
        string name = "Chapter12_level" + curLevel;
        Debug.Log("Loading " + name);
        SceneManager.LoadScene(name);
    }

    public void UpdateData(int curLevel, int maxLevel)
    {
        this.curLevel = curLevel;
        this.maxLevel = maxLevel;
    }
}
