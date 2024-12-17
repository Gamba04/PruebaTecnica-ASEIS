using System;
using System.Collections.Generic;
using UnityEngine;

public class AnalysisController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Animator anim;

    [Header("Settings")]
    [SerializeField]
    private uint colorsAmount = 3;

    private Texture2D image;

    private readonly int revealID = Animator.StringToHash("Reveal");

    #region Public Methods

    public void SetImage(Texture2D image)
    {
        this.image = image;
    }

    public void Reveal()
    {
        anim.SetTrigger(revealID);
    }

    public void OnAnimComplete()
    {
        Button.Interactions = true;
    }

    public void OnBtnExport()
    {
        string path = GetTargetPath();

        if (path == null) return;

        List<Color> colors = ImageAnalyzer.GetPredominantColors(image, colorsAmount);

        ColorExporter.ExportColors(path, colors);
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Other

    private string GetTargetPath()
    {
        string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        return WindowsFileDialogs.SaveFile("Save File", initialDirectory, "Export.csv", "csv", new WindowsFileDialogs.Filter("CSV", "*.csv"));
    }

    #endregion

}