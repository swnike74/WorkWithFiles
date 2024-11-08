using System;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime;
using static System.Net.Mime.MediaTypeNames;

namespace Task1
{
    internal class Program
    {
        /// <summary>
        /// программа, которая чистит нужную нам папку от файлов  и папок, которые не использовались более 30 минут 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Now;
            TimeSpan timeSpan = TimeSpan.FromMinutes(30);

            Console.WriteLine("Task 1 - Enter full path for directory delete:");
            string FolderPath = Console.ReadLine();

            if (Directory.Exists(FolderPath))
            {
                Console.WriteLine("Процесс пошел...");
                var dirinfo = new DirectoryInfo(FolderPath);
                int count = 0;

                if (dirinfo.Exists)
                {
                    while (DelFiles(dirinfo))
                    {
                        count++;
                    }
                }
            }
            else
            {
                Console.WriteLine("Папка не существует !!!");
            }
        }

        public static bool DelFiles(DirectoryInfo d)
        {
            DateTime dateTime = DateTime.Now;
            DateTime at;
            TimeSpan timeSpan = TimeSpan.FromMinutes(30);
            bool folderIsEmpty = false;
            int cntDeletedFiles = 0;

            if (Directory.Exists(d.FullName))
            {
                FileInfo[] files = d.GetFiles();
                foreach (FileInfo file in files)
                {
                    Console.WriteLine(file.FullName);
                    try
                    {
                        at = file.LastAccessTime;
                        Console.WriteLine($"file:{file.FullName} has last access time: {at.ToString()}");
                        if (dateTime - at > timeSpan)
                        {
                            Console.WriteLine($" Файл {file.Name} устарел и будет удален");
                            file.Delete();
                            cntDeletedFiles++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{file.FullName} - что-то пошло не так...{ex.Message}");
                    }
                }
                if (files.Length == cntDeletedFiles)
                {
                    folderIsEmpty = true;
                }

                DirectoryInfo[] dirs = d.GetDirectories();
                int l = dirs.GetLength(0);
                if (l == 0 && folderIsEmpty)
                {
                    d.Delete();
                    Console.WriteLine($" Папка {d.FullName} удалена");
                }

                foreach (DirectoryInfo dir in dirs)
                {
                    Console.WriteLine(dir.FullName);
                    DelFiles(dir);
                }
                return folderIsEmpty;
            }

            return false;
        }
    }
}
