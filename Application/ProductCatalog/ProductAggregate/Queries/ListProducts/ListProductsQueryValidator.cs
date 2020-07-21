using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.Queries.ListProducts
{
    public class ListProductsQueryValidator : AbstractValidator<ListProductsQuery>
    {
        public ListProductsQueryValidator()
        {
            //RuleFor(x => x.BrandId).Must(GuidValidator.IsGuid).WithMessage("Bad BrandId Format Id must be GUID").WithErrorCode("invalid_guid");
            //RuleFor(x => x.ProductCategoryId).Must(GuidValidator.IsGuid).WithMessage("Bad ProductCategoryId Format Id must be GUID").WithErrorCode("invalid_guid");
        }
    }
}
