using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    List<GameplayElement> gameElements = new();
    List<GameplayElement> elementsToRemove = new();

    private WaveManager waves;
    [SerializeField]
    private int cookies;
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
    private void LateUpdate()
    {
        if(elementsToRemove.Count() > 0)
        {
            while(elementsToRemove.Count() != 0)
            {
                gameElements.Remove(elementsToRemove[0]);
                Destroy(elementsToRemove[0].gameObject);
                elementsToRemove.RemoveAt(0);
            }
        }
    }
    public void RemoveElement(GameplayElement element)
    {
        Debug.Log(element);
        elementsToRemove.Add(element);
    }

    public void LoseCookie(ObstacleType type)
    {
        cookies--;
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

    protected void Vanish()
    {
        services.gameplay.RemoveElement(this);
    }
}