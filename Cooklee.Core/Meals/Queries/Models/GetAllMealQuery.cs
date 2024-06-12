using Cooklee.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.Meals.Queries.Models
{
    public class GetAllMealQuery : IRequest<List<Meal>>
    {
    }
}
