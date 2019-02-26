using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //[SerializeField] private Text scoreLabel = null; // Not Used
    //[SerializeField] private SettingsPopup settingsPopup = null; // Not Used
    [SerializeField] private Text healthLabel = null;
    [SerializeField] private InventoryPopup popup = null;
    [SerializeField] private Text levelEnding = null;
    private int _score;

    void Awake()
    {
        //Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit); // Not Used
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.AddListener(GameEvent.GAME_COMPLETE, OnGameComplete);
    }

    void OnDestroy()
    {
        //Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit); // Not Used
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.RemoveListener(GameEvent.GAME_COMPLETE, OnGameComplete);
    }


    void Start()
    {
        //_score = 0; // Not Used
        //scoreLabel.text = _score.ToString(); // Not Used
        //settingsPopup.Close(); // Not Used

        OnHealthUpdated();

        levelEnding.gameObject.SetActive(false);
        popup.gameObject.SetActive(false);
    }

    private void OnGameComplete()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "You Finished the Game!";
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            // bool isShowing = settingsPopup.gameObject.activeSelf;
            // settingsPopup.gameObject.SetActive(!isShowing);
            bool isShowing = popup.gameObject.activeSelf;
            popup.gameObject.SetActive(!isShowing);
            popup.Refresh();

            /* //Used for fps controls
            if(isShowing)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            */
        }
    }

    private void OnHealthUpdated()
    {
        string message = "Health: " + Managers.Player.health + "/" + Managers.Player.maxHealth;
        healthLabel.text = message;
    }

    private void OnLevelFailed() => StartCoroutine(FailLevel());
    private void OnLevelComplete() => StartCoroutine(CompleteLevel ());

    private IEnumerator CompleteLevel()
    { 
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "Level Complete!";

        yield return new WaitForSeconds(2);

        Managers.Mission.GoToNext();
    }

    private IEnumerator FailLevel()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "Level Failed";

        yield return new WaitForSeconds(2);

        Managers.Player.Respawn();
        Managers.Mission.RestartCurrent();
    }

    public void SaveGame()
    {
        Managers.Data.SaveGameState();
    }

    public void LoadGame()
    {
        Managers.Data.LoadGameState();
    }

    /* // used for fps game
    private void OnEnemyHit()
    {
        _score += 1;
        scoreLabel.text = _score.ToString();
    }

    public void OnOpenSettings() => settingsPopup.Open();
    public void OnPointerDown() => Debug.Log("pointer down");
    */
}
