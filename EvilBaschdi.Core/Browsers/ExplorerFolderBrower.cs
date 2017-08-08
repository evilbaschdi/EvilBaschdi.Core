using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Microsoft.Win32;

namespace EvilBaschdi.Core.Browsers
{
    /// <inheritdoc />
    /// <summary>
    ///     Stellt einen Auswahldialog für Ordner und Systemelemente ab Windows Vista bereit.
    /// </summary>
    [GeneratedCode("EvilBaschdi.Core", "1.0")]
    public sealed partial class ExplorerFolderBrowser : IFolderBrowser
    {
        #region Properties

        /// <inheritdoc />
        /// <summary>
        ///     Ruft den ausgewählten Ordnerpfad ab bzw. legt diesen fest.
        /// </summary>
        public string SelectedPath { get; set; }

        /// <summary>
        ///     Ruft den Anzeigenamen eines einzelnen, ausgewählten Elements ab.
        /// </summary>
        public string SelectedElementName { get; private set; }

        /// <summary>
        ///     Ruft ein Array mit Ordnerpfaden der ausgewählten Ordner ab.
        /// </summary>
        public string[] SelectedPaths { get; private set; }

        /// <summary>
        ///     Ruft ein Array mit den Namen der ausgewählten Elemente ab.
        /// </summary>
        public string[] SelectedElementNames { get; private set; }

        /// <summary>
        ///     Ruft einen Wert ab der angibt ob auch Elemente ausgewählt werden können, die keine Ordner sind oder legt diesen
        ///     fest.
        /// </summary>
        public bool AllowNonStoragePlaces { get; set; }

        /// <summary>
        ///     Ruft einen Wert ab der angibt ob mehrere Elemente ausgewählt werden können oder legt diesen fest.
        /// </summary>
        public bool Multiselect { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Zeigt den Auswahldialog an.
        /// </summary>
        /// <returns><c>true</c> wenn der Benutzer die Ordnerauswahl bestätigte; andernfalls <c>false</c></returns>
        public void ShowDialog()
        {
            ShowDialog(IntPtr.Zero);
        }

        /// <summary>
        ///     Zeigt den Auswahldialog an.
        /// </summary>
        /// <param name="owner">Der Besitzer des Fensters</param>
        /// <returns><c>true</c> wenn der Benutzer die Ordnerauswahl bestätigte; andernfalls <c>false</c></returns>
        public bool ShowDialog(Window owner)
        {
            return ShowDialog(owner == null ? IntPtr.Zero : new WindowInteropHelper(owner).Handle);
        }

        /// <summary>
        ///     Zeigt den Auswahldialog an.
        /// </summary>
        /// <param name="owner">Der Besitzer des Fensters</param>
        /// <returns><c>true</c> wenn der Benutzer die Ordnerauswahl bestätigte; andernfalls <c>false</c></returns>
        public bool ShowDialog(IWin32Window owner)
        {
            return ShowDialog(owner?.Handle ?? IntPtr.Zero);
        }

        /// <summary>
        ///     Zeigt den Auswahldialog an.
        /// </summary>
        /// <param name="owner">Der Besitzer des Fensters</param>
        /// <returns><c>true</c> wenn der Benutzer die Ordnerauswahl bestätigte; andernfalls <c>false</c></returns>
        public bool ShowDialog(IntPtr owner)
        {
            if (Environment.OSVersion.Version.Major < 6)
            {
                throw new InvalidOperationException("The dialog needs at least Windows Vista to work.");
            }

            var dialog = CreateNativeDialog();
            try
            {
                SetInitialFolder(dialog);
                SetOptions(dialog);

                if (dialog.Show(owner) != 0)
                {
                    return false;
                }

                SetDialogResults(dialog);

                return true;
            }
            finally
            {
                Marshal.ReleaseComObject(dialog);
            }
        }

        #endregion

        #region Helper

        private void GetPathAndElementName(IShellItem item, out string path, out string elementName)
        {
            item.GetDisplayName(Sigdn.Parentrelativeforaddressbar, out elementName);
            try
            {
                item.GetDisplayName(Sigdn.Filesyspath, out path);
            }
            catch (ArgumentException ex) when (ex.HResult == -2147024809)
            {
                path = null;
            }
        }

        private IFileOpenDialog CreateNativeDialog()
        {
            return new FileOpenDialog() as IFileOpenDialog;
        }

        private void SetInitialFolder(IFileOpenDialog dialog)
        {
            if (!string.IsNullOrEmpty(SelectedPath))
            {
                IntPtr idl;
                uint atts = 0;
                IShellItem item;
                if (NativeMethods.SHILCreateFromPath(SelectedPath, out idl, ref atts) == 0
                    && NativeMethods.SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, idl, out item) == 0)
                {
                    dialog.SetFolder(item);
                }
            }
        }

        private void SetOptions(IFileOpenDialog dialog)
        {
            dialog.SetOptions(GetDialogOptions());
        }

        private Fos GetDialogOptions()
        {
            var options = Fos.Pickfolders;
            if (Multiselect)
            {
                options |= Fos.Allowmultiselect;
            }
            if (!AllowNonStoragePlaces)
            {
                options |= Fos.Forcefilesystem;
            }
            return options;
        }

