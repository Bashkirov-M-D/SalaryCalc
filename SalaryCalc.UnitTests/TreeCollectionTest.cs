using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalaryCalc.Other;

namespace SalaryCalc.UnitTests
{
    [TestClass]
    public class TreeCollectionTest
    {
        TreeCollection<int> tree;

        [TestInitialize]
        public void TestInitialize()
        {
            tree = new TreeCollection<int>();
            for(int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (j == 0)
                        tree.Add(i);
                    else
                        tree.Add(i + 10 * j, i);
                }
            }
        }
        [TestMethod]
        public void FindChildren_Root_Array()
        {
            int[] expected = {10, 20, 30, 40, 50, 60, 70, 80, 90, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            int[] res = tree.FindChildren(0).ToArray();

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], res[i]);
            }
        }
        [TestMethod]
        public void FindChildren_One_Array()
        {
            int[] expected = { 11, 21, 31, 41, 51, 61, 71, 81, 91};

            int[] res = tree.FindChildren(1).ToArray();

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], res[i]);
            }
        }
    }
}
