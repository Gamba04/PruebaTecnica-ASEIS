using System.Collections.Generic;
using UnityEngine;

public class AnalysisController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private uint colorsAmount = 3;

    [Space]
    [ReadOnly, SerializeField]
    private List<Color> predominantColors;

    private Texture2D image;

    #region Public Methods

    public void SetImage(Texture2D image)
    {
        this.image = image;
    }

    public void OnBtnExport()
    {
        predominantColors = ImageAnalyzer.GetPredominantColors(image, colorsAmount);
    }

    #endregion

}