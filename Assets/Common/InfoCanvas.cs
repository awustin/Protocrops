using UnityEngine;
using TMPro;

public class InfoCanvas : MonoBehaviour, IPrintable
{
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _infoResources;

    void Awake()
    {
        _infoText.text = "";
        _infoResources.text = "";
    }

    public void Print(string text)
    {
        _infoText.text = text;
    }

    public void PrintResources(int resources)
    {
        _infoResources.text = resources.ToString();
    }
}