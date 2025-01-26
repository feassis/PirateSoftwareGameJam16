using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameLevelService gameLevelService;
    private EventService eventService;

    public EventService EventService { get => eventService; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CreateServices();
        InitializeServices();
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
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
