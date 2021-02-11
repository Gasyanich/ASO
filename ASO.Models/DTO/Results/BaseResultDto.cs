namespace ASO.Models.DTO.Results
{
    public record BaseResultDto
    {
        public BaseResultDto()
        {
            ErrorMessage = string.Empty;
        }

        public BaseResultDto(bool isSuccess, string errorMessage = "")
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}