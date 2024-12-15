using System;
using UnityEngine;
using UnityEngine.UI;

public class FileSelector : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Text text;

    private string path;

    public string Path => path;

    #region Public Methods

    public void SelectFile()
    {
        string path = GetFilePath();

        if (path != null) ApplyPath(path);
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Other

    private string GetFilePath()
    {
        string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        return WindowsFileDialogs.OpenFile("Select Image", initialDirectory, new WindowsFileDialogs.Filter("Image Files", "*.jpg", "*.png"));
    }

    private void ApplyPath(string path)
    {
        this.path = path;

        text.text = path;
    }

    #endregion

}