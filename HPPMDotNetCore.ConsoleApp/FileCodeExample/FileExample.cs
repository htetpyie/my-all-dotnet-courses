using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HPPMDotNetCore.ConsoleApp.FileCodeExample
{
    public class FileExample
    {
        public static void Run()
        {
            //Create Directory
            CreateDirectory("aa");
            DeleteEmptyDirectory("aa");
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        public static void DeleteEmptyDirectory(string path)
        {
            if (Directory.Exists(path)) Directory.Delete(path);
        }

        public static void DeleteDirectoryAndChildren(string path)
        {
            if (Directory.Exists(path)) Directory.Delete(path, true);
        }

        public static void MoveDirectory(string source, string destination)
        {
            if (Directory.Exists(source)) Directory.Move(source, destination);
        }

        public static void WriteToFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public static void WriteAllLines(string path, string[] contentArray)
        {
            File.WriteAllLines(path, contentArray);
        }

        public static string ReadAllTexts(string path)
        {
            string readAllText = default;
            if (File.Exists(path))
            {
                readAllText = File.ReadAllText(path);
            }
            return readAllText;
        }

        public static string[] ReadAllLines(string path)
        {
            string[] readAllLines = default;
            if (File.Exists(path))
            {
                readAllLines = File.ReadAllLines(path);
            }
            return readAllLines;
        }

        public static void MoveFile(string path, string moveToPath)
        {
            if (File.Exists(path))
            {
                if (File.Exists(moveToPath)) File.Delete(path);
                File.Move(path, moveToPath);
            }
        }

        public static void AppendAllText(string path, string content)
        {
            File.AppendAllText(path, content);
        }

        public static void AppendAllLines(string path, string[] content)
        {
            File.AppendAllLines(path, content);
        }

        public static void DeleteFile(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }
    }
}
