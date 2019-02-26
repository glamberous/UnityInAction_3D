using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]

public class FPSMovementController : MonoBehaviour
{
    private CharacterController charController;
    public const float baseSpeed = 6.0f;
    public float actualSpeed = 6.0f;
    public float gravity = -9.8f;

    public void Awake() => Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    public void OnDestroy() => Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    private void OnSpeedChanged(float speedMultiplier) => actualSpeed = baseSpeed * speedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * actualSpeed;
        float deltaZ = Input.GetAxis("Vertical") * actualSpeed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, actualSpeed);
        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);

    }
}
