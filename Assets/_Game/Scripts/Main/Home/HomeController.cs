using System;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private ImageSelector imageSelector;
    [SerializeField]
    private ButtonDisabler startButton;
    [SerializeField]
    private Animator anim;

    public event Action onComplete;

    private readonly int hideID = Animator.StringToHash("Hide");

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

    public void Hide()
    {
        anim.SetTrigger(hideID);
    }

    #endregion

}