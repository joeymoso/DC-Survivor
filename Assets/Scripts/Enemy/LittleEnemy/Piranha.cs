using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piranha : MonoBehaviour
{
    [SerializeField] private LittleEnemy littleEnemy;
    public GameObject PiranhaBullet;
    // public float startTimeBtwShots;
    // public float StartShootingDistance;
    // private float timeBtwShots;
    private float Distance2Player;
    private Transform player_transform;
    private bool Piranha_status = true;
    private Vector2 OriginalPosition;
    private float height;
    public float moveSpeed;
    public Transform groundDetectionDown;
    public Transform groundDetectionUp;
    public UnityEngine.Rendering.Universal.Light2D Light2D;
    private float ground_distance = 0.01f;
    public float appearDistance;
    public float godownDistance;
    private Animator animator;
    // private Golem golem;




    // Start is called before the first frame update
    private void Awake()
    {
        player_transform = GameObject.FindWithTag("Player").transform;
        // timeBtwShots = startTimeBtwShots;
        OriginalPosition = new Vector2(transform.position.x, transform.position.y);
        height = GetComponent<SpriteRenderer>().bounds.size.y;
        
        // Collider2D = GetComponent<Collider2D>();
        // damage = littleEnemy.damage;
    }

    // Update is called once per frame
    void Update()
    {
        Distance2Player = Vector2.Distance(transform.position, player_transform.position);
        // timeBtwShots = golem.CreateProjectiles(timeBtwShots, Piranha_status, Distance2Player, StartShootingDistance, startTimeBtwShots);
        // if(timeBtwShots <= 0 && Piranha_status == true && Distance2Player < StartShootingDistance)
        // {
        // Instantiate(PiranhaBullet, transform.position, Quaternion.identity);
        // Debug.Log("Shot bullets!!!!!!!!!!!!!!!!");
        // timeBtwShots = startTimeBtwShots;
        // }
        // else
        // {
        // timeBtwShots -= Time.deltaTime;
        // }
        if(Vector2.Distance(player_transform.position, transform.position) < appearDistance && Vector2.Distance(player_transform.position, transform.position) > godownDistance)
        {
            Appear();
        }
        if(Vector2.Distance(player_transform.position, transform.position) > appearDistance || Vector2.Distance(player_transform.position, transform.position) < godownDistance)
        {
            Godown();
        }
        if( player_transform.position.x > transform.position.x )
        {
            Godown();
        }
        
        
         
    }

    private void Appear()
    {
        int layer_mask_ground = LayerMask.GetMask("Ground");
        RaycastHit2D groundInfo_down = Physics2D.Raycast(groundDetectionDown.position, Vector2.down, ground_distance, layer_mask_ground);
        if(groundInfo_down.collider == true)
        {
            transform.position += transform.up * Time.deltaTime * moveSpeed;
        }
        Light2D.enabled = true;
    }

    private void Godown()
    {
        int layer_mask_ground = LayerMask.GetMask("Ground");
        RaycastHit2D groundInfo_up = Physics2D.Raycast(groundDetectionUp.position, Vector2.up, ground_distance, layer_mask_ground);
        if(groundInfo_up.collider == false)
        {
            transform.position += - transform.up * Time.deltaTime * moveSpeed;
        }
        Light2D.enabled = false;
    }

    private void CreateBullets()
    {
        if(player_transform.position.x < gameObject.transform.position.x)
        {
            Instantiate(PiranhaBullet, transform.position, Quaternion.identity);
        }
        
    }
}
