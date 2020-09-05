using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ShoppingVan.ViewModels
{
    public class ShoppingVanItemVM
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string ImgUrl { get; set; }
        public List<UnitVM> Units { get; set; }
    }
}
