﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASPNET_MVC_Bootstrap4_Template.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CineDBEntities : DbContext
    {
        public CineDBEntities()
            : base("name=CineDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Acteur> Acteurs { get; set; }
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<Contenu> Contenus { get; set; }
        public virtual DbSet<Directeur> Directeurs { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Responsable> Responsables { get; set; }
        public virtual DbSet<Salle> Salles { get; set; }
        public virtual DbSet<Seance> Seances { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
