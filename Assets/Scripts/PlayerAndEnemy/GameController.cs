using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Transform cam;
    [SerializeField] Transform spawnPlayer;
    [SerializeField] Transform[] spawnEnemy;
    [SerializeField] Player playerPrefab;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] EnemyMiddle enemyMiddlePrefab;
    [SerializeField] AudioSource agony;
    
    public int boneUi;
    public int killedUi;
    public int hpUi;

    public static Action onPlayerBone;
    public static Action onEnemyKilled;
    public static Action onPlayerHit;
    public static Action onPlayerHealt;
    public static Action onEnemySpawn;
    public static Action onEnemySpawnMiddle;

    Player player;
    Enemy enemy;
    EnemyMiddle enemyMiddle;

    int randomRange;
    int minId = 0;
    int maxId = 2;


    private void Start()
    {
        player = Instantiate(playerPrefab, spawnPlayer.position, Quaternion.identity);
        onEnemySpawn += PlayCorutine;
        onEnemySpawnMiddle += PlayCorutineMiddle;
        onPlayerBone += SetBone;
        onEnemyKilled += SetKilled;
        onPlayerHealt += SetHp;
        onPlayerHit += SetHpHit;
    }
    private void OnDestroy()
    {
        onEnemySpawn -= PlayCorutine;
        onEnemySpawnMiddle -= PlayCorutineMiddle;
        onPlayerBone -= SetBone;
        onPlayerHealt -= SetHp;
        onPlayerHit -= SetHpHit;
    }
    private void LateUpdate()
    {
        CameraFollow();
    }
    private void Update()
    {
        randomRange = UnityEngine.Random.Range(minId, maxId);
        hpUi = player.hp;
        player.PlayerShot();
        if (player.hp <= 0)
        {
            player.PlayerDead();
        }        
        if (enemyMiddle) 
        {
            if (enemyMiddle.hp <= 0)
            {
                enemyMiddle.EnemyMiddleDead();
            }
        }     
    }
    private void FixedUpdate()
    {
        player.PlayerRun();
        player.PlayerRotation();
    }
    void CameraFollow()
    {
        cam.transform.position = player.transform.position + offset;
    }
    
    void SpawnCountPlus()
    {
        minId += 2;
        maxId += 2;
    }
    #region Corutine
    IEnumerator Wait()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1.5f);
            EnemySpawn();
        }
        SpawnCountPlus();
        StopCoroutine(Wait());
    }
    IEnumerator WaitMiddle()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(3f);
            EnemySpawnMiddle();
        }
        SpawnCountPlus();
        StopCoroutine(WaitMiddle());
    }
    #endregion  
    #region EnemySpawn
    void EnemySpawn()
    {
        enemy = Instantiate(enemyPrefab, spawnEnemy[randomRange].position, Quaternion.identity);
    }
    void EnemySpawnMiddle()
    {
        enemyMiddle = Instantiate(enemyMiddlePrefab, spawnEnemy[randomRange].position, Quaternion.identity);
    }
    #endregion
    #region Corutine Call
    void PlayCorutine()
    {
        StartCoroutine(Wait());
    }
    void PlayCorutineMiddle()
    {
        StartCoroutine(WaitMiddle());
    }
    #endregion
    #region Actions
    void SetBone()
    {
        boneUi++;
    }
    void SetKilled()
    {
        killedUi++;
        agony.Play();
    }
    void SetHp()
    {
        player.hp += 50;
        if (player.hp >= 100)
        {
            player.hp = 100;
        }
    }
    void SetHpHit()
    {
        player.hp -= enemy.damage;
    }
    #endregion
}
