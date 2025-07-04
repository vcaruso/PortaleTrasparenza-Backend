using System.ComponentModel.DataAnnotations;

namespace ENINET.TransaprentPortal.Persistence.Entities
{
    public class Element
    {
        [Key]
        public string Name { get; set; } = default!;


    }
}
