﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TV2Presets2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TV2PresetsModelContainer : DbContext
    {
        public TV2PresetsModelContainer()
            : base("name=TV2PresetsModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DownlinkChannel> DownlinkChannels { get; set; }
        public virtual DbSet<Satellite> Satellites { get; set; }
        public virtual DbSet<SatellitePosition> SatellitePositions { get; set; }
        public virtual DbSet<FixedAntenna> FixedAntennas { get; set; }
        public virtual DbSet<SteerableAntenna> SteerableAntennas { get; set; }
        public virtual DbSet<IRD> IRDs { get; set; }
        public virtual DbSet<EXTCard> EXTCards { get; set; }
        public virtual DbSet<BISSCode> BISSCodes { get; set; }
        public virtual DbSet<UplinkChannel> UplinkChannels { get; set; }
    }
}
