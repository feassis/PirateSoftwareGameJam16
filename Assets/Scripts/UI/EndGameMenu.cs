using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI peopleKilledText;
    [SerializeField] private TextMeshProUGUI totalCostSavingsText;
    [SerializeField] private TextMeshProUGUI timeSpentText;
    [SerializeField] private TextMeshProUGUI totalOperationCostText;
    [SerializeField] private TextMeshProUGUI totalText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private string deathMessage = "Unsatisfatory result, sending an other useless robot";
    [SerializeField] private string successMessage = "Termination successful!";
    [SerializeField] private string failureMessage = "Proceed to your own termination";
    [SerializeField] private GameObject pnlPanel;

    [SerializeField] private GameObject timerUI;
    [SerializeField] private GameObject CrosshairUI;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
    }

    private void OnPlayAgainButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowEndGameMenu(bool hasDied, int peopleKilled, float savingsPerMurder, float timeSpent, float opCostPerSecond)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        if(hasDied)
        {
            pnlPanel.SetActive(false);
            messageText.text = deathMessage;
            gameObject.SetActive(true);
            return;
        }

        float totalSavings = savingsPerMurder * peopleKilled;
        float totalOperationCost = opCostPerSecond * timeSpent;

        peopleKilledText.text = peopleKilled.ToString();
        totalCostSavingsText.text = (totalSavings).ToString("C");
        timeSpentText.text = timeSpent.ToString("F2") + "s";
        totalOperationCostText.text = totalOperationCost.ToString("C");
        totalText.text = (totalSavings - totalOperationCost).ToString("C");

        if (totalSavings > totalOperationCost)
        {
            messageText.text = successMessage;
            messageText.color = Color.green;
        }
        else
        {
            messageText.text = failureMessage;
            messageText.color = Color.red;
        }

        gameObject.SetActive(true);
    }
}
