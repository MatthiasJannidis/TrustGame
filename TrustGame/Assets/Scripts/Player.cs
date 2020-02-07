using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator)), RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Hearts hearts;

    [Header("Movement")]
    [SerializeField] float speed = .0f;
    [SerializeField] AudioClip stepClip01 = null;
    [SerializeField] AudioClip stepClip02 = null;
    [SerializeField] AudioClip stepClip03 = null;
    [SerializeField] AudioSource stepSource = null;
    AudioClip[] stepClips = new AudioClip[3];
    float stepSpeed = 0.35f;
    float stepTimer = 0;

    [Header("Shooting")]
    [SerializeField] float shootTimer = .0f;
    [SerializeField] int bulletNumber = 0;
    [SerializeField] float bulletsSpread = .0f;
    [SerializeField] float bulletsSpeed = .0f;
    [SerializeField] GameObject bulletPrefab = null;
    [SerializeField] Transform bulletSpawnPoint = null;
    [SerializeField] Light muzzleFlash = null;
    [SerializeField] AudioSource shootSource = null;
    [SerializeField] AudioClip shootClip = null;


    Rigidbody2D rb;
    [Header("Flashlight")]
    [SerializeField] GameObject flashLight = null;

    const int bufferSize = 100;
    RingBuffer<GameObject> bulletBuffer = null;

    Animator animator;

    bool canShoot = false;
    bool shootTimeout = false;

    void Start()
    {
        
        stepClips[0] = stepClip01;
        stepClips[1] = stepClip02;
        stepClips[2] = stepClip03;
        //flashLight.SetActive(false);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        GameObject[] bullets = new GameObject[bufferSize];
        for(int i = 0; i < bufferSize; i++) 
        {
            bullets[i] = GameObject.Instantiate(bulletPrefab);
            bullets[i].SetActive(false);
        }
        bulletBuffer = new RingBuffer<GameObject>(bullets);
        bulletSpawnPoint.gameObject.SetActive(false);
    }

    void Update()
    {
        Vector2 moveDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) // up
            moveDirection.y += 1.0f;
        if (Input.GetKey(KeyCode.A)) // left
            moveDirection.x -= 1.0f;
        if (Input.GetKey(KeyCode.S)) // down
            moveDirection.y -= 1.0f;
        if (Input.GetKey(KeyCode.D)) // right
            moveDirection.x += 1.0f;

        if (Vector2.SqrMagnitude(moveDirection) > .001f)
        {
            if(stepSpeed <= stepTimer)
            {
                int stepSound = Random.Range(0, 2);
                stepSource.PlayOneShot(stepClips[stepSound]);
                stepTimer = 0;
            }
            stepTimer += Time.deltaTime;
            animator.speed = rb.velocity.magnitude * .1f;
        }
        else
        {
            animator.speed = .0f;
        }

        moveDirection.Normalize();
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, .0f) * speed * Time.deltaTime;
        
        
        

      

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        Vector3 lookAt = mousePos - transform.position;
        float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(.0f, .0f,angle);

        if (canShoot && !shootTimeout && Input.GetMouseButtonDown(0))
        {
            Shoot(lookAt);
        }
    }

    public void OnGunPickup()
    {
        bulletSpawnPoint.gameObject.SetActive(true);
        canShoot = true;
    }

    public void OnFlashlightPickup() 
    {
        flashLight.SetActive(true);
    }

    public void OnEnemyCollisionStay() 
    {
        hearts.IncrementHealth(Time.deltaTime);
    }

    void Shoot(Vector3 lookingAt)
    {
        for(int i = 0; i < bulletNumber; i++) 
        {
            Vector2 r = Random.insideUnitCircle;
            Vector2 shootingDir = new Vector2(lookingAt.x, lookingAt.y).normalized + r*bulletsSpread;
            GameObject g = bulletBuffer.getNextValue();
            g.transform.rotation = bulletSpawnPoint.rotation * Quaternion.Euler(.0f,.0f,180.0f);
            g.transform.position = new Vector3(bulletSpawnPoint.position.x, bulletSpawnPoint.position.y, transform.position.z);
            g.SetActive(true);
            var bulletRigidbody = g.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = shootingDir * bulletsSpeed;
        }
        shootTimeout = true;
        shootSource.PlayOneShot(shootClip);
        StartCoroutine(ShootTimer());
        StartCoroutine(HandleMuzzleFlash());
    }

    IEnumerator ShootTimer() 
    {
        yield return new WaitForSeconds(shootTimer);
        shootTimeout = false;
    }

    IEnumerator HandleMuzzleFlash() 
    {
        muzzleFlash.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        muzzleFlash.gameObject.SetActive(false);
    }
}
