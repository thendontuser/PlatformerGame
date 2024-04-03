using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private int Speed;
    [SerializeField] private int Lives;
    [SerializeField] private int JumpForce;
    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool IsGrounded;

    // Start is called before the first frame update
    void Start()
    {
        IsGrounded = false;
        Speed = 1;
        Lives = 1;
        JumpForce = 1;
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Speed * Time.deltaTime);
        sprite.flipX = direction.x < 0;
    }

    void Jump()
    {
        rigidbody.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        IsGrounded = colliders.Length > 1;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
        if (IsGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
}