//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Satellite
    {
        public Satellite()
        {
            this.DownlinkChannels = new HashSet<DownlinkChannel>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int SatellitePositionId { get; set; }
    
        public virtual SatellitePosition SatellitePosition { get; set; }
        public virtual ICollection<DownlinkChannel> DownlinkChannels { get; set; }
    }
}
