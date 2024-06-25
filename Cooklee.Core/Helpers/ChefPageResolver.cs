using AutoMapper;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.Helpers
{
    //public class ChefPageResolver : IValueResolver<SpecialMealDto, SpecialMeal, ChefPage>
    //{
    //    private readonly IUnitOfWork _unitOfWork;

    //    public ChefPageResolver(IUnitOfWork unitOfWork)
    //    {
    //        _unitOfWork = unitOfWork;
    //    }

    //    //public ChefPage Resolve(SpecialMealDto source, SpecialMeal destination, ChefPage destMember, ResolutionContext context)
    //    //{
    //    //    return _unitOfWork.ChefPageRepo.Find(c => c.DisplayName == source.ChefPageName).FirstOrDefault();
    //    //}
    //}
}
