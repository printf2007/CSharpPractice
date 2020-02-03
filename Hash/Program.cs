using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hash
{
    public class ConsistenDemo
    {
        HashSet<String> server = new HashSet<string>()
        {
            "我是1号","我是2号","我是3号","我是4号"
        };
        Dictionary<long, string> vServers = new Dictionary<long, string>();
        /// <summary>
        /// 虚拟多少倍
        /// </summary>
        int Multiples = 1;

        long FNVH(string name)
        {
            return name.GetHashCode();
        }

        public ConsistenDemo()
        {
            foreach (var item in server)
            {
                AddServer(item);
            }
        }

        private void AddServer(string name)
        {
            for (int index = 0; index < Multiples; index++)
            {
                long hash = FNVH($"{name}:Virtual Server{index}");
                vServers.Add(hash, name);
            }
        }

        public void RemoveServer(string name)
        {
            for (int index = 0; index < Multiples; index++)
            {
                long hash = FNVH($"{name}:Virtual Server{index}");
                vServers.Remove(hash);
            }
        }

        public string GetServer(string theValueWhichWantToSaveToServer)
        {
            long hash = FNVH(theValueWhichWantToSaveToServer);
            Dictionary<long, string> tempMap = new Dictionary<long, string>();
            List<long> kList = new List<long>();
            foreach (var item in vServers)
            {
                ///离我最近的那个
                kList.Add(item.Key);
            }
            kList.Sort();
            var target = kList.First(item => item > hash);
            //tempMap.Add(target, vServers[target]);
            return vServers[target];
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var xx = new ConsistenDemo();
            Console.ReadLine();
        }


    }
}

