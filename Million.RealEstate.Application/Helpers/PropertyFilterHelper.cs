using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Domain.Entities;
using System.Linq.Expressions;

namespace Million.RealEstate.Application.Helpers
{
    public static class PropertyFilterHelper
    {
        public static Expression<Func<Property, bool>> BuildPropertyFilter(PropertyFilterDto? filter)
        {
            if (filter == null)
                return _ => true;

            return p => 
                (string.IsNullOrEmpty(filter.Name) || p.Name.Contains(filter.Name)) &&
                (string.IsNullOrEmpty(filter.Address) || p.Address.Contains(filter.Address)) &&
                (string.IsNullOrEmpty(filter.CodeInternal) || p.CodeInternal == filter.CodeInternal) &&
                (!filter.MinPrice.HasValue || p.Price >= filter.MinPrice) &&
                (!filter.MaxPrice.HasValue || p.Price <= filter.MaxPrice) &&
                (!filter.Year.HasValue || p.Year == filter.Year) &&
                (!filter.IdOwner.HasValue || p.IdOwner == filter.IdOwner) &&
                (!filter.MinBedrooms.HasValue || p.Bedrooms >= filter.MinBedrooms) &&
                (!filter.MaxBedrooms.HasValue || p.Bedrooms <= filter.MaxBedrooms) &&
                (!filter.MinBathrooms.HasValue || p.Bathrooms >= filter.MinBathrooms) &&
                (!filter.MaxBathrooms.HasValue || p.Bathrooms <= filter.MaxBathrooms) &&
                (!filter.IsActive.HasValue || p.IsActive == filter.IsActive);
        }
    }
}