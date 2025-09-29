using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float sprintSpeed = 6f;

    public Transform bitePoint;
    public float biteRange = 0.5f;
    public LayerMask biteableObjects;

    private Collider2D objInMouf;

    private Vector2 movement;
    private float speed;

    private Rigidbody2D rb;
    private Animator animator;

    private const string _horizontal = "horizontal";
    private const string _speed = "speed";
    private const string _lastHorizontal = "lastHorizontal";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize(); // make diagonal movement same as other movement

        if (movement.x != 0)
        {
            animator.SetFloat(_horizontal, movement.x);

            // move bite point
            Vector3 bitePos = bitePoint.localPosition;
            bitePos.x = Mathf.Abs(bitePos.x) * Mathf.Sign(movement.x);
            bitePoint.localPosition = bitePos;
        }
        animator.SetFloat(_speed, movement.sqrMagnitude);

        if (movement != Vector2.zero)
        {
            if (movement.x != 0)
            {
                animator.SetFloat(_lastHorizontal, movement.x);
            }
        }


        // zoom
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = moveSpeed;
        }

        // bite if player presses E
        if (Input.GetKeyDown(KeyCode.E) && objInMouf == null)
        {
            Bite();
        }
        else if (Input.GetKeyDown(KeyCode.E) && objInMouf != null)
        {
            Release();
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }


    private void Bite()
    {
        // check if there are biteable objects in range
        Collider2D biteObj = Physics2D.OverlapCircle(bitePoint.position, biteRange, biteableObjects);

        // bite them
        if (biteObj != null)
        {
            // set new parent
            biteObj.transform.SetParent(bitePoint);

            // new mouth pos 
            biteObj.transform.position = bitePoint.position;

            // this obj belongs to us now
            objInMouf = biteObj;
        }
    }

    private void Release()
    {
        objInMouf.transform.SetParent(null);
        objInMouf = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (bitePoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(bitePoint.position, biteRange);
    }
}
