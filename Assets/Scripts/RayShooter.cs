using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource = null;
    [SerializeField] private AudioClip hitWallSound = null;
    [SerializeField] private AudioClip hitEnemySound = null;

    private Camera Camera = null;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GetComponent<Camera>();

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 point = new Vector3(Camera.pixelWidth / 2, Camera.pixelHeight / 2, 0);
            Ray ray = Camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToHit();
                    soundSource.PlayOneShot(hitEnemySound);
                    Messenger.Broadcast(GameEvent.ENEMY_HIT);
                }
                else
                {
                    soundSource.PlayOneShot(hitWallSound);
                    StartCoroutine(SphereIndicator(hit.point)); 
                }
            }
        }
    }

    void OnGUI()
    {
        int size = 12;
        float posX = Camera.pixelWidth / 2 - size / 4;
        float posY = Camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
