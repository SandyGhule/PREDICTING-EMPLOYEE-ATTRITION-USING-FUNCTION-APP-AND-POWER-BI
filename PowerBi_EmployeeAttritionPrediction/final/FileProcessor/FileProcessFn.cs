using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using FileProcessor.Classes;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace FileProcessor
{
    public static class FileProcessFn
    {
        [FunctionName("FileProcessFn")]
        public static async Task Run([BlobTrigger("import/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            if (myBlob.Length > 0)
            {
                using (var reader = new StreamReader(myBlob))
                {
                    var lineNumber = 1;
                    var line = await reader.ReadLineAsync();
                    while (line != null)
                    {
                        await ProcessLine(name, line, lineNumber, log);
                        line = await reader.ReadLineAsync();
                        lineNumber++;
                    }
                }
            }
        }

        private static async Task ProcessLine(string name, string line, int lineNumber, TraceWriter log)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                log.Warning($"{name}: {lineNumber} is empty.");
                return;
            }
            if(lineNumber==1)
            {
                return;
            }
            var parts = line.Split(',');
            if (parts.Length < 21)
            {
                log.Error($"{name}: {lineNumber} invalid data: {line}.");
                return;
            }

            EmployeeData item = BindData(parts,log);
            if(item==null)
            {
                return;
            }

            Rules.BasicRules rules= new Rules.BasicRules();
            bool isEmpLeaving = false;
            isEmpLeaving= rules.Formula1(item) || rules.Formula2(item)|| rules.Formula3(item)|| rules.Formula4(item)|| rules.Formula5(item);

            if(isEmpLeaving)
            {
                item.Prediction = "Yes";
            }
            else
            {
                item.Prediction = "No";
            }

            EmpData empData = new EmpData()
            {
                EmpId = item.EmpId,
                Salary = item.Salary.ToString(),
                Age = item.Age,
                DistanceFromHome = item.DistanceFromHome,
                Education = item.Education.ToString(),
                EnvironmentSatisfaction = item.EnvironmentSatisfaction,
                JobInvolvement = item.JobInvolvement,
                JobLevel = item.JobLevel.ToString(),
                JobSatisfaction = item.JobSatisfaction,
                Department = item.Department.ToString(),
                NumCompaniesWorked = item.NumCompaniesWorked,
                PercentSalaryHike = item.PercentSalaryHike,
                PerformanceRating = item.PerformanceRating.ToString(),
                RelationshipSatisfaction = item.RelationshipSatisfaction,
                TotalWorkingYears = item.TotalWorkingYears,
                TrainingTimesLastYear = item.TrainingTimesLastYear,
                WorkLifeBalance = item.WorkLifeBalance.ToString(),
                YearsAtCompany =item.YearsAtCompany,
                YearsInCurrentRole = item.YearsInCurrentRole,
                YearsSinceLastPromotion = item.YearsSinceLastPromotion,
                YearsWithCurrManager = item.YearsWithCurrManager,
                Prediction=item.Prediction
            };

            using (var context = new DatabaseContext())
            {
                try
                {
                    if (context.Emp_Prediction.Any(emp => emp.EmpId == item.EmpId))
                    {
                        log.Error($"{name}: {lineNumber} duplicate task: \"{item.EmpId}\".");
                        return;
                    }
                    context.Emp_Prediction.Add(empData);
                    await context.SaveChangesAsync();
                    log.Info($"{name}: {lineNumber} inserted task: \"{item.EmpId}\" with id: {item.EmpId}.");
                }catch(Exception ex)
                {

                }
            }
        }

        private static EmployeeData BindData(string[] parts,TraceWriter log)
        {
            try
            {
                return new EmployeeData
                {
                    EmpId = Convert.ToInt32(parts[0]),
                    Salary = (Enums.SalaryLevel)Enum.Parse(typeof(Enums.SalaryLevel), parts[1]),
                    Age = Convert.ToInt32(parts[2]),
                    DistanceFromHome = Convert.ToInt32(parts[3]),
                    Education = (Enums.EducationDegree)Enum.Parse(typeof(Enums.EducationDegree), parts[4]),
                    EnvironmentSatisfaction = Convert.ToInt32(parts[5]),
                    JobInvolvement = Convert.ToInt32(parts[6]),
                    JobLevel = (Enums.JobCategory)Enum.Parse(typeof(Enums.JobCategory), parts[7]),
                    JobSatisfaction = Convert.ToInt32(parts[8]),
                    Department = (Enums.Departments)Enum.Parse(typeof(Enums.Departments), parts[9]),
                    NumCompaniesWorked = Convert.ToInt32(parts[10]),
                    PercentSalaryHike = Convert.ToInt32(parts[11]),
                    PerformanceRating = (Enums.PerformanceRatings)Enum.Parse(typeof(Enums.PerformanceRatings), parts[12]),
                    RelationshipSatisfaction = Convert.ToInt32(parts[13]),
                    TotalWorkingYears = Convert.ToInt32(parts[14]),
                    TrainingTimesLastYear = Convert.ToInt32(parts[15]),
                    WorkLifeBalance = (Enums.WorkLifeBalanceRatio)Enum.Parse(typeof(Enums.WorkLifeBalanceRatio), parts[16]),
                    YearsAtCompany = Convert.ToInt32(parts[17]),
                    YearsInCurrentRole = Convert.ToInt32(parts[18]),
                    YearsSinceLastPromotion = Convert.ToInt32(parts[19]),
                    YearsWithCurrManager = Convert.ToInt32(parts[20])
                };
            }
            catch(Exception ex)
            {
                log.Error(ex.ToString());
                return null;
            }
        }
    }
}
