using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private HomeController homeController;
    [SerializeField]
    private BoxController boxController;

    #region Init

    private void Start()
    {
        InitEvents();

        homeController.Init();
    }

    private void InitEvents()
    {
        homeController.onComplete += OnHomeComplete;
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Gameplay

    private void OnHomeComplete()
    {
        Button.Interactions = false;

        Texture2D image = homeController.GetImage();

        boxController.SetImage(image);
        homeController.Exit();
    }

    #endregion

}