using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
namespace MusicPortal_2016.Models
{
    public class Songs
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо указать название трэка!")]
        public string Name { get; set; }
        public string FileNameAndType { get; set; }

        public string UrlRelativeAdress { get; set; }
        public string UrlAbsoluteAdress { get; set; }

        public int? GenreId { get; set; }

        //public Nullable<int> GenreId { get; set; }
        
        public virtual Genre Genre { get; set; }
    }
}