using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using static Payroll_Lib.Payroll_Lib;
namespace Payroll_Lib_UnitTests
{
    [TestClass]
    public class CheckPhoneTests
    {
        const string greeceNumStartsfrom = "Greece starting number must be '0030' or '+30' ";
        const string cyprusNumStartsfrom = "Cyprus starting number must be '00357' or '+357' ";
        const string italyNumStartsfrom = "Italy starting number must be '0039' or '+39' ";
        const string englandNumStartsfrom = "England starting number must be '0044' or '+44' ";
        private void PhoneTestCases(Payroll_Lib.Payroll_Lib payroll, object[,] testcases, string expectedCountry = null)
        {
            bool failed = false;
            for (int i = 0; i < testcases.GetLength(0); i++)
            {
                try
                {
                    string PhoneCountry = "";
                    Assert.AreEqual((bool)testcases[i, 1], payroll.CheckPhone((string)testcases[i, 2], ref PhoneCountry));
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
        [TestMethod]
        public void TestInvalidCharacters()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            object[,] testcases =
            {
                {1, false, "0030 6975927329", "Contains space"},
                {2, false, "0030-6975927329", "Contains dash"},
                {3, false, "0030)6975927329", "Contains parenthesis"},
                {4, false, "=021991", "Unrelated input"},
                {5, false, " ","Unrelated input"},
                {6, false, "+111111331", "Unrelated input"},
            };
            PhoneTestCases(payroll, testcases);
        }
        [TestMethod]
        public void TestGreekPhoneCode()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            object[,] testcases =
            {
                {1, true, "+306975927329",greeceNumStartsfrom},
                {2, true, "00306973423343",greeceNumStartsfrom},
                {3, false, "-306973401050",greeceNumStartsfrom},
                {4, false, "2105499201",greeceNumStartsfrom},
            };
            PhoneTestCases(payroll, testcases, "Greece");
        }
        [TestMethod]
        public void TestCyprusPhoneCode()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            object[,] testcases =
            {
                    {1, true, "+35722302968",cyprusNumStartsfrom},
                    {2, true, "0035799040104",cyprusNumStartsfrom},
                    {3, false, "+357 99302104",cyprusNumStartsfrom},
                    {4, false, "+357012045)",cyprusNumStartsfrom},
            };
            PhoneTestCases(payroll, testcases, "Cyprus");
        }
        [TestMethod]
        public void TestItalyPhoneCode()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            object[,] testcases =
            {
                    {1, true, "+39335031201",italyNumStartsfrom},
                    {2, true, "003906932931",italyNumStartsfrom},
                    {3, false, "3910194501",italyNumStartsfrom},
                    {4, false, "+003903241",italyNumStartsfrom},
            };
            PhoneTestCases(payroll, testcases, "Italy");
        }
        [TestMethod]
        public void TestEnglandPhoneCode()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();
            object[,] testcases =
            {
                    {1, true, "0044069329312",englandNumStartsfrom},
                    {2, true, "0035799040104",englandNumStartsfrom},
                    {3, false, "35799302104",englandNumStartsfrom},
                    {4, false, "-+357012045",englandNumStartsfrom},
            };
            PhoneTestCases(payroll, testcases, "England");
        }
    }
    /*[TestClass]
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
    }*/
    /*[TestClass]
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
    }*/ 
    /*[TestClass]
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
    }*/
    /*[TestClass]
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
    }*/
    
    /*[TestClass]
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
                                errorMsg += $"- Employee {j+1} doesn't have a Department.\n";
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
            
              # Employee 1: (4000 / 14000) * 1400 = 0.285714 * 1400 = 400.00 
              # Employee 2: (5000 / 14000) * 1400 = 0.357142 * 1400 = 500.00
              # Employee 3: (5000 / 14000) * 1400 = 0.357142 * 1400 = 500.00 
            
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
    }*/
}
