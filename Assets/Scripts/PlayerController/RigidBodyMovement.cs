using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodyMovement : BaseCharacterMovement
{
    [SerializeField] private float jumpForce = 5f; // Сила прыжка
    [SerializeField] private LayerMask groundLayer; // Слой земли
    private new Rigidbody rigidBody;
    private bool isGrounded;

    private void Start() => rigidBody = GetComponent<Rigidbody>();

    private void FixedUpdate() {
        rigidBody.MovePosition(transform.position + movementVector * movementSpeed * Time.fixedDeltaTime);
        CheckGround();
    }

    private void Update() {
        base.Update();
        if (Input.GetButtonDown("Jump") && isGrounded) {
            Jump();
        }
    }

    private void Jump() {
        rigidBody.linearVelocity = new Vector3(rigidBody.linearVelocity.x, jumpForce, rigidBody.linearVelocity.z);
    }

    private void CheckGround() {
        // Проверяем, стоит ли персонаж на земле с помощью Raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }
}

