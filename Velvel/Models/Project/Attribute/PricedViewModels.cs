using System.Collections.Generic;

namespace Velvel.Models.Project.Attribute
{
    public class PricedViewModels
    {
        public int ProjectId { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<PricedViewModel> Enteries { get; set; }
    }
    public class PricedViewModel:ProjectEntityViewModel
    {
        public int Price { get; set; }
    }
    public class GroutViewModel : PricedViewModel
    {
    }
    public class ModelViewModel : PricedViewModel
    {
    }
    public class AccessoryViewModel : PricedViewModel
    {
    }
}