using ZakusochnayaModel;
using System.Collections.Generic;

namespace ZakusochnayaServiceImplementList
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Pokupatel> Pokupatels { get; set; }
        public List<Element> Elements { get; set; }
        public List<Zakaz> Zakazs { get; set; }
        public List<Output> Outputs { get; set; }
        public List<OutputElement> OutputElements { get; set; }
        private DataListSingleton()
        {
            Pokupatels = new List<Pokupatel>();
            Elements = new List<Element>();
            Zakazs = new List<Zakaz>();
            Outputs = new List<Output>();
            OutputElements = new List<OutputElement>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
