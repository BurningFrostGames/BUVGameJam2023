using System;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    #region Public vars
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public bool shouldRotate;
    public LayerMask whatIsPlayer;
    public Vector3 dir; 
    public int _damage;
    public bool isInAttackRange;
    #endregion Public vars

    #region Private vars
    [SerializeField] private bool isInChaseRange;
    [SerializeField] private bool isInMeeleRange;
    [SerializeField] private Animator anim;
    private Transform target;
    private Rigidbody2D self;
    private Vector2 movement;
    #endregion Private vars

    // Start is called before the first frame update
    private void Start()
    {
        self = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        //anim.SetBool("isRunning", isInChaseRange);

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if (shouldRotate)
        {
            /*anim.SetFloat("X", dir.x);
            anim.SetFloat("Y", dir.y);*/
        }
    }

    private void FixedUpdate()
    {
        if (isInChaseRange && isInAttackRange)
        {
            MoveCharacter(movement);
            
        }
        if (isInAttackRange)
        {
            self.velocity = Vector2.zero;
            //AudioSource.PlayClipAtPoint(shootingSound, transform.position);
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        self.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "WhiteGhost")
            {
                Debug.Log("TouchPlayer");
                Destroy(this.gameObject);
            }

            if (this.gameObject.tag == "RedGhost")
            {
                Debug.Log("PlayerTouched");
                StartCoroutine(WaitforExplode());
            }

            if (this.gameObject.tag == "BlueGhost")
            {
                Debug.Log("PlayerTouched");
            }
        }
        
    }
    private IEnumerator WaitforExplode()
    {
        //anim.SetBool("isDead", true);
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Explodddddd");
        Destroy(this.gameObject);
    }
}