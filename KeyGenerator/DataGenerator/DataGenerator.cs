using Backend.Models;
using Backend.Services.Repository;

namespace KeyGenerator.DataGenerator
{
    public class DataGenerator
    {
        public static DataGenerator instance;
        IList<Key> keys;
        public DataGenerator()
        {
            instance = this;
        }
        public DataGenerator GetCurrentKeyGenerator()
        {
            return instance;
        }
        public void CreateNewKey()
        {
            Key key = new Key(null, GenerateSomeData(), true, DateTime.Now.AddYears(50));
            KeyRepository kr = new KeyRepository();
            kr.Add(key);
        }
        public string GenerateSomeData()
        {
            KeyRepository kr = new KeyRepository();
            if(keys is null) 
            keys = kr.GetAll();
            string s = GenerateString();
            while (keys.Any(s => s.Data.Equals(s)))
                s = GenerateString();
            return s;
        }
        public string GenerateString()
        {
            int num_letters = 20;
            string word = "";
            // Создаем массив букв, которые мы будем использовать.
            char[] letters = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            // Создаем генератор случайных чисел.
            Random rand = new Random();
            for (int j = 1; j <= num_letters; j++)
            {
                int letter_num = rand.Next(0, letters.Length - 1);
                word += letters[letter_num];
            }
            return word;
        }
       
    }
}
