using Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SmartService.ViewModels
{
    public static class ModelExtensions
    {
        //    public static OrderViewModel ToViewModel(this Order order)
        //    {
        //        return new OrderViewModel
        //        {
        //            Id = order.Id,
        //            Urgent = order.Urgent,
        //            Number = order.Number,
        //            StatusId = order.Status.Id,
        //            Status = order.Status,
        //            //Deadline = $"{(int)Math.Round(order.Deadline.Subtract(DateTime.Now).TotalDays)}",
        //            AcceptedBy = order.AcceptedBy.UserName,               
        //            SerialNumber = order.Device.SerialNumber.ToString(),
        //            DeviceType = order.Device.DeviceType,
        //            DeviceSet = order.Device.DeviceSet,
        //            DeviceAppearance = order.Device.DeviceAppearance,
        //            Email = order.Client.Email,                
        //            DeviceModel = order.Device.DeviceModel,                
        //            FaultCondition = order.Device.FaultCondition,
        //            Client = order.Client.UserName,
        //            Cost = $"{order.Cost} UAH",
        //            Comment = order.Comment,

        //            Advertise = order.Advertise,

        //            Prepayment = order.Prepayment.ToString(),
        //            //DeadLine = order.Deadline.ToString("MM-dd-yyyy"),
        //            Deadline = order.Deadline,
        //            Telephone = order.Client.TelephoneNumber,
        //            AssignetTo = "accepted at",

        //            OrderType = order.OrderType,



        //            //це ті самі випадаючі списки, які в репозиторії добавляються, але летять у в'ю моделі

        //            OrderTypesList = order.OrderTypesList,
        //            AdvertisesList = order.AdvertisesList,
        //            DeviceTypesList = order.DeviceTypesList,
        //            DeviceSetList = order.DeviceSetList,
        //            DeviceModelsList = order.DeviceModelsList,
        //            FaultConditionsList = order.FaultConditionsList,
        //            OrderStatusesList = order.OrderStatusesList



        //        };
        //    }

        //    public static SecurityLogEventViewModel ToViewModel(this SecurityLogEvent model)
        //    {
        //        return new SecurityLogEventViewModel
        //        {
        //            IP = model.IP,
        //            Date = model.Date.ToString("dd.MM.yyyy '/' H:mm"),
        //            Status = model.EventStatus.Name,
        //            Event = model.Event.Name
        //        };
        //    }

        //    public static IEnumerable<OrderHistoryViewModel> ToViewModel(this IEnumerable<OrderHistory> enumberable)
        //    {
        //        return enumberable.GroupBy(x => new { x.Date.Year, x.Date.Month, x.Date.Day })
        //            .Select(x => new
        //            {
        //                Date = new DateTime(x.Key.Year, x.Key.Month, x.Key.Day),
        //                Count = x.Count()
        //            })
        //            .Select(x => new OrderHistoryViewModel
        //            {
        //                Date = x.Date,
        //                Events = enumberable.Where(y => y.Date.Year == x.Date.Year && y.Date.Month == x.Date.Month && y.Date.Day == x.Date.Day)
        //                    .Select(y => new OrderEventViewModel
        //                    {
        //                        OrderId = y.OrderId,
        //                        StatusId = y.OrderStatusId,
        //                        Status= y.OrderStatus.Label,
        //                        Date = y.Date,
        //                        EventId = y.OrderEventId,
        //                        EventInitiator = y.EventInitiator,
        //                        EventName = y.OrderEvent.Name,
        //                        EventValue = y.EventValue
        //                    })
        //            });
        //    }
        //}
    }
}