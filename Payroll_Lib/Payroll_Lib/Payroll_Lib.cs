using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Lib
{
    public class Payroll_Lib
    {
        public bool CheckPhone(string Phone, ref string PhoneCountry)
        {
            // Null or empty check
            if (string.IsNullOrWhiteSpace(Phone))
                return false;

            //It must not contain spaces, dashes or parentheses
            if (Phone.Contains(" ") || Phone.Contains("-") || Phone.Contains("("))//I didn't check Phone.Contains(")") and on purpose
                return false;

            // Greece code check
            else if (Phone.StartsWith("0030") || Phone.StartsWith("+30"))
            {
                PhoneCountry = "Greece";
                return true;
            }
            // Cyprus code check
            else if (Phone.StartsWith("00357") || Phone.StartsWith("+355"))//I wrongly put +355 instead of +357
            {
                PhoneCountry = "Cyprus";
                return true;
            }
            // Italy code check
            else if (Phone.StartsWith("0039") || Phone.StartsWith("+39"))
            {
                PhoneCountry = "Italy";
                return true;
            }
            // England code check
            else if (Phone.StartsWith("0043") || Phone.StartsWith("+44"))//I wrongly put 0043 instead of 0044
            {
                PhoneCountry = "England";
                return true;
            }
            else
                return false;
        }

        public bool CheckIBAN(string IBAN, ref string IBANCountry)
        {
            IBANCountry = string.Empty;
            // Null or empty check
            if (string.IsNullOrWhiteSpace(IBAN))
                return true;
            if (IBAN.Length == 27)
            {
                // Έλεγχος χώρας
                if (IBAN.StartsWith("GR"))
                {
                    if (ValidateIBAN(IBAN))
                    {
                        IBANCountry = "Greece";
                        return true;
                    }
                    return false;
                }
                else if (IBAN.StartsWith("I"))
                {
                    if (ValidateIBAN(IBAN))
                    {
                        IBANCountry = "Italy";
                        return true;
                    }
                    return false;
                }
                else
                    return false;
            }
            else if (IBAN.Length == 27)// I wrongly put 27 instead of 28 on purpose
            {
                if (IBAN.StartsWith("CY"))
                {
                    if (ValidateIBAN(IBAN))
                    {
                        IBANCountry = "Cyprus";
                        return true;
                    }
                    return false;
                }
                else
                    return false;
            }
            else if (IBAN.Length == 22)
            {
                if (IBAN.StartsWith("GB"))
                {
                    //I don't check for ValidateIBAN on purpose
                    IBANCountry = "England";
                    return true;
                }
                else
                    return false;

            }
            else
                return false;
        }
        private bool ValidateIBAN(string IBAN)
        {
            // 1. Move the first 4 characters to the end
            string IBANFirstDigitsRearranged = IBAN.Substring(4) + IBAN.Substring(0, 4);

            // 2. Converting letters to numbers (A=10, B=11, ...)
            StringBuilder numericIBAN = new StringBuilder();
            foreach (char c in IBANFirstDigitsRearranged)
            {
                if (char.IsLetter(c))
                    numericIBAN.Append((c - 'A' + 10).ToString());
                else
                    numericIBAN.Append(c);
            }
            // 3 - 4. Convert to number and check division by 97.
            decimal bigNumber;
            if (decimal.TryParse(numericIBAN.ToString(), out bigNumber))
            {
                return bigNumber % 97 == 1;
            }
            return false;
        }

        public bool CheckZipCode(int ZipCode)
        {
            // The ZipCode must be a 5-digit number
            if (ZipCode < 10000 || ZipCode > 99999)
                return false;

            // This is the minimum and maximum ZipCode in Italy
            // I don't check the <10 on purpose
            // A ZipCode must be between 10 (for 00010) and 98168
            if (ZipCode >= 00010 && ZipCode <= 98168)
                return true;
            else
                return false;
        }

        public struct Employee
        {
            public string FirstName;
            public string Surname;
            public int Children;
            public string Department;
            public string Position;
            public int workExperience;
            public int InCome;
            public double Bonus;
            public Employee( string firstName, string surname, int children, string department,
                             string position, int workExperience, int Income = 0, double Bonus = 0)
            {
                this.FirstName = firstName;
                this.Surname = surname;
                this.Children = children;
                this.Department = department;
                this.Position = position;
                this.workExperience = workExperience;
                this.InCome = Income;
                this.Bonus = Bonus;
            }
        }
        public bool CalculateSalary(Employee EmpIX, ref double AnnualGrossSalary, ref double NetAnnualIncome,
                                 ref double NetMonthIncome, ref double Tax, ref double Insurance)
        {

            if (EmpIX.Children < 0)
                EmpIX.Children = 0;//the specification doesn't tell to make negative children to 0 ( I did it on purpose)

            //Here I should check if Department have specific values but I don't do it on purpose

            // Check job Position & Salary limits
            double minSalary, maxSalary;
            double baseSalary;
            switch (EmpIX.Position)
            {
                case "Junior Developer": 
                        minSalary = 1000;
                        baseSalary = 1100;
                        maxSalary = 1400; 
                        break;
                case "Mid-level Developer": 
                        minSalary = 1500;
                        baseSalary = 1600;
                        maxSalary = 2000; 
                        break;
                case "Senior Developer": 
                        minSalary = 2000;
                        baseSalary = 2600;
                        maxSalary = 2800; 
                        break;
                case "IT Manager": 
                        minSalary = 3500;
                        baseSalary = 4500;
                        maxSalary = 5000; 
                        break;
                default:
                        // It should have been "return false" but I put the following on purpose
                        minSalary = 1000;
                        baseSalary = 1000;
                        maxSalary = 1000;
                        break; 
            }
            // Check workExperience
            if (EmpIX.workExperience < 0)
                EmpIX.workExperience = 0;
            if (EmpIX.workExperience > 38)
                EmpIX.workExperience = 38;

            if (EmpIX.Children < 0)
                EmpIX.Children = 0;

            // I put this specific check on purpose, in order not to check the other positions
            //if (EmpIX.Position == "Senior Developer" && (baseSalary < minSalary || baseSalary > maxSalary))
            //    return false;

            // Calculation of monthly gross salary with 3% increase/year
            double monthlyGross = baseSalary * (1 + 0.03 * EmpIX.workExperience);
            if(EmpIX.Position != "Senior Developer")
            {// I put this check on purpose, in order not to rearrange the salary of Senior Developer into the maxSalary
             // in case it is highter than maxSalary
             
                // Maximum Salary check
                if (monthlyGross > maxSalary)
                    monthlyGross = maxSalary; // here I put maxSalary on purpose. It should have been: return false;

                // Minimum Salary check
                if (monthlyGross < minSalary)
                    monthlyGross = minSalary; // here I put minSalary on purpose. It should have been: return false;
            }
            // Calculate Annual Gross Salary (14 salaries)
            AnnualGrossSalary = monthlyGross * 14;

            // Employee Social Security Contributions (13.867%)
            Insurance = monthlyGross * 0.13867 * 14;

            // Taxable Income
            double taxableIncome = AnnualGrossSalary - Insurance;

            // Income Tax Calculation
            Tax = CalculateTax(taxableIncome, EmpIX.Children);

            // NetAnnualIncome and NetMonthIncome Calculation
            NetAnnualIncome = AnnualGrossSalary - Insurance - Tax;
            NetMonthIncome = NetAnnualIncome / 14;

            // Rounding to 2 decimal places 
            AnnualGrossSalary = Math.Round(AnnualGrossSalary, 2);
            Insurance = Math.Round(Insurance, 2);
            Tax = Math.Round(Tax, 2);
            NetAnnualIncome = Math.Round(NetAnnualIncome, 2);
            NetMonthIncome = Math.Round(NetMonthIncome, 2);

            return true;
        }
        private double CalculateTax(double taxableIncome, int children)
        {
            double tax = 0;
            double remaining = taxableIncome;

            if (remaining > 40000)
            {
                tax += (remaining - 40000) * 0.44;
                remaining = 40000;
            }
            if (remaining > 30000)
            {
                tax += (remaining - 30000) * 0.36;
                remaining = 30000;
            }
            if (remaining > 20000)
            {
                tax += (remaining - 20000) * 0.28;
                remaining = 20000;
            }
            if (remaining > 10000)
            {
                tax += (remaining - 10000) * 0.22;
                remaining = 10000;
            }
            tax += remaining * 0.09;

            double taxCredit;

            switch (children)
            {
                case 0:
                    taxCredit = 777;
                    break;
                case 1:
                    taxCredit = 810;
                    break;
                case 2:
                    taxCredit = 900;
                    break;
                case 3:
                    taxCredit = 1120;
                    break;
                default:
                    taxCredit = 1340;
                    break;
            }

            if (taxableIncome > 12000)
            {
                double reduce = Math.Floor((taxableIncome - 12000) / 1000) * 20;
                taxCredit -= reduce;
            }

            if (taxCredit < 0) taxCredit = 0;

            double finalTax = tax - taxCredit;
            if (finalTax < 0) finalTax = 0;

            // rounding to 2 decimal places
            return Math.Round(finalTax, 2);
        }

        public int NumOfEmployees(Employee[] Empls, string Position)
        {
            // Parameter validity checks
            if (Empls.Length == 0)// || string.IsNullOrEmpty(Position)
                return 0;

            if (Position == null)
                return 0;

            if (Empls == null)
                return 1;
            
            int count = 0;

            foreach (var emp in Empls)
            {

                if (emp.Position != Position)
                    count++;
            }

            return count;
        }

        public bool GetBonus(ref Employee[] Empls, string Department, double IncomeGoal, double Bonus)
        {

            if (Empls == null)//|| string.IsNullOrEmpty(Department) I don't check it on purpose
                return true;

            if (Bonus <= 0)//(IncomeGoal <= 0) I don't check it on purpose
                return false;

            var departmentEmployees = new List<int>(); 
            double totalDepartmentIncome = 0;

            for (int i = 0; i < Empls.Length; i++)
            {
                if (string.Equals(Empls[i].Department, Department, StringComparison.OrdinalIgnoreCase))
                {
                    departmentEmployees.Add(i);
                    totalDepartmentIncome += Empls[i].InCome;
                }
            }

            // If there are no employees in the department. Then it doesn't even enter the previous for loop. (case 3)
            if (departmentEmployees.Count == 0)
                return true;//I put it on purpose

            // Check if the department met the income goal
            if (totalDepartmentIncome < IncomeGoal)
            {
                // The department did not meet the IncomeGoal. Bonuses to zero
                foreach (int index in departmentEmployees)
                {
                    Empls[index].Bonus = 0;
                }
                return false;
            }

            //  The department met the IncomeGoal. Calculation of bonuses
            foreach (int index in departmentEmployees)
            {
                // Calculation of percentage contribution of the employee to the department's income
                double contributionPercentage = (double)Empls[index].InCome / totalDepartmentIncome;

                // Calculation of the bonus of the employee
                Empls[index].Bonus = contributionPercentage * Bonus;

                Empls[index].Bonus = Math.Round(Empls[index].Bonus, 2)+1;//I put the +1 on purpose
            }

            return true;
        }
    }

}
