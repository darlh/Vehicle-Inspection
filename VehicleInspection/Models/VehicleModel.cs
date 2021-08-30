using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleInspection.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        [Display (Name ="Unit #")]
        public string UnitNumber { get; set; }

        [Display(Name = "Vehicle Type")]
        public int? VehicleTypeId { get; set; }

        [Display(Name = "Last Inspection")]
        public DateTime? LastInspect { get; set; }

        public virtual ICollection<Inspection> Inspections { get; set; }

        public virtual VehicleType VehicleType { get; set; }

    }

    public class VehicleType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

}