using Backend.Models;
using Backend.Services.Repository;

namespace Backend.DataGenerator
{
    public class Generator
    {
        public string GenerateSomeData()
        {
            KeyRepository kr = new KeyRepository();
            IList<Key> keys = kr.GetAll();
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
            char[] letters = "123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
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
