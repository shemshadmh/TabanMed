using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources.DataDictionary;

namespace Application.Dtos.MedicalCenters
{
    public class MedicalCenterListItemDto
    {
        public int Id { get; set; }
        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Name))]
        
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? AgentPhoneNumber { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}
