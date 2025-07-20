using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsWithCSharp.ChainOfResponsibility
{
    public class Resume
    {
        public string CandidateName { get; set; }
        public int YearsOfExperience { get; set; }
        public string EnglishLevel { get; set; }
        public int ExpectedSalary { get; set; }
    }

    public enum ResumeStatus
    {
        Accepted,
        RejectedByExperience,
        RejectedByEnglishLevel,
        RequiresManagerApproval
    }

    public delegate ResumeStatus? ResumeCheck(Resume resume);
    
    public class ResumeProcessor
    {
        private List<ResumeCheck> _checks = new();
        public ResumeProcessor AddCheck(ResumeCheck check) 
        {
            _checks.Add(check);
            return this;
        }

        public ResumeStatus Process(Resume resume)
        {
            foreach(var check in _checks)
            {
                var result = check(resume);
                if(result.HasValue && result.Value !=ResumeStatus.Accepted)
                {
                    return result.Value;
                }
            }
            return ResumeStatus.Accepted;
        }
    }
}
