using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneToFifty.Models
{
    [Table("tb_trace", Schema = "test")]
    public class Record
    {
        [Key, Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("score")]
        public long score { get; set; }
    }

    public class OneToFiftyContext : DbContext
    {
        private string _connectionString;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        public OneToFiftyContext() : base()
        {
            _connectionString = "Host = 192.168.30.20; Database=one_to_fifty;Username=postgres;Password=gis123123;Port=5432";
        }

        public DbSet<Record> record { get; set; }
    }
}
