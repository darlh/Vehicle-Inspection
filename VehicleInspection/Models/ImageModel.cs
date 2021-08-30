using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleInspection.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        
        [DisplayName("Upload Image(s)")]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase[] ImageFiles { get; set; }
                
        public int InspectionID { get; set; }

        public virtual Inspection Inspection { get; set; }
    }
}