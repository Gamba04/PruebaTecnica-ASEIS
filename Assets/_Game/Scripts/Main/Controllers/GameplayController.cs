using System;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private HomeController homeController;

    [Space]
    [ReadOnly, SerializeField]
    private Texture2D image;

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

        image = homeController.GetImage();

        // Trigger BoxController

        homeController.Exit();
    }

    #endregion

}