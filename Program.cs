using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Student(string name, string group, DateTime dateofbirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateofbirth;
        }
        static public Student[] FileRead(string source)
        {
            if (File.Exists(source))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Console.WriteLine($"Чтение файла: {source}");
                using (var fs = new FileStream(source, FileMode.Open))
                {
                    Student[] Students = (Student[])formatter.Deserialize(fs);
                    Console.WriteLine("Объект десериализован");
                    return Students;
                }
            }
            else
            {
                Console.WriteLine($"Файл не найден:{source}");
                return null;
            }
        }
    }
    class Program
    {
        static void Main()
        {
            string source = $@"C:\Users\{Environment.UserName}\Desktop\Students.dat";
            string target = $@"C:\Users\{Environment.UserName}\Desktop\Students";
            if (Directory.Exists(target))
            {
                Console.WriteLine($"Папка {target} уже существует. Работа программы приостановлена.");
            }
            else
            {
                Directory.CreateDirectory(target);
                foreach (Student student in Student.FileRead(source))
                {
                    string GroupFile = target + @"\" + student.Group + ".txt";
                    using (StreamWriter sw = File.AppendText(GroupFile))
                    {
                        sw.WriteLine($"{student.Name}, {student.DateOfBirth}");
                    }
                }
                Console.WriteLine($"Файлы созданы в папке: {target}");
            }

        }
    }
}


