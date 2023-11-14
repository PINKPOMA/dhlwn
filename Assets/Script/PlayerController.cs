using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    [Header("PlayerInfo")]
    private Rigidbody2D rb;
    [SerializeField] private float hp;
    [SerializeField] private int score;
    [Space] 
    [Header("PlayerUI")] 
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameEndUI;
    [Header("MOVE")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxMovePos;
    [Space]
    [Header("JUMP")]
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float jumpForce = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
        hpText.text = $"HP: {hp}";
        
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float moveX = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -moveSpeed;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = moveSpeed;

        Vector2 newVelocity = new Vector2(moveX, rb.velocity.y);
        rb.velocity = newVelocity;

        float xPos = Mathf.Clamp(rb.position.x, -maxMovePos, maxMovePos);
        rb.position = new Vector2(xPos, rb.position.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bomb"))
        {
            Damaged(5);
        }


        if (other.CompareTag("Coin"))
        {
            score += 20;
            scoreText.text = $"Score: {score}";
        }

        Destroy(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.transform.CompareTag("Spike"))
        {
            Damaged(7.5f);
        }
        if (other.transform.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void Damaged(float damage)
    {
        hp -= damage;
        hpSlider.value = hp;
        hpText.text = $"HP: {hp}";
        if (hp <= 0)
        {
            gameEndUI.SetActive(true);
            gameEndUI.GetComponent<GameEndUI>().INITUI(score);
        }
    }
}
