using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private void Start()
    {
        var initializer = Object.FindFirstObjectByType<GameInitializer>();

        if (initializer != null)
        {
            var controller = initializer.GetController();
            controller.OnTimeChanged += UpdateTimer;
        }
    }

    private void UpdateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}