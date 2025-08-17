using System.ComponentModel.DataAnnotations;

namespace SMSAuthentication.Models
{
    public class VerifyModel
    {
        [Required(ErrorMessage = "لطفاً شماره موبایل را وارد کنید.")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "لطفاً کد تأیید را وارد کنید.")]
        public string VerificationCode { get; set; }

        [Required(ErrorMessage = "لطفاً پیام را وارد کنید.")]
        public string Message { get; set; }
    }
}
