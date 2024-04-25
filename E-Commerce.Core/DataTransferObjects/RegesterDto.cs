using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.DataTransferObjects
{
    public class RegesterDto
    {
        public string DisplayName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$" , ErrorMessage ="Password Must Contain Upper Letters , Lower Letters , Special Characters") ]
        public string Password { get; set; }
    }
}
