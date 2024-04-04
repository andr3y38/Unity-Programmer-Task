using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    private const float MOVE_SPEED = 10f;

    private new Rigidbody2D rigidbody2D;
    private Vector2 moveDir;
    private Animator animator;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W)) {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S)) {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A)) {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D)) {
            moveX = +1f;
        }

        moveDir = new Vector2(moveX, moveY).normalized;
        
        bool isIdle = moveX == 0 && moveY == 0;
        if (isIdle)
        {
            animator.SetBool("isMoving", false);
            rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("HorizontalMovement", moveDir.x);
            animator.SetFloat("VerticalMovement", moveDir.y);
        }
    }

    private void FixedUpdate() {
        rigidbody2D.velocity = moveDir * MOVE_SPEED;
    }

}
