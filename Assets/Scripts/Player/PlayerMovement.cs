using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Body parts")]
    [Tooltip("Only the upper body of the character")]
    [SerializeField] private GameObject[] topPlayerParts;
    [SerializeField] private GameObject leftLeg; //дальн€€ нога
    [SerializeField] private GameObject rightLeg; //ближн€€ нога
    [SerializeField] private Tail tail;

    [Header("Movement")]
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float stepWait = 0.5f;
    [SerializeField] private float jumpForce = 10f;
    private bool isPlayerGoLeft;

    [Header("Jumping")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private float positionRadius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Rigidbody2D bodyRb;
    private bool isOnGround;

    private Animator animator;
    private AudioSource audioSource;

    private Rigidbody2D leftLegRB;
    private Rigidbody2D rightLegRB;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        leftLegRB = leftLeg.GetComponent<Rigidbody2D>();
        rightLegRB = rightLeg.GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        // ƒвижение персонажа переделать
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isRight", true);
                TurnBodyToRight();
                StartCoroutine(MoveRight(stepWait));
            }
            else
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isRight", false);
                TurnBodyToLeft();
                StartCoroutine(MoveLeft(stepWait));
            }
        }
        else
        {
            if (isPlayerGoLeft)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isRight", false);
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isRight", true);
            }
        }
    }

    public void Jump()
    {
        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);

        if (isOnGround && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            bodyRb.AddForce(Vector2.up * jumpForce * 100f);

            if (audioSource != null)
            {
                audioSource.PlayOneShot(jumpClip);
            }
        }
    }

    IEnumerator MoveRight(float seconds)
    {
        leftLegRB.AddForce(Vector2.right * (speed * 1000) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        rightLegRB.AddForce(Vector2.right * (speed * 1000) * Time.deltaTime);
    }

    IEnumerator MoveLeft(float seconds)
    {
        rightLegRB.AddForce(Vector2.left * (speed * 1000) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        leftLegRB.AddForce(Vector2.left * (speed * 1000) * Time.deltaTime);
    }

    private void TurnBodyToRight()
    {
        if (!isPlayerGoLeft) //мы уже идем вправо
            return;

        InverseBodyParts();

        isPlayerGoLeft = false;
    }

    private void TurnBodyToLeft()
    {
        if (isPlayerGoLeft) //мы уже идем влево
            return;

        InverseBodyParts();

        isPlayerGoLeft = true;
    }

    private void InverseBodyParts()
    {
        foreach (var part in topPlayerParts) //весь вверх
        {
            InversePart(part);
        }

        InversePart(leftLeg);
        InversePart(rightLeg);
    }

    private void InversePart(GameObject part)
    {
        part.transform.localScale = new Vector3(part.transform.localScale.x * (-1), part.transform.localScale.y, part.transform.localScale.z);
    }
  
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(playerPos.position, positionRadius);
    }
}
