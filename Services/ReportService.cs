using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;

namespace GYMFeeManagement_System_BE.Services
{
    public class ReportService : IReportService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IAlertService _alertService;
        private readonly IAlertRepository _alertRepository;
        private readonly IMemberService _memberService;
        private readonly IEnrollProgramService _enrollProgramService;
        private readonly ITrainingProgramRepository _trainingProgramRepository;
        private readonly ITrainingProgramService _trainingProgramService;


        public ReportService(IPaymentRepository paymentRepository, ITrainingProgramService trainingProgram, ITrainingProgramRepository trainingProgramRepository, IEnrollProgramService enrollProgramService, IAlertRepository alertRepository, IAlertService alertService, IMemberService memberService)
        {
            _paymentRepository = paymentRepository;
            _alertService = alertService;
            _memberService = memberService;
            _alertRepository = alertRepository;
            _enrollProgramService = enrollProgramService;
            _trainingProgramRepository = trainingProgramRepository;
            _trainingProgramService = trainingProgram;
        }

        public async Task<ICollection<PaymentReportResDTO>> GetPaymentReport(int? branchId, string paymentType, DateTime? startDate, DateTime? endDate)
        {
            
            var payments = await _paymentRepository.GetAllPaymentsByBranchId(branchId);

            
            var filteredPayments = payments.AsEnumerable();

            if (paymentType == "Allincome")
            {
               
                filteredPayments = filteredPayments.Where(p => p.PaymentType != "Initial");
            }
            else if (paymentType == "Initial")
            {
                
                filteredPayments = filteredPayments.Where(p => p.PaymentType == "Initial");
            }

            
            if (startDate.HasValue && endDate.HasValue)
            {
                filteredPayments = filteredPayments.Where(p => p.PaidDate >= startDate.Value && p.PaidDate <= endDate.Value);
            }

            if (startDate.HasValue && !endDate.HasValue)
            {
                filteredPayments = filteredPayments.Where(p => p.PaidDate >= startDate.Value);
            }

            
            var paymentReport = filteredPayments.Select(p => new PaymentReportResDTO
            {
                PaymentId = p.PaymentId,
                PaymentType = p.PaymentType,
                PaymentDate = p.PaidDate,
                DueDate = p.DueDate,
                MemberId = p.MemberId,
                MemberName = $"{p.Member.FirstName} {p.Member.LastName}",
                Amount = p.Amount,
                PaymentMethod = p.PaymentMethod
            }).ToList();

            return paymentReport;
        }


        public async Task<ICollection<PaymentReportResDTO>> GetOverdueReport(int? branchId, string alertType)
        {
            
            var alerts = await _alertRepository.GetAlertsByAlertType(alertType, branchId);

           
            var paymentReport = alerts.Select(a => new PaymentReportResDTO
            {
                PaymentId = null,  
                PaymentType = null,  
                PaymentDate = null,  
                DueDate = a.DueDate,  
                MemberId = a.MemberId ?? 0,  
                MemberName = $"{a.member.FirstName} {a.member.LastName}",  
                Amount = a.Amount ?? 0,  
                PaymentMethod = null 
            }).ToList();

            return paymentReport;
        }

        public async Task<ICollection<MemberReportResDTO>> GetMemberReports(int? pageNumber, int? pageSize, bool? isActive, int branchId = 0)
        {
            
            var paginatedMembers = await _memberService.GetAllMembers(0, 0, isActive, branchId);

           
            var memberReports = new List<MemberReportResDTO>();
            var members = paginatedMembers.Data;
            if (members.Count==0)
            {
                throw new Exception("Members Not Found");
            }

            
            foreach (var member in members)
            {
               
                var trainingPrograms = await _enrollProgramService.GetTrainingProgramsByMemberId(member.MemberId);

                
                var memberReport = new MemberReportResDTO
                {
                    MemberId = member.MemberId,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Email = member.Email,
                    NIC = member.NIC,
                    Phone = member.Phone,
                    DoB = member.DoB,
                    Gender = member.Gender,
                    EmergencyContactName = member.EmergencyContactName,
                    EmergencyContactNumber = member.EmergencyContactNumber,
                    MonthlyPayment = member.MonthlyPayment,
                    ImagePath = member.ImagePath,
                    TrainerId = member.TrainerId,
                    BranchId = member.BranchId,
                    IsActive = member.IsActive,
                    Address = member.Address,
                   
                    trainingProgramsList = trainingPrograms
                };

                
                memberReports.Add(memberReport);
            }

         
            return memberReports;
        }

