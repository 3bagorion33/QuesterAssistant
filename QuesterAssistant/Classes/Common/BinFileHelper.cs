using Astral;
using QuesterAssistant.Panels;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32.SafeHandles;

namespace QuesterAssistant.Classes.Common
{
    public static class BinFileHelper
    {
        private static readonly BinaryFormatter binaryFormatter = new BinaryFormatter { AssemblyFormat = FormatterAssemblyStyle.Simple };
        private const uint GenericRead  = 0x80000000;
        private const uint GenericWrite = 0x40000000;

        public static void Save (object o, string s)
        {
            using (FileStream fileStream = new FileStream(s, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                try
                {
                    binaryFormatter.Serialize(fileStream, o);
                }
                catch (Exception ex)
                {
                    QMessageBox.ShowError(ex.ToString());
                    Logger.WriteLine(ex.ToString());
                }
            }
        }

        public static T Load<T> (string s)
        {
            T result;
            using (FileStream fileStream = new FileStream(s, FileMode.OpenOrCreate))
            {
                try
                {
                    result = (T)binaryFormatter.Deserialize(fileStream);
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(ex.ToString());
                    result = Activator.CreateInstance<T>();
                }
            }
            return result;
        }

        public static bool Delete(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
            bool result = DeleteFile(fileName);
            if (!result)
                ThrowLastIOError(fileName);
            return result;
        }

        public static bool FileExists(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
            return GetFileAttributes(fileName) != -1;
        }

        private static void ThrowLastIOError(string fileName)
        {
            int win32error = Marshal.GetLastWin32Error();
            Marshal.ThrowExceptionForHR(win32error);
            switch (win32error)
            {
                case 2:
                    throw new FileNotFoundException("Failed to open file with specified stream", fileName);
                default:
                    throw new IOException("Failed to open file with specified stream");
            }
        }

        public static FileStream OpenWithStream(string fileName, FileMode mode, FileAccess access)
        {
            return OpenWithStream(fileName, mode, access, FileShare.None);
        }

        public static FileStream OpenWithStream(string fileName, FileMode mode, FileAccess access, FileShare share)
        {
            uint desiredAccess = (access & FileAccess.Read) == FileAccess.Read ? GenericRead : 0;
            desiredAccess |= (access & FileAccess.Write) == FileAccess.Write ? GenericWrite : 0;

            SafeFileHandle fileHandle = CreateFile(
                fileName,
                desiredAccess,
                share,
                IntPtr.Zero,
                mode,
                FileAttributes.Normal,
                IntPtr.Zero
            );
            if (fileHandle.IsInvalid)
                ThrowLastIOError(fileName);
            return new FileStream(fileHandle, access);
        }

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern SafeFileHandle CreateFile(
            string fileName,
            uint desiredAccess,
            [MarshalAs(UnmanagedType.U4)] FileShare share,
            IntPtr lpSecurityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
            IntPtr hTemplateFile
        );
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteFile(string name);
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetFileAttributes(string fileName);
    }
}
