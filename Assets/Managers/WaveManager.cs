using System.Linq;
using System.Collections.Generic;
using UnityEngine;




public class WaveManager : MonoBehaviour
{
    private List<HazardSpawner> hazards;
    // Start is called before the first frame update
    private void Awake()
    {
        hazards = Resources.LoadAll<HazardSpawner>("Wave Spawners").ToList();
    }

    public void LoadWave(Stage stage)
    {
        SelectValidSpawners(stage);
    }

    private List<HazardSpawner> SelectValidSpawners(Stage stage)
    {
        List<HazardSpawner> validHazards = new();

        while(validHazards.Count() == 0)
        {
            int randomizedRarity = Random.Range(0, 100);
            
            Rarity rar = randomizedRarity < 60 ? Rarity.Common :
                         randomizedRarity < 90 ? Rarity.Uncommon :
                                                 Rarity.Rare;

            validHazards = hazards.Where(x => x.CheckValidity(rar, stage)).ToList();
        }

        return validHazards;
    }
}
