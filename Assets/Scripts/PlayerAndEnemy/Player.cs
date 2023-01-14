using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameController;
using System;

public class Player : Data
{
    [SerializeField] Joystick joystick;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform spawnBullet;
    [SerializeField] AudioSource shot;
    //[SerializeField] private DialogueTrigger dialogueTrigger;
    
    Bullet bullet;
    Vector3 mousePosition;
    Vector2 moveInput;
    Vector2 moveVelocity;
    Vector3 diference;
    //RaycastHit2D hit;

    public ControlType controlType;
    public enum ControlType { PC, Android }

    private void Update()
    {
        /*hit = Physics2D.Raycast(GetComponent<Rigidbody2D>().position + Vector2.up, transform.up, 5f, LayerMask.GetMask("Door"));
        Debug.DrawRay(transform.position, transform.up * 5f, Color.yellow);
        if (hit.collider)
        {
            dialogueTrigger.NPCDialog();
        }
        else
        {
            Debug.DrawRay(transform.position, transform.up * 5f, Color.green);
        }
        */
        InputPlayerMove();
    }
    void InputPlayerMove()
    {
        if (controlType == ControlType.PC)
        {
            joystick.enabled = false;
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else if (controlType == ControlType.Android)
        {
            joystick.enabled = true;
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        }
        moveVelocity = moveInput.normalized * speed;
    }
    public void PlayerRun()
    {
        rb.velocity = moveVelocity;
    }
    public void PlayerRotation()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        diference = mousePosition - transform.position;
        diference.Normalize();
        float rotationZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }
    public void PlayerDead()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayerShot()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            shot.Play();
            bullet = Instantiate(bulletPrefab, spawnBullet.position, Quaternion.identity);
            bullet.rb.AddForce(diference * 20f, ForceMode2D.Impulse);
        }      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Bone bone))
        {
            GameController.onPlayerBone?.Invoke();
            Destroy(bone.gameObject);
        }
        if (collision.gameObject.TryGetComponent(out Healt healt))
        {
            GameController.onPlayerHealt?.Invoke();
            Destroy(healt.gameObject);
        }
        if(collision.gameObject.tag == "SpawnEnemy")
        {
            GameController.onEnemySpawn?.Invoke();
        }
        if (collision.gameObject.tag == "SpawnEnemyMiddle")
        {
            GameController.onEnemySpawnMiddle?.Invoke();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            GameController.onPlayerHit?.Invoke();
        }       
    }
}
