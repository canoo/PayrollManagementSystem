# C# Payroll Management System

This is a C# console application. The program demonstrates key OOP principles by modeling a payroll system for an organization with different types of employees.

## Core Concepts Demonstrated

This project serves as a practical application of the following object-oriented programming concepts:

-   **Inheritance**: A class hierarchy is built with `Employee` as the abstract base class and more specific employee types (`SalariedEmployee`, `HourlyEmployee`, etc.) as derived classes.
-   **Polymorphism**: The program processes an array of `Employee` objects. At runtime, the correct `earnings()` and `ToString()` method for each object's actual type is invoked, showcasing polymorphic behavior.
-   **Abstract Classes & Methods**: The `Employee` class is abstract, meaning it cannot be instantiated directly. It contains an abstract method `earnings()`, which forces all concrete subclasses to provide their own implementation for calculating pay.
-   **Method Overriding**: Derived classes override the `earnings()` and `ToString()` methods to provide specialized implementations suitable for that employee type. The `BasePlusCommissionEmployee` also overrides the virtual `UpdateSalary` method to provide a correct percentage-based calculation.

## Class Hierarchy

The project uses the following inheritance structure to model the different employee types:

```mermaid
classDiagram
    direction TB
    
    class Employee {
        <<Abstract>>
        +Name: string
        +Ssn: string
        +BaseSalary: double
        +UpdateSalary(percent: double): void
        +ToString(): string
        +earnings(): double
    }

    class SalariedEmployee {
        +earnings(): double
        +ToString(): string
    }

    class HourlyEmployee {
        +Wage: double
        +Hours: double
        +OvertimeRate: double
        +earnings(): double
        +ToString(): string
    }

    class CommissionEmployee {
        +GrossSales: double
        +CommissionRate: double
        +earnings(): double
        +ToString(): string
    }

    class BasePlusCommissionEmployee {
        +earnings(): double
        +UpdateSalary(percent: double): void
        +ToString(): string
    }

    Employee <|-- SalariedEmployee
    Employee <|-- HourlyEmployee
    Employee <|-- CommissionEmployee
    CommissionEmployee <|-- BasePlusCommissionEmployee
```

## Expected Output
```bash
Weekly salary of all employees in the collection:

Salaried employee: Mahbub Murshed
Social security number: 111-22-3333
Weekly salary $800.00
Earnings $800.00

Hourly employee: Stuart Russell
Social security number: 111-22-4444
Hourly Salary $16.75, Hours Worked 50
Earnings $921.25

Commission Employee: Susan Harper
Social security number: 444-44-4444
Gross Sales $10000, Commission Rate 0.06%
Earnings $600

Base Plus Commission Employee: David Whatmore
Social security number: 777-77-7777
Gross Sales $5000, Commission Rate 0.04%, Base Salary $300
Earnings $500

Base Plus Commission Employee:
The new base salary with a 30% increase is: $390
The new commission rate is: 0.05%

Employee Mahbub Murshed is SalariedEmployee
Total Earnings: 800
Employee Stuart Russell is HourlyEmployee
Total Earnings: 921.25
Employee Susan Harper is CommissionEmployee
Total Earnings: 600
Employee David Whatmore is BasePlusCommissionEmployee
Total Earnings: 640
```
