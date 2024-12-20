using System;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private BoxGraphics box;
    [SerializeField]
    private Animator cameraAnim;

    public event Action onComplete;

    private readonly int revealID = Animator.StringToHash("Reveal");

    #region Init

    public void Init()
    {
        InitEvents();

        box.Init();
    }

    private void InitEvents()
    {
        box.onComplete += onComplete;
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Public Methods

    public void Reveal(Texture2D image)
    {
        box.SetImage(image);

        cameraAnim.SetTrigger(revealID);
    }

    public void OnCameraComplete()
    {
        box.Reveal();
    }

    #endregion

}