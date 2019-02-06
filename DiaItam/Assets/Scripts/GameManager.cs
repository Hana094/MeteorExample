using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas StartMenu;
    public Camera viewCamera;
    public Player player;

    public GameObject meteorPrefab;
    //
    public Vector2 ScreenHalfSizeWorldUnits;

    public static GameManager instance;

    Coroutine timerRoutine = null;

    bool stop = false;

    float nextSpawnTime;

    public Vector2 timeBetweenSpawnMaxMin;

    public float secondsToMaxDifficulty;

    Vector3 spawnPosition = new Vector3(0,0,0);

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);


        //Player.gameObject.SetActive(false);

        viewCamera = Camera.main;

        //set the world units
        ScreenHalfSizeWorldUnits = new Vector2(viewCamera.aspect * viewCamera.orthographicSize, viewCamera.orthographicSize);
        //StartGame();
    }

    // Update is called once per frame
    public void StartGame()
    {
        stop = false;
        player.ResetPlayer();
        if (timerRoutine != null)
        {
            StopCoroutine(timerRoutine);
        }
        timerRoutine = StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        nextSpawnTime = 0;
        float secondsBetweenSpawns = 0;
        float timePassed = 0;
        stop = false;
        while (!stop)
        {
            if (timePassed >= nextSpawnTime)
            {
                spawnPosition = Vector3.right* (ScreenHalfSizeWorldUnits.x+1) + Vector3.up* Random.Range( -ScreenHalfSizeWorldUnits.y, ScreenHalfSizeWorldUnits.y);
                secondsBetweenSpawns = Mathf.Lerp(timeBetweenSpawnMaxMin.x, timeBetweenSpawnMaxMin.y, timePassed / secondsToMaxDifficulty);
                Instantiate(meteorPrefab,spawnPosition,Quaternion.identity,null);
                nextSpawnTime = timePassed + secondsBetweenSpawns;
            }
            timePassed += Time.deltaTime;
            yield return null;
        }
    }

    public void KillPlayer()
    {
        StartMenu.gameObject.SetActive(true);
        stop = true;
        player.Die();
    }
}
