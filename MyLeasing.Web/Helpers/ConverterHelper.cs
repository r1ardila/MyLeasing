using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(
            DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }

        public async Task<Property> ToPropertyAsync(PropertyViewModel view, bool isNew)
        {
            return new Property
            {
                Address = view.Address,
                Contracts = isNew ? new List<Contract>() : view.Contracts,
                HasParkingLot = view.HasParkingLot,
                Id = isNew ? 0 : view.Id,
                IsAvailable = view.IsAvailable,
                Neighborhood = view.Neighborhood,
                Price = view.Price,
                PropertyImages = isNew ? new List<PropertyImage>() : view.PropertyImages,
                Rooms = view.Rooms,
                SquareMeters = view.SquareMeters,
                Stratum = view.Stratum,
                Owner = await _dataContext.Owners.FindAsync(view.OwnerId),
                PropertyType = await _dataContext.PropertyTypes.FindAsync(view.PropertyTypeId),
                Remarks = view.Remarks
            };
        }


    }
}
