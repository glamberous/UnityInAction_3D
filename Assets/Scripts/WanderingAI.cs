using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float actualSpeed = 3.0f;
    public float obstacleRange = 5.0f;
    private bool alive = true;
    [SerializeField] private GameObject fireballPrefab = null;
    private GameObject Fireball = null;
    public const float baseSpeed = 3.0f;

    public void SetAlive(bool state) => alive = state;

    public void Awake() => Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    public void OnDestroy() => Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    public void OnSpeedChanged(float speedMultiplier) => actualSpeed = baseSpeed * speedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            transform.Translate(0, 0, actualSpeed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (Fireball == null)
                    {
                        Fireball = Instantiate(fireballPrefab) as GameObject;
                        Fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        Fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }
}
