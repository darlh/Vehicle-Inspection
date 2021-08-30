using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleInspection.Models
{
    public class Inspection
    {
        public int InspectionID { get; set; }

        [Required]
        [Display(Name ="Unit #")]
        public int VehicleId { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Timestamp")]
        [DisplayFormat(DataFormatString = "{0:M/d/yyyy h:mm tt}")]
        public DateTime Timestamp { get; set; }

        [Required]
        [Display(Name = "Mileage")]
        public int Mileage { get; set; }

        [Required]
        [Display(Name = "Tires")]
        public bool Tires { get; set; }

        [Required]
        [Display(Name = "Brakes")]
        public bool Brakes { get; set; }

        [Required]
        [Display(Name = "Fluid Levels")]
        public bool Fluids { get; set; }

        [Required]
        [Display(Name = "Lights")]
        public bool Lights { get; set; }

        [Required]
        [Display(Name ="Windshield")]
        public bool Windshield { get; set; }

        [Required]
        [Display(Name = "Exterior Condition")]
        public bool Exterior { get; set; }

        [Required]
        [Display(Name = "Interior Condition")]
        public bool Interior { get; set; }

        [Required]
        [Display(Name = "Supplies")]
        public bool Supplies { get; set; }

        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Issues")]
        public string Issues { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual IEnumerable<Image> Images { get; set; }

        [Required]
        [Display(Name ="Has attached images")]
        public bool HasImages { get; set; }
    }

    public class InspectionIndex
    {
        public IEnumerable<Inspection> Inspections { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }
    }
}