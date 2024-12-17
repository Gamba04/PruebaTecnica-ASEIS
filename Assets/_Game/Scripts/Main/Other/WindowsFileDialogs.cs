using System;
using System.IO;
using System.Runtime.InteropServices;

public static class WindowsFileDialogs
{

    #region Custom Data

    #region Comdlg32.dll

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct OPENFILENAME
    {
        public int lStructSize;
        public IntPtr hwndOwner;
        public IntPtr hInstance;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpstrFilter;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpstrCustomFilter;
        public int nMaxCustFilter;
        public int nFilterIndex;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpstrFile;
        public int nMaxFile;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpstrFileTitle;
        public int nMaxFileTitle;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpstrInitialDir;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpstrTitle;
        public uint Flags;
        public short nFileOffset;
        public short nFileExtension;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpstrDefExt;
        public IntPtr lCustData;
        public IntPtr lpfnHook;
        public IntPtr lpTemplateName;
        public IntPtr pvReserved;
        public int dwReserved;
        public int FlagsEx;

        private const int MAX_PATH_SIZE = 260;

        private const int OFN_EXPLORER = 0x00080000;
        private const int OFN_FILEMUSTEXIST = 0x00001000;
        private const int OFN_PATHMUSTEXIST = 0x00000800;
        private const int OFN_DONTADDTORECENT = 0x02000000;
        private const int OFN_NONETWORKBUTTON = 0x00020000;
        private const int OFN_OVERWRITEPROMPT = 0x00000002;
        private const int OFN_EXTENSIONDIFFERENT = 0x00000400;

        public OPENFILENAME(string title, string filter, string initialDirectory, string defaultFileName, string targetExtension)
        {
            char[] fileChars = new char[MAX_PATH_SIZE];

            if (defaultFileName != null) defaultFileName.CopyTo(0, fileChars, 0, defaultFileName.Length);

            this = new OPENFILENAME()
            {
                lpstrTitle = title,
                lpstrFilter = filter,
                lpstrInitialDir = initialDirectory,
                lpstrDefExt = targetExtension,
                Flags = OFN_EXPLORER | OFN_FILEMUSTEXIST | OFN_PATHMUSTEXIST | OFN_DONTADDTORECENT | OFN_NONETWORKBUTTON | OFN_OVERWRITEPROMPT | OFN_EXTENSIONDIFFERENT,
                lpstrFile = new string(fileChars),
                nMaxFile = MAX_PATH_SIZE,
                lStructSize = Marshal.SizeOf(typeof(OPENFILENAME))
            };
        }
    }

    [DllImport("Comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool GetOpenFileName(ref OPENFILENAME dialog);

    [DllImport("Comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool GetSaveFileName(ref OPENFILENAME dialog);

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Filter

    public struct Filter
    {
        public string title;
        public string[] rules;

        public Filter(string title, params string[] rules)
        {
            this.title = title;
            this.rules = rules;
        }

        public override string ToString()
        {
            string rules = string.Join(";", this.rules);

            return $"{title} ({rules})\0{rules}\0";
        }
    }

    #endregion

    #endregion

    private delegate bool FileDialogFunction(ref OPENFILENAME dialog);

    #region Public Methods

    public static string OpenFile(string title, string initialDirectory = null, params Filter[] filters)
    {
        return ShowFileDialog(title, initialDirectory, null, null, filters, GetOpenFileName);
    }

    public static string SaveFile(string title, string initialDirectory = null, string defaultFileName = null, string targetExtension = null, params Filter[] filters)
    {
        return ShowFileDialog(title, initialDirectory, defaultFileName, targetExtension, filters, GetSaveFileName);
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Other

    private static string ShowFileDialog(string title, string initialDirectory, string defaultFileName, string targetExtension, Filter[] filters, FileDialogFunction getFileName)
    {
        string directory = Directory.GetCurrentDirectory();

        try
        {
            string filter = GetFilterString(filters);

            OPENFILENAME dialog = new OPENFILENAME(title, filter, initialDirectory, defaultFileName, targetExtension);

            if (getFileName(ref dialog))
            {
                string path = dialog.lpstrFile;

                if (targetExtension != null) path = Path.ChangeExtension(path, targetExtension);

                return path;
            }

            return null;
        }
        catch
        {
            return null;
        }
        finally
        {
            Directory.SetCurrentDirectory(directory);
        }
    }

    private static string GetFilterString(Filter[] filters)
    {
        if (filters.Length == 0) filters = new Filter[] { new Filter("All Files", "*.*") };

        return string.Join("", filters) + "\0";
    }

    #endregion

}