using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace lab_13_1_
{
    static class BOALog
    {
        public static void NewMessage(string message)   // запись
        {
            StreamWriter write = new StreamWriter(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\BOAlogfile.txt", true);
            write.WriteLine(message + "-----" + DateTime.Now);
            write.Close();
        }
        public static void Show()   // чтение и вывод на консоль
        {
            StreamReader read = new StreamReader(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\BOAlogfile.txt");
            Console.Write(read.ReadToEnd());
        }
        public static int Counter()   // счетчик записей
        {
            int count = 0;
            string str;
            StreamReader read = new StreamReader(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\BOAlogfile.txt");
            str = read.ReadLine();
            while (str != null)
            {
                count++;
                str = read.ReadLine();
            }
            return count;
        }
        public static void FindMessageWord(string message)   // поиск записи
        {
            StreamReader read = new StreamReader(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\BOAlogfile.txt");
            string str;
            for (int i = 0; i < Counter(); i++)
            {
                str = read.ReadLine();
                if (str.Contains(message))
                {
                    Console.WriteLine(str);
                }
            }
            read.Close();
        }
        public static void Delete(string del) // Оставить записи за текущий час
        {
            File.Delete(del);
        }

        static class BOADiskInfo   // класс для вывода инфы
        {
            public static DriveInfo[] driver = DriveInfo.GetDrives();   // получение списка всех доступных дисков в системе
            public static void ShowFreeSpace(string name)   // свободное место на дисках
            {
                NewMessage("Method for free space information: ");
                foreach (DriveInfo x in driver)
                {
                    if (x.Name.Contains(name) && x.IsReady)
                    {
                        Console.WriteLine("Free space on disk " + x.Name + ": " + x.TotalFreeSpace / Math.Pow(10, 9) + "GB");
                    }
                }
            }
            public static void FileSystemInfo(string name)   // файловая система
            {
                NewMessage("Method for file system information: ");
                foreach (DriveInfo x in driver)
                {
                    if (x.Name.Contains(name) && x.IsReady)
                    {
                        Console.WriteLine("File system on disk " + x.Name + ":" + x.DriveFormat);
                    }
                }
            }
            public static void DriverInfo()   // имя, объем, доступный объем, метка тома
            {
                NewMessage("Method for driver information: ");
                foreach (DriveInfo x in driver)
                {
                    Console.Write("Disk name: " + x.Name);
                    if (x.IsReady)
                    {
                        Console.Write(" Driver size: " + x.TotalSize / Math.Pow(10, 9) + "GB\n Free space: " + x.TotalFreeSpace / Math.Pow(10, 9) + "GB\n Mark: " + x.VolumeLabel + "\n");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
        }
        static class BOAFileInfo   // методами для вывода информации о конкретном файле
        {
            public static void Path(string path)   // путь
            {
                NewMessage("Method for file path: ");
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    Console.WriteLine("File name: " + fileInf.Name + " ----- Path: " + fileInf.FullName);
                }
                else
                {
                    Console.WriteLine("There no file like this(");
                }
            }
            public static void SizeResolName(string path)   // Размер, расширение, имя
            {
                NewMessage("Method for size, resolution, filename: ");
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                    Console.WriteLine("File name: " + fileInf.Name + " ----- Size: " + fileInf.Length + "----- Resolution: " + fileInf.Extension);
                else
                {
                    Console.WriteLine("There no file like this(");
                }
            }
            public static void CreationTime(string path)   // Время создания
            {
                NewMessage("Method for creation time: ");
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    Console.WriteLine("File name: " + fileInf.Name + " ----- Creation time: " + fileInf.CreationTime);
                }
                else
                {
                    Console.WriteLine("There no file like this(");
                }
            }
        }
        static class BOADirInfo   // информации о конкретном директории
        {
            public static void NumberOfFiles(string path)   // Количество файлов
            {
                NewMessage("Method for number of files: ");
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (dirInfo.Exists)
                {
                    Console.WriteLine("Directory name: " + dirInfo.Name + " ----- Number of files: " + dirInfo.GetFiles().Length);
                }
                else
                {
                    Console.WriteLine("There no directory like this(");
                }
            }
            public static void CreationTime(string path)   // Время создания
            {
                NewMessage("Method for creation time: ");
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (dirInfo.Exists)
                {
                    Console.WriteLine("Directory name: " + dirInfo.Name + " ----- Creation time: " + dirInfo.CreationTime);
                }
                else
                {
                    Console.WriteLine("There no directory like this(");
                }
            }
            public static void NumberOfDirectory(string path) // Количесвто поддиректорий
            {
                NewMessage("Method for number of directory: ");
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (dirInfo.Exists)
                    Console.WriteLine("Directory name: " + dirInfo.Name + " ----- Number of directory: " + dirInfo.GetDirectories().Length);
                else
                {
                    Console.WriteLine("There no directory like this(");
                }
            }
            public static void ParentDirectory(string path) // Список родительских поддиректорий
            {
                NewMessage("Method for parent directory: ");
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (dirInfo.Exists)
                {
                    Console.Write("Directory name: " + dirInfo.Name + " ----- Parent directory: ");
                    while (dirInfo != null)
                    {
                        Console.Write((dirInfo = dirInfo.Parent) + " ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("There no directory like this(");
                }
            }
        }
        static class BOAFileManager
        {
            public static DriveInfo[] drives = DriveInfo.GetDrives();
            public static void FirstMethod(string name)
            {
                NewMessage("First method FileMenager: ");
                foreach (DriveInfo x in drives)
                {
                    if (x.Name.Contains(name) && x.IsReady)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(x.Name);   // получение инфы о директории
                        if (dirInfo.Exists)   // существует ли каталог
                        {
                            foreach (var y in dirInfo.GetFiles())   // все файлы
                            {
                                Console.WriteLine("File name: " + y);
                            }
                            foreach (var y in dirInfo.GetDirectories())   // все папки
                            {
                                Console.WriteLine("Folder name: " + y);
                            }
                            DirectoryInfo dirInfo2 = new DirectoryInfo(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13");
                            dirInfo2.CreateSubdirectory("BOAInspect");
                            FileInfo fileInfo = new FileInfo(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\BOAInspect\BOAdirinfo.txt");
                            StreamWriter write = File.CreateText(fileInfo.FullName);
                            write.WriteLine("Information: ");
                            write.Close();
                            fileInfo.CopyTo(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\BOAInspect\BOAnewfile.txt", true);
                            fileInfo.Delete();
                        }
                        else
                        {
                            Console.WriteLine("There no directory like this(");
                        }
                    }
                }
            }
            public static void SecondMethod(string path)
            {
                NewMessage("Second method FileMenager:");
                DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13");
                dirInfo.CreateSubdirectory("BOAFiles");
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                DirectoryInfo dirInfo2 = new DirectoryInfo(path);
                FileInfo[] fileInf = dirInfo2.GetFiles("*.txt", SearchOption.AllDirectories);   // поиск всех папок
                foreach (var x in fileInf)
                {
                    x.CopyTo($@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\BOAFiles\{x.Name}", true);   // при копировании перезапись
                }
                DirectoryInfo dir = new DirectoryInfo(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\BOAInspect\");
                dir.CreateSubdirectory("BOAFiles");
                if (!dir.Exists)
                {
                    dir.Create();
                }
                DirectoryInfo dir2 = new DirectoryInfo(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\BOAInspect\");
                FileInfo[] fileInfo2 = dir2.GetFiles();
                foreach (var item in fileInfo2)
                {
                    item.CopyTo($@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\BOAInspect\BOAFiles\{ item.Name}", true);
                }
                dir2.Delete(true);   // удалять подкаталоги и файлы
            }
        }
        class Program
        {
            static void Main(string[] args)
            {
                
                    BOADiskInfo.ShowFreeSpace("C");
                    BOADiskInfo.FileSystemInfo("C");
                    BOADiskInfo.DriverInfo();
                    BOAFileInfo.Path(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\file1.txt");
                    BOAFileInfo.Path(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\file2.txt");
                    BOAFileInfo.SizeResolName(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\file3.txt");
                    BOAFileInfo.CreationTime(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13\file4.txt");
                    BOADirInfo.NumberOfFiles(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13");
                    BOADirInfo.CreationTime(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13");
                    BOADirInfo.NumberOfDirectory(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13");
                    BOADirInfo.ParentDirectory(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13");
                    BOAFileManager.FirstMethod("C");
                    Console.WriteLine("Number of records: " + Counter());
                    FindMessageWord("Method for free space information: ");
                    Delete("BOAlogfile.txt");
                //try
                //{
                //    BOAFileManager.SecondMethod(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_13\Lab_13");
                    
                //}
                //catch
                //{
                //    Console.WriteLine("Exeption!");
                //}
                //finally
                //{
                //    Console.WriteLine("Finally...");
                //}
            }
        }
    }
}
