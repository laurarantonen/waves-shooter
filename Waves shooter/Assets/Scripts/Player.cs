using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    // config params
    [Header("Player movement")]
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] int health = 200;

    [Header("Projectile settings")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header("VFX settings")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;

    [SerializeField] GameObject healthUpVFX;
    [SerializeField] float durationOfAnimation= 1f;

    [Header("SFX settings")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.1f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.2f;

    private SpriteRenderer spriteRenderer;
    private Material matDefault;
    private Material matWhite;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;

    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        spriteRenderer = GetComponent<SpriteRenderer>();
        matDefault = spriteRenderer.material;
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("PowerUp"))
        {
            health += 100;
            PlayHealthUpAnimation();
            Destroy(other.gameObject);
        }
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {

        spriteRenderer.material = matWhite;

        health -= damageDealer.GetDamage();

        if (damageDealer.gameObject != GameObject.FindGameObjectWithTag("Boss"))
        {
            damageDealer.Hit();
        }

        if (health <= 0)
        {
            Die();
        }
        else
        {
            Invoke("ResetMaterial", .1f);
        }
    }

    void ResetMaterial()
    {
        spriteRenderer.material = matDefault;
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        PlayExplosion();
    }

    private void PlayHealthUpAnimation()
    {
        GameObject animation = Instantiate(healthUpVFX, transform.position, transform.rotation);
        Destroy(animation, durationOfAnimation);
    }

    private void PlayExplosion()
    {
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }


    public int GetHealth()
    {
        return health;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1")){

            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(
                           laserPrefab,
                           transform.position,
                           Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
        
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp (transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Vector2 size = GetComponent<Renderer>().bounds.size;

        float halfWidth = size.x * 0.5f;
        float halfHeight = size.y * 0.5f;

        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + halfWidth;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - halfWidth;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + halfHeight;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - halfHeight;
    }

}
