using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameLevelService gameLevelService;
    [SerializeField] private EndGameMenu endGameMenu;
    [SerializeField] private float savingsPerMurder = 500;
    [SerializeField] private float opCostPerSecond = 10;
    [SerializeField] private TextMeshProUGUI timerText;
    private EventService eventService;
    private EnemyManager enemyManager;

    private float levelTimer;

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
        InitializeGameRules();
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    private void Update()
    {
        levelTimer += Time.deltaTime;
        UpdateTimerTextUI();
    }

    private void CreateServices()
    {
        eventService = new EventService();
        enemyManager = new EnemyManager();
    }

    private void InitializeServices()
    {
        eventService.Init();
        gameLevelService.Init(eventService);
        enemyManager.Init(eventService);
    }

    private void InitializeGameRules()
    {
        eventService.OnAllEnemiesDead.AddListener(OnAllEnemiesDead);
        eventService.OnPlayerDeath.AddListener(OnPlayerDeath);
        levelTimer = 0;
    }

    private void OnPlayerDeath(PlayerController controller)
    {
        endGameMenu.ShowEndGameMenu(true, enemyManager.EnemyDeadCount, savingsPerMurder, levelTimer, opCostPerSecond);
    }

    private void OnAllEnemiesDead()
    {
        endGameMenu.ShowEndGameMenu(false, enemyManager.EnemyDeadCount, savingsPerMurder, levelTimer, opCostPerSecond);
    }

    private void UpdateTimerTextUI()
    {
        timerText.text = levelTimer.ToString("F2");
    }
}
