using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class ReviewDto
    {
        public int? Id { get; set; }
        public string? Comment { get; set; }

        [AllowedValues(1, 2, 3, 4, 5)]
        public int Rate { get; set; } = 1;

    }
}
