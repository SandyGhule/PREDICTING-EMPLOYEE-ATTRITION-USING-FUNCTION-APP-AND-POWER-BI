using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FileProcessor.Classes.Enums;

namespace FileProcessor.Classes
{
    public class EmployeeData
    {
        public int EmpId { get; set; }
        public SalaryLevel Salary { get; set; }
        public int Age { get; set; }
        public int DistanceFromHome { get; set; }
        public EducationDegree Education { get; set; }
        public int EnvironmentSatisfaction { get; set; }
        public int JobInvolvement { get; set; }
        public JobCategory JobLevel { get; set; }
        public int JobSatisfaction { get; set; }
        public Departments Department { get; set; }
        public int NumCompaniesWorked { get; set; }
        public int PercentSalaryHike { get; set; }
        public PerformanceRatings PerformanceRating { get; set; }
        public int RelationshipSatisfaction { get; set; }
        public int TotalWorkingYears { get; set; }
        public int TrainingTimesLastYear { get; set; }
        public WorkLifeBalanceRatio WorkLifeBalance { get; set; }
        public int YearsAtCompany { get; set; }
        public int YearsInCurrentRole { get; set; }
        public int YearsSinceLastPromotion { get; set; }
        public int YearsWithCurrManager { get; set; }
        public string Prediction { get; set; }
    }

}
