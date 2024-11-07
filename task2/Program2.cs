namespace task2
{
    internal class Program2
    {
        /// <summary>
        /// Программа, которая считает размер папки на диске (вместе со всеми вложенными папками и файлами). 
        /// На вход метод принимает URL директории, в ответ — размер в байтах.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Task 2 - Enter full path:");
            string Path = Console.ReadLine();

            if (Directory.Exists(Path))
            {
                Console.WriteLine("Процесс пошел...");
                var dirinfo = new DirectoryInfo(Path);

                if (dirinfo.Exists)
                {
                    Console.WriteLine($"Общий размер = {TotalFoldersSize(dirinfo)} байт");
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
                Console.WriteLine(dir.FullName);
                size += TotalFoldersSize(dir);
            }

            return size;
        }
    }
}
