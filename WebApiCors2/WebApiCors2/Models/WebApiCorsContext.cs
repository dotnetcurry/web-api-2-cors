using System.Data.Entity;

namespace WebApiCors2.Models
{
    public class WebApiCorsContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<WebApiCors2.Models.WebApiCorsContext>());

        public WebApiCorsContext()
            : base("name=WebApiCorsContext2")
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}
