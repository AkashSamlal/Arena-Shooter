using UnityEngine;
using UnityEngine.AI; 
using System.Collections;

public class Chase : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    Animator anim;

    string state = "patrol";
    public GameObject[] waypoints;
    int currentWP = 0;

    public float speed = 1.5f;
    public float runDist = 50f;
    public float attackDist = 5f;
    float accuracyWP = 5.0f;

    public GameObject Explosion;

    public AudioClip Hurt;
    

    public int currentHealth = 3;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        // HurtSource.clip = Hurt;
        

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;

        float angle = Vector3.Angle(direction, this.transform.forward);
        float distance = Vector3.Distance(this.transform.position, player.position);

        if (state == "patrol" && waypoints.Length > 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", true);

            if (Vector3.Distance(waypoints[currentWP].transform.position, this.transform.position) < accuracyWP)
            {

                currentWP = Random.Range(0, waypoints.Length);

            }

            //rotate towards waypoint
            agent.SetDestination(waypoints[currentWP].transform.position);


        }

        if (distance < runDist && (angle < 90 || state == "pursuing"))
        {

            state = "pursuing";
            agent.enabled = true;
            agent.SetDestination(player.transform.position);

            if (direction.magnitude > attackDist)
            {
                this.transform.Translate(0, 0, Time.deltaTime * speed);
                anim.SetBool("isRunning", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", false);

            }
            else
            {
                
                //stop the enemy from targeting the player position 
                agent.enabled = false;
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
            }

        }
        else
        {
            agent.enabled = true;
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);
            state = "patrol";
        }

    }
    /*IEnumerator sound()
    {
        HurtSource.Play();
        yield return new WaitForSeconds(25);
        
    }*/
    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            
            //      HurtSource.Play(1);
            GameObject pause = Instantiate(Explosion, this.transform.position, Quaternion.identity); //as GameObject;

            this.gameObject.SetActive(false);

            AudioSource.PlayClipAtPoint(Hurt, this.gameObject.transform.position);

            Destroy(pause, .5f);


            Destroy(this.gameObject);
           
        }
    }

  
}

