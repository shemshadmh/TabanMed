using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.MedicalCenters
{
    public class MedicalCenterTranslationsDto
    {

        public string Name { get; set; } 
        public string Address { get; set; } 
        public string? AgentName { get; set; }
        public int LanguageId { get; set; }      

    }
}
