using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private readonly GymDbContext _dbContext;

        public AlertRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Alert> GetAlertById(int alertId)
        {
            var alert = await _dbContext.Alerts.SingleOrDefaultAsync(b => b.AlertId == alertId);
            if (alert == null)
            {
                throw new Exception("Alert not Found!");
            }
            return alert;

        }

        public async Task<List<Alert>> GetAlertsByMemberId(int memberId)
        {
            var memberAlerts = await _dbContext.Alerts
                .Where(a => a.MemberId == memberId && a.Status == true)
                .ToListAsync();
            if (memberAlerts == null)
            {
                throw new Exception("Alerts not Found!");
            }
            return memberAlerts;

        }

        public async Task<List<Alert>> GetAlertsByAlertType(string alertType, int? branchId = null)
        {
            
            var query = _dbContext.Alerts.AsQueryable();

            
            query = query.Where(a => a.AlertType.ToLower() == alertType.ToLower() && a.Status == true);

            
            if (branchId.HasValue)
            {
                query = query.Where(a => a.member.BranchId == branchId.Value);
            }

            query = query.Include(a => a.member);

            var memberAlerts = await query.ToListAsync();

            
            if (memberAlerts == null || !memberAlerts.Any())
            {
                throw new Exception($"{alertType} Alerts not found for the specified branch!");
            }

            return memberAlerts;
        }


        public async Task<List<Alert>> GetAllAlerts()
        {
            var alerts = await _dbContext.Alerts.ToListAsync();
            if (alerts.Count == 0)
            {
                throw new Exception("Alerts not Found!");
            }
            return alerts;

        }


        public async Task<Alert> AddAlert(Alert alert)
        {
            await _dbContext.Alerts.AddAsync(alert);
            await _dbContext.SaveChangesAsync();
            return alert;
        }

        public async Task<Alert> UpdateAlert(Alert alert)
        {
            var findedAlert = await GetAlertById(alert.AlertId);
            if (findedAlert == null)
            {
                throw new Exception("Alert not Found!");
            }
            _dbContext.Alerts.Update(alert);
            await _dbContext.SaveChangesAsync();
            return alert;
        }

        public async Task DeleteAlert(int alertId)
        {
            var alert = await GetAlertById(alertId);
            _dbContext.Alerts.Remove(alert);
            await _dbContext.SaveChangesAsync();
        }
    }
}
