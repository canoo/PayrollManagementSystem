using System;
using System.Text;

namespace Assignment02
{
    public abstract class Employee
    {
        public string Name { get; set; }
        public string Ssn { get; set; }
        public double BaseSalary { get; set; }

        public Employee(string name, string ssn)
        {
            Name = name;
            Ssn = ssn;
            BaseSalary = 0;
        }

        public virtual void UpdateSalary(double percent)
        {
            BaseSalary = BaseSalary + BaseSalary * percent;
        }

        public override string ToString()
        {
            return Name + "\nSocial security number: " + Ssn;
        }

        // Abstract method will be overridden by inherited subclasses.
        public abstract double earnings();
        // No implementation is allowed here, derived class must override this method!
    }   
  
    public class SalariedEmployee : Employee
    {
        public SalariedEmployee(string name, string ssn, double salary) : base(name, ssn)
        {
            // Set weekly salary
            BaseSalary = salary;
        }
                                                          
        public override double earnings()
        {
            return BaseSalary;
        }
                                                 
        public override string ToString()
        {
            string s = "Salaried employee: " + base.ToString() + "\nWeekly salary " + string.Format("${0:0.00}", BaseSalary) + string.Format("\nEarnings ${0:0.00}", earnings()) + "\n";
            return s;
        }
    }

    public class HourlyEmployee : Employee
    {
        // - Define properties for wage, hours, and overtimeRate.
        public double Wage { get; set; }
        public double Hours { get; set; }
        public double OvertimeRate { get; set; }

        // - Define constructor method by matching the total parameters from the main method object instance creation statement
        public HourlyEmployee(string name, string ssn, double wage, double overtimeRate, double hours)
            : base(name, ssn)
        {
            Wage = wage;
            OvertimeRate = overtimeRate;
            Hours = hours;
        }

        // - Override the base class earnings method and implement earnings for HourlyEmployee class
        public override double earnings()
        {
            if (Hours <= 40)
            {
                return Wage * Hours;
            }
            else
            {
                // Calculate pay with overtime
                double basePay = Wage * 40;
                double overtimePay = (Hours - 40) * Wage * OvertimeRate;
                return basePay + overtimePay;
            }
        }

        // - Override ToString() method
        public override string ToString()
        {
            return "Hourly employee: " + base.ToString() +
                   "\nHourly Salary $" + string.Format("{0:0.00}", Wage) +
                   ", Hours Worked " + string.Format("{0:0}", Hours) +
                   "\nEarnings $" + string.Format("{0:0.00}", earnings()) + "\n";
        }
    }

    public class CommissionEmployee : Employee
    {
        // - Define properties for gross sales and commission rate
        public double GrossSales { get; set; }
        public double CommissionRate { get; set; }

        // - Define constructor method by matching the total parameters from the main method object instance creation statement  
        public CommissionEmployee(string name, string ssn, double sales, double rate)
            : base(name, ssn)
        {
            GrossSales = sales;
            CommissionRate = rate;
        }

        // - Override the base class earnings method and implement earnings for CommissionEmployee class
        public override double earnings()
        {
            return GrossSales * CommissionRate;
        }

        // - Override ToString() method
        public override string ToString()
        {
            return "Commission Employee: " + base.ToString() +
                   "\nGross Sales $" + string.Format("{0:0}", GrossSales) +
                   ", Commission Rate " + CommissionRate + "%" +
                   "\nEarnings $" + string.Format("{0:0.00}", earnings()) + "\n";
        }
    }

    public class BasePlusCommissionEmployee : CommissionEmployee
    {
        // - Define constructor method by matching the total parameters from the main method object instance creation statement  
        public BasePlusCommissionEmployee(string name, string ssn, double sales, double rate, double salary)
            : base(name, ssn, sales, rate)
        {
            // Use the BaseSalary property from the top-level Employee class
            BaseSalary = salary;
        }

        // - Override the base class earnings method and implement earnings for BasePlusCommissionEmployee class 
        public override double earnings()
        {
            // Call base class (CommissionEmployee) earnings() to get commission and add base salary.
            return base.earnings() + BaseSalary;
        }

        // - Override ToString() method
        public override string ToString()
        {
            return "Base Plus Commission Employee: " + Name +
                   "\nSocial security number: " + Ssn +
                   "\nGross Sales $" + string.Format("{0:0}", GrossSales) +
                   ", Commission Rate " + CommissionRate + "%" +
                   ", Base Salary $" + string.Format("{0:0}", BaseSalary) +
                   "\nEarnings $" + string.Format("{0:0.00}", earnings()) + "\n";
        }

        // - Override the UpdateSalary method (if required)
        // This override is required because the base implementation does not handle
        // the percentage value correctly as it is passed from Main.
        public override void UpdateSalary(double percent)
        {
            // Correctly apply percentage increase (e.g., input of 30 becomes 0.30 for calculation)
            BaseSalary = BaseSalary + BaseSalary * (percent / 100.0);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // DO NOT CHANGE THE CONTENT OF THIS MAIN METHOD. USE IT TO TEST YOUR CLASSES

            // Create subclass objects
            SalariedEmployee salariedEmployee = new SalariedEmployee("Mahbub Murshed", "111-22-3333", 800.00);
            HourlyEmployee hourlyEmployee = new HourlyEmployee("Stuart Russell", "111-22-4444", 16.75, 1.5, 50);
            CommissionEmployee commissionEmployee = new CommissionEmployee("Susan Harper", "444-44-4444", 10000, .06);
            BasePlusCommissionEmployee basePlusCommissionEmployee = new BasePlusCommissionEmployee("David Whatmore", "777-77-7777", 5000, .04, 300);

            // Create an Employee array
            var employees = new Employee[4];

            // Initialize array with different Employees
            employees[0] = salariedEmployee;
            employees[1] = hourlyEmployee;
            employees[2] = commissionEmployee;
            employees[3] = basePlusCommissionEmployee;

            Console.WriteLine("Weekly salary of all employees in the collection:\n");

            foreach (var currentEmployee in employees)
            {
                // Invoke ToString 
                Console.WriteLine(currentEmployee);   
            }

            // Update Salary for BasePlusCommissionEmployee
            double percentageIncrease = 30;
            double newCommissionRate = 0.05;
            
            foreach (var currentEmployee in employees)
            { 
                // Check if employee is a BasePlusCommissionEmployee
                if (currentEmployee is BasePlusCommissionEmployee)
                {
                    // 30% base salary update
                    currentEmployee.UpdateSalary(percentageIncrease);
                    Console.WriteLine("Base Plus Commission Employee:\nThe new base salary with a {0}% increase is: ${1}", percentageIncrease, currentEmployee.BaseSalary);
                    
                    // Downcast Employee reference to BasePlusCommissionEmployee reference
                    // Downcast is required to access the "CommissionRate" property.
                    BasePlusCommissionEmployee employee = (BasePlusCommissionEmployee) currentEmployee;
                    employee.CommissionRate = newCommissionRate;
                    Console.WriteLine("The new commission rate is: " + employee.CommissionRate+"%");

                }
            }

            Console.WriteLine();

            foreach (var currentEmployee in employees)
            {
                Console.WriteLine("Employee {0} is {1}", currentEmployee.Name, currentEmployee.GetType().Name);
                Console.WriteLine("Total Earnings: " + currentEmployee.earnings());    
            }
        }
    }
}