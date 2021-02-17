using ASO.Models.DTO.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASO.Models.DTO.Login
{
    public record ChangePasswordResultDto:BaseResultDto
    {
        public ChangePasswordResultDto(bool isSuccess, string errorMessage = "") : base(isSuccess, errorMessage)
        {

        }
    }
}
