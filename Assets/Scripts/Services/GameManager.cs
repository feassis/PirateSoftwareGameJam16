using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameLevelService gameLevelService;
    private EventService eventService;

    private void Awake()
    {
        CreateServices();
        InitializeServices();
    }

    private void CreateServices()
    {
        eventService = new EventService();
    }

    private void InitializeServices()
    {
        gameLevelService.Init(eventService);
    }
}
