using System.Linq;
using System.Collections.Generic;
using UnityEngine;




public class WaveManager : MonoBehaviour
{
    private List<HazardSpawner> hazards = new();
    private float spawnPosition = 6f;

    private void Awake()
    {
        hazards = Resources.LoadAll<HazardSpawner>("Wave Spawners").ToList();
    }

    public void LoadWave(Stage stage)
    {
        while (spawnPosition > -6f)
        {
            HazardSpawner chosenSpawner = hazards.Find(x => SelectValidSpawner(stage));

            chosenSpawner.SpawnWave(spawnPosition);

            spawnPosition = chosenSpawner.NextPositionDistance(spawnPosition);

            hazards.Remove(chosenSpawner);
        }
    }

    private HazardSpawner SelectValidSpawner(Stage stage)
    {
        List<HazardSpawner> validHazards = new();

        while(validHazards.Count() == 0)
        {
            int randomizedRarity = Random.Range(0, 100);
            
            Rarity rar = randomizedRarity < 60 ? Rarity.Common :
                         randomizedRarity < 90 ? Rarity.Uncommon :
                                                 Rarity.Rare;

            validHazards = hazards.Where(x => x.CheckValidity(rar, stage, 6f + spawnPosition)).ToList();

        }

        return ChooseNextWave(validHazards);
    }

    private HazardSpawner ChooseNextWave(List<HazardSpawner> options)
    {
        return options[Random.Range(0, options.Count())];
    }
}
