using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KimsWoodWorking.Models.ViewModels
{
    public class EditProductViewModel
    {
        public List<ProductModel> ProductsList { get; set; } = new List<ProductModel>();

        public ProductModel SelectedProduct { get; set; } = new ProductModel();

        public ProductModel editedProduct { get; set; } = new ProductModel();

        [Display(Name = "Product Name:")]
        public string ProductSearchedFor { get; set; } = string.Empty;
    }
}