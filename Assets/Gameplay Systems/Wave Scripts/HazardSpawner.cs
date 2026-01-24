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

    public float size;
    public int density;

    public bool CheckValidity(Rarity rar, Stage stage, float remainingSpace)
    {
        return rarity == rar && biome == stage && size <= remainingSpace;
    }

    public virtual void SpawnWave(float position)
    {
        for(int i = density; i > 0; i--)
        {
            Instantiate(obstacles[Random.Range(0, obstacles.Count())], GetRandomPoint(position), Quaternion.identity);
        }
    }

    protected virtual Vector3 GetRandomPoint(float maxX)
    {
        return new Vector3(Random.Range(maxX - size, maxX),
                           0,
                           Random.Range(15f, 50f));
    }

    public virtual float NextPositionDistance(float currentPosition)
    {
        return currentPosition - size;
    }
}
