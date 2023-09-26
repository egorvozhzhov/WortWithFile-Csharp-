using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace HelloApp
{
    class Files
    {
        private static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static void CreateFile()
        {
            Console.WriteLine("Введите имя файла");
            var FilePath = Path.Combine(dir, Console.ReadLine());
            var FileInfo = new FileInfo(FilePath);
            using var stream = FileInfo.Create();
            Console.WriteLine("Файл создан");
        }

        public static void WriteFile()
        {
            Console.WriteLine("Введите имя файла");
            var FilePath = Path.Combine(dir, Console.ReadLine());
            var FileInfo = new FileInfo(FilePath);
            if (FileInfo.Exists)
            {
                Console.WriteLine("Введите строку для записи в файл:");
                string text = Console.ReadLine();

                using (FileStream fstream = new FileStream(FilePath, FileMode.Append))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(text);
                    fstream.Write(array, 0, array.Length);
                    Console.WriteLine("Текст записан в файл");
                }
            }
            else
            {
                Console.WriteLine("Файл не найден");
            }

        }

        public static void DeleteFile()
        {
            Console.WriteLine("Введите имя файла");
            var FilePath = Path.Combine(dir, Console.ReadLine());
            var FileInfo = new FileInfo(FilePath);
            if (FileInfo.Exists)
            {
                FileInfo.Delete();
                Console.WriteLine("Файл удален");
            }
            else
            {
                Console.WriteLine("Файл не найден");
            }
        }

        public static void ReadFile()
        {
            Console.WriteLine("Введите имя файла");
            var FilePath = Path.Combine(dir, Console.ReadLine());
            var FileInfo = new FileInfo(FilePath);
            if (FileInfo.Exists)
            {
                using (FileStream fstream = File.OpenRead(FilePath))
                {
                    // преобразуем строку в байты
                    byte[] array = new byte[fstream.Length];
                    // считываем данные
                    fstream.Read(array, 0, array.Length);
                    // декодируем байты в строку
                    string textFromFile = System.Text.Encoding.Default.GetString(array);
                    Console.WriteLine($"Текст из файла: {textFromFile}");
                }

                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Файл не найден");
            }
        }

    }
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Sale { get; set; }
    }

    public static class JSON
    {
        private static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static void CreateAndWriteFile()
        {
            Console.WriteLine("Введите имя файла");
            string fname = Console.ReadLine() + ".json";
            Console.WriteLine("Введите номер покупателя: ");
            int _id = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите ммя покупателя: ");
            string _name = Console.ReadLine();
            Console.WriteLine("Введите скидку покупателя: ");
            int _sale = int.Parse(Console.ReadLine());
            var client = new Client
            {
                ID = _id,
                Name = _name,
                Sale = _sale
            };

            //Console.WriteLine("Введите имя файла");
            var FilePath = Path.Combine(dir, fname);
            var FileInfo = new FileInfo(FilePath);
            //string fileName = "Client.json";
            
            string jsonString = JsonSerializer.Serialize(client);
            //File.AppendAllText(FilePath, jsonString);
            File.WriteAllText(FilePath, jsonString);
            Console.WriteLine("JSON файл создан и запись сделана");
            //Console.WriteLine(File.ReadAllText(FilePath));
        }

        public static void ReadFile()
        {
            Console.WriteLine("Введите имя файла");
            string fname = Console.ReadLine() + ".json";
            var FilePath = Path.Combine(dir, fname);
            var FileInfo = new FileInfo(FilePath);
            if (FileInfo.Exists)
            {
                string jsonString = File.ReadAllText(FilePath);
          
                Client client = JsonSerializer.Deserialize<Client>(jsonString)!;

                Console.WriteLine($"ID: {client.ID}");
                Console.WriteLine($"Name: {client.Name}");
                Console.WriteLine($"Sale: {client.Sale}");
            }
            else
            {
                Console.WriteLine("Файла с таким названием нет");
            }

        }

        public static void Delete()
        {
            Console.WriteLine("Введите имя файла");
            var FilePath = Path.Combine(dir, Console.ReadLine() + ".json");
            var FileInfo = new FileInfo(FilePath);
            if (FileInfo.Exists)
            {
                FileInfo.Delete();
                Console.WriteLine("Файл удален");
            }
            else
            {
                Console.WriteLine("Файл не найден");
            }
        }
    }

    public static class XML
    {
      private static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      public static void Create()
      {
        Console.WriteLine("Введите имя файла");
        string fname = Console.ReadLine() + ".xml";
        Add(fname, "101", "Alex", "10");
      }
      public static void AddToXML()
      {
        Console.WriteLine("Введите имя файла");
        string fname = Console.ReadLine() + ".xml";
        var FilePath = Path.Combine(dir, fname);
        var FileInfo = new FileInfo(FilePath);

        if (FileInfo.Exists)
        {
          Console.WriteLine("Введите ID клиента");
          string _id = Console.ReadLine();
          Console.WriteLine("Введите имя клиента");
          string _name = Console.ReadLine();
          Console.WriteLine("Введите скидку клиента");
          string _sale = Console.ReadLine();

          XDocument xdoc = XDocument.Load(FilePath);
          XElement root = xdoc.Element("persones");
          root.Add(new XElement("person",
                                        new XAttribute("ID", _id), new XElement("name", _name), new XElement("sale", _sale)));
          xdoc.Save(FilePath);
        }
        else
        {
          Console.WriteLine("Файла с таким названием нет");
        }
    }

      public static void Add(string fname, string Id, string name, string sale)
      {
        XDocument xdoc = new XDocument();
        XElement pers1 = new XElement("person");
        XAttribute persID = new XAttribute("ID", Id);
        XElement persName = new XElement("name", name);
        XElement persSale = new XElement("sale", sale);
        pers1.Add(persID);
        pers1.Add(persName);
        pers1.Add(persSale);

        XElement persones = new XElement("persones");
        persones.Add(pers1);
        xdoc.Add(persones);

        var FilePath = Path.Combine(dir, fname);
        xdoc.Save(FilePath);
        Console.WriteLine("Запись добавлена в XML файл");
      }

      public static void ReadXml()
      {

        Console.WriteLine("Введите имя файла");
        string fname = Console.ReadLine() + ".xml";
        var FilePath = Path.Combine(dir, fname);
        var FileInfo = new FileInfo(FilePath);
        if (FileInfo.Exists)
        {
          XDocument xdoc = XDocument.Load(FilePath);
          foreach (XElement phoneElement in xdoc.Element("persones").Elements("person"))
          {
            XAttribute nameAttribute = phoneElement.Attribute("ID");
            XElement companyElement = phoneElement.Element("name");
            XElement priceElement = phoneElement.Element("sale");

            if (nameAttribute != null && companyElement != null && priceElement != null)
            {
              Console.WriteLine($"ID: {nameAttribute.Value}");
              Console.WriteLine($"Имя: {companyElement.Value}");
              Console.WriteLine($"Скидка: {priceElement.Value}");
            }
            Console.WriteLine();
          }
        }
        else
        {
          Console.WriteLine("Файла с таким названием нет");
        }

      
      }

      public static void DeleteXML()
      {
        
        Console.WriteLine("Введите имя файла");
        var FilePath = Path.Combine(dir, Console.ReadLine() + ".xml");
        var FileInfo = new FileInfo(FilePath);
        if (FileInfo.Exists)
        {
          FileInfo.Delete();
          Console.WriteLine("Файл удален");
        }
        else
        {
          Console.WriteLine("Файл не найден");
        }
      }
    }
    public static class ZIP
    {
      private static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

      public static void CreateZip()
      {
        Console.WriteLine("Введите имя  архива");
        string filename = Console.ReadLine();
        /*var FilePath = Path.Combine(dir, filename);
        DirectoryInfo dirInfo = new DirectoryInfo(FilePath);
        if (!dirInfo.Exists)
        {
          dirInfo.Create();
        }*/
        var FilePath2 = Path.Combine(dir, filename + ".zip");
        var FileInfo = new FileInfo(FilePath2);
        using var stream = FileInfo.Create();
        Console.WriteLine("Файл создан");
        //ZipFile.CreateFromDirectory(FilePath, FilePath2);
        //Console.WriteLine("Архив создан");

      }

      public static void AddToZip()
      {
        Console.WriteLine("Введите имя  архива");
        var FilePath2 = Path.Combine(dir, Console.ReadLine() + ".zip");
        var FileInfo = new FileInfo(FilePath2);

        if (FileInfo.Exists)
        {
          Console.WriteLine("Введите имя файла для добавления в архив");
          string fname = Console.ReadLine();
          var FilePath3 = Path.Combine(dir, fname);
          var FileInfo2 = new FileInfo(FilePath3);
          if (FileInfo.Exists)
          {
            using (FileStream zipToOpen = new FileStream(FilePath2, FileMode.Open))
            {
              using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
              {
                ZipArchiveEntry readmeEntry = archive.CreateEntry(fname);
                using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                {
                }
              }
            }
            Console.WriteLine($"Файл с названием {fname} добавлен в архив");
          }
          else
          {
            Console.WriteLine("Файла с таким названием нет");
          }
        }
        else
        {
          Console.WriteLine("Архива с таким названием нет");
        }
      }

      public static void ExtractFromZip()
      {
        Console.WriteLine("Введите имя  архива");
        var FilePath2 = Path.Combine(dir, Console.ReadLine() + ".zip");
        Console.WriteLine("Введите имя извлекаемого файла");
        string fname = Console.ReadLine();
        var FileInfo = new FileInfo(FilePath2);

        if (FileInfo.Exists)
        {
          // открытие архива в режиме чтения
          using (ZipArchive zipArchive = ZipFile.OpenRead(FilePath2))
          {
            // путь, куда необходимо извлечь файл
            string pathExtractFile = Path.Combine(dir, fname + "unzipped");
            // поиск необходимого файла в архиве
            // если он есть, то будет вызван метод, который его извлечёт
            zipArchive.Entries.FirstOrDefault(x => x.Name == fname)?.
                ExtractToFile(pathExtractFile);
          }
        }
        else
        {
          Console.WriteLine("Архива с таким названием нет");
        }

      }
      public static void DeleteZIP()
      {
        Console.WriteLine("Введите имя архива");
        var FilePath = Path.Combine(dir, Console.ReadLine() + ".zip");
        var FileInfo = new FileInfo(FilePath);
        if (FileInfo.Exists)
        {
          FileInfo.Delete();
          Console.WriteLine("Архив удален");
        }
        else
        {
          Console.WriteLine("Архив не найден");
        }
      }
    }


  class Program
    {
        public static void PrintInfo()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Название: {drive.Name}");
                Console.WriteLine($"Тип: {drive.DriveType}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Объем диска: {drive.TotalSize}");
                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                }
                Console.WriteLine();
            }
        }

        
        static void Main(string[] args)
        {
            int i;
            while (true)
            {
                Console.WriteLine("Выберите нужный пункт: 1 - Вывод информациия: \n" +
                  "2 - создание файла, 3 - запись в файл, 4 - Чтение из файла, 5 - удаление файла \n" +
                  "6 - JSON создание и запись, 7 - JSON чтение, 8 - JSON удаление \n " +
                  "9 - XML создание, 10 - XML добавление записи, 11 - XML чтение, 12 - XML удаление \n" +
                  "13 - Создание ZIP, 14 - Добавление в архив, 15 - Извлечение конкретного файла из архива, 16 - Удаление архива");

                i = int.Parse(Console.ReadLine());
                switch (i)
                {
                    case 1:
                        PrintInfo();
                        break;
                    case 2:
                        Files.CreateFile();
                        break;
                    case 3:
                        Files.WriteFile();
                        break;
                    case 4:
                        Files.ReadFile();
                        break;
                    case 5:
                        Files.DeleteFile();
                        break;
                    case 6:
                        JSON.CreateAndWriteFile();
                        break;
                    case 7:
                      JSON.ReadFile();
                      break;
                    case 8:
                        JSON.Delete();
                        break;
                    case 9:
                        XML.Create();
                        break;
                    case 10:
                      XML.AddToXML();
                      break;
                    case 11:
                      XML.ReadXml();
                      break;
                    case 12:
                      XML.DeleteXML();
                      break;
                    case 13:
                      ZIP.CreateZip();
                      break;
                    case 14:
                      ZIP.AddToZip();
                      break;
                    case 15:
                      ZIP.ExtractFromZip();
                      break;
                    case 16:
                      ZIP.DeleteZIP();
                      break;

                    default:
                        Console.WriteLine("Выберите действие");
                        break;
                }
            }
        }
    }

}
