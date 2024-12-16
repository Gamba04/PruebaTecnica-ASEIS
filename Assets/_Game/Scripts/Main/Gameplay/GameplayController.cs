using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private HomeController homeController;
    [SerializeField]
    private BoxController boxController;
    [SerializeField]
    private AnalysisController analysisController;

    #region Init

    private void Start()
    {
        InitEvents();

        homeController.Init();
        boxController.Init();
    }

    private void InitEvents()
    {
        homeController.onComplete += OnHomeComplete;
        boxController.onComplete += OnBoxComplete;
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Gameplay

    private void OnHomeComplete()
    {
        Button.Interactions = false;

        Texture2D image = homeController.GetImage();

        boxController.Reveal(image);
        analysisController.SetImage(image);

        homeController.Hide();
    }

    private void OnBoxComplete()
    {
        analysisController.Reveal();
    }

    #endregion

}