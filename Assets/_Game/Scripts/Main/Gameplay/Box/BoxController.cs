using UnityEngine;

public class BoxController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private BoxGraphics box;
    [SerializeField]
    private Animator cameraAnim;

    private readonly int revealID = Animator.StringToHash("Reveal");

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