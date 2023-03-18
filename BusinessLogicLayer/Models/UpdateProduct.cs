using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public  class UpdateProduct
    {
        /// <summary>
        /// ProductId
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [Required]
        public decimal Price { get; set; }
    }
}
