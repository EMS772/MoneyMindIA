using Microsoft.EntityFrameworkCore;
using MoneyMindIA.Models.Entidades;

namespace MoneyMindIA.Models.Data
{
    public class MoneyMindDbContext : DbContext
    {
        public MoneyMindDbContext(DbContextOptions<MoneyMindDbContext> options)
        : base(options)
        {
        }
        // DbSets (Tablas de la base de datos)
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Billetera> Billeteras { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet<ChatMensaje> ChatMensajes { get; set; }
        public DbSet<Recomendacion> Recomendaciones { get; set; }


        // Configuración del modelo de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //--------------------- CONFIGURACIONES DE ENTIDADES ---------------------

            // Entidad Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.UsuarioId);
                entity.Property(u => u.Nombre).HasMaxLength(100).IsRequired();
                entity.Property(u => u.Email).IsRequired();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.GoogleId);
                entity.Property(u => u.PasswordHash);
                entity.Property(u => u.EsRegistroNormal).IsRequired();
            });

            // Entidad Billetera (Relación 1:1 con Usuario)
            modelBuilder.Entity<Billetera>(entity =>
            {
                entity.HasKey(b => b.BilleteraId);
                entity.Property(b => b.PayPalEmail).IsRequired();
                entity.Property(b => b.PayPalAccessToken).IsRequired();
                entity.Property(b => b.BalanceActual)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);

                // Relación 1:1 con Usuario
                entity.HasOne(b => b.Usuario)
                    .WithOne(u => u.Billetera)
                    .HasForeignKey<Billetera>(b => b.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Entidad Transaccion
            modelBuilder.Entity<Transaccion>(entity =>
            {
                entity.HasKey(t => t.TransaccionId);
                entity.Property(t => t.Monto)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                entity.Property(t => t.Fecha).IsRequired();
                entity.Property(t => t.Descripcion).HasMaxLength(200);
                entity.Property(t => t.Tipo).IsRequired();

                // Relación con Billetera
                entity.HasOne(t => t.Billetera)
                    .WithMany(b => b.Transacciones)
                    .HasForeignKey(t => t.BilleteraId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relación con Categoria
                entity.HasOne(t => t.Categoria)
                    .WithMany(c => c.Transacciones)
                    .HasForeignKey(t => t.CategoriaId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Entidad Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.CategoriaId);
                entity.Property(c => c.Nombre)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.HasIndex(c => c.Nombre).IsUnique();
            });

            // Entidad Meta
            modelBuilder.Entity<Meta>(entity =>
            {
                entity.HasKey(m => m.MetaId);
                entity.Property(m => m.Descripcion)
                    .HasMaxLength(200)
                    .IsRequired();
                entity.Property(m => m.MontoObjetivo)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                entity.Property(m => m.MontoActual)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);

                // Relación con Usuario
                entity.HasOne(m => m.Usuario)
                    .WithMany(u => u.Metas)
                    .HasForeignKey(m => m.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Entidad ChatMensaje
            modelBuilder.Entity<ChatMensaje>(entity =>
            {
                entity.HasKey(c => c.MensajeId);
                entity.Property(c => c.Contenido).IsRequired();
                entity.Property(c => c.EsUsuario).IsRequired();

                // Relación con Usuario
                entity.HasOne(c => c.Usuario)
                    .WithMany(u => u.ChatMensajes)
                    .HasForeignKey(c => c.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Entidad Recomendacion
            modelBuilder.Entity<Recomendacion>(entity =>
            {
                entity.HasKey(r => r.RecomendacionId);
                entity.Property(r => r.Mensaje)
                    .HasMaxLength(1500)
                    .IsRequired();

                // Relación con Usuario
                entity.HasOne(r => r.Usuario)
                    .WithMany(u => u.Recomendaciones)
                    .HasForeignKey(r => r.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //--------------------- CONFIGURACIONES ADICIONALES ---------------------



            // Configuración de valores por defecto
            modelBuilder.Entity<Usuario>()
                .Property(u => u.FechaRegistro)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Meta>()
                .Property(m => m.FechaCreacion)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
