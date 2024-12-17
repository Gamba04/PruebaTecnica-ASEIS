using UnityEngine;

public class Exit : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Button button;

    [Header("Settings")]
    [SerializeField]
    private float delay = 0.25f;

    public void OnBtnExit()
    {
        Button.Interactions = false;
        button.interactable = false;

        Timer.CallOnDelay(Application.Quit, delay, "Exit");
    }
}