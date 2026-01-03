using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    List<GameplayElement> gameElements = new();

    public void AddElement(GameplayElement newElement)
    {
        gameElements.Add(newElement);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < gameElements.Count();i++)
        {
            gameElements[i].GameUpdate();
        }
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