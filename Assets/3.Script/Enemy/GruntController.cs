using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Patrol,
    Track,
    Attack,
    Dead
}

public class GruntController : MonoBehaviour
{
    [Header("State")]
    public State currentState;
    private int direction = 1;
    private bool canMove = true;
    private bool playerOut = false;

    // Components
    private Animator animator;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    // Timers
    private float timer = 0;
    private float walkTime = 3.0f;
    private float waitTime = 1.0f;

    [Header("Prefabs")]
    [SerializeField] private GameObject[] bloodPrefabs;
    [SerializeField] private GameObject smallBloodPrefab;

    [Header("LayerMask")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask slopeMask;

    [Header("AI")]
    [SerializeField] private GameObject sight;
    [SerializeField] private GameObject attackRange;
    [SerializeField] private GameObject alert;
    private GameObject player;
    private IEnumerator findCoroutine;
    private bool isAttack = false;

    // Slope
    private RaycastHit2D slopeHit;
    private bool isSlope = false;
    private Vector2 slopeNormalPerp;

    [Header("AttackHitBox")]
    [SerializeField] private Vector2 boxSize;

    private void Awake()
    {
        currentState = State.Idle;

        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!GameManager.instance.gameStart)
        {
            return;
        }

        isSlope = SlopeCheck();

        if (isSlope)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (currentState == State.Idle)
        {
            if (animator.GetBool("isWalk"))
            {
                animator.SetBool("isWalk", false);
            }

            if (timer < waitTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                currentState = State.Patrol;
            }
        }

        if (currentState == State.Patrol && canMove)
        {
            if (timer < walkTime)
            {
                Walk(direction);
                timer += Time.deltaTime;
            }
            else
            {
                ChangeDirection();
                timer = 0;
                currentState = State.Idle;
            }
        }

        if (currentState == State.Track)
        {
            int trackDirection = 0;

            if (player.transform.position.x < transform.position.x)
            {
                trackDirection = -1;
                sr.flipX = true;
            }
            else
            {
                trackDirection = 1;
                sr.flipX = false;
            }

            if (isSlope) // 경사로에서 이동
            {
                transform.position += 5.0f * Time.deltaTime * new Vector3(slopeNormalPerp.x * -trackDirection, slopeNormalPerp.y * -trackDirection, 0);
            }
            else
            {
                transform.position += 5.0f * Time.deltaTime * new Vector3(trackDirection, 0, 0);
            }
        }

        if (currentState == State.Attack)
        {
            if (isAttack)
            {
                return;
            }

            isAttack = true;
            StartCoroutine(Attack_co());
        }
    }

    public void PlayerInAttackRange()
    {
        currentState = State.Attack;
    }

    public void PlayerOutAttackRange()
    {
        playerOut = true;
    }

    private IEnumerator Attack_co()
    {
        canMove = false;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);

        AttackHitbox();
        yield return new WaitForSeconds(1.0f);

        isAttack = false;

        if (playerOut)
        {
            playerOut = false;
            animator.SetTrigger("Track");
            currentState = State.Track;
        }

        yield break;
    }

    private void AttackHitbox()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), boxSize, 0);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                if (!collider.GetComponent<PlayerController>().invincible && !collider.GetComponent<PlayerController>().isDead)
                {
                    collider.GetComponent<PlayerController>().Dead();

                    if (collider.transform.position.x <= transform.position.x)
                    {
                        collider.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10.0f, ForceMode2D.Impulse);
                    }
                    else
                    {
                        collider.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10.0f, ForceMode2D.Impulse);
                    }

                    animator.SetTrigger("Idle");
                }
            }
        }
    }

    private void OnDrawGizmos() // Attack Hitbox 그리기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y), boxSize);
    }

    public void FindPlayer(GameObject targetPlayer)
    {
        player = targetPlayer;
        findCoroutine = FindPlayer_co(targetPlayer);
        StartCoroutine(findCoroutine);
    }

    private IEnumerator FindPlayer_co(GameObject targetPlayer)
    {
        canMove = false;

        animator.SetBool("isWalk", false);

        animator.speed = 0f;

        if (targetPlayer.transform.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        alert.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        currentState = State.Track;

        canMove = true;

        animator.speed = 1.0f;

        animator.SetTrigger("Track");
    }

    public void Dead(Vector3 playerPosition)
    {
        if (currentState == State.Dead)
        {
            return;
        }

        currentState = State.Dead;

        if (findCoroutine != null)
        {
            StopCoroutine(findCoroutine);
        }

        alert.SetActive(false);

        GameManager.instance.GetScore();

        rb.AddForce(((transform.position - playerPosition).normalized) * 15.0f, ForceMode2D.Impulse);

        for (int i = 0; i < 10; i++)
        {
            float power = Random.Range(7.5f, 15.0f);

            GameObject smallBlood = Instantiate(smallBloodPrefab, transform.position, Quaternion.identity);
            smallBlood.GetComponent<Rigidbody2D>().AddForce(((transform.position - playerPosition).normalized + Vector3.up) * power, ForceMode2D.Impulse);
            Destroy(smallBlood, 2.0f);
        }

        StartCoroutine(Dead_co());
    }

    private IEnumerator Dead_co()
    {
        int num = Random.Range(0, 4);

        if (!GameManager.instance.isSlow)
        {
            Time.timeScale = 0.25f;
        }

        animator.speed = 1.0f;
        animator.SetTrigger("Dead");

        GameObject blood = Instantiate(bloodPrefabs[num], new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);

        if (rb.velocity.x < 0)
        {
            blood.GetComponent<SpriteRenderer>().flipX = true;
        }

        yield return new WaitForSeconds(0.05f);

        if (!GameManager.instance.isSlow)
        {
            Time.timeScale = 1.0f;
        }
    }

    private void Walk(int direction)
    {
        if (!animator.GetBool("isWalk"))
        {
            animator.SetBool("isWalk", true);
        }

        // -1: Left, 1: Right
        if (direction == -1)
        {
            sr.flipX = true;
            sight.transform.localPosition = new Vector3(-2, 0, 0);
            attackRange.transform.localPosition = new Vector3(-0.5f, 0, 0);
        }
        else
        {
            sr.flipX = false;
            sight.transform.localPosition = new Vector3(2, 0, 0);
            attackRange.transform.localPosition = new Vector3(0.5f, 0, 0);
        }
        
        //rb.velocity = new Vector2(direction * 2.0f, 0);
        transform.position += 2.0f * Time.deltaTime * new Vector3(direction, 0, 0);
    }

    private void ChangeDirection()
    {
        direction *= -1;
    }

    private bool SlopeCheck()
    {
        slopeHit = Physics2D.Raycast(transform.position, -Vector2.up, 2.0f, slopeMask);

        float slopeAngle = 0;

        RaycastHit2D temp = Physics2D.Raycast(transform.position, -Vector2.up, 2.0f, groundMask);

        if (slopeHit)
        {
            slopeNormalPerp = Vector2.Perpendicular(slopeHit.normal);
            slopeAngle = Vector2.Angle(slopeHit.normal, Vector2.up);

            Debug.DrawRay(slopeHit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(slopeHit.point, slopeHit.normal, Color.green);
        }
        else if (temp)
        {
            return false;
        }

        if (slopeAngle != 0 && (transform.position.y - slopeHit.point.y) < 1.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
