namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MVC5Course.Models.InputValidations;


    [MetadataType(typeof(ClientMetaData))]
    public partial class Client : IValidatableObject
    {
        partial void Init()
        {
            this.DateOfBirth = DateTime.Now.AddYears(-18);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.ClientId == 0)
            {
                // 通常 P.K. 為 0 代表著正在執行「新增」動作
            }
            if (this.Longitude.HasValue != this.Latitude.HasValue)
            {
                yield return new ValidationResult("經緯度欄位必須一起設定", new string[] { "Longitude", "Latitude" });
            }
        }
    }

    public partial class ClientMetaData
    {
        [Required]
        public int ClientId { get; set; }
        
        [StringLength(40, ErrorMessage="欄位長度不得大於 40 個字元")]
        [Required]
        public string FirstName { get; set; }
        
        [StringLength(40, ErrorMessage="欄位長度不得大於 40 個字元")]
        [Required]
        public string MiddleName { get; set; }
        
        [StringLength(40, ErrorMessage="欄位長度不得大於 40 個字元")]
        [Required]
        public string LastName { get; set; }
        
        [StringLength(1, ErrorMessage="欄位長度不得大於 1 個字元")]
        [Required]
        public string Gender { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [Required]
        public Nullable<double> CreditRating { get; set; }
        
        [StringLength(7, ErrorMessage="欄位長度不得大於 7 個字元")]
        public string XCode { get; set; }
        public Nullable<int> OccupationId { get; set; }
        
        [StringLength(20, ErrorMessage="欄位長度不得大於 20 個字元")]
        public string TelephoneNumber { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string Street1 { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string Street2 { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string City { get; set; }
        
        [StringLength(15, ErrorMessage="欄位長度不得大於 15 個字元")]
        public string ZipCode { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public string Notes { get; set; }

        [身份證字號]
        public string IdNumber { get; set; }


        public virtual Occupation Occupation { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
