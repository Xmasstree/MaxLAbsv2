using MaxLabv2;

namespace TestProject1
{
    public class Tests
    {

        [Test]
        public void revers1_areony()
        {
            string st = "areony";
            string expected = "erayno";

            string actual = Programs.revers1(st);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void revers2_areon()
        {
            string st = "areon";
            string expected = "areonnoera";

            string actual = Programs.revers1(st);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void exept_Areon() 
        {
            string st = "Areon";
            string expected = "A";

            string actual = Programs.inval(st);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void let_areon()
        {
            string st = "areon";
            Dictionary<char, int> expected = new Dictionary<char, int>()
            {
                {'a',1},
                {'r',1 },
                {'e',1},
                {'o',1},
                {'n',1 }
            };

            Dictionary<char, int> actual = Programs.let(st);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void glas_areon()
        {
            string st = "areon";
            string expected = "areo";

            string actual = Programs.glas(st);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QuickSort_areon()
        {
            string a = "dbagc";
            char[] st = new char[a.Length];
            for (int i = 0; i < a.Length; i++)
                st[i] = a[i];
            string expected = "abcdg";

            string actual = Programs.QuickSort(st, 0, st.Length - 1);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TreeSort_areon()
        {
            string a = "dbagc";
            char[] st = new char[a.Length];
            for (int i = 0; i < a.Length; i++)
                st[i] = a[i];
            string expected = "abcdg";

            string actual = Programs.TreeSort(st);
            Assert.AreEqual(expected, actual);
        }
    }
}