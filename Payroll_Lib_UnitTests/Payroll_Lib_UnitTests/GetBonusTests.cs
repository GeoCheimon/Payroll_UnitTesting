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
    public class GetBonusTests
    {
        private void GetBonusTestCases(Payroll_Lib.Payroll_Lib payroll, object[,] testcases)
        {
            bool failed = false;

            for (int i = 0; i < testcases.GetLength(0); i++)
            {
                int caseId = (int)testcases[i, 0];
                bool expectedFlag = (bool)testcases[i, 1];
                Employee[] Empls = (Employee[])testcases[i, 2];
                string Department = (string)testcases[i, 3];
                double IncomeGoal = (double)testcases[i, 4];
                double Bonus = (double)testcases[i, 5];
                double[] expectedBonuses = (double[])testcases[i, 6];
                string hint = (string)testcases[i, 7];

                try
                {
                    Employee[] Empls_copy = null;
                    if (Empls != null)
                    {
                        Empls_copy = new Employee[Empls.Length];
                        for (int j = 0; j < Empls.Length; j++)
                            Empls_copy[j] = Empls[j];
                    }

                    bool actualFlag = payroll.GetBonus(ref Empls_copy, Department, IncomeGoal, Bonus);

                    string errorMsg = "";

                    // Flag check 
                    if (actualFlag != expectedFlag)
                        errorMsg += $"Expected:<{expectedFlag}>. Actual:<{actualFlag}>.\n";

                    // Empty department check
                    if (Empls_copy != null)
                    {
                        for (int j = 0; j < Empls_copy.Length; j++)
                        {
                            if (Empls_copy[j].Department == "")
                                errorMsg += $"- Employee {j + 1} doesn't have a Department.\n";
                        }
                    }

                    // Bonus check 
                    if (Empls_copy != null && expectedBonuses != null)
                    {
                        string bonusErrors = "";
                        for (int j = 0; j < Empls_copy.Length; j++)
                        {
                            double expected = expectedBonuses[j];
                            double actual = Empls_copy[j].Bonus;
                            if (Math.Abs(actual - expected) > 0.01)
                                bonusErrors +=
                                    $"- Emp[{j}] {Empls_copy[j].FirstName} {Empls_copy[j].Surname} "
                                  + $"expected {expected} got {actual}\n";
                        }

                        if (!string.IsNullOrEmpty(bonusErrors))
                            errorMsg += $"Bonus mismatches:\n{bonusErrors}";
                    }
                    if (!string.IsNullOrEmpty(errorMsg))
                        Assert.Fail($"{errorMsg}");
                }
                catch (Exception ex)
                {
                    failed = true;
                    Console.WriteLine(
                        "Failed Test Case: {0}. Hint: {1}\nException: {2}",
                        caseId, hint, ex.Message);
                }
            }

            if (failed) Assert.Fail();
        }

        [TestMethod]
        public void GetBonus_TestCase1()
        {
            var payroll = new Payroll_Lib.Payroll_Lib();
            Employee[] Empls =
            {
                new Employee("Christos","Skiadaresis",0,"Δικτύων","Junior Developer",3,4000,0),
                new Employee("Katerina","Giorgatzi",0,"Δικτύων","Mid-level Developer",4,5000,0),
                new Employee("Giorgos","Cheimonidis",0,"Δικτύων","Senior Developer",6,5000,0)
            };
            double[] expectedBonuses = { 400.0, 500.0, 500.0 };
            /*
                Employee 1: (4000 / 14000) * 1400 = 0.285714 * 1400 = 400.00 
                Employee 2: (5000 / 14000) * 1400 = 0.357142 * 1400 = 500.00
                Employee 3: (5000 / 14000) * 1400 = 0.357142 * 1400 = 500.00 
             */
            object[,] testcases =
            {
                {1, true, Empls, "Δικτύων", 12000.0, 1400.0, expectedBonuses,"It should have returned true."}
            };
            GetBonusTestCases(payroll, testcases);
        }

        [TestMethod]
        public void GetBonus_TestCase2()
        {
            var payroll = new Payroll_Lib.Payroll_Lib();
            Employee[] Empls =
            {
                new Employee("Christos","Skiadaresis",0,"Τραπεζικών έργων","Junior Developer",2,3000,100),
                new Employee("Katerina","Giorgatzi",0,"Τραπεζικών έργων","Mid-level Developer",4,2000,200),
                new Employee("Giorgos","Cheimonidis",0,"Τραπεζικών έργων","Senior Developer",6,2500,150),
                new Employee("Chrysa","Chardaloupa",0,"Τραπεζικών έργων","Junior Developer",1,1500,120),
                new Employee("Dora","Exer",0,"Τραπεζικών έργων","Mid-level Developer",3,3500,130),
                new Employee("Nikos","Kalofwnos",0,"Τραπεζικών έργων","Senior Developer",5,2500,110)
            };
            double[] expectedBonuses = { 0, 0, 0, 0, 0, 0 };
            object[,] testcases =
            {
                {2, false, Empls, "Τραπεζικών έργων", 17000.0, 1200.0, expectedBonuses,"It should have been false because " +
                                                                            "IncomeGoal > than total income from all Employees."}
            };
            GetBonusTestCases(payroll, testcases);
        }

        [TestMethod]
        public void GetBonus_TestCase3()
        {
            var payroll = new Payroll_Lib.Payroll_Lib();
            Employee[] Empls = new Employee[0];
            double[] expectedBonuses = new double[0];
            object[,] testcases =
            {
                {3, false, Empls, "Τραπεζικών έργων", 10000.0, 1100.0, expectedBonuses,"It should have returned 'false' because\n" +
                                                                            "the array of employess (Empls) is empty."}
            };
            GetBonusTestCases(payroll, testcases);
        }

        [TestMethod]
        public void GetBonus_TestCase4()
        {
            var payroll = new Payroll_Lib.Payroll_Lib();
            object[,] testcases =
            {
                {4, false, null, "Δημοσίων Έργων", 11000.0, 1000.0, null,"It should have returned 'false' because\n" +
                                                                    "the array of employess (Empls) is null."}
            };
            GetBonusTestCases(payroll, testcases);
        }

        [TestMethod]
        public void GetBonus_TestCase5()
        {
            var payroll = new Payroll_Lib.Payroll_Lib();
            Employee[] Empls =
            {
                new Employee("Christos","Skiadaresis",0,"","Junior Developer",1,2000,0),
                new Employee("Katerina","Giorgatzi",0,"","Mid-level Developer",2,3000,0),
                new Employee("Giorgos","Cheimonidis",0,"","Senior Developer",3,2500,0),
                new Employee("Nikos","Kalofwnos",0,"","Junior Developer",1,2500,0)
            };
            double[] expectedBonuses = { 0, 0, 0, 0 };
            object[,] testcases =
            {
                {5, false, Empls, "", 10000.0, 1000.0, expectedBonuses,"It should have returned 'false' because\n" +
                                                                    "the employess exist but the department doesn't."}
            };
            GetBonusTestCases(payroll, testcases);
        }

        [TestMethod]
        public void GetBonus_TestCase6()
        {
            var payroll = new Payroll_Lib.Payroll_Lib();
            Employee[] emps =
            {
                new Employee("Christos","Skiadaresis",0,"Δικτύων","Junior Developer",1,1000,0),
                new Employee("Katerina","Giorgatzi",0,"Δικτύων","Junior Developer",1,1500,0),
                new Employee("Giorgos","Cheimonidis",0,"Δικτύων","Mid-level Developer",3,2000,0),
                new Employee("Nikos","Kalofwnos",0,"Δικτύων","Senior Developer",5,1500,0),
                new Employee("Dora","Exer",0,"Δικτύων","Junior Developer",2,2000,0)
            };
            double[] expectedBonuses = { 0, 0, 0, 0, 0 };
            object[,] testcases =
            {
                {6, false, emps, "Δικτύων", 0.0, 1000.0, expectedBonuses,"It should have returned 'false' because the IncomeGoal is 0."}
            };
            GetBonusTestCases(payroll, testcases);
        }

        [TestMethod]
        public void GetBonus_TestCase7()
        {
            var payroll = new Payroll_Lib.Payroll_Lib();
            Employee[] emps =
            {
                new Employee("Christos","Skiadaresis",0,"Δικτύων","Junior Developer",2,8000,0),
                new Employee("Katerina","Giorgatzi",0,"Δικτύων","Mid-level Developer",4,7000,0),
                new Employee("Giorgos","Cheimonidis",0,"Δικτύων","Senior Developer",6,8000,0)
            };
            double[] expectedBonuses = { 0, 0, 0 };
            object[,] testcases =
            {
                {7, false, emps, "Δικτύων", 22000.0, 0.0, expectedBonuses,"It should have returned 'false' because the Bonus is 0."}
            };
            GetBonusTestCases(payroll, testcases);
        }
    }
}