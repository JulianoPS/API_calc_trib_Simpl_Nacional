using Microsoft.EntityFrameworkCore;
using APISimplesNacional.Infra.Entidades;

namespace APISimplesNacional.Infra.Contexto
{
    public class SimplesNacionalDbContext : DbContext
    {
        public SimplesNacionalDbContext(DbContextOptions<SimplesNacionalDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Empresas> Empresas { get; set; }
        public virtual DbSet<AnexoIII> AnexoIII { get; set; }
        public virtual DbSet<AnexoV> AnexoV { get; set; }
        public virtual DbSet<TabelaINSS> TabelaINSS { get; set; }
        public virtual DbSet<TabelaIR> TabelaIR { get; set; }
        public virtual DbSet<ErroLog> ErrosLog { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações básicas se quiser, por enquanto deixa o padrão
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Empresas>().ToTable("Empresas");
            modelBuilder.Entity<Empresas>().HasData(
                new Empresas { Id = 1, Nome = "JPS Technology in Development", Celular = "(62)99213-7872", Email = "julianops79@gmail.com", IrDependente = 189.29m, IrVlrIsento = 3036.00m }
            );

            modelBuilder.Entity<TabelaIR>()
                .HasOne(t => t.Empresa)
                .WithMany(e => e.TabelaIRs)
                .HasForeignKey(t => t.IdEmpresa);

            modelBuilder.Entity<TabelaINSS>()
                .HasOne(t => t.Empresa)
                .WithMany(e => e.TabelaINSSs)
                .HasForeignKey(t => t.IdEmpresa);

            modelBuilder.Entity<AnexoIII>()
                .HasOne(t => t.Empresa)
                .WithMany(e => e.AnexosIII)
                .HasForeignKey(t => t.IdEmpresa);

            modelBuilder.Entity<AnexoV>()
                .HasOne(t => t.Empresa)
                .WithMany(e => e.AnexosV)
                .HasForeignKey(t => t.IdEmpresa);

            modelBuilder.Entity<AnexoIII>().HasData(
                new AnexoIII { Id = 1, IdEmpresa = 1, Faixa = 1, LimiteInic = 0, LimiteFin = 180000, Aliquota = 6.0m, VlrDeduzir = 0 },
                new AnexoIII { Id = 2, IdEmpresa = 1, Faixa = 2, LimiteInic = 180000.01m, LimiteFin = 360000, Aliquota = 11.2m, VlrDeduzir = 9360 },
                new AnexoIII { Id = 3, IdEmpresa = 1, Faixa = 3, LimiteInic = 360000.01m, LimiteFin = 720000, Aliquota = 13.5m, VlrDeduzir = 17640 },
                new AnexoIII { Id = 4, IdEmpresa = 1, Faixa = 4, LimiteInic = 720000.01m, LimiteFin = 1800000, Aliquota = 16.0m, VlrDeduzir = 35640 },
                new AnexoIII { Id = 5, IdEmpresa = 1, Faixa = 5, LimiteInic = 1800000.01m, LimiteFin = 3600000, Aliquota = 20.5m, VlrDeduzir = 125640 },
                new AnexoIII { Id = 6, IdEmpresa = 1, Faixa = 6, LimiteInic = 3600000.01m, LimiteFin = 4800000, Aliquota = 33.0m, VlrDeduzir = 648000 }
            );

            modelBuilder.Entity<AnexoV>().HasData(
                new AnexoV { Id = 1, IdEmpresa = 1, Faixa = 1, LimiteInic = 0, LimiteFin = 180000, Aliquota = 15.5m, VlrDeduzir = 0 },
                new AnexoV { Id = 2, IdEmpresa = 1, Faixa = 2, LimiteInic = 180000.01m, LimiteFin = 360000, Aliquota = 18.0m, VlrDeduzir = 4500 },
                new AnexoV { Id = 3, IdEmpresa = 1, Faixa = 3, LimiteInic = 360000.01m, LimiteFin = 720000, Aliquota = 19.5m, VlrDeduzir = 9900 },
                new AnexoV { Id = 4, IdEmpresa = 1, Faixa = 4, LimiteInic = 720000.01m, LimiteFin = 1800000, Aliquota = 20.5m, VlrDeduzir = 17100 },
                new AnexoV { Id = 5, IdEmpresa = 1, Faixa = 5, LimiteInic = 1800000.01m, LimiteFin = 3600000, Aliquota = 23.0m, VlrDeduzir = 62100 },
                new AnexoV { Id = 6, IdEmpresa = 1, Faixa = 6, LimiteInic = 3600000.01m, LimiteFin = 4800000, Aliquota = 30.5m, VlrDeduzir = 540 }
            );

            modelBuilder.Entity<TabelaINSS>().HasData(
                new TabelaINSS { Id = 1, IdEmpresa = 1, Faixa = 1, LimiteInic = 0, LimiteFin = 1518, Aliquota = 7.5m, Deducao = 0.00m },
                new TabelaINSS { Id = 2, IdEmpresa = 1, Faixa = 2, LimiteInic = 1518.01m, LimiteFin = 2793.88m, Aliquota = 9m, Deducao = 22.77m },
                new TabelaINSS { Id = 3, IdEmpresa = 1, Faixa = 3, LimiteInic = 2793.89m, LimiteFin = 4190.83m, Aliquota = 12m, Deducao = 106.59m },
                new TabelaINSS { Id = 4, IdEmpresa = 1, Faixa = 4, LimiteInic = 4190.84m, LimiteFin = 8157.41m, Aliquota = 14m, Deducao = 190.40m }
            );

            modelBuilder.Entity<TabelaIR>().HasData(
                new TabelaIR { Id = 1, IdEmpresa = 1, Faixa = 1, LimiteInic = 0, LimiteFin = 2428.80m, Aliquota = 0, VlrDeduzir = 0 },
                new TabelaIR { Id = 2, IdEmpresa = 1, Faixa = 2, LimiteInic = 2428.01m, LimiteFin = 2826.65m, Aliquota = 7.5m, VlrDeduzir = 182.16m },
                new TabelaIR { Id = 3, IdEmpresa = 1, Faixa = 3, LimiteInic = 2826.65m, LimiteFin = 3751.05m, Aliquota = 15.0m, VlrDeduzir = 394.16m },
                new TabelaIR { Id = 4, IdEmpresa = 1, Faixa = 4, LimiteInic = 3751.06m, LimiteFin = 4664.68m, Aliquota = 22.5m, VlrDeduzir = 675.49m },
                new TabelaIR { Id = 5, IdEmpresa = 1, Faixa = 5, LimiteInic = 4664.69m, LimiteFin = 99999999, Aliquota = 27.5m, VlrDeduzir = 908.73m }
            );
        

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=SimplesNacionalDB;Username=postgres;Password=jps79");
            }
        }

    }
}
