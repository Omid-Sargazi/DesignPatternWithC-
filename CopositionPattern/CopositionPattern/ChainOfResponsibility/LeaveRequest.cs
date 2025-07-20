using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopositionPattern.CompositionPattern
{
   public class LeaveRequest
    {
        public string EmployeeName { get; set; }
        public int NumberOfDay { get; set; }
        public string Reason { get; set; }
    }

    public abstract class LeaveRequestHandler
    {
        protected LeaveRequestHandler _next;
        public void SetNext(LeaveRequestHandler next)
        {
            _next = next;
        }

        public abstract void Handle(LeaveRequest next);
    }

    public class TeamLeaderHandler : LeaveRequestHandler
    {
        public override void Handle(LeaveRequest request)
        {
            if(request.NumberOfDay<=2 && request.Reason.ToLower() !="medical")
            {
                Console.WriteLine($"TeamLead approved {request.EmployeeName}`s leave for {request.Reason} with {request.NumberOfDay}days");
            }
            _next?.Handle(request);
        }
    }

    public class ProjectManagerHandler : LeaveRequestHandler
    {
        public override void Handle(LeaveRequest request)
        {
            if(request.NumberOfDay<=5 && request.Reason.ToLower() !="medical")
            {
                Console.WriteLine($"Project manager approved {request.EmployeeName}`s leave for {request.NumberOfDay}");
            }
            _next?.Handle(request);
        }
    }

    public class HRManagerHandler : LeaveRequestHandler
    {
        public override void Handle(LeaveRequest request)
        {
            if(request.Reason.ToLower()=="medical"|| request.NumberOfDay>5)
            {
                Console.WriteLine($"HRmanager approved {request.EmployeeName}`s for {request.NumberOfDay}");
            }
            Console.WriteLine($"No Handler could approve {request.EmployeeName}`s request");
        }
    }
}
