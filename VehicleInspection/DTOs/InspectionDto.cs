using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleInspection.DTOs
{
    public class InspectionDto
    {
        public int InspectionID { get; set; }

        [Required]
        public int VehicleId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public bool Tires { get; set; }

        [Required]
        public bool Brakes { get; set; }

        [Required]
        public bool Fluids { get; set; }

        [Required]
        public bool Lights { get; set; }

        [Required]
        public bool Windshield { get; set; }

        [Required]
        public bool Exterior { get; set; }

        [Required]
        public bool Interior { get; set; }

        [Required]
        public bool Supplies { get; set; }

        public string Notes { get; set; }

        public string Issues { get; set; }

        public VehicleDto Vehicle { get; set; }

        [Required]
        public bool HasImages { get; set; }
    }
}