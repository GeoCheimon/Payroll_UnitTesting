using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Lib_UnitTests
{
    [TestClass]
    public class CheckIBANTests
    {
        private void IBANTestCases(Payroll_Lib.Payroll_Lib payroll, object[,] testcases, string expectedCountry)
        {
            bool failed = false;
            for (int i = 0; i < testcases.GetLength(0); i++)
            {
                try
                {
                    string IBANCountry = "";
                    Assert.AreEqual((bool)testcases[i, 1], payroll.CheckIBAN((string)testcases[i, 2], ref IBANCountry));
                    if ((bool)testcases[i, 1] == true)
                    {
                        Assert.AreEqual(expectedCountry, IBANCountry);
                    }
                }
                catch (Exception ex)
                {
                    //the test case failed
                    failed = true;
                    //I report the failed test case
                    Console.WriteLine("Failed Test Case: {0}. Reason: {1}. \n \t Hint: {2} \n \t Hint: {3}",
                        (int)testcases[i, 0], (string)testcases[i, 2], (string)testcases[i, 3], ex.Message);
                }
            }
            //In case of a failed test case it throws an exception
            if (failed) Assert.Fail();
        }
        const string greeceCountryCode = "Greece IBAN must start with 'GR'";
        const string greeceLength = "IBAN must be 27 characters long. \n \t Not higher or less. The IBAN is not valid.";
        const string italyCountryCode = "Italy IBAN must start with 'IT'";
        const string italyLength = "IBAN must be 27 characters long. \n \t Not higher or less. The IBAN is not valid.";
        const string cyprusCountryCode = "Cyprus IBAN must start with 'CY'";
        const string cyprusLength = "IBAN must be 28 characters long. \n \t Not higher or less. The IBAN is not valid.";
        const string englandCountryCode = "England IBAN must start with 'GB'.";
        const string englandLength = "IBAN must be 22 characters long. Not higher or less. \n \t The IBAN is not valid.";
        const string UnacceptableDivResult = "Unacceptable division result. The remainder isn't 1. \n \t The IBAN is not valid.";
        [TestMethod]
        public void TestGreeceIBAN()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            object[,] testcases =
            {
                {1, true, "GR1601101250000000012300695",greeceLength+" and "+greeceCountryCode},
                {2, false, "GR160110125000000001230069",greeceLength},
                {3, false, "GR16011012500000000123006951",greeceLength},
                {4, false, "GR1601101250000000012300696",UnacceptableDivResult},
            };
            IBANTestCases(payroll, testcases, "Greece");
        }

        [TestMethod]
        public void TestItalyIBAN()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            object[,] testcases =
            {
                {1, true, "IT60X0542811101000000123456",italyCountryCode+" and "+italyLength},
                {2, false, "IT60Y054281110100000012345",italyLength},
                {3, false, "IT60Y05428111010000001234561",italyLength},
                {4, false, "IT60Y0542811101000000123457",UnacceptableDivResult},
            };
            IBANTestCases(payroll, testcases, "Italy");
        }
        [TestMethod]
        public void TestCyprusIBAN()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            object[,] testcases =
            {
                {1, true, "CY17002001280000001200527600",cyprusCountryCode+" and "+cyprusLength},
                {2, false, "CY1700200128000000120052760",cyprusLength},
                {3, false, "CY170020012800000012005276001",cyprusLength},
                {4, false, "CY17002001280000001200527601",UnacceptableDivResult},
            };
            IBANTestCases(payroll, testcases, "Cyprus");
        }
        [TestMethod]
        public void TestEnglandIBAN()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            object[,] testcases =
            {
                {1, true, "GB94BARC10201530093459",englandCountryCode+" and "+englandLength},
                {2, false, "GB94BARC1020153009345",englandLength},
                {3, false, "GB94BARC102015300934591",englandLength},
                {4, false, "GB94BARC10201530093460",UnacceptableDivResult},
            };
            IBANTestCases(payroll, testcases, "England");
        }
        [TestMethod]
        public void TestUnsupportedOrEmptyIBAN()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            object[,] testcases =
            {
                {1, false, " ", "Empty IBAN should not be valid."},
                {2, false, "FR7630006000011234567890189", "France IBAN not supported in this implementation."}
            };

            IBANTestCases(payroll, testcases, "");
        }
    }
}
