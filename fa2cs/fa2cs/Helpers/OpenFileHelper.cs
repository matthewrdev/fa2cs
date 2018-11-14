using System;
using System.IO;

namespace fa2cs.Helpers
{
    public static class OpenFileHelper
    {
        public static bool IsWindows => !(Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX);

        public static bool IsLinux => false;

        public static bool IsOsx => Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX;

        public static bool OpenAndSelect(string path)
        {
            if (IsOsx)
            {
                OpenAndSelectMac(path);
                return true;
            }
            else if (IsWindows)
            {
                OpenAndSelectWindows(path);
                return true;
            }

            return false;
        }

        public static void OpenAndSelectMac(string path)
        {
            bool openInsidesOfFolder = false;

            // try mac
            string macPath = path.Replace("\\", "/"); // mac finder doesn't like backward slashes

            if (Directory.Exists(macPath)) // if path requested is a folder, automatically open insides of that folder
            {
                openInsidesOfFolder = true;
            }

            if (!macPath.StartsWith("\""))
            {
                macPath = "\"" + macPath;
            }
            if (!macPath.EndsWith("\""))
            {
                macPath = macPath + "\"";
            }
            string arguments = (openInsidesOfFolder ? "" : "-R ") + macPath;
            //Debug.Log("arguments: " + arguments);
            try
            {
                System.Diagnostics.Process.Start("open", arguments);
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                // tried to open mac finder in windows
                // just silently skip error
                // we currently have no platform define for the current OS we are in, so we resort to this
                e.HelpLink = ""; // do anything with this variable to silence warning about not using it
            }
        }

        public static void OpenAndSelectWindows(string path)
        {
            bool openInsidesOfFolder = false;

            // try windows
            string winPath = path.Replace("/", "\\"); // windows explorer doesn't like forward slashes

            if (Directory.Exists(winPath)) // if path requested is a folder, automatically open insides of that folder
            {
                openInsidesOfFolder = true;
            }
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", (openInsidesOfFolder ? "/root," : "/select,") + winPath);
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                // tried to open win explorer in mac
                // just silently skip error
                // we currently have no platform define for the current OS we are in, so we resort to this
                e.HelpLink = ""; // do anything with this variable to silence warning about not using it
            }
        }
    }
}

