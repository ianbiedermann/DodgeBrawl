using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    public GameObject BallPrefab;
    private float gameTime;
    public int spawnTimeMultiplier = 1;
    private float respawn_time;
    public float spawn_speed;


    void Start()
    {
        StartCoroutine(SpawnWave());
        gameTime = 0.0f;
    }

    private void Update()
    {
        gameTime = gameTime + Time.deltaTime;
    }


    IEnumerator SpawnWave()
    {

        while (true)
        {

            if(gameTime <= 30)
            {
                respawn_time = Random.Range(10, 30) / spawnTimeMultiplier;
                spawn_speed = 10.0f;
            }
            if ((gameTime > 30) && (gameTime <= 60))
            {
                respawn_time = Random.Range(9, 28) / spawnTimeMultiplier;
                spawn_speed = 12.0f;
            }
            if ((gameTime > 60) && (gameTime <= 90))
            {
                respawn_time = Random.Range(8, 26) / spawnTimeMultiplier;
                spawn_speed = 14.0f;
            }
            if ((gameTime > 90) && (gameTime <= 120))
            {
                respawn_time = Random.Range(7, 24) / spawnTimeMultiplier;
                spawn_speed = 16.0f;
            }
            if ((gameTime > 120) && (gameTime <= 150))
            {
                respawn_time = Random.Range(6, 22) / spawnTimeMultiplier;
                spawn_speed = 18.0f;
            }
            if ((gameTime > 150) && (gameTime <= 180))
            {
                respawn_time = Random.Range(5, 20) / spawnTimeMultiplier;
                spawn_speed = 20.0f;
            }
            if ((gameTime > 180) && (gameTime <= 210))
            {
                respawn_time = Random.Range(4, 18) / spawnTimeMultiplier;
                spawn_speed = 22.0f;
            }
            if ((gameTime > 210) && (gameTime <= 240))
            {
                respawn_time = Random.Range(3, 16) / spawnTimeMultiplier;
                spawn_speed = 24.0f;
            }
            if ((gameTime > 240) && (gameTime <= 270))
            {
                respawn_time = Random.Range(2, 14) / spawnTimeMultiplier;
                spawn_speed = 26.0f;
            }
            if ((gameTime > 270) && (gameTime <= 300))
            {
                respawn_time = Random.Range(1, 12) / spawnTimeMultiplier;
                spawn_speed = 28.0f;
            }
            if (gameTime > 300)
            {
                respawn_time = Random.Range(0, 10) / spawnTimeMultiplier;
                spawn_speed = 30.0f;
            }

            yield return new WaitForSeconds(respawn_time);
            spawnEnemy();
        }
    }


    private void spawnEnemy()
    {

        GameObject a = Instantiate(BallPrefab) as GameObject;
        Rigidbody2D rb = a.GetComponent<Rigidbody2D>();

        int spawn_random_number = Random.Range(0, 4);
        int spawn_position_x_1 = -11;
        int spawn_position_x_2 = 11;
        int spawn_position_y = Random.Range(0, 11);
        float spawn_speed_x;
        float spawn_speed_y;
        float cannons_angle = Random.Range(3.1415926f * 0.47f * (-1), 3.1415926f * 0.47f);          // Kosinus wird nicht in Grad sondern in pi hier angegeben; pi = 180 Grad. also gehts von knapp unter -90 bis +90 Grad


        spawn_speed_x = spawn_speed * (Mathf.Cos(cannons_angle));
        spawn_speed_y = spawn_speed * (Mathf.Sin(cannons_angle));


        if (spawn_random_number <= 1)
        {
            a.transform.position = new Vector2(spawn_position_x_1, spawn_position_y);
            rb.velocity = new Vector2(spawn_speed_x, spawn_speed_y);
        }
        
        else
        {
            a.transform.position = new Vector2(spawn_position_x_2, spawn_position_y);
            rb.velocity = new Vector2((-1) * spawn_speed_x, spawn_speed_y);
        }

        if( (rb.velocity.x == 0) && (rb.velocity.y == 0) )
        {
            Destroy(a);
        }
        
    }

}
