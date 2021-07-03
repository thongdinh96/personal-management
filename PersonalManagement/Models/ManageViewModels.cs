using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace PersonalManagement.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage ="Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu hiện tại")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có độ dài ít nhất {2} kí tự", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại sai định dạng")]
        [Display(Name = "Số điện thoại")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessage ="Mã xác nhận không được để trống")]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại sai định dạng")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
    }
    public enum ExperEnum
    {
        [Description("Junior")]
        Junior,
        [Description("Senior")]
        Senior,
        [Description("Expert")]
        Expert
    }
    public enum EnglishLevel
    {
        
        [Description("Elementary proficiency")]
        Elementary,
        [Description("Limited Working proficiency")]
        LimitedWorking,
        [Description("Professional Working proficiency")]
        ProfessionalWorking,
        [Description("Full Professional proficiency")]
        FullProfessional,
        [Description("Native Or Bilingual proficiency")]
        NativeOrBilingual
    }
    public class ProfileViewModel
    {
        public string AvaUrl { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string WorkLink { get; set; }
        [Range(minimum: 1, maximum: 10)]
        public byte Rank { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ExperEnum ExperEnum { get; set; }
        public decimal HourlyRate { get; set; }
        public int TotalProjects { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
        public string Availability { get; set; }
        public string Bio { get; set; }

        public ICollection<Skill> Skills { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
    public class AlertViewModel
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public int Id { get; set; }
    }
}