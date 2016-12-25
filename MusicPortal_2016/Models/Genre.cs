using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
namespace MusicPortal_2016.Models
{//Add-Migration "MigrateDB"
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо указать название жанра!")]
        public string Name { get; set; }
        public virtual ICollection<Songs> Songses
        {
            get; set;
            
        }  //связь оидн ко многим - один жанр много песен

    }
}