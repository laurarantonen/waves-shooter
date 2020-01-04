using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] List<PowerUpWaveConfig> waveConfigs;
    int startingWave = 0;
    [SerializeField] bool looping = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);

    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return new WaitForSeconds(currentWave.GetStartDelay());
            yield return StartCoroutine(SpawnAllPowerUpsInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllPowerUpsInWave(PowerUpWaveConfig waveConfig)
    {
        for (int powerUpCount = 0; powerUpCount < waveConfig.GetNumberOfPowerUps(); powerUpCount++)
        {
            var newPowerUp = Instantiate(
            waveConfig.GetPowerUpPrefab(),
            waveConfig.GetWaypoints()[0].transform.position,
            Quaternion.identity);

            newPowerUp.GetComponent<PowerUpPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }

    }
}
