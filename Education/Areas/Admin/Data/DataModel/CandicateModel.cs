using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Education.Areas.Admin.Data.DataModel
{
    public class CandicateModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Student code is not left blank")]
        [RegularExpression("^[A-Z][A-Z0-9]{2,9}$", ErrorMessage = "Code starts with flower print, including flower printing and numbers. From 3 to 10 characters")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Student Name is not left blank")]
        public string Name { get; set; }
        [Display(Name = "Parent Name")]
        [Required(ErrorMessage = "Student Parent Name is not left blank")]
        public string ParentName { get; set; }
        [Required(ErrorMessage = "Student Email is not left blank")]
        [RegularExpression("^[\\w-.]+@([\\w-]+.)+[\\w-]{2,4}$", ErrorMessage = "Email invalidate")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Student Phone is not left blank")]
        [RegularExpression("^[0]([0-9]){9,12}$", ErrorMessage = "Enter the phone number with a length of 10 to 13 characters, start 0 ****!")]
        public string Phone { get; set; }
        [Display(Name= "Parent Phone")]
        [Required(ErrorMessage = "Student Parent Phone is not left blank")]
        [RegularExpression("^[0]([0-9]){9,12}$", ErrorMessage = "Enter the phone number with a length of 10 to 13 characters, start 0 ****!")]
        public string ParentPhone { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public bool Gender { get; set; }
        [Required(ErrorMessage = "Student Address is not left blank")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Student Birthday is not left blank")]
        public DateTime Birthday { get; set; }
        [Display(Name = "Joining Date")]
        [Required(ErrorMessage = "Student Joining Date is not left blank")]
        public DateTime JoiningDate { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Student Full Name is not left blank")]
        public string FullName { get; set; }
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Student UserName is not left blank")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Student Password is not left blank")]
        [RegularExpression("^[a-zA-Z]([a-zA-Z0-9]){4,14}$", ErrorMessage = "Enter the password that does not contain special characters, lengths from 5 to 15 characters!")]
        public string Password { get; set; }
        [Display(Name = "Repeat Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string RepeatPassword { get; set; }
    }
}