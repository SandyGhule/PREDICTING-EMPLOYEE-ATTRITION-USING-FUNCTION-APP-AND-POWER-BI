using FileProcessor.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Rules
{
    public class BasicRules
    {

        public BasicRules() { }

        public bool Formula1(EmployeeData employeeData)
        {
            if(employeeData.Age<30 && employeeData.JobInvolvement<3 && employeeData.JobSatisfaction<5 && employeeData.PercentSalaryHike<10 && employeeData.YearsSinceLastPromotion<1)
            {
                return true;
            }
            return false;
        }

        public bool Formula2(EmployeeData employeeData)
        {
            if (employeeData.DistanceFromHome >20 && (employeeData.JobLevel.ToString()==Enums.JobCategory.EntryLevel.ToString()|| employeeData.JobLevel.ToString() == Enums.JobCategory.Intermediate.ToString() || employeeData.JobLevel.ToString() == Enums.JobCategory.Experienced.ToString())
                && (employeeData.WorkLifeBalance.ToString()==Enums.WorkLifeBalanceRatio.Poor.ToString()|| employeeData.WorkLifeBalance.ToString() == Enums.WorkLifeBalanceRatio.Fair.ToString())
                && employeeData.YearsAtCompany < 7)
            {
                return true;
            }
            return false;
        }

        public bool Formula3(EmployeeData employeeData)
        {
            if (employeeData.JobSatisfaction <7 && (employeeData.Education.ToString() == Enums.EducationDegree.PostGraduate.ToString() || employeeData.Education.ToString() == Enums.EducationDegree.PGDiploma.ToString())
                && employeeData.TotalWorkingYears < 15 && employeeData.YearsAtCompany>10 && employeeData.YearsSinceLastPromotion>7)
            {
                return true;
            }
            return false;
        }

        public bool Formula4(EmployeeData employeeData)
        {
            if (employeeData.Salary.ToString() == Enums.SalaryLevel.Low.ToString() && employeeData.EnvironmentSatisfaction < 3 && employeeData.JobSatisfaction < 7 && employeeData.PercentSalaryHike < 10 && employeeData.TrainingTimesLastYear < 80)
            {
                return true;
            }
            return false;
        }

        public bool Formula5(EmployeeData employeeData)
        {
            if ((employeeData.PerformanceRating.ToString() == Enums.PerformanceRatings.Unacceptable.ToString() || employeeData.PerformanceRating.ToString() == Enums.PerformanceRatings.NeedsImprovement.ToString())&&
                (employeeData.Salary.ToString() == Enums.SalaryLevel.Medium.ToString() || employeeData.Salary.ToString() == Enums.SalaryLevel.High.ToString())
                && employeeData.RelationshipSatisfaction<3)
            {
                return true;
            }
            return false;
        }
    }
}
