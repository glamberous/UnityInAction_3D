using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab = null;
    private GameObject Enemy = null;
    private float speedMultiplier = 1.0f;
    private WanderingAI wanderingAI = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Awake() => Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    public void OnDestroy() => Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    private void OnSpeedChanged(float value) => speedMultiplier = value;


    // Update is called once per frame
    void Update()
    {
        if (Enemy == null)
        {
            Enemy = Instantiate(enemyPrefab) as GameObject;
            Enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 360);
            Enemy.transform.Rotate(0, angle, 0);
            wanderingAI = Enemy.GetComponent<WanderingAI>();
            wanderingAI.OnSpeedChanged(speedMultiplier);
        }
    }
}
