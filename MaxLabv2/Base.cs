using System.Collections.Generic;
using System.Text.Json;

namespace MaxLabv2
{
    public class Tree_node
    {
        public Tree_node(char data)
        {
            this.data = data;
        }
        public char data { get; set; }
        public Tree_node Left { get; set; }
        public Tree_node Right { get; set; }

        public void Insert(Tree_node node)
        {
            if (node.data < data)
            {
                if (Left == null)
                    Left = node;
                else
                    Left.Insert(node);
            }
            else
            {
                if (Right == null)
                    Right = node;
                else
                    Right.Insert(node);
            }
        }

        public string Tostring(List<char> list = null)
        {
            if (list == null)
                list = new List<char>();
            if (Left != null)
                Left.Tostring(list);

            list.Add(data);

            if (Right != null)
                Right.Tostring(list);
            //return list.ToArray();

            string a = new string(list.ToArray());
            return a;
        }


    }
    public class Programs
    {
        static string filepath = @"appsettings.json";
        static string json = File.ReadAllText(filepath);
        public static FileJson data = JsonSerializer.Deserialize<FileJson>(json);
        static WebOut webOut = new WebOut();
        //метод рандомных чисел
        async static Task HttpRand(string str, FileJson data)
        {
            int length = str.Length - 1;
            try
            {
                using (var client = new HttpClient())
                {
                    string url = string.Format("{1}?min=0&max={0}&count=1", length, data.RandomApi);
                    using HttpResponseMessage response = await client.GetAsync(url);
                    string content = await response.Content.ReadAsStringAsync();

                    webOut.rand = content;
                    content = content.Replace("[", "").Replace("]", "");
                    int rnd = Convert.ToInt32(content);
                    webOut.shortst = str.Remove(rnd, 1);
                }
            }
            catch
            {
                Random rnd = new Random();
                int value = rnd.Next(0, length);
                webOut.rand = Convert.ToString(value);
                webOut.shortst = str.Remove(value, 1);


            }
        }
        //поск строки гласной
        public static string glas(string output)
        {
            int flag = 0;
            int id1 = 0;
            int id2 = 0;

            for (int i = 0; i < output.Length; i++)
            {
                if ("aeiouy".Contains(output[i]) && flag == 0)
                {
                    flag = 1;
                    id1 = i;
                }
                else if ("aeiouy".Contains(output[i]))
                {
                    flag = 2;
                    id2 = i;
                }

            }
            if (flag == 2)
                return output.Substring(id1, ++id2);
            else
                return "";
        }
        //быстрая сортировка
        public static string QuickSort(char[] str, int minID, int maxID)
        {
            if (minID >= maxID)
                return new string(str);
            int pivotID = GetPivot(str, minID, maxID);
            QuickSort(str, minID, pivotID - 1);
            QuickSort(str, pivotID + 1, maxID);
            string a = new string(str);
            return a;
        }

        static int GetPivot(char[] str, int minID, int maxID)
        {
            int pivot = minID - 1;
            for (int i = minID; i <= maxID; i++)
            {
                if (str[i] < str[maxID])
                {
                    pivot++;
                    Swap(ref str[pivot], ref str[i]);
                }
            }

            pivot++;
            Swap(ref str[pivot], ref str[maxID]);

            return pivot;
        }

        static void Swap(ref char left, ref char right)
        {
            char temp = left;

            left = right;

            right = temp;
        }

         public static string TreeSort(char[] str)
        {

            var tree = new Tree_node(str[0]);
            for (int i = 1; i < str.Length; i++)
                tree.Insert(new Tree_node(str[i]));

            return tree.Tostring();
        }

        public static Dictionary<char, int> let(string str)
        {
            var letters = new Dictionary<char, int>();
            foreach (char c in str)
            {
                if (letters.ContainsKey(c))
                    letters[c] = ++letters[c];
                else
                    letters.Add(c, 1);
            }
            return letters;
        }

        public static string revers1(string a) 
        {
            string b = a.Substring(0, a.Length / 2);
            char[] bc = b.ToCharArray();
            Array.Reverse(bc);
            a = a.Substring(a.Length / 2);
            char[] ac = a.ToCharArray();
            Array.Reverse(ac);
            return String.Concat<char>(bc) + String.Concat<char>(ac);
        }

        public static string revers2(string a) 
        {
            char[] b = a.ToCharArray();
            Array.Reverse(b);
            return String.Concat<char>(b) + a;
        }

        public static string inval(string a)
        {
            string exept = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] < 97 || a[i] > 122)
                    exept = exept + a[i];
            }
            return exept;
        }



        public static WebOut Base(string a, string sw)
        {
           
                //lab1 Smolnikov
                char[] str = new char[a.Length];
                for (int i = 0; i < a.Length; i++)
                    str[i] = a[i];
                
                string output = "";
                string exept = "";
                //была ошибка в ограничении for
                //проверка на соотрветсвие требованиям строки
                exept = inval( a);

                if (String.IsNullOrEmpty(exept))
                {
                    string[] blacklist = data.Settings.Blacklist;
                    bool flag = false;
                    for (int i = 0; i < blacklist.Length; i++)
                    {
                        if (a.Contains(blacklist[i]))
                            flag = true;
                    }
                    if (!flag)
                    {

                        if (a.Length % 2 == 0)
                        {

                            output = revers1(a);
                            webOut.st1 = output;
                            webOut.letters = let(output);
                            webOut.st2 = glas(output);
                        }
                        else
                        {
                            output = revers2(a);
                            webOut.st1 = output;
                            webOut.letters = let(output);
                            webOut.st2 = glas(output);

                    }

                        switch (sw)
                        {
                            case "1":
                                webOut.sortST = QuickSort(str, 0, str.Length - 1);
                                break;
                            case "2":
                                webOut.sortST = TreeSort(str);
                                break;

                        }

                        HttpRand(output, data);

                        return webOut;
                    }
                    else
                    {
                        webOut.st1 = "ex2";
                        return webOut;
                    }
                }
                else
                {
                    webOut.st1 = "ex1";
                    webOut.st2 = exept;
                    return webOut;
                }
        }
    }
}
