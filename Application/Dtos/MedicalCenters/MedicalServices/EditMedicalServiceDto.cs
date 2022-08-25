using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.MedicalCenters.MedicalServices
{
    public  class EditMedicalServiceDto
    {
        public ICollection<MedicalServiceForCheckBox>? AllMedicalService { get; set; }

        public List<int> SelectedMedicalServices { get; set; } = null!;
        public int MedicalCenterId { get; set; }
    }
}
