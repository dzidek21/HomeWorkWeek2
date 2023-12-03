using Newtonsoft.Json;
using PasswordManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassworManager.App.Manager
{
    public class FileManager
    {
        private string _filePath;

        public FileManager(string filePath)
        {
            _filePath = filePath;
            
        }
        public List<Password> ReadFile()
        {
            
            if (File.Exists(_filePath))
            {
                var list = JsonConvert.DeserializeObject<List<Password>>(File.ReadAllText(_filePath));
                return list;
            }
            else
            {
                var file = File.Create(_filePath);
                file.Close();
                var list = JsonConvert.DeserializeObject<List<Password>>(File.ReadAllText(_filePath));
                return list;
            }
        }
        public void SaveListToFile(List<Password> passwords)
        {
            using StreamWriter sw = File.CreateText(_filePath);
            JsonSerializer jwSerializer = new JsonSerializer();
            jwSerializer.Serialize(sw, passwords);
        }
    }
}
