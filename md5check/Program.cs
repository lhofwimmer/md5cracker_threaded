using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Threading;

namespace md5check
{
    class Program
    {
        static void Main(string[] args)
        {
            var parameters = File.ReadAllLines(Environment.CurrentDirectory + "\\cfg.txt");
            ProcessConfig(parameters);
            CheckforResult(md5result);
            Console.ReadKey();
        }

        private static void ProcessConfig(string[] parameters)
        {
            md5result = parameters[0];
            numThreads = int.Parse(parameters[1]);
        }

        static string md5result;
        static int numThreads;


        static List<string> fullArray;

        private static void CheckforResult(string md5result)
        {
            var path = $@"{Environment.CurrentDirectory}\wordlist.txt";
            var array = File.ReadAllLines(path).ToList();
            fullArray = array;


            var arrlen = array.Count() / numThreads;
            for (int i=0;i<numThreads;i++)
            {
                var temparray = array.GetRange(arrlen*i,arrlen);
                Task t = Task.Factory.StartNew(() => RealStart(temparray));   

            }
            var countert = new Thread(() => PrintCounter());
            countert.Start();
        }

        private static void PrintCounter()
        {
            while(true)
            {
                Console.WriteLine($"{globalcounter} | {md5result}");
                Thread.Sleep(2000);
            } 
        }

        static long globalcounter = 0;
        private static void RealStart(List<string> array)
        {
            
            //int counter = 0;
            var resFound = false;

            for (int i= 0;i < array.Count();i++)
            {
                for (int j=0;j<fullArray.Count()-1;j++)
                {
                    var testword = array[i] + fullArray[j];
                    string result = CalculateMD5Hash(testword);
                    globalcounter++;

                    //Console.WriteLine(globalcounter + " | " + md5result);
                    if (result.Equals(md5result))
                    {
                        Console.WriteLine(testword + " | " + globalcounter);
                        resFound = true;
                    }
                    if (resFound) break;
                }
                if (resFound) break;
            }
        }

        //static MD5 md5 = MD5.Create();
        private static string CalculateMD5Hash(string testword)
        {
            var md5hash = "";

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputbytes = System.Text.Encoding.ASCII.GetBytes(testword);
                byte[] computehash = md5.ComputeHash(inputbytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < computehash.Length; i++)
                {
                    sb.Append(computehash[i].ToString("X2"));
                }
                md5hash = sb.ToString();
            }

            return md5hash;
        }
    }
}
