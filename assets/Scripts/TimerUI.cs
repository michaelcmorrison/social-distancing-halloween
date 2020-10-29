using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    private TMP_Text _text;
    [SerializeField] private Timer timer;
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        var minutes = Mathf.Floor(timer.time / 60).ToString("00");
        var seconds = Mathf.Floor(timer.time % 60).ToString("00");
        _text.text = $"{minutes}:{seconds}";
    }
    
}