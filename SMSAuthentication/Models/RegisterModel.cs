using System.ComponentModel.DataAnnotations;

namespace SMSAuthentication.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "لطفاً شماره موبایل را وارد کنید.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "شماره موبایل باید 11 رقم باشد.")]
        public string MobileNumber { get; set; }
    }
}
