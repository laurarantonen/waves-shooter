using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Power Up Wave Config")]
public class PowerUpWaveConfig : ScriptableObject
{
    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] int numberOfPowerUps = 1;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float startDelay = 0f;


    public GameObject GetPowerUpPrefab(){ return powerUpPrefab; }

    public List<Transform> GetWaypoints() 
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public int GetNumberOfPowerUps() { return numberOfPowerUps; }
    public float GetMoveSpeed() { return moveSpeed; }
    public float GetStartDelay() { return startDelay; }
}
