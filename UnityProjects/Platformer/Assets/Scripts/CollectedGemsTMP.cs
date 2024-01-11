using UnityEngine;
using TMPro;

public class CollectedGemsTMP : MonoBehaviour
{
    private int _gems = 0;
    private TextMeshProUGUI _gemsText;

    private void Start()
    {
        _gemsText = GetComponent<TextMeshProUGUI>();
        Events.GemCollected.AddListener(RecalculateGems);
    }

    private void RecalculateGems(Transform _)
    {
        _gems++;
        _gemsText.SetText($"Gems: {_gems}");
    }
}