        private void SetDialogResults(IFileOpenDialog dialog)
        {
            IShellItem item;
            try
            {
                dialog.GetResult(out item);
                string path, value;
                GetPathAndElementName(item, out path, out value);
                SelectedPath = path;
                SelectedPaths = new[] { path };
                SelectedElementName = value;
                SelectedElementNames = new[] { value };
            }
            catch (COMException ex) when (ex.HResult == -2147418113)
            {
                IShellItemArray items;
                dialog.GetResults(out items);

                uint count;
                items.GetCount(out count);

                SelectedPaths = new string[count];
                SelectedElementNames = new string[count];

                for (uint i = 0; i < count; ++i)
                {
                    items.GetItemAt(i, out item);
                    string path, value;
                    GetPathAndElementName(item, out path, out value);
                    SelectedPaths[i] = path;
                    SelectedElementNames[i] = value;
                }

                SelectedPath = null;
                SelectedElementName = null;
            }
        }

        [ComImport]
        [Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IShellItem
        {
            void BindToHandler([In] [MarshalAs(UnmanagedType.Interface)] IntPtr pbc, [In] ref Guid bhid, [In] ref Guid riid, out IntPtr ppv);
            void GetParent([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);
            void GetDisplayName([In] Sigdn sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);
            void GetAttributes([In] uint sfgaoMask, out uint psfgaoAttribs);
            void Compare([In] [MarshalAs(UnmanagedType.Interface)] IShellItem psi, [In] uint hint, out int piOrder);
        }

        [ComImport]
        [Guid("B63EA76D-1F85-456F-A19C-48159EFA858B")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IShellItemArray
        {
            void BindToHandler([In] [MarshalAs(UnmanagedType.Interface)] IntPtr pbc, [In] ref Guid rbhid, [In] ref Guid riid, out IntPtr ppvOut);
            void GetPropertyStore([In] int flags, [In] ref Guid riid, out IntPtr ppv);
            void GetPropertyDescriptionList([In] [MarshalAs(UnmanagedType.Struct)] ref IntPtr keyType, [In] ref Guid riid, out IntPtr ppv);
            void GetAttributes([In] [MarshalAs(UnmanagedType.I4)] IntPtr dwAttribFlags, [In] uint sfgaoMask, out uint psfgaoAttribs);
            void GetCount(out uint pdwNumItems);
            void GetItemAt([In] uint dwIndex, [MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);
            void EnumItems([MarshalAs(UnmanagedType.Interface)] out IntPtr ppenumShellItems);
        }

        [ComImport]
        [Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CoClass(typeof(FileOpenDialog))]
        private interface IFileOpenDialog //: IFileDialog
        {
            [PreserveSig]
            int Show([In] IntPtr parent);

            void SetFileTypes([In] uint cFileTypes, [In] [MarshalAs(UnmanagedType.Struct)] ref IntPtr rgFilterSpec);
            void SetFileTypeIndex([In] uint iFileType);
            void GetFileTypeIndex(out uint piFileType);
            void Advise([In] [MarshalAs(UnmanagedType.Interface)] IntPtr pfde, out uint pdwCookie);
            void Unadvise([In] uint dwCookie);
            void SetOptions([In] Fos fos);
            void GetOptions(out Fos pfos);
            void SetDefaultFolder([In] [MarshalAs(UnmanagedType.Interface)] IShellItem psi);
            void SetFolder([In] [MarshalAs(UnmanagedType.Interface)] IShellItem psi);
            void GetFolder([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);
            void GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);
            void SetFileName([In] [MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);
            void SetTitle([In] [MarshalAs(UnmanagedType.LPWStr)] string pszTitle);
            void SetOkButtonLabel([In] [MarshalAs(UnmanagedType.LPWStr)] string pszText);
            void SetFileNameLabel([In] [MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
            void GetResult([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);
            void AddPlace([In] [MarshalAs(UnmanagedType.Interface)] IShellItem psi, FileDialogCustomPlace fdcp);
            void SetDefaultExtension([In] [MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);
            void Close([MarshalAs(UnmanagedType.Error)] int hr);
            void SetClientGuid([In] ref Guid guid);
            void ClearClientData();
            void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);
            void GetResults([MarshalAs(UnmanagedType.Interface)] out IShellItemArray ppenum);
            void GetSelectedItems([MarshalAs(UnmanagedType.Interface)] out IShellItemArray ppsai);
        }

        [ComImport]
        [Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")]
        private class FileOpenDialog
        {
        }

        private enum Sigdn : uint
        {
            Desktopabsoluteediting = 0x8004c000,
            Desktopabsoluteparsing = 0x80028000,
            Filesyspath = 0x80058000,
            Normaldisplay = 0,
            Parentrelative = 0x80080001,
            Parentrelativeediting = 0x80031001,
            Parentrelativeforaddressbar = 0x8007c001,
            Parentrelativeparsing = 0x80018001,
            Url = 0x80068000
        }

        [Flags]
        private enum Fos
        {
            Allnonstorageitems = 0x80,
            Allowmultiselect = 0x200,
            Createprompt = 0x2000,
            Defaultnominimode = 0x20000000,
            Dontaddtorecent = 0x2000000,
            Filemustexist = 0x1000,
            Forcefilesystem = 0x40,
            Forceshowhidden = 0x10000000,
            Hidemruplaces = 0x20000,
            Hidepinnedplaces = 0x40000,
            Nochangedir = 8,
            Nodereferencelinks = 0x100000,
            Noreadonlyreturn = 0x8000,
            Notestfilecreate = 0x10000,
            Novalidate = 0x100,
            Overwriteprompt = 2,
            Pathmustexist = 0x800,
            Pickfolders = 0x20,
            Shareaware = 0x4000,
            Strictfiletypes = 4
        }

        #endregion
    }
}