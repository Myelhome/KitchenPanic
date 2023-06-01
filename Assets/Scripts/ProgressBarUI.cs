using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private CuttingCounter counter;

    public void Start()
    {
        counter.OnProgressChanged += Counter_OnProgressChanged;
        counter.OnItemPut += Counter_OnItemPut;
        counter.OnItemTaken += Counter_OnItemTaken;

        Hide();
    }

    private void Counter_OnItemPut(object sender, EventArgs e)
    {
        image.fillAmount = 0f;
        Show();
    }

    private void Counter_OnItemTaken(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        Hide();
    }

    private void Counter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        image.fillAmount = e.progress;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
