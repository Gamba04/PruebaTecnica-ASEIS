using System;
using System.Collections.Generic;
using UnityEngine;

public class AnalysisController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private uint colorsAmount = 3;

    private Texture2D image;

    #region Public Methods

    public void SetImage(Texture2D image)
    {
        this.image = image;
    }

    public void OnBtnExport()
    {
        string path = GetTargetPath();
        List<Color> colors = ImageAnalyzer.GetPredominantColors(image, colorsAmount);

        ColorExporter.ExportColors(path, colors);
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Other

    private string GetTargetPath()
    {
        string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        return WindowsFileDialogs.SaveFile("Save File", initialDirectory, new WindowsFileDialogs.Filter("CSV", "*.csv"));
    }

    #endregion

}