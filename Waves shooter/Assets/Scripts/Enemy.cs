using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] bool nonShooting = false;

    [Header("VFX settings")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    /*
    [SerializeField] Text damageDisplayText;
    [SerializeField] Canvas renderCanvas;
    */


    private SpriteRenderer spriteRenderer;
    private Material matDefault;
    private Material matWhite;

    [Header("SFX settings")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.1f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        // Getting materials and spriterenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        matDefault = spriteRenderer.material;
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime; // Time.deltaTime = the time that the frame takes every single frame
        if (shotCounter <= 0f && !nonShooting)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            projectile,
            transform.position,
            Quaternion.identity // takes care of rotation
            ) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            PlayExplosion();
        }
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        Debug.Log("Enemy hit");
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        spriteRenderer.material = matWhite;
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
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
        if (gameObject == GameObject.FindGameObjectWithTag("Boss"))
        {
            FindObjectOfType<Level>().LoadHighScore();
        }

        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        PlayExplosion();
        // DisplayDamage();
    }

    /*

       1. Attach Score Value to score text
       2. Instantiate ScoreDisplay gameobject when enemy is destroyed
       3. Destroy ScoreDisplay gameobject


  

    private void DisplayDamage()
    {
        Text tempTextBox = Instantiate(damageDisplayText, transform.position, transform.rotation) as Text;
        tempTextBox.transform.SetParent(renderCanvas.transform, false);
        //Set the text box's text element font size and style:
        tempTextBox.fontSize = 50;
        //Set the text box's text element to the current textToDisplay:
        tempTextBox.text = scoreValue.ToString();
    }

    */

    private void PlayExplosion()
    {
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}
