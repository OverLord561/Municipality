using Models;
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
                StatusId = model.IncidentStatusId,
                Lng = model.Longitude,
                Lat = model.Latitude,
                Adress = model.Adress,
                InFocus = false
            };
        }

    }
}