using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPPMDotNetCore.BlazorServerApp.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FruitDataModel> Fruits { get; set; }
    }

    [Table("Tbl_Fruit")]
    public class FruitDataModel
    {
        [Key]
        public int FruitId { get; set; }
        public string FruitName { get; set; }
    }

    public enum EnumMessageType
    {
        Default,
        Success,
        Information,
        Warning,
        Error,
    }

    public class MessageModel
    {
        public EnumMessageType MessageType { get; set; }
        public string Message { get; set; }
    }


    public enum EnumFormType
    {
        Create,
        Edit,
        Delete,
        List
    }

    public enum EnumModelStatusType
    {
        Show,
        Hide
    }
}
