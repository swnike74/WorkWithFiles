namespace Task3
{
    internal class Program3
    {
        /// <summary>
        /// Модифиация Task 1
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int count = 0;

            Console.WriteLine("Task 3  - Updated task 1");
            Console.WriteLine("Task 3 - Enter full path:");
            string Path = Console.ReadLine();

            if (Directory.Exists(Path))
            {
                Console.WriteLine("Процесс пошел...");
                var dirinfo = new DirectoryInfo(Path);

                if (dirinfo.Exists)
                {
                    long size = 0;
                    long size_initial = TotalFoldersSize(dirinfo);
                    Console.WriteLine($"Исходный размер папки: {size_initial} байт");
                    while (DelFiles(dirinfo))
                    {
                        if (count++ == 0)
                        {
                            size = TotalFoldersSize(dirinfo);
                            Console.WriteLine($"Текущий размер папки: {size} байт");
                            Console.WriteLine($"Освобождено: {size_initial - size} байт");
                        }
                    }
                    if (count == 0)
                    {
                        size = TotalFoldersSize(dirinfo);
                        Console.WriteLine($"Текущий размер папки: {size} байт");
                        Console.WriteLine($"Освобождено: {size_initial - size} байт");
                    }
                }
            }
            else
            {
                Console.WriteLine("Папка не существует !!!");
            }
        }

        public static long TotalFoldersSize(DirectoryInfo d)
        {
            long size = 0;

            FileInfo[] files = d.GetFiles();

            foreach (FileInfo file in files)
            {
                //Console.WriteLine(file.FullName);
                try
                {
                    //Console.WriteLine($"file:{file.FullName}");
                    size += file.Length;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{file.FullName} - не могу посчитать...{ex.Message}");
                }
            }

            DirectoryInfo[] dirs = d.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                //Console.WriteLine(dir.FullName);
                size += TotalFoldersSize(dir);
            }
            return size;
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
                    //Console.WriteLine(file.FullName);
                    try
                    {
                        at = file.LastAccessTime;
                        //Console.WriteLine($"file:{file.FullName} has last access time: {at.ToString()}");
                        if (dateTime - at > timeSpan)
                        {
                            //Console.WriteLine($" Файл {file.Name} устарел и будет удален");
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
                    //Console.WriteLine($" Папка {d.FullName} удалена");
                }

                foreach (DirectoryInfo dir in dirs)
                {
                    //Console.WriteLine(dir.FullName);
                    DelFiles(dir);
                }
                return folderIsEmpty;
            }

            return false;
        }
    }
}
