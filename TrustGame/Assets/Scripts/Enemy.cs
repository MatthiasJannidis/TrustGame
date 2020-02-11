using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    static int layer = 1;


    Player player = null;
    Rigidbody2D rb = null;
    float speed = 100.0f;
    [SerializeField] GameObject deadEnemy;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        GetComponent<SpriteRenderer>().sortingOrder = layer;
        layer++;
        if (layer > 19) layer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 toPlayer = player.transform.position.xy() - transform.position.xy();

        rb.velocity = toPlayer.normalized * Time.deltaTime * speed;

        float angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(.0f, .0f, angle+90.0f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<Player>().OnEnemyCollisionStay();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) 
        {
            GameObject obj = Instantiate(deadEnemy, transform.position, transform.rotation*Quaternion.Euler(.0f,.0f,-90.0f));
            obj.GetComponent<SpriteRenderer>().sortingOrder = layer-19;
            layer++;
            if (layer > 19) layer = 0;

            Destroy(gameObject);
        }
    }
}
