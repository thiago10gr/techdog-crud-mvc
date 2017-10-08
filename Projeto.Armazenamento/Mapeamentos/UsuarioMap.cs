using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;
using Projeto.Entidades;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Armazenamento.Mapeamentos
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {

            ToTable("tbl_usuario");

            HasKey(u => u.IdUsuario);

            Property(u => u.IdUsuario)
                .HasColumnName("id_usuario")
                .IsRequired();

            Property(u => u.Senha)
                .HasColumnName("senha")
                .HasMaxLength(32)
                .IsRequired();

            Property(u => u.Nome)
              .HasColumnName("nome")
              .HasMaxLength(100)
              .IsRequired();

            Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                    new IndexAttribute("index_email_usuario") { IsUnique = true }));

            Property(u => u.DataCadastro)
                .HasColumnName("data_cadastro")
                .IsRequired();

            Property(u => u.Ativo)
                .HasColumnName("ativo")
                .IsRequired();

        }
    }
}
