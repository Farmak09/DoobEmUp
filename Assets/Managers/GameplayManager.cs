using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    List<GameplayElement> gameElements = new();

    private WaveManager waves;
    public void AddElement(GameplayElement newElement)
    {
        gameElements.Add(newElement);
    }
    private void Awake()
    {
        waves = GetComponent<WaveManager>();
    }

    private void Start()
    {
        waves.LoadWave(Stage.Tutorial);
    }

    // Update is called once per frame
    void Update()
    {
        gameElements.ForEach(x => x.GameUpdate());
    }
}

public class GameplayElement : MonoBehaviour
{
    ServiceManager services;
    public virtual void Awake()
    {
        services = GameObject.FindGameObjectWithTag("ServiceProvider").GetComponent<ServiceManager>();
        AddSelfToListOfELements();
    }

    protected void AddSelfToListOfELements()
    {

        services.gameplay.AddElement(this);
    }

    public virtual void GameUpdate()
    {

    }
}