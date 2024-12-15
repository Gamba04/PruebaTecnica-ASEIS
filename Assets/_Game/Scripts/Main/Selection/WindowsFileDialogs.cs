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

        private const int OFN_EXPLORER = 0x00080000;
        private const int OFN_FILEMUSTEXIST = 0x00001000;
        private const int OFN_PATHMUSTEXIST = 0x00000800;
        private const int OFN_DONTADDTORECENT = 0x02000000;

        public OPENFILENAME(string title, string filter, string initialDirectory = null)
        {
            this = new OPENFILENAME();

            lpstrTitle = title;
            lpstrFilter = filter;
            lpstrInitialDir = initialDirectory;
            Flags = OFN_EXPLORER | OFN_FILEMUSTEXIST | OFN_PATHMUSTEXIST | OFN_DONTADDTORECENT;
            lpstrFile = new string(new char[256]);
            nMaxFile = lpstrFile.Length;
            lStructSize = Marshal.SizeOf(this);
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
        return ShowFileDialog(title, initialDirectory, filters, GetOpenFileName);
    }

    public static string SaveFile(string title, string initialDirectory = null, params Filter[] filters)
    {
        return ShowFileDialog(title, initialDirectory, filters, GetSaveFileName);
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Other

    private static string ShowFileDialog(string title, string initialDirectory, Filter[] filters, FileDialogFunction getFileName)
    {
        string directory = Directory.GetCurrentDirectory();

        try
        {
            string filter = GetFilterString(filters);

            OPENFILENAME dialog = new OPENFILENAME(title, filter, initialDirectory);

            if (getFileName(ref dialog))
            {
                return dialog.lpstrFile;
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