        /*   public async Task<List<ProgramReportResDTO>> GetProgramReport(int? branchId)
           {

               var allMembers = await _memberService.GetAllMembers(1, int.MaxValue, true, branchId ?? 0);

               // If branchId is provided, filter members by that branch
               int totalMembersCount = branchId.HasValue ? allMembers.Data.Count(m => m.BranchId == branchId.Value) : allMembers.Data.Count;


               var allPrograms = await _enrollProgramService.GetAllEnrollPrograms();

               // Prepare a dictionary to store program follower counts
               var programFollowerCounts = new Dictionary<int, int>();

               // Count program followers for each program
               foreach (var member in allMembers.Data)
               {
                   var enrolledPrograms = await _enrollProgramService.GetTrainingProgramsByMemberId(member.MemberId);

                   foreach (var program in enrolledPrograms)
                   {
                       if (programFollowerCounts.ContainsKey(program.ProgramId))
                       {
                           programFollowerCounts[program.ProgramId]++;
                       }
                       else
                       {
                           programFollowerCounts[program.ProgramId] = 1;
                       }
                   }
               }

               var programReport = new List<ProgramReportResDTO>();

               foreach (var program in allPrograms)
               {
                   // Get the follower count for the program
                   int programFollowerCount = programFollowerCounts.ContainsKey(program.ProgramId) ? programFollowerCounts[program.ProgramId] : 0;

                   // Calculate the follower percentage
                   decimal followerPercentage = totalMembersCount > 0 ? (decimal)programFollowerCount / totalMembersCount * 100 : 0;

                   // Get the total enrolling members for this program
                   int totalEnrollingMembers = programFollowerCount;

                   // Find the program details
                   var findedProgram = await _trainingProgramRepository.GetProgramById(program.ProgramId);


                   programReport.Add(new ProgramReportResDTO
                   {
                       ProgramId = program.ProgramId,
                       ProgramName = findedProgram.ProgramName,
                       FollowerPercentage = Math.Round(followerPercentage, 2), // Rounding
                       TotalMembers = totalMembersCount, 
                       TotalEnrollingMembers = totalEnrollingMembers 
                   });
               }

               return programReport;
           }
   */

        public async Task<List<ProgramReportResDTO>> GetProgramReport(int? branchId)
        {
            // Get all members and all programs
            var allMembers = await _memberService.GetAllMembers(1, int.MaxValue, true, branchId ?? 0);
            int totalMembersCount = branchId.HasValue ? allMembers.Data.Count(m => m.BranchId == branchId.Value) : allMembers.Data.Count;

            var allPrograms = await _enrollProgramService.GetAllEnrollPrograms();
            var trainingPrograms = await _trainingProgramService.GetAllTrainingPrograms();

            // Group programs by their TypeName
            var groupedProgramsByType = trainingPrograms
                .GroupBy(p => new { p.TypeId, p.TypeName })
                .ToDictionary(g => g.Key, g => g.ToList());

            // Initialize the result list
            var programReport = new List<ProgramReportResDTO>();

            // Create a dictionary for program follower counts
            var programFollowerCounts = new Dictionary<int, int>();

            // Calculate the follower count for each program
            foreach (var member in allMembers.Data)
            {
                var enrolledPrograms = await _enrollProgramService.GetTrainingProgramsByMemberId(member.MemberId);

                foreach (var program in enrolledPrograms)
                {
                    if (programFollowerCounts.ContainsKey(program.ProgramId))
                    {
                        programFollowerCounts[program.ProgramId]++;
                    }
                    else
                    {
                        programFollowerCounts[program.ProgramId] = 1;
                    }
                }
            }

            // Prepare the response in the required format
            foreach (var programTypeGroup in groupedProgramsByType)
            {
                var typeId = programTypeGroup.Key.TypeId;
                var typeName = programTypeGroup.Key.TypeName;
                var programsInType = programTypeGroup.Value;

                var typeReport = new ProgramReportResDTO
                {
                    TypeId = typeId,
                    TypeName = typeName,
                    TotalMembers = totalMembersCount,
                    Programs = new List<ProgramDetailResDTO>()
                };

                int totalEnrollingMembersInType = 0;

                foreach (var program in programsInType)
                {
                    var programFollowerCount = programFollowerCounts.ContainsKey(program.ProgramId) ? programFollowerCounts[program.ProgramId] : 0;

                    // Calculate the follower percentage
                    decimal followerPercentage = totalMembersCount > 0 ? (decimal)programFollowerCount / totalMembersCount * 100 : 0;

                    totalEnrollingMembersInType += programFollowerCount;

                    typeReport.Programs.Add(new ProgramDetailResDTO
                    {
                        ProgramId = program.ProgramId,
                        ProgramName = program.ProgramName,
                        TypeId = typeId,
                        FollowerPercentage = Math.Round(followerPercentage, 2), // Round the percentage
                        TotalMembers = totalMembersCount,
                        TotalEnrollingMembers = programFollowerCount
                    });
                }

                // Calculate the overall percentage for this type
                decimal typeFollowerPercentage = totalMembersCount > 0 ? (decimal)totalEnrollingMembersInType / totalMembersCount * 100 : 0;
                typeReport.FollowersPercentage = Math.Round(typeFollowerPercentage, 2);
                typeReport.TotalEnrollingMembers = totalEnrollingMembersInType;

                // Add the type report to the main list
                programReport.Add(typeReport);
            }

            return programReport;
        }



    }
}
