using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.DTOs.Response;

namespace GYMFeeManagement_System_BE.Services
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;

        public AlertService(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public async Task<List<AlertResDTO>> GetAllAlerts()
        {
            var alerts = await _alertRepository.GetAllAlerts();

            var alertResDTOList = alerts.Select(alert => new AlertResDTO
            {
                AlertId = alert.AlertId,
                AlertType = alert.AlertType,
                MemberId = alert.MemberId,
                Amount = alert.Amount,
                ProgramId = alert.ProgramId,
                DueDate = alert.DueDate,
                AccessedDate = alert.AccessedDate,
                Status = alert.Status,
                Action = alert.Action
            }).ToList();

            return alertResDTOList;
        }

        public async Task<AlertResDTO> GetAlertById(int alertId)
        {
            var alert = await _alertRepository.GetAlertById(alertId);

            var alertResDTO = new AlertResDTO
            {
                AlertId = alert.AlertId,
                AlertType = alert.AlertType,
                MemberId = alert.MemberId,
                Amount = alert.Amount,
                ProgramId = alert.ProgramId,
                DueDate = alert.DueDate,
                AccessedDate = alert.AccessedDate,
                Status = alert.Status,
                Action = alert.Action
            };

            return alertResDTO;
        }

        public async Task<List<AlertResDTO>> GetAlertsByMemberId(int memberId)
        {
            var memberAlerts = await _alertRepository.GetAlertsByMemberId(memberId);

            var alertResDTOList = memberAlerts.Select(alert => new AlertResDTO
            {
                AlertId = alert.AlertId,
                AlertType = alert.AlertType,
                MemberId = alert.MemberId,
                Amount = alert.Amount,
                ProgramId = alert.ProgramId,
                DueDate = alert.DueDate,
                AccessedDate = alert.AccessedDate,
                Status = alert.Status,
                Action = alert.Action
            }).ToList();

            return alertResDTOList;
        }

        public async Task<List<AlertResDTO>> GetAlertsByAlertType(string alertType, int? branchId = null)
        {
            var alerts = await _alertRepository.GetAlertsByAlertType(alertType, branchId);

            var alertResDTOList = alerts.Select(alert => new AlertResDTO
            {
                AlertId = alert.AlertId,
                AlertType = alert.AlertType,
                MemberId = alert.MemberId,
                Amount = alert.Amount,
                ProgramId = alert.ProgramId,
                DueDate = alert.DueDate,
                AccessedDate = alert.AccessedDate,
                Status = alert.Status,
                Action = alert.Action
            }).ToList();

            return alertResDTOList;
        }

        public async Task<AlertResDTO> AddAlert(AlertReqDTO addAlertReq)
        {

            var programEnroll = new Alert
            {
                MemberId = addAlertReq.MemberId,
                ProgramId = addAlertReq.ProgramId,
                AlertType = addAlertReq.AlertType,  
                Amount = addAlertReq.Amount,
                DueDate = addAlertReq.DueDate,
                Action = addAlertReq.Action,
                AccessedDate = addAlertReq.AccessedDate,
                Status = addAlertReq.Status
            };

            var addedAlert = await _alertRepository.AddAlert(programEnroll);

            var alertResDTO = new AlertResDTO
            {
                AlertId = addedAlert.AlertId,
                AlertType = addedAlert.AlertType,
                MemberId = addedAlert.MemberId,
                Amount = addedAlert.Amount,
                ProgramId = addedAlert.ProgramId,
                DueDate = addedAlert.DueDate,
                AccessedDate = addedAlert.AccessedDate,
                Status = addedAlert.Status,
                Action = addedAlert.Action
            };

            return alertResDTO;
        }



        public async Task<AlertResDTO> UpdateAlert(int alertId, AlertReqDTO updateAlertReq)
        {
            var existingAlert = await _alertRepository.GetAlertById(alertId);
            if (existingAlert == null)
            {
                throw new Exception("Alert id is invalid");
            }

            // Update fields for existingAlert with values from updateAlertReq if they are provided
            existingAlert.AlertType = !string.IsNullOrEmpty(updateAlertReq.AlertType) ? updateAlertReq.AlertType : existingAlert.AlertType;
            existingAlert.Amount = updateAlertReq.Amount.HasValue ? updateAlertReq.Amount : existingAlert.Amount;
            existingAlert.ProgramId = updateAlertReq.ProgramId.HasValue ? updateAlertReq.ProgramId : existingAlert.ProgramId;
            existingAlert.DueDate = updateAlertReq.DueDate.HasValue ? updateAlertReq.DueDate : existingAlert.DueDate;
            existingAlert.MemberId = updateAlertReq.MemberId.HasValue ? updateAlertReq.MemberId : existingAlert.MemberId;
            existingAlert.Status = updateAlertReq.Status.HasValue ? updateAlertReq.Status : existingAlert.Status;
            existingAlert.Action = updateAlertReq.Action.HasValue ? updateAlertReq.Action : existingAlert.Action;
            existingAlert.AccessedDate = updateAlertReq.AccessedDate.HasValue ? updateAlertReq.AccessedDate : existingAlert.AccessedDate;


            var updatedAlert = await _alertRepository.UpdateAlert(existingAlert);

            var alertResDTO = new AlertResDTO
            {
                AlertId = updatedAlert.AlertId,
                AlertType = updatedAlert.AlertType,
                MemberId = updatedAlert.MemberId,
                Amount = updatedAlert.Amount,
                ProgramId = updatedAlert.ProgramId,
                DueDate = updatedAlert.DueDate,
                AccessedDate = updatedAlert.AccessedDate,
                Status = updatedAlert.Status,
                Action = updatedAlert.Action
            };

            return alertResDTO;

        }

        public async Task DeleteAlert(int alertId)
        {
            await _alertRepository.DeleteAlert(alertId);
        }

    }
}
