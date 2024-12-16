using System;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private ImageSelector imageSelector;
    [SerializeField]
    private ButtonDisabler startButton;

    public event Action onComplete;

    #region Init

    public void Init()
    {
        InitEvents();   
    }

    private void InitEvents()
    {
        imageSelector.onPathApplied += OnPathApplied;
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Events

    private void OnPathApplied()
    {
        startButton.SetDisabled(false);
    }

    public void OnBtnStart()
    {
        onComplete?.Invoke();
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Public Methods

    public Texture2D GetImage()
    {
        return imageSelector.GetImage();
    }

    public void Exit()
    {

    }

    #endregion

}