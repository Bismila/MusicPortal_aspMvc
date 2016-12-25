using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MusicPortal_2016.Models
{
    /*Контекст данных использует EntityFramework для доступа к БД на основе некоторой модели. 
     Чтобы создать контекст, нам надо унаследовать новый класс от класса DbContext.
     Свойства наподобие public DbSet<Users> UsersObjDbSet { get; set; } помогают получать из БД набор данных 
     определенного типа (например, набор объектов Users).*/

    public class Context : DbContext
    {
        public Context() : base("Context")
        {

        }

        public DbSet<Users> UsersObjDbSet { get; set; }
        public DbSet<Genre> GenresObjDbSet { get; set; }
        public DbSet<Songs> SongesObjDbSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasMany(c => c.Songses)
                .WithOptional(o => o.Genre).WillCascadeOnDelete(false);
        }
    }
}

/*Хотя мы будем использовать базу данных, но создавать явным образом мы ее не будем.
 * За нас все сделает EntityFramework. Это так называемый подход Code First - у нас есть модели, и по ним фреймворк 
 * будет создавать таблицы в базе данных.

И в заключении работы над модельной частью установим строку подключения. Для этого откроем файл web.config, 
найдем секцию configSections и сразу после нее вставим секцию connectionStrings:...
<connectionStrings>
    <add name="Context" connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\GenreSongsUsers.mdf';Integrated Security=True"
 providerName="System.Data.SqlClient" />
</connectionStrings>*/
