using Community.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ModelViews.MeetingViewModels
{
    [MeetingEditValidator]
    public class MeetingEditViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 5)]
        [Display(Name = "Name of event")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of event")]
        public DateTime MeetingDate { get; set; }

        [Required]
        [StringLength(maximumLength: 2000, MinimumLength = 20)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string currentPhotoPath { get; set; }

        [Display(Name = "Photo")]
        public IFormFile PhotoSource { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int MeetingCategoryId { get; set; }
        public IEnumerable<MeetingCategory> MeetingCategories { get; set; }

    }

    public class MeetingEditValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            MeetingEditViewModel model = (MeetingEditViewModel)validationContext.ObjectInstance;

            if (model.PhotoSource != null)
            {
                if (model.PhotoSource.Length > 500 * 1024)
                {
                    return new ValidationResult("File size cant be beeger than 500 kb");
                }

                string[] allowedExtensions = { ".jpeg", ".png", ".jpg" };
                string fileExt = Path.GetExtension(model.PhotoSource.FileName);
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
