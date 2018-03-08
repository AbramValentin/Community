using Community.Data;
using Community.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ModelViews.MeetingViewModels
{
    [MeetingCreateValidator]
    public class MeetingCreateViewModel
    {
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 5)]
        [Display(Name = "Name of event")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of event")]
        public DateTime MeetingDate  = DateTime.Now;

        [Required]
        [StringLength(maximumLength: 2000, MinimumLength = 20)]
        [Display(Name = "Describe your event")]
        public string Description { get; set; }

        [Display(Name = "Add a photo")]
        public IFormFile PhotoPath { get; set; }

        [Required]
        [Display(Name = "Choose a city")]
        public int CityId { get; set; }

        [Required]
        [Display(Name = "Choose a category")]
        public int MeetingCategoryId { get; set; }
        public IEnumerable<MeetingCategory> MeetingCategories { get; set; }
    }


    public class MeetingCreateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            MeetingCreateViewModel model = (MeetingCreateViewModel)validationContext.ObjectInstance;

            if (model.PhotoPath != null)
            {
                if (model.PhotoPath.Length > 500*1024)
                {
                    return new ValidationResult("File size cant be beeger than 500 kb");
                }

                string[] allowedExtensions = { ".jpeg", ".png", ".jpg" };
                string fileExt = Path.GetExtension(model.PhotoPath.FileName);
                if (!allowedExtensions.Contains(fileExt))
                {
                    return new ValidationResult("File format is not supported , available is : .jpg .jpeg .png");
                }
            }

            /*ADD CITY VALIDATION , IF CITY DOES NOT EXITST : RETURN VALIDATION ERROR*/

            return ValidationResult.Success;
        }

    }
}
