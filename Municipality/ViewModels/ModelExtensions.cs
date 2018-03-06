using Models;
using System;

namespace Municipality.ViewModels
{
    public static class ModelExtensions
    {

        public static IncidentViewModel ToViewModel(this Incident model)
        {
            return new IncidentViewModel
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Status = model.IncidentStatus.Name,
                StatusId = model.IncidentStatusId.Value,
                Lng = model.Longitude,
                Lat = model.Latitude,
                Adress = model.Adress,
                InFocus = false,
                Approved = model.Approved,
                PriorityId = model.PriorityId.Value,
                Priority = model.Priority.Name,
                Estimate = model.Estimate,
                TimeLeft = (model.DateOfApprove.AddHours(model.Estimate) - DateTime.Now).TotalHours.ToString("N2")
            };
        }

    }
}