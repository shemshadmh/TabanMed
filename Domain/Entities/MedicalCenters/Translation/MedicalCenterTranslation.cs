using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Hotels;
using Domain.Entities.Localization;

namespace Domain.Entities.MedicalCenters.Translation
{
    public class MedicalCenterTranslation
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? AgentName { get; set; }

        #region Relations

        public int MedicalCenterId { get; set; }
        public MedicalCenter MedicalCenter { get; set; } = null!;
        
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
        #endregion
    }
}
