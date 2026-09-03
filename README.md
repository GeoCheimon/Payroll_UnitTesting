# Payroll Management System with MSTest Unit Testing

This repository contains a comprehensive C# software solution developed for the **Software Quality and Reliability**.
## Project Architecture

The solution is split into two independent Visual Studio projects to maintain a clean separation of concerns:

1. **`Payroll_Lib` (Core Library)**: A C# Dynamic Link Library (DLL) designed to handle employee data and payroll calculations. It purposely contains **intentional logical and boundary errors** across its core methods to test and evaluate the robustness of verification techniques.
2. **`Payroll_Lib_UnitTests` (Testing Suite)**: An MSTest-based unit testing project implementing equivalence partitioning, boundary value analysis, and structured test runner configurations.

## Core Methods & Specifications

The library implements six primary business-logic components:
- **`CheckPhone`**: Validates phone numbers against international and country-specific formatting rules for Greece, Cyprus, Italy, and England.
- **`CheckIBAN`**: Verifies bank account numbers using length checks, prefix validation (`GR`, `IT`, `CY`, `GB`), and modulus-97 algorithms.
- **`CheckZipCode`**: Evaluates Italian postal codes against 5-digit formatting rules and strict numerical boundaries (`00010` to `98168`).
- **`CalculateSalary`**: Computes gross earnings, progressive income taxes (with child-based tax deductions), insurance contributions, and net pay based on job position tiers and professional experience.
- **`NumOfEmployees`**: Counts personnel assigned to specific corporate positions with null/empty array guard clauses.
- **`GetBonus`**: Manages department revenue goals, bonus distributions, and proportional percentage contributions.

## Project Manual (`manual.pdf`)

The repository includes a comprehensive, multi-page technical report (`manual.pdf`) authored by Georgios-Panagiotis Cheimonidis. 

### Why You Should Read It:
The manual serves as the definitive reference document for understanding the architectural decisions, expected behaviors, and failure analysis of the system. Reading it provides deep insight into *why* specific tests fail against the intentional bugs embedded in the core library.

### What It Contains:
- **Implementation Assumptions**: Detailed structural descriptions and validation rules for each function.
- **Equivalence Classes Tables**: Systematic partitioning of valid and invalid input domains (e.g., boundary limits, incorrect prefixes, unaccepted character injection).
- **Test Case Definitions**: Granular test case tables mapping input test data parameters to expected boolean flags and reference outputs.
- **Unit Test Implementation Code**: Complete code snippets showing how MSTest data-driven assertions and exception handlings are structured.
- **Test Execution Reports**: Visual Test Explorer results, stack traces, and analytical error breakdowns explaining the exact logical flaws discovered during execution.
