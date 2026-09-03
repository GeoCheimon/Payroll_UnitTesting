using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll_Lib.Payroll_Lib;

namespace Payroll_Lib_UnitTests
{
    [TestClass]
    public class NumOfEmployeesTests
    {
        private void NumOfEmployees_TestCases(Payroll_Lib.Payroll_Lib payroll, object[,] testcases)
        {
            bool failed = false;

            for (int i = 0; i < testcases.GetLength(0); i++)
            {
                int caseId = (int)testcases[i, 0];
                int expected = (int)testcases[i, 1];
                Employee[] emps = (Employee[])testcases[i, 2];
                string position = (string)testcases[i, 3];
                string hint = (string)testcases[i, 4];

                try
                {
                    Assert.AreEqual(expected, payroll.NumOfEmployees(emps, position));
                }
                catch (Exception ex)
                {
                    failed = true;
                    Console.WriteLine("Failed Test Case: {0}. Position='{1}'.\nHint: {2}\nException: {3}",
                        caseId, position ?? "null", hint, ex.Message);
                }
            }

            if (failed) Assert.Fail();
        }

        [TestMethod]
        public void TestCase1()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            Employee[] Employee_testcase1 =
            {
                new Employee("Axil","Rak",0,"Δικτύων","Junior Developer",2),
                new Employee("Basil","Karast",1,"Δικτύων","Junior Developer",2),
                new Employee("Chrysa","Chardal",0,"Τραπεζικών έργων","Junior Developer",34),
                new Employee("Dora","Exer",2,"Δημοσίων Έργων","Senior Developer",4),
                new Employee("Eirini","Fivh",0,"Δικτύων","Senior Developer",5),
            };

            object[,] testcases =
            {
                {1, 3, Employee_testcase1, "Junior Developer", "There are 3 Junior Developers employees in the Empls list. " +
                                           "\nSo, it should have been a valid case. Make sure to verify your test conditions. " }
            };

            NumOfEmployees_TestCases(payroll, testcases);
        }

        [TestMethod]
        public void TestCase2()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            Employee[] Employee_testcase2 =
            {
                new Employee("Axil","Rak",0,"Δικτύων","Junior Developer",1),
                new Employee("Basil","Karast",1,"Δικτύων","Junior Developer",2),
                new Employee("Chrysa","Chardal",0,"Δικτύων","IT Manager",5),
                new Employee("Dora","Exer",2,"Δικτύων","IT Manager",6),
                new Employee("Eirini","Fivh",0,"Δημοσίων Έργων","IT Manager",7),
                new Employee("Xaris","Oikon",1,"Τραπεζικών έργων","IT Manager",4),
            };

            object[,] testcases =
            {
                {2, 0, Employee_testcase2, "Senior Developer", "In the specified case, there are no Senior Developer employees." +
                                            "\nSo, it should have been a valid case. Make sure to verify your test conditions." }
            };

            NumOfEmployees_TestCases(payroll, testcases);
        }

        [TestMethod]
        public void TestCase3()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            object[,] testcases =
            {
                {3, 0, new Employee[0], "Junior Developer", "The array of Employees is an empty string." +
                                                            "\nSo it should have returned 0.Make sure to verify your test conditions." }
            };

            NumOfEmployees_TestCases(payroll, testcases);
        }

        [TestMethod]
        public void TestCase4()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            Employee[] Employee_testcase4 =
            {
                new Employee("Axil","Rak",0,"Δικτύων","Junior Developer",1),
                new Employee("Basil","Karast",1,"Δικτύων","Junior Developer",2),
                new Employee("Chrysa","Chardal",0,"Δικτύων","Junior Developer",3),
            };

            object[,] testcases =
            {
                {4, 0, Employee_testcase4, "Manager", "The Position is non valid but it was passed as valid." +
                                                      "\nMake sure to verify your test conditions." }
            };

            NumOfEmployees_TestCases(payroll, testcases);
        }

        [TestMethod]
        public void TestCase5()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            object[,] testcases =
            {
                {5, 0, null, "Mid-level Developer", "The Employee array is null." +
                                                    "\nSo it should have returned 0. Make sure to verify your test conditions." }
            };

            NumOfEmployees_TestCases(payroll, testcases);
        }

        [TestMethod]
        public void TestCase6()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            Employee[] Employee_testcase1 =
            {
                new Employee("Axil","Rak",0,"Δικτύων","Junior Developer",2),
                new Employee("Basil","Karast",1,"Δικτύων","Junior Developer",2),
                new Employee("Chrysa","Chardal",0,"Τραπεζικών έργων","Junior Developer",34),
                new Employee("Eirini","Fivh",2,"Δημοσίων Έργων","Senior Developer",4),
                new Employee("Xaris","Oikon",0,"Δικτύων","Senior Developer",5),
            };

            object[,] testcases =
            {
                {6, 0, Employee_testcase1, "", "The Position is an empty string. Make sure to verify your test conditions." }
            };

            NumOfEmployees_TestCases(payroll, testcases);
        }

        [TestMethod]
        public void TestCase7()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            Employee[] Employee_testcase1 =
            {
                new Employee("Axil","Rak",0,"Δικτύων","Junior Developer",2),
                new Employee("Basil","Karast",1,"Δικτύων","Junior Developer",2),
                new Employee("Chrysa","Chardal",0,"Τραπεζικών έργων","Junior Developer",34),
            };

            object[,] testcases =
            {
                {7, 0, Employee_testcase1, null, "The Position is null. Make sure to verify your test conditions." }
            };

            NumOfEmployees_TestCases(payroll, testcases);
        }
    }
}
