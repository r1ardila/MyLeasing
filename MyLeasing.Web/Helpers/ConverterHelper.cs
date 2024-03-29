﻿using MyLeasing.Web.Data;
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

        public async Task<Contract> ToContractAsync(ContractViewModel model, bool isNew)
        {
            return new Contract
            {
                EndDate = model.EndDate.ToUniversalTime(),
                IsActive = model.IsActive,
                Lessee = await _dataContext.Lessees.FindAsync(model.LesseeId),
                Owner = await _dataContext.Owners.FindAsync(model.OwnerId),
                Price = model.Price,
                Property = await _dataContext.Properties.FindAsync(model.PropertyId),
                Remarks = model.Remarks,
                StartDate = model.StartDate.ToUniversalTime(),
                Id = isNew ? 0 : model.Id
            };
        }

        public ContractViewModel ToContractViewModel(Contract contract)
        {
            return new ContractViewModel
            {
                EndDate = contract.EndDateLocal,
                IsActive = contract.IsActive,
                Lessees = _combosHelper.GetComboLessees(),
                Lessee = contract.Lessee,
                Owner =contract.Owner,
                Price = contract.Price,
                Property = contract.Property,
                Remarks = contract.Remarks,
                StartDate = contract.StartDateLocal,
                Id = contract.Id,
                LesseeId = contract.Lessee.Id,
                PropertyId = contract.Property.Id,
                OwnerId = contract.Owner.Id
                
            };
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
                Owner = await _dataContext.Owners.FindAsync(view.OwnerId),
                Price = view.Price,
                PropertyImages = isNew ? new List<PropertyImage>() : view.PropertyImages,
                PropertyType = await _dataContext.PropertyTypes.FindAsync(view.PropertyTypeId),
                Remarks = view.Remarks,
                Rooms = view.Rooms,
                SquareMeters = view.SquareMeters,
                Stratum = view.Stratum,
                
            };
        }

        public PropertyViewModel ToPropertyViewModel(Property property)
        {
            return new PropertyViewModel
            {
                Address = property.Address,
                Contracts = property.Contracts,
                HasParkingLot = property.HasParkingLot,
                Id = property.Id,
                IsAvailable = property.IsAvailable,
                Neighborhood = property.Neighborhood,
                Owner = property.Owner,
                Price = property.Price,
                PropertyImages = property.PropertyImages,
                PropertyType = property.PropertyType,
                Remarks = property.Remarks,
                Rooms = property.Rooms,
                SquareMeters = property.SquareMeters,
                Stratum = property.Stratum,
                OwnerId = property.Owner.Id,
                PropertyTypeId = property.PropertyType.Id,
                PropertyTypes = _combosHelper.GetComboPropertyTypes(),
                
            };
        }
    }
}
