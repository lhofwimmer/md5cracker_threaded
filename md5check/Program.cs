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
            CheckforResult(md5result);
            Console.ReadKey();
        }
        static string md5result = "38959589619129abc573d5130445cb6d";


        static List<string> fullArray;

        static List<string> array1, array2, array3, array4, array5, array6, array7, array8, array9, array10, array11, array12, array13, array14;
        private static void CheckforResult(string md5result)
        {
            var path = $@"{Environment.CurrentDirectory}\wordlist.txt";
            var array = File.ReadAllLines(path).ToList();
            fullArray = array;

            var arrlen = array.Count() / 14;
            array1 = array.GetRange(0, arrlen);
            array2 = array.GetRange(arrlen, arrlen);
            array3 = array.GetRange(arrlen * 2, arrlen);
            array4 = array.GetRange(arrlen * 3, arrlen);
            array5 = array.GetRange(arrlen * 4, arrlen);
            array6 = array.GetRange(arrlen * 5, arrlen);
            array7 = array.GetRange(arrlen * 6, arrlen);
            array8 = array.GetRange(arrlen * 7, arrlen);
            array9 = array.GetRange(arrlen * 8, arrlen);
            array10 = array.GetRange(arrlen * 9, arrlen);
            array11 = array.GetRange(arrlen * 10, arrlen);
            array12 = array.GetRange(arrlen * 11, arrlen);
            array13 = array.GetRange(arrlen * 12, arrlen);
            array14 = array.GetRange(arrlen * 13, arrlen);

            var t1 = new Thread(() => RealStart(array1));
            t1.Start();

            var t2 = new Thread(() => RealStart(array2));
            t2.Start();

            var t3 = new Thread(() => RealStart(array3));
            t3.Start();

            var t4 = new Thread(() => RealStart(array4));
            t4.Start();

            var t5 = new Thread(() => RealStart(array5));
            t5.Start();

            var t6 = new Thread(() => RealStart(array6));
            t6.Start();

            var t7 = new Thread(() => RealStart(array7));
            t7.Start();

            var t8 = new Thread(() => RealStart(array8));
            t8.Start();

            var t9 = new Thread(() => RealStart(array9));
            t9.Start();

            var t10 = new Thread(() => RealStart(array10));
            t10.Start();

            var t11 = new Thread(() => RealStart(array11));
            t11.Start();

            var t12 = new Thread(() => RealStart(array12));
            t12.Start();

            var t13 = new Thread(() => RealStart(array13));
            t13.Start();

            var t14 = new Thread(() => RealStart(array14));
            t14.Start();
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
                    Console.WriteLine(globalcounter);
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
