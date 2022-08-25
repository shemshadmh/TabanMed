using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.MedicalCenters.MedicalServices
{
    public class MedicalServiceForCheckBox
    {
        public string Title { get; set; } = null!;
        public int Value { get; set; }
        public int? ParentId { get; set; }
        public bool IsSelected { get; set; }
    }
}
