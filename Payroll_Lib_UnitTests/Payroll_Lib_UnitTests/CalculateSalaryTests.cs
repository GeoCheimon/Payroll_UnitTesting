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
    public class CalculateSalaryTests
    {
        private void CalculateSalaryTestsCases(Payroll_Lib.Payroll_Lib payroll, object[,] testcases)

        {

            bool failed = false;
            for (int i = 0; i < testcases.GetLength(0); i++)
            {
                int caseId = (int)testcases[i, 0];
                try
                {
                    bool expectedFlag = (bool)testcases[i, 1];
                    Employee EmpIX = (Employee)testcases[i, 2];
                    double expectedAnnualGross = (double)testcases[i, 3];
                    double expectedNetAnnual = (double)testcases[i, 4];
                    double expectedNetMonth = (double)testcases[i, 5];
                    double expectedTax = (double)testcases[i, 6];
                    double expectedInsurance = (double)testcases[i, 7];

                    double AnnualGrossSalary = 0;
                    double NetAnnualIncome = 0;
                    double NetMonthIncome = 0;
                    double Tax = 0;
                    double Insurance = 0;

                    bool actualFlag = payroll.CalculateSalary(
                        EmpIX,
                        ref AnnualGrossSalary,
                        ref NetAnnualIncome,
                        ref NetMonthIncome,
                        ref Tax,
                        ref Insurance);


                    string errorMsg = "";

                    // This is the case where I expect success and I have success
                    // I validate numeric results to ensure they match the expected ones.
                    // If there is any mismatch, detailed error messages are collected.
                    if (expectedFlag && actualFlag)
                    {
                        if (AnnualGrossSalary != expectedAnnualGross)
                            errorMsg += $"\n - AnnualGrossSalary: expected: {expectedAnnualGross}, actual: {AnnualGrossSalary}\n";
                        if (NetAnnualIncome != expectedNetAnnual)
                            errorMsg += $" - NetAnnualIncome: expected: {expectedNetAnnual}, actual: {NetAnnualIncome}\n";
                        if (NetMonthIncome != expectedNetMonth)
                            errorMsg += $" - NetMonthIncome: expected: {expectedNetMonth}, actual: {NetMonthIncome}\n";
                        if (Tax != expectedTax)
                            errorMsg += $" - Tax: expected: {expectedTax}, actual {Tax}\n";
                        if (Insurance != expectedInsurance)
                            errorMsg += $" - Insurance: expected: {expectedInsurance}, actual: {Insurance}\n";

                        if ((EmpIX.Position == "Junior Developer" || EmpIX.Position == "Senior Developer" || EmpIX.Position == "Mid-level Developer" ||
                            EmpIX.Position == "IT Manager") && (AnnualGrossSalary != expectedAnnualGross || NetAnnualIncome != expectedNetAnnual ||
                                                                NetMonthIncome != expectedNetMonth))
                        {
                            if (EmpIX.Position == "Junior Developer")
                                errorMsg += " - Calculations for position 'Junior Developer' do not match the expected specification values.\n" +
                                                "   Verify if logical errors exist";
                            else if (EmpIX.Position == "Mid-level Developer")
                                errorMsg += " - Calculations for position 'Mid-level Developer' do not match the expected specification values.\n" +
                                                "   Verify if logical errors exist.";
                            else if (EmpIX.Position == "Senior Developer")
                                errorMsg += " - Calculations for position 'Senior Developer' do not match the expected specification values.\n" +
                                                "   Verify if logical errors exist.";
                            else
                                errorMsg += " - Calculations for position 'IT Manager' do not match the expected specification values.\n" +
                                                "   Verify if logical errors exist.";
                        }
                    }

                    // This is the case where I expect failure and I get failure
                    // I verify that the employee data is outside valid ranges and that all amounts of the results remain zero.
                    // If not, descriptive error messages explain why the case should have failed.
                    if (actualFlag != expectedFlag)
                    {
                        if (EmpIX.Department != "Τραπεζικών έργων" && EmpIX.Department != "Δικτύων" && EmpIX.Department != "Δημοσίων Έργων")
                            errorMsg += $"\n - The Department \"{EmpIX.Department}\" is not among the allowed ones:\n" +
                                                                                    $"   'Δημοσίων Έργων'\n" +
                                                                                    $"   'Τραπεζικών έργων'\n" +
                                                                                    $"   'Δικτύων'\n";
                        if (EmpIX.Position != "Junior Developer" && EmpIX.Position != "Senior Developer" && EmpIX.Position != "Mid-level Developer" &&
                            EmpIX.Position != "IT Manager")
                            errorMsg += $"\n - The Position \"{EmpIX.Position}\" is not among the allowed ones:\n" +
                                            $"   'Junior Developer'\n" +
                                            $"   'Mid-level Developer'\n" +
                                            $"   'Senior Developer'\n" +
                                            $"   'IT Manager'\n";
                        if (EmpIX.workExperience < 0 || EmpIX.workExperience > 38)
                            errorMsg += $"\n - Work experience ({EmpIX.workExperience}) is outside the acceptable range (0–38).\n";
                        if (EmpIX.Children < 0)
                            errorMsg += $"\n - The number of children ({EmpIX.Children}) cannot be negative.\n";


                        if (AnnualGrossSalary != 0 || NetAnnualIncome != 0 || NetMonthIncome != 0 || Tax != 0 || Insurance != 0)
                            errorMsg += $" - Therefore, all amounts should have been 0. Instead they are:" +
                                $"\n - AnnualGrossSalary={AnnualGrossSalary}, " +
                                $"NetAnnualIncome={NetAnnualIncome}, " +
                                $"NetMonthIncome={NetMonthIncome}, " +
                                $"Tax={Tax}, " +
                                $"Insurance={Insurance}\n";

                        //Neutral informational notes for invalid inputs 
                        if (EmpIX.workExperience < 0)
                            errorMsg += " - Verify that negative Work Experience is rejected.\n";
                        if (EmpIX.workExperience > 38)
                            errorMsg += " - Verify that Work Experience > 38 is rejected.\n";
                        if (EmpIX.Children < 0)
                            errorMsg += " - Verify that negative children count is rejected.\n";
                        if (EmpIX.Position != "Junior Developer" && EmpIX.Position != "Senior Developer" && EmpIX.Position
                            != "Mid-level Developer" && EmpIX.Position != "IT Manager")
                            errorMsg += " - Verify that invalid Position is rejected.\n";
                        if (EmpIX.Department != "Τραπεζικών έργων" && EmpIX.Department != "Δικτύων" &&
                            EmpIX.Department != "Δημοσίων Έργων")
                            errorMsg += " - Verify that invalid Department is rejected.\n";
                    }

                    if (!string.IsNullOrEmpty(errorMsg))
                        Assert.IsTrue(false, errorMsg);
                }
                catch (Exception ex)
                {
                    failed = true;
                    Console.WriteLine(" Failed Test Case: {0}. \n Reason: {1}", caseId, ex.Message);
                }
                // In case of a failed test case it throws an exception
                if (failed) Assert.Fail();
            }

        }

        [TestMethod]
        public void TestCase_id_1()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            object[,] testcases =
            {
                {
                    1,              // caseId
                    true,           // expectedFlag for success
                    new Employee("Giorgos", "Cheimonidis", 3, "Τραπεζικών έργων", "Junior Developer", 3),
                    16786.0,        // expected AnnualGrossSalary 
                    13657.47,       // expected NetAnnualIncome
                    975.53,         // expected NetMonthIncome
                    800.82,         // expected Tax
                    2327.71         // expected Insurance
                }
            };
            CalculateSalaryTestsCases(payroll, testcases);
        }

        [TestMethod]
        public void TestCase_id_2()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            object[,] testcases =
            {
                {
                    2,              // caseId
                    true,           // expectedFlag for success
                    new Employee("Katerina", "Giorgatzi", 0, "Δικτύων", "Senior Developer", 7),
                    39200.0,        // expected AnnualGrossSalary 
                    26866.05,       // expected NetAnnualIncome
                    1918.99,        // expected NetMonthIncome
                    6898.09,        // expected Tax
                    5435.86,        // expected Insurance
                }
            };
            CalculateSalaryTestsCases(payroll, testcases);
        }

        [TestMethod]
        public void TestCase_id_3()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            object[,] testcases =
            {
                {
                    3,              // caseId
                    false,          // expectedFlag for success
                    new Employee("Nikos", "Kalofwnos", 7, "Τραπεζικών έργων", "IT Manager", -1),
                    0.0,            // expected AnnualGrossSalary 
                    0.0,            // expected NetAnnualIncome
                    0.0,            // expected NetMonthIncome
                    0.0,            // expected Tax
                    0.0             // expected Insurance
                }
            };
            CalculateSalaryTestsCases(payroll, testcases);
        }

        [TestMethod]
        public void TestCase_id_4()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            object[,] testcases =
            {
                {
                    4,              // caseId
                    false,          // expectedFlag for success
                    new Employee("Christos", "Skiadaresis", 2, "Δημοσίων Έργων", "IT Manager", 39),
                    0.0,            // expected AnnualGrossSalary 
                    0.0,            // expected NetAnnualIncome
                    0.0,            // expected NetMonthIncome
                    0.0,            // expected Tax
                    0.0             // expected Insurance
                }
            };
            CalculateSalaryTestsCases(payroll, testcases);
        }

        [TestMethod]
        public void TestCase_id_5()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            object[,] testcases =
            {
                {
                    5,              // caseId
                    false,          // expectedFlag for success
                    new Employee("Chrysa", "Chardaloupa", 4, "Υπάλληλος γραφείου", "Mid-level Developer", 15),
                    0.0,            // expected AnnualGrossSalary 
                    0.0,            // expected NetAnnualIncome
                    0.0,            // expected NetMonthIncome
                    0.0,            // expected Tax
                    0.0,            // expected Insurance
                }
            };
            CalculateSalaryTestsCases(payroll, testcases);
        }


        [TestMethod]
        public void TestCase_id_6()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            // Collection of test cases for TestCase_id_2,3,4,5 with expected value 0 for all salary components
            object[,] testcases =
            {
        
                // Case 6: Mid-level Developer, Children=4, WorkExp=15, Department=Υπάλληλος γραφείου (άκυρο)
                {
                    6,              // caseId
                    false,          // expectedFlag for success
                    new Employee("Anestis", "Tsalidis", -1, "Δικτύων", "Junior Developer", 0),
                    0.0,            // expected AnnualGrossSalary 
                    0.0,            // expected NetAnnualIncome
                    0.0,            // expected NetMonthIncome
                    0.0,            // expected Tax
                    0.0             // expected Insurance
                }
            };
            CalculateSalaryTestsCases(payroll, testcases);
        }

        [TestMethod]
        public void TestCase_id_7()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            // Collection of test cases for TestCase_id_2,3,4,5 with expected value 0 for all salary components
            object[,] testcases =
            {
        
                // Case 6: Mid-level Developer, Children=4, WorkExp=15, Department=Υπάλληλος γραφείου (άκυρο)
                {
                    7,              // caseId
                    false,          // expectedFlag for success
                    new Employee("Kwstas", "Dedes", 0, "Δικτύων", "Manager", 0),
                    0.0,            // expected AnnualGrossSalary 
                    0.0,            // expected NetAnnualIncome
                    0.0,            // expected NetMonthIncome
                    0.0,            // expected Tax
                    0.0             // expected Insurance
                }
            };
            CalculateSalaryTestsCases(payroll, testcases);
        }
    }
}
