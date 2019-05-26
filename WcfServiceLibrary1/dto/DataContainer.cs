using Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary1
{
    public class DataContainer
    {
        private static DataContainer instance = null;
        private DataDTO dataDTO = null;
        private DataContainer()
        {

        }


        public static DataContainer Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataContainer();

                return instance;
            }
        }

        public static void loadData(DataDTO dataDTO)
        {
            Instance.dataDTO = dataDTO;
        }

        public static DataDTO read()
        {
            return Instance.dataDTO;
        }
    }

    public class DataDTO
    {
        private int minWordsInSentence;
        private String pathToFiles;
        private List<FileToCompare> allFiles;
        private ObservableCollection<FilesToCompare> uniquePairsOfFilesToCompare;
        private ObservableCollection<string> logMessages = new ObservableCollection<string>();
        //private Dictionary<string, Worker> allClients = new Dictionary<string, Worker>();

        private ObservableCollection<Worker> allClients = new ObservableCollection<Worker>();


        public string PathToFiles { get => pathToFiles; set => pathToFiles = value; }
        public List<FileToCompare> AllFiles { get => allFiles; set => allFiles = value; }
        public ObservableCollection<string> LogMessages { get => logMessages; }
      
        public ObservableCollection<Worker> AllClients { get => allClients; }
        public ObservableCollection<FilesToCompare> UniquePairsOfFilesToCompare { get => uniquePairsOfFilesToCompare; set => uniquePairsOfFilesToCompare = value; }
        public int MinWordsInSentence { get => minWordsInSentence; set => minWordsInSentence = value; }
    }
}
