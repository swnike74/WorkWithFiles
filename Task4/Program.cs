namespace Task4
{
    internal class Program
    {
        /// <summary>
        /// программа-загрузчик данных из бинарного формата в текст.
        /// </summary>
        /// <param name="args"></param>
        /// 
        internal class Student
        {
            public string Name { get; set; }
            public string Group { get; set; }
            public DateTime DateOfBirth { get; set; }
            public decimal AverageScore { get; set; }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Task 4 - Enter full path:");
            string Filename = Console.ReadLine();

            //string Filename = @"C:\Users\swnik\source\repos\BinaryReadWrite\BinaryReadWrite\SampleDataFile\students.dat";
            string DestinationDirectory = @"C:\Users\swnike\Desktop\Students";

            List<Student> students = ReadStudentsFromBinFile(Filename);

            //Создаем папку Students на рабочем столе
            try
            {
                if (Directory.Exists(DestinationDirectory))
                {
                    Console.WriteLine("That path exists already.");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(DestinationDirectory);
                    Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(DestinationDirectory));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            // раскидать всех студентов из файла по группам
            foreach (Student student in students)
            {
                Console.WriteLine(student.Name + " " + student.Group + " " + student.DateOfBirth + " " + student.AverageScore);
                string df = DestinationDirectory + @"\Group_" + student.Group + ".txt";

                if (!File.Exists(df))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(df))
                    {
                        sw.WriteLine(student.Name + " " + student.DateOfBirth + " " + student.AverageScore);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(df))
                    {
                        sw.WriteLine(student.Name + " " + student.DateOfBirth + " " + student.AverageScore);
                    }
                }
            }
        }

        static List<Student> ReadStudentsFromBinFile(string fileName)
        {
            List<Student> result = new();
            using FileStream fs = new FileStream(fileName, FileMode.Open);
            using StreamReader sr = new StreamReader(fs);

            Console.WriteLine(sr.ReadToEnd());

            fs.Position = 0;

            BinaryReader br = new BinaryReader(fs);

            while (fs.Position < fs.Length)
            {
                Student student = new Student();
                student.Name = br.ReadString();
                student.Group = br.ReadString();
                long dt = br.ReadInt64();
                student.DateOfBirth = DateTime.FromBinary(dt);
                student.AverageScore = br.ReadDecimal();

                result.Add(student);
            }

            fs.Close();
            return result;
        }
    }
}
