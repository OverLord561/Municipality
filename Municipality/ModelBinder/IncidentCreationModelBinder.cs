using Microsoft.AspNetCore.Mvc.ModelBinding;
using Municipality.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.ModelBinder
{
    public class IncidentCreationModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            string incident = bindingContext.HttpContext.Request.Form["incident"];

            IncidentViewModel incidentModel = JsonConvert.DeserializeObject<IncidentViewModel>(incident);

            if (bindingContext.HttpContext.Request.Form.Files.Count > 0)
            {
                incidentModel.AttachedFiles = new List<AttachedFileViewModel>(bindingContext.HttpContext.Request.Form.Files.Count);

                foreach (var file in bindingContext.HttpContext.Request.Form.Files)
                {
                    incidentModel.AttachedFiles.Add(new AttachedFileViewModel
                    {
                        ContentType = file.ContentType,
                        Name = file.FileName,
                        FormFile = file
                    });
                }
            }

            bindingContext.Result = ModelBindingResult.Success(incidentModel);
            return Task.CompletedTask;
        }
    }
}
