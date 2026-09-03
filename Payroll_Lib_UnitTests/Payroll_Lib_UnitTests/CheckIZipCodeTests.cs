using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Lib_UnitTests
{
    [TestClass]
    public class CheckIZipCodeTests
    {
        [TestMethod]
        public void TestItalianZipCodes()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            object[,] testcases =
            {
                {1, true, 10000,"The ZipCode must be a 5-digit number"},
                {2, true, 00011,"The ZipCode must be a 5-digit number"},
                {3, false, 00009,"Below minimum. ZipCode must > 10 and < 98168."},
                {4, false, -50000," It is a 5-digit number but it's negative zip therefor invalid."},
                {5, false, 98169,"Above maximum. ZipCode must > 10 and < 98168."},
                {6, false, 93291039,"The ZipCode must be a 5-digit number"},
            };

            bool failed = false;
            for (int i = 0; i < testcases.GetLength(0); i++)
            {
                try
                {
                    Assert.AreEqual((bool)testcases[i, 1], payroll.CheckZipCode((int)testcases[i, 2]));
                }
                catch (Exception ex)
                {
                    // the test case failed
                    failed = true;
                    // I report the failed test case
                    Console.WriteLine("Failed Test Case: {0}. Reason: {1}. \n \t Hint: {2} \n \t Hint: {3}",
                        (int)testcases[i, 0], (int)testcases[i, 2], (string)testcases[i, 3], ex.Message);
                }
            }
            // In case of a failed test case it throws an exception
            if (failed) Assert.Fail();
        }
    }
}
