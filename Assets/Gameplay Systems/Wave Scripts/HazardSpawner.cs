using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum Stage
{
    Tutorial,
    Extra
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare
}

public class HazardSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Obstacle> obstacles;

    public Stage biome;
    public Rarity rarity;


    public bool CheckValidity(Rarity rar, Stage stage)
    {
        return rarity == rar && biome == stage;
    }
}
