using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private bool EnemyDied = false;
    [SerializeField] private Animator Animate;
    private Score _score;
    public GameObject hitEffect;
    public GameObject floatingPoints;
    public AudioClip deadSound;
    

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        Animate = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        //Animate.SetTrigger("Hurt");
        currentHealth -= damage;
        Instantiate(floatingPoints, transform.position, Quaternion.identity);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        EnemyDied = true;
        Debug.Log("Enemy dead");
        StartCoroutine(WaitForDestroy());
        


        if (EnemyDied)
        {
            if (this.gameObject.tag == "WhiteGhost")
            {
                Debug.Log("WhiteGhost died");
                AudioSource.PlayClipAtPoint(deadSound, transform.position);
            }
            else if (this.gameObject.tag == "RedGhost")
            {
                Debug.Log("RedGhost died");
                AudioSource.PlayClipAtPoint(deadSound, transform.position);
            }
            else if (this.gameObject.tag == "BlueGhost")
            {
                Debug.Log("BlueGhost died");
                AudioSource.PlayClipAtPoint(deadSound, transform.position);
            }
        }
    }

    private IEnumerator WaitForDestroy()
    {
        Animate.SetBool("isDead", true);
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}