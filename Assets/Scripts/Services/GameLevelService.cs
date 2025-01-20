using UnityEngine;

public class GameLevelService : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerSO playerSO;

    private PlayerController playerController;
    private EventService eventService;

    public void Init(EventService eventService)
    {
        this.eventService = eventService;
        PlayerModel playerModel = new PlayerModel(playerSO);
        playerController = new PlayerController(playerSO.PlayerViewPrefab, playerModel, spawnPoint, eventService);
    }
}
