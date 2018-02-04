using System.IO;

namespace Raid
{
    public class StorageFile
    {
        private string temp;
        private int maxInt;
        private int tempInt;

        public StorageFile()
        {
            temp = "NO DATA";
            tempInt = 0;
            maxInt = 0;
        }

        public void SaveFile()
        {            
            File.WriteAllText(@"datafile.txt", temp);
            File.WriteAllText(@"datacount.txt", tempInt.ToString());
            File.WriteAllText(@"highscore.txt", maxInt.ToString());
        }

        public void LoadFile()
        {
            temp = File.ReadAllText(@"datafile.txt");
            tempInt = System.Int32.Parse(File.ReadAllText(@"datacount.txt"));
            maxInt = System.Int32.Parse(File.ReadAllText(@"highscore.txt"));
        }

        public void StoreData(string newText, int score)
        {
            temp = newText;
            tempInt++;            
            if (score > maxInt)
            {
                maxInt = score;
            }
        }

        public string GetData()
        {
            return temp;
        }

        public int GetDataNum()
        {
            return tempInt;
        }

        public int GetHighscore()
        {
            return maxInt;
        }
    }
}
