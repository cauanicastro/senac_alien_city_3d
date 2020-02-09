using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Math;

[System.Serializable]
public class GameManager
{
    [Header("Player props")]
    public GameObject player;
    public PlayerController playerController;
    public Transform playerPos; 
    public float playerHealth = 100;
    public float playerMaxHealth = 100;
    public float playerLives = 5;
    public float playerGems = 0;
    public List<KeyScOb> playerKeys = new List<KeyScOb>();

    [Header("Stage 1 props")]
    public DoorController lockedDoor;

    private static GameManager instance;

    private GameManager() { }

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }

    public void KeyFind(KeyScOb key)
    {
        playerKeys.Add(key);
        KeyCheck();
    }

    private void KeyCheck()
    {

        if (playerKeys.Exists(key => key.colour.Equals("red")) &&  playerKeys.Exists(key => key.colour.Equals("blue")))
        {
            lockedDoor.Open();
        }
    }

    public void DealDamage(float amount)
    {
        playerHealth = Max(playerHealth - amount, 0);
        if (playerHealth == 0)
        {
            LifeDecrease();
        }
    }

    public void DealRecover(float amount)
    {
        playerHealth = Min(playerHealth + amount, playerMaxHealth);
    }

    public void LifeIncrease()
    {
        ++playerLives;
    }

    public void LifeDecrease()
    {
        --playerLives;
        //TODO: check if lives are under 0
        playerController.StartCoroutine(RestartScene());
    }

    private IEnumerator RestartScene()
    {
        playerController.isDead = true;
        player.GetComponent<Animator>().SetTrigger("Morre");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        playerHealth = playerMaxHealth;
        playerGems = 0;
        playerKeys = new List<KeyScOb>();
    }

    private void GameOver()
    {
        //TODO: implement gameover logic
    }
}
