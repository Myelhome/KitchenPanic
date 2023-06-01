using UnityEngine;

public class CounterHightlightScript : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjects;

    private void Start()
    {
        PlayerScript.Instance.OnSelectedCounterChanged += PlayerScript_OnSelectedCounterChanged;
    }

    private void PlayerScript_OnSelectedCounterChanged(object sender, PlayerScript.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (var visualGameObject in visualGameObjects)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (var visualGameObject in visualGameObjects)
        {
            visualGameObject.SetActive(false);
        }
    }
